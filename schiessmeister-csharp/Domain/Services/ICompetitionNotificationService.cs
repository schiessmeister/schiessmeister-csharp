using schiessmeister_csharp.Domain.Models;

namespace schiessmeister_csharp.Domain.Services {

    public interface ICompetitionNotificationService {

        /// <summary>
        /// Notifies all clients subscribed to a specific competition about updates
        /// </summary>
        Task NotifyCompetitionUpdated(Competition competition);
    }
}