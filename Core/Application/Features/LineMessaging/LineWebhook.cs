using Application.Interfaces;
using Domain.Configs;
using Line.Messaging.Webhooks;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;

namespace Application.Features.LineMessaging;

public class LineWebhook
{
    public record Command(HttpRequest request) : IRequest<bool>;

    public class Handler : WebhookApplication, IRequestHandler<Command, bool>
    {
        private readonly ICleanDbContext _context;
        private readonly LineConfig _lineConfig;
        public Handler(ICleanDbContext context, IOptions<LineConfig> line_config)
        {
            _context = context;
            _lineConfig = line_config.Value;
        }

        public async Task<bool> Handle(Command request, CancellationToken cancellationToken)
        {
            IEnumerable<WebhookEvent> events = await request.request.GetWebhookEventsAsync(_lineConfig.ChannelSecret);
            if (events.Count() == 0) return true;
            await base.RunAsync(events);
            return true;
        }

        protected override Task OnMessageAsync(MessageEvent ev)
        {






            return base.OnMessageAsync(ev);
        }

        protected override Task OnPostbackAsync(PostbackEvent ev)
        {
            return base.OnPostbackAsync(ev);
        }
    }
}
