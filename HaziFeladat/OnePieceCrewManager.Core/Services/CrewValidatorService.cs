using OnePieceCrewManager.Core;
using OnePieceCrewManager.Core.Exceptions;
using OnePieceCrewManager.Core.Services;

public class CrewValidatorService : ICrewValidatorService
{
    public void Validate(CrewMember member, IReadOnlyList<CrewMember> existingCrew)
    {
        if (existingCrew.Any(c => c.Name.Equals(member.Name, StringComparison.OrdinalIgnoreCase)))
            throw new CrewException($"Már létezik ilyen nevű tag: {member.Name}");

        if (member.Bounty < 0)
            throw new CrewException("A bounty nem lehet negatív.");

        if (member.StrengthLevel is < 1 or > 100)
            throw new CrewException("Az erősség 1 és 100 között kell legyen.");
    }
}

