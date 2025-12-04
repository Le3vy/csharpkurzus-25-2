namespace OnePieceCrewManager.Core.Services
{
    public interface ICrewAnalytics
    {
        IEnumerable<CrewMember> FindHighBounty(int minBounty);
        IEnumerable<(Role Role, int Count)> CountByRole();
        (double avgBounty, int maxStrength, int youngestYear) Stats();
    }

}
