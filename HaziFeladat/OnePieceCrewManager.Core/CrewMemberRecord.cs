namespace OnePieceCrewManager.Core
{
    public enum Role
    {
        Captain,
        Swordsman,
        Navigator,
        Sniper,
        Cook,
        Doctor,
        Archaeologist,
        Shipwright,
        Musician,
        Helmsman
    }

    public record CrewMember(
        string Name,
        Role Role,
        int Bounty,       // millió Berry
        int Age,
        int StrengthLevel // 1–100
    )
    {
        public string Name { get; } = Name;
        public Role Role { get; } = Role;
        public int Bounty { get; } = Bounty;
        public int Age { get; } = Age;
        public int StrengthLevel { get; } = StrengthLevel;
    }

}
