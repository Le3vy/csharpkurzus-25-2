using OnePieceCrewManager.Core.Services;

string dataPath = Path.Combine(AppContext.BaseDirectory, "Data", "members.json");
var storage = new JsonStorageService(dataPath);
var repository = new CrewRepositoryService(storage.Load());
var validator = new CrewValidatorService();
var service = new CrewMemberService(repository, validator);
var analytics = new CrewAnalyticsService(repository);

var actions = new List<IMenuAction>
{
    new ListCrewAction(repository),
    new AddCrewAction(service, storage),
    new FindByNameAction(service),
    new HighBountyAction(analytics),
    new CountByRoleAction(analytics),
    new StatsAction(analytics)
};

var menu = new AssembleMenu(actions);
menu.Run();
