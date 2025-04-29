using schiessmeister_csharp.Domain.Models.ValueTypes;
using schiessmeister_csharp.Domain.Repositories;

namespace schiessmeister_csharp.Domain.Models;

public class Participation : IEntity {
    private string _dqstatus;

    public int Id { get; set; }
    public string ShooterClass { get; set; }
    public int LaneNb { get; set; }
    public string Results { get; set; } // JSON string containing the results
    public string? Team { get; set; }

    public string? DqStatus {
        get => _dqstatus;
        set {
            (bool isValid, string dqStatus) = DqStatusValues.IsValid(value);

            if (!isValid) {
                throw new ArgumentException($"Ungültiger DisqualificationStatus-Wert: ${value}");
            }

            _dqstatus = dqStatus;
        }
    }

    public int DisciplineId { get; set; }
    public Discipline? Discipline { get; set; }

    public int ShooterId { get; set; }
    public AppUser? Shooter { get; set; }

    public int RecorderId { get; set; }
    public AppUser? Recorder { get; set; }

    public int CompetitionId { get; set; }
    public Competition? Competition { get; set; }

    public int? ParticipationGroupId { get; set; }
    public ParticipationGroup? ParticipationGroup { get; set; }
}