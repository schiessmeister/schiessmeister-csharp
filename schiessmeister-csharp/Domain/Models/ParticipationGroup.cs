using schiessmeister_csharp.Domain.Repositories;

namespace schiessmeister_csharp.Domain.Models;

public class ParticipationGroup : IEntity {
    public int Id { get; set; }
    public string Title { get; set; }
    public DateTime? StartDateTime { get; set; }
    public DateTime? EndDateTime { get; set; }

    public int? CompetitionId { get; set; } // Only the top-level group can have a competition.
    public Competition? Competition { get; set; }

    public int? ParentGroupId { get; set; } // Can't have a parent group if it's a top-level group.
    public ParticipationGroup? ParentGroup { get; set; }

    public List<ParticipationGroup> SubGroups { get; set; } = [];

    // Can only have participations if it's a bottom-level group. Meaning it has no sub-groups.
    public List<Participation> Participations { get; set; } = [];
}