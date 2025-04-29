using Microsoft.AspNetCore.SignalR;
using schiessmeister_csharp.API.Hubs;
using schiessmeister_csharp.Domain.Models;
using schiessmeister_csharp.Domain.Services;

namespace schiessmeister_csharp.API.Services {

    public class CompetitionNotificationService : ICompetitionNotificationService {
        private readonly IHubContext<CompetitionHub> _hubContext;

        public CompetitionNotificationService(IHubContext<CompetitionHub> hubContext) {
            _hubContext = hubContext;
        }

        public async Task NotifyCompetitionUpdated(Competition competition) {
            string groupName = CompetitionHub.GetGroupName(competition.Id);
            await _hubContext.Clients.Group(groupName).SendAsync("CompetitionUpdated", competition);
        }
    }
}