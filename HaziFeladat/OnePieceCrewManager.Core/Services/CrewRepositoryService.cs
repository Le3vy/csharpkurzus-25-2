namespace OnePieceCrewManager.Core.Services
{
    public class CrewRepositoryService : ICrewRepositoryService
    {
        private readonly List<CrewMember> _crew = new();

        public CrewRepositoryService(List<CrewMember>? initialCrew = null)
        {
            if (initialCrew != null) _crew.AddRange(initialCrew);
        }

        public IReadOnlyList<CrewMember> ListAll() => _crew;

        public void Add(CrewMember member) => _crew.Add(member);

        public CrewMember? FindByName(string name) =>
            _crew.FirstOrDefault(c => c.Name.Equals(name, StringComparison.OrdinalIgnoreCase));
    }

}
