using OnePieceCrewManager.Core;
using OnePieceCrewManager.Core.Services;

public class AddCrewAction : IMenuAction
{
    private readonly CrewMemberService _service;
    private readonly JsonStorageService _storage;

    public string Key => "2";
    public string Description => "Új tag hozzáadása";

    public AddCrewAction(CrewMemberService service, JsonStorageService storage)
    {
        _service = service;
        _storage = storage;
    }

    public void Execute()
    {
        Console.Write("Név: "); var name = Console.ReadLine()!;
        Console.Write("Szerep: "); var roleStr = Console.ReadLine()!;
        Console.Write("Bounty: "); var bountyStr = Console.ReadLine()!;
        Console.Write("Kor: "); var ageStr = Console.ReadLine()!;
        Console.Write("Erősség (1–100): "); var strengthStr = Console.ReadLine()!;

        if (!Enum.TryParse<Role>(roleStr, true, out var role) ||
            !int.TryParse(bountyStr, out var bounty) ||
            !int.TryParse(ageStr, out var age) ||
            !int.TryParse(strengthStr, out var strength))
        {
            Console.WriteLine("Hiba: érvénytelen adat.");
            return;
        }

        var member = new CrewMember(name, role, bounty, age, strength);
        _service.Add(member);

        if (_storage.Save(_service.ListAll().ToList()))
            Console.WriteLine("Tag sikeresen hozzáadva és mentve.");
        else
            Console.WriteLine("Hiba: a tag hozzáadva, de mentés nem sikerült.");
    }
}
