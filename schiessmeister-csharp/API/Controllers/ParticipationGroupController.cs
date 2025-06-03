using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using schiessmeister_csharp.API.Extensions;
using schiessmeister_csharp.Domain.Models;
using schiessmeister_csharp.Domain.Repositories;

namespace schiessmeister_csharp.API.Controllers;

[ApiController]
[Route("api/participation-groups")]
[Authorize(Roles = "User")]
[ProducesResponseType(StatusCodes.Status401Unauthorized)]
public class ParticipationGroupController : ControllerBase {
    private readonly IParticipationGroupRepository _participationGroup;
    private readonly IParticipationRepository _participation;

    public ParticipationGroupController(IParticipationGroupRepository participationGroup, IParticipationRepository participation) {
        _participationGroup = participationGroup;
        _participation = participation;
    }

    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<ActionResult<Competition>> UpdateParticipationGroup(int id, ParticipationGroup newGroup) {
        var participationGroup = await _participationGroup.FindByIdWithAllParentsCompAsync(id);

        if (participationGroup == null)
            return NotFound();

        if (User.GetUserId() != participationGroup.GetCompetition().Organizer!.OwnerId)
            return Forbid();

        if (newGroup.ParentGroupId == null && newGroup.CompetitionId == null)
            return BadRequest("A participation group must either have a parent group or a competition.");

        if (newGroup.ParentGroupId != null && newGroup.CompetitionId != null)
            return BadRequest("A participation group cannot have both a parent group and a competition.");

        if (newGroup.SubGroups.Count > 0 && newGroup.Participations.Count > 0)
            return BadRequest("A participation group cannot have both subgrous and participations.");

        newGroup.Id = id;

        return Ok(await _participationGroup.UpdateAsync(newGroup));
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<IActionResult> DeleteParticipationGroup(int id, [FromQuery] bool preserveSubGroups = false) {
        var participationGroup = await _participationGroup.FindByIdWithAllParentsChildsCompAsync(id);

        if (participationGroup == null)
            return NotFound();

        if (User.GetUserId() != participationGroup.GetCompetition().Organizer!.OwnerId)
            return Forbid();

        if (preserveSubGroups) {
            var subGroups = participationGroup.SubGroups.ToList();

            if (participationGroup.ParentGroupId == null) {
                // Move all subGroups to the top-level (the competition)
                foreach (var subGroup in subGroups) {
                    subGroup.ParentGroupId = null;
                    subGroup.CompetitionId = participationGroup.CompetitionId;
                }
            } else {
                // Move all subGroups to the parent group
                foreach (var subGroup in subGroups) {
                    subGroup.ParentGroupId = participationGroup.ParentGroupId;
                }
            }

            await _participationGroup.UpdateRangeAsync(subGroups);
        } else {
            // Delete all subGroups
            await _participationGroup.DeleteRangeAsync(participationGroup.GetAllSubGroups());
        }

        await _participationGroup.DeleteAsync(participationGroup);

        return NoContent();
    }

    [HttpPost("{id}/sub-groups")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<ActionResult<ParticipationGroup>> CreateSubGroup(int id, ParticipationGroup newSubGroup) {
        var participationGroup = await _participationGroup.FindByIdWithAllParentsCompAsync(id);

        if (participationGroup == null)
            return NotFound();

        if (User.GetUserId() != participationGroup.GetCompetition().Organizer!.OwnerId)
            return Forbid();

        if (newSubGroup.CompetitionId != null)
            return BadRequest("A participation group cannot have both a parent group and a competition.");

        if (newSubGroup.SubGroups.Count > 0 && newSubGroup.Participations.Count > 0)
            return BadRequest("A participation group cannot have both subgrous and participations.");

        newSubGroup.ParentGroupId = id;
        await _participationGroup.AddAsync(newSubGroup);

        return CreatedAtAction(nameof(CompetitionController.GetCompetition), new { id = newSubGroup.Id }, newSubGroup);
    }

    [HttpPost("{id}/participations")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<ActionResult<ParticipationGroup>> CreateParticipation(int id, Participation participation) {
        var participationGroup = await _participationGroup.FindByIdWithAllParentsCompAsync(id);

        if (participationGroup == null)
            return NotFound();

        var comp = participationGroup.GetCompetition();

        if (User.GetUserId() != comp.Organizer!.OwnerId)
            return Forbid();

        if (participation.Discipline?.CompetitionId != comp.Id)
            return BadRequest("A participation can only have a discipline of the attended competition.");

        if (participation.ShooterId != participation.RecorderId)
            return BadRequest("The shooter is not allowed to record himself.");

        participation.ParticipationGroupId = id;
        await _participation.AddAsync(participation);

        return CreatedAtAction(nameof(CompetitionController.GetCompetition), new { id = participation.Id }, participation);
    }
}