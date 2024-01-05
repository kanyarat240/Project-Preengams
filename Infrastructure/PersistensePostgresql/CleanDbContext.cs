using System;
using System.Data;
using Application.Interfaces;
using Domain.Entities;
using Domain.Entities.DB;
using Domain.Entities.Public;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;
using PersistensePostgresql.Extensions;

namespace PersistensePostgresql;

public partial class CleanDbContext : DbContext, ICleanDbContext
{
    private readonly ICurrentUserAccessor _currentUserAccessor;
    private IDbContextTransaction _currentTransaction;

    public CleanDbContext(ICurrentUserAccessor currentUserAccessor, DbContextOptions options) : base(options)
    {
        _currentUserAccessor = currentUserAccessor;
        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
    }

    public bool HasActiveTransaction => _currentTransaction != null;

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return this.SaveChangesAsync(_currentUserAccessor.UserId, cancellationToken);
    }

    public Task<int> SaveChangesAsync(int? userId, CancellationToken cancellationToken)
    {
        this.SetAudit(userId, "programCode");

        //var events = ChangeTracker.Entries<IHasDomainEvent>()
        //.Select(x => x.Entity.DomainEvents)
        //.SelectMany(x => x)
        //.Where(domainEvent => !domainEvent.IsPublished)
        //.ToArray();

        //var result = await ;

        //await DispatchEvents(events);

        return base.SaveChangesAsync(cancellationToken);
    }

    public async Task<IDbContextTransaction> BeginTransactionAsync()
    {
        if (_currentTransaction != null) return null;

        _currentTransaction = await Database.BeginTransactionAsync(IsolationLevel.ReadCommitted).ConfigureAwait(false);

        return _currentTransaction;
    }

    public async Task CommitTransactionAsync(IDbContextTransaction transaction)
    {
        if (transaction == null) throw new ArgumentNullException(nameof(transaction));
        if (transaction != _currentTransaction) throw new InvalidOperationException($"Transaction {transaction.TransactionId} is not current");

        try
        {
            await base.SaveChangesAsync().ConfigureAwait(false);
            transaction.Commit();
        }
        catch
        {
            RollbackTransaction();
            throw;
        }
        finally
        {
            if (_currentTransaction != null)
            {
                _currentTransaction.Dispose();
                _currentTransaction = null;
            }
        }
    }

    public void RollbackTransaction()
    {
        try
        {
            _currentTransaction?.Rollback();
        }
        catch
        {
            //prevent throw error
        }
        finally
        {
            //this.ClearTracker();
            if (_currentTransaction != null)
            {
                _currentTransaction.Dispose();
                _currentTransaction = null;
            }
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(CleanDbContext).Assembly);

        modelBuilder.EntityTypes().Configure(e => e.SetTableName(e.ClrType.Name.ToLowercaseNamingConvention()));
        modelBuilder.EntityTypes().Configure(e => e.SetSchema(e.GetSchema().ToLowercaseNamingConvention()));
        modelBuilder.Properties().Configure(p => p.SetColumnName(p.Name.ToLowercaseNamingConvention()));
    }

    //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    //=> optionsBuilder
    //    .UseSnakeCaseNamingConvention();

    private void SetAudit(int? userId, string programCode)
    {
        foreach (var entry in ChangeTracker.Entries<EntityDomain>())
        {
            switch (entry.State)
            {
                case EntityState.Added:
                    entry.Entity.CreateAudit(userId, programCode);
                    entry.Entity.UpdateAudit(userId, programCode);
                    break;
                case EntityState.Modified:
                    entry.Entity.UpdateAudit(userId, programCode);
                    break;
                case EntityState.Deleted:
                    break;
            }
        }
    }

    public Task<int> SaveChangesAsync(string userName, string programCode, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

}

