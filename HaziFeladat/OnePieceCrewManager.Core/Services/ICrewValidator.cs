namespace OnePieceCrewManager.Core.Services
{
    public interface ICrewValidator
    {
        void Validate(CrewMember member, IReadOnlyList<CrewMember> existingCrew);
    }

}
