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
        Musician
    }

    public record CrewMember(
        string Name,
        Role Role,
        int Bounty,       // millió Berry
        int BirthYear,
        int StrengthLevel // 1–100
    )
    {
        public string Name { get; } = Name;
        public Role Role { get; } = Role;
        public int Bounty { get; } = Bounty;
        public int BirthYear { get; } = BirthYear;
        public int StrengthLevel { get; } = StrengthLevel;
    }

}
