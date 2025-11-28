using OnePieceCrewManager.Core.Exceptions;

namespace OnePieceCrewManager.Core.Services
{
    public class CrewMemberService
    {
        private readonly List<CrewMember> _crew;

        public CrewMemberService(List<CrewMember> initialCrew)
        {
            _crew = initialCrew ?? new List<CrewMember>();
        }

        // Generikus kollekció visszaadása
        public IReadOnlyList<CrewMember> ListAll() => _crew;

        // Új tag hozzáadása, üzleti szabályokkal
        public void Add(CrewMember member)
        {
            if (_crew.Any(c => c.Name.Equals(member.Name, StringComparison.OrdinalIgnoreCase)))
                throw new CrewException($"Már létezik ilyen nevű tag: {member.Name}");

            if (member.Bounty < 0)
                throw new CrewException("A bounty nem lehet negatív.");

            if (member.StrengthLevel is < 1 or > 100)
                throw new CrewException("Az erősség 1 és 100 között kell legyen.");

            _crew.Add(member);
        }

        // LINQ: Szűrés bounty alapján
        public IEnumerable<CrewMember> FindHighBounty(int minBounty) =>
            _crew.Where(c => c.Bounty >= minBounty).OrderByDescending(c => c.Bounty);

        // LINQ: Csoportosítás szerep szerint
        public IEnumerable<(Role Role, int Count)> CountByRole() =>
            _crew.GroupBy(c => c.Role)
                 .Select(g => (Role: g.Key, Count: g.Count()))
                 .OrderByDescending(x => x.Count);

        // LINQ: Aggregációk
        public (double avgBounty, int maxStrength, int youngestYear) Stats()
        {
            if (_crew.Count == 0) return (0, 0, 0);

            double avg = _crew.Average(c => c.Bounty);
            int maxStrength = _crew.Max(c => c.StrengthLevel);
            int youngestYear = _crew.Min(c => c.BirthYear);

            return (avg, maxStrength, youngestYear);
        }

        // LINQ: FirstOrDefault keresés név alapján
        public CrewMember? FindByName(string name) =>
            _crew.FirstOrDefault(c => c.Name.Equals(name, StringComparison.OrdinalIgnoreCase));
    }
}
