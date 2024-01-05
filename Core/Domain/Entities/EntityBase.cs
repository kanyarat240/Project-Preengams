using System;
namespace Domain.Entities;

public enum RowState
{
    Normal, Add, Edit, Delete
}

public interface EntityDomain
{
    int? CreatedBy { get; }
    DateTime? CreatedDateTime { get; }
    string? CreatedProgramCode { get; }
    int? UpdatedBy { get; }
    DateTime? UpdatedDateTime { get; }
    string? UpdatedProgramCode { get; }
    void CreateAudit(int? userID, string progCode);
    void UpdateAudit(int? userID, string progCode);
}
public abstract class EntityBase : EntityDomain
{
    public int? CreatedBy { get; private set; }
    public DateTime? CreatedDateTime { get; private set; }
    public string? CreatedProgramCode { get; private set; }
    public int? UpdatedBy { get; private set; }
    public DateTime? UpdatedDateTime { get; private set; }
    public string? UpdatedProgramCode { get; private set; }


    public void CreateAudit(int? userID, string progCode)
    {
        CreatedBy = userID;
        CreatedDateTime = DateTime.UtcNow;
        CreatedProgramCode = progCode;
    }

    public void UpdateAudit(int? userID, string progCode)
    {
        UpdatedBy = userID;
        UpdatedDateTime = DateTime.UtcNow;
        UpdatedProgramCode = progCode;
    }
}
