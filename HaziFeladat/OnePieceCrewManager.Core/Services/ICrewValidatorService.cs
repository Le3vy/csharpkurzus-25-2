namespace OnePieceCrewManager.Core.Services
{
    public interface ICrewValidatorService
    {
        void Validate(CrewMember member, IReadOnlyList<CrewMember> existingCrew);
    }

}
