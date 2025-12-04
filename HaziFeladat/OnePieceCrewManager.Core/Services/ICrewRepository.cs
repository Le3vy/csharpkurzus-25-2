namespace OnePieceCrewManager.Core.Services
{
    public interface ICrewRepository
    {
        IReadOnlyList<CrewMember> ListAll();
        void Add(CrewMember member);
        CrewMember? FindByName(string name);
    }

}
