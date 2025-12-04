using OnePieceCrewManager.Core;
using OnePieceCrewManager.Core.Exceptions;
using OnePieceCrewManager.Core.Services;

public class AssembleMenu
{

    public static void assembleMenu()
    {
        string dataPath = Path.Combine(AppContext.BaseDirectory, "Data", "members.json");
        var storage = new JsonStorage(dataPath);
        var service = new CrewMemberService(storage.Load());

        Console.WriteLine("< One Piece Crew Manager >\n");

        bool exit = false;
        while (!exit)
        {
            Console.WriteLine("Válassz műveletet:");
            Console.WriteLine("1) Lista");
            Console.WriteLine("2) Új tag hozzáadása");
            Console.WriteLine("3) Keresés név alapján");
            Console.WriteLine("4) Nagy bounty-s tagok");
            Console.WriteLine("5) Szerepenkénti darabszám");
            Console.WriteLine("6) Statisztikák");
            Console.WriteLine("0) Kilépés");
            Console.Write("> ");

            var choice = Console.ReadLine();

            try
            {
                switch (choice)
                {
                    case "1":
                        foreach (var c in service.ListAll())
                            Console.WriteLine($"{c.Name} — {c.Role}, {c.Bounty}M Berry, {c.StrengthLevel}/100 erő");
                        break;

                    case "2":
                        Console.Write("Név: "); var name = Console.ReadLine()!;
                        Console.Write("Szerep (Captain, Swordsman, Navigator, Sniper, Cook, Doctor, Archaeologist, Shipwright, Musician, Helmsman): ");
                        var roleStr = Console.ReadLine()!;
                        Console.Write("Bounty (millió Berry): "); var bountyStr = Console.ReadLine()!;
                        Console.Write("Születési év: "); var yearStr = Console.ReadLine()!;
                        Console.Write("Erősség (1–100): "); var strengthStr = Console.ReadLine()!;

                        if (!Enum.TryParse<Role>(roleStr, true, out var role))
                        {
                            Console.WriteLine("Hiba: Ismeretlen szerep.");
                            break;
                        }
                        if (!int.TryParse(bountyStr, out var bounty))
                        {
                            Console.WriteLine("Hiba: bounty nem szám.");
                            break;
                        }
                        if (!int.TryParse(yearStr, out var year))
                        {
                            Console.WriteLine("Hiba: év nem szám.");
                            break;
                        }
                        if (!int.TryParse(strengthStr, out var strength))
                        {
                            Console.WriteLine("Hiba: erősség nem szám.");
                            break;
                        }

                        var member = new CrewMember(name, role, bounty, year, strength);
                        service.Add(member);

                        if (storage.Save(service.ListAll().ToList()))
                            Console.WriteLine("Tag sikeresen hozzáadva és mentve.");
                        else
                            Console.WriteLine("Hiba: a tag hozzáadva, de mentés nem sikerült.");
                        break;

                    case "3":
                        Console.Write("Név: "); var n = Console.ReadLine()!;
                        var found = service.FindByName(n);
                        Console.WriteLine(found is null
                            ? "Nincs ilyen tag."
                            : $"{found.Name} — {found.Role}, {found.Bounty}M Berry, {found.StrengthLevel}/100 erő");
                        break;

                    case "4":
                        Console.Write("Minimum bounty (millió Berry): ");
                        var minStr = Console.ReadLine()!;
                        if (!int.TryParse(minStr, out var min))
                        {
                            Console.WriteLine("Hiba: nem szám.");
                            break;
                        }
                        var high = service.FindHighBounty(min);
                        if (!high.Any()) Console.WriteLine("Nincs találat.");
                        else foreach (var c in high) Console.WriteLine($"{c.Name} — {c.Bounty}M Berry");
                        break;

                    case "5":
                        foreach (var pair in service.CountByRole())
                            Console.WriteLine($"{pair.Role}: {pair.Count} tag");
                        break;

                    case "6":
                        var (avgBounty, maxStrength, youngestYear) = service.Stats();
                        Console.WriteLine($"Átlag bounty: {avgBounty:F1}M Berry");
                        Console.WriteLine($"Legnagyobb erősség: {maxStrength}/100");
                        Console.WriteLine($"Legfiatalabb születési év: {youngestYear}");
                        break;

                    case "0":
                        exit = true;
                        break;

                    default:
                        Console.WriteLine("Ismeretlen választás.");
                        break;
                }
            }
            catch (CrewException ex)
            {
                Console.WriteLine($"Üzleti hiba: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Váratlan hiba: {ex.Message}");
            }

            Console.WriteLine();
        }

        Console.WriteLine("Kilépés. Viszlát, kalóz!");
    }
}