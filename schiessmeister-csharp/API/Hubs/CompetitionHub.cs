using Microsoft.AspNetCore.SignalR;
using schiessmeister_csharp.Domain.Models;

namespace schiessmeister_csharp.API.Hubs {

    public class CompetitionHub : Hub {
        private const string CompetitionGroupPrefix = "competition-";

        /// <summary>
        /// Allows clients to subscribe to updates for a specific competition
        /// </summary>
        public async Task SubscribeToCompetition(int competitionId) {
            string groupName = GetGroupName(competitionId);
            await Groups.AddToGroupAsync(Context.ConnectionId, groupName);
        }

        /// <summary>
        /// Allows clients to unsubscribe from updates for a specific competition
        /// </summary>
        public async Task UnsubscribeFromCompetition(int competitionId) {
            string groupName = GetGroupName(competitionId);
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, groupName);
        }

        /// <summary>
        /// Gets the unique group name for a competition
        /// </summary>
        public static string GetGroupName(int competitionId) {
            return $"{CompetitionGroupPrefix}{competitionId}";
        }
    }
}