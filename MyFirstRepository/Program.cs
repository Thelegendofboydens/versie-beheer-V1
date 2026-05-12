using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

class Program
{
    static void Main()
    {
        Console.Clear();
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine("╔════════════════════════════════════════╗");
        Console.WriteLine("║     WELCOME TO DUNGEON RPG QUEST       ║");
        Console.WriteLine("╚════════════════════════════════════════╝\n");
        Console.ResetColor();

        Game game = new Game();
        game.Start();
    }
}

class Game
{
    // Constants for efficiency and maintainability
    private const ConsoleColor HeaderColor = ConsoleColor.DarkCyan;
    private const ConsoleColor MenuColor = ConsoleColor.Cyan;
    private const ConsoleColor PromptColor = ConsoleColor.Yellow;
    private const ConsoleColor SuccessColor = ConsoleColor.Green;
    private const ConsoleColor ErrorColor = ConsoleColor.Red;
    private const ConsoleColor InfoColor = ConsoleColor.White;
    private const ConsoleColor EnemyColor = ConsoleColor.Red;
    private const ConsoleColor PlayerColor = ConsoleColor.Green;
    private const ConsoleColor GoldColor = ConsoleColor.Yellow;
    private const ConsoleColor NarrationColor = ConsoleColor.Gray;

    private Player player = null!;
    private Store store = null!;
    private Random random = new Random();
    private bool gameRunning = true;

    // Helper methods for efficiency
    private void DrawHeader(string title)
    {
        Console.Clear();
        WriteLine("═══════════════════════════════════════", HeaderColor);
        WriteLine($"            {title}", HeaderColor);
        WriteLine("═══════════════════════════════════════\n", HeaderColor);
    }

    private void DisplayMenu(params string[] options)
    {
        foreach (var option in options)
        {
            WriteLine(option, MenuColor);
        }
    }

    private void DisplayPrompt(string prompt)
    {
        Write(prompt, PromptColor);
    }

    private void DisplayStats()
    {
        WriteLine(player.GetStats(), InfoColor);
    }

    public void Start()
    {
        CreateCharacter();
        MainMenu();
    }

    private void CreateCharacter()
    {
        DrawHeader("CHARACTER CREATION");
        DisplayPrompt("Enter your character name: ");
        string name = Console.ReadLine() ?? "Hero";

        WriteLine("\nChoose your class:", ConsoleColor.Magenta);
        DisplayMenu(
            "1. Warrior (High HP, Medium Damage)",
            "2. Mage (Low HP, High Magic Damage)",
            "3. Rogue (Medium HP, High Physical Damage)"
        );
        DisplayPrompt("Choose (1-3): ");

        string classChoice = Console.ReadLine() ?? "1";
        PlayerClass playerClass = classChoice switch
        {
            "1" => PlayerClass.Warrior,
            "2" => PlayerClass.Mage,
            "3" => PlayerClass.Rogue,
            _ => PlayerClass.Warrior
        };

        player = new Player(name, playerClass);
        store = new Store();

        Console.Clear();
        WriteLine($"Welcome {player.Name}, the {playerClass}!\n", SuccessColor, true);
        DisplayStats();
        PressAnyKey();
    }

    private void MainMenu()
    {
        while (gameRunning)
        {
            DrawHeader($"{player.Name} - Level {player.Level} | HP: {player.CurrentHP}/{player.MaxHP} | Gold: {player.Gold}");

            if (player.CurrentHP <= 0)
            {
                WriteLine("You have been defeated! Game Over.", ErrorColor);
                gameRunning = false;
                break;
            }

            WriteLine("What would you like to do?", ConsoleColor.Magenta);
            DisplayMenu(
                "1. Enter the Dungeon",
                "2. Visit the Store",
                "3. Check Inventory",
                "4. View Stats",
                "5. Rest and Heal",
                "6. Exit Game"
            );
            DisplayPrompt("Choose (1-6): ");

            string choice = Console.ReadLine() ?? "";
            switch (choice)
            {
                case "1":
                    EnterDungeon();
                    break;
                case "2":
                    VisitStore();
                    break;
                case "3":
                    ViewInventory();
                    break;
                case "4":
                    ViewStats();
                    break;
                case "5":
                    RestAndHeal();
                    break;
                case "6":
                    ExitGame();
                    break;
                default:
                    WriteLine("Invalid choice. Try again.", ErrorColor);
                    PressAnyKey();
                    break;
            }
        }
    }

    private void EnterDungeon()
    {
        DrawHeader("ENTERING THE DUNGEON");
        WriteLine("You stand at the entrance of a dark, mysterious dungeon.", NarrationColor, true);
        WriteLine("The air is cold and you can hear strange sounds echoing...\n", NarrationColor, true);

        WriteLine("Choose your path:", PromptColor);
        DisplayMenu(
            "1. Go LEFT into the shadows",
            "2. Go RIGHT toward a faint light",
            "3. Go STRAIGHT into the darkness"
        );
        DisplayPrompt("Choose (1-3): ");

        string pathChoice = Console.ReadLine() ?? "";

        switch (pathChoice)
        {
            case "1":
                LeftPath();
                break;
            case "2":
                RightPath();
                break;
            case "3":
                StraightPath();
                break;
            default:
                WriteLine("You freeze in fear and don't move.", InfoColor);
                PressAnyKey();
                break;
        }
    }

    private void LeftPath()
    {
        DrawHeader("LEFT PATH - THE SHADOWS");
        WriteLine("You venture into the shadows...", NarrationColor, true);
        WriteLine("Suddenly, a GOBLIN jumps out!\n", EnemyColor, true);

        Enemy goblin = new Enemy("Goblin", 15, 3, 5);
        DoCombat(goblin);

        if (player.CurrentHP > 0)
        {
            Console.Clear();
            WriteLine("\nYou defeated the Goblin!", SuccessColor);
            int goldReward = random.Next(10, 25);
            player.GainGold(goldReward);
            WriteLine($"You gained {goldReward} gold!\n", GoldColor);

            WriteLine("You find a treasure chest!", InfoColor);
            DisplayMenu("1. Open it", "2. Leave it and continue");
            DisplayPrompt("Choose: ");

            if ((Console.ReadLine() ?? "") == "1")
            {
                player.AddItem(new Item("Health Potion", ItemType.Potion, 25));
                WriteLine("You found a Health Potion!\n", SuccessColor);
            }
        }

        PressAnyKey();
    }

    private void RightPath()
    {
        DrawHeader("RIGHT PATH - THE LIGHT");
        WriteLine("You follow the light and discover a peaceful chamber.", InfoColor, true);
        WriteLine("An old merchant stands here with glowing eyes.\n", ConsoleColor.Magenta, true);

        WriteLine("The merchant speaks: 'I see the warrior spirit in you...'", ConsoleColor.Cyan);
        DisplayMenu(
            "1. Buy rare items from the merchant",
            "2. Fight the merchant for his treasures",
            "3. Leave the chamber"
        );
        DisplayPrompt("Choose: ");

        string merchantChoice = Console.ReadLine() ?? "";

        if (merchantChoice == "1")
        {
            Console.Clear();
            WriteLine("\nThe merchant's wares:", InfoColor);
            WriteLine("- Magic Scroll (50 gold) - +10 Magic Damage", MenuColor);
            WriteLine("- Enchanted Shield (75 gold) - +20 Defense", MenuColor);
            WriteLine("- Elixir of Power (100 gold) - Full HP restore", MenuColor);

            DisplayPrompt("What would you like to buy? (name or 0 to skip): ");
            string itemChoice = (Console.ReadLine() ?? "").ToLower();

            switch (itemChoice)
            {
                case "magic scroll":
                    if (player.Gold >= 50)
                    {
                        player.SpendGold(50);
                        player.AddItem(new Item("Magic Scroll", ItemType.Weapon, 0));
                        WriteLine("You purchased the Magic Scroll!", SuccessColor);
                    }
                    else
                        WriteLine("Not enough gold!", ErrorColor);
                    break;
                case "enchanted shield":
                    if (player.Gold >= 75)
                    {
                        player.SpendGold(75);
                        player.AddItem(new Item("Enchanted Shield", ItemType.Armor, 0));
                        WriteLine("You purchased the Enchanted Shield!", SuccessColor);
                    }
                    else
                        WriteLine("Not enough gold!", ErrorColor);
                    break;
                case "elixir of power":
                    if (player.Gold >= 100)
                    {
                        player.SpendGold(100);
                        player.Heal(player.MaxHP);
                        WriteLine("You drank the Elixir! Full health restored!", SuccessColor);
                    }
                    else
                        WriteLine("Not enough gold!", ErrorColor);
                    break;
            }
        }
        else if (merchantChoice == "2")
        {
            WriteLine("\nThe merchant's eyes glow red!", EnemyColor);
            Enemy merchant = new Enemy("Dark Merchant", 30, 6, 50);
            DoCombat(merchant);

            if (player.CurrentHP > 0)
            {
                WriteLine("\nYou defeated the merchant!", SuccessColor);
                player.GainGold(100);
                WriteLine("You gained 100 gold!", GoldColor);
            }
        }

        PressAnyKey();
    }

    private void StraightPath()
    {
        DrawHeader("STRAIGHT PATH - DARKNESS");
        WriteLine("You walk deeper into absolute darkness...", NarrationColor, true);
        WriteLine("Your eyes adjust and you see TWO ENEMIES!\n", EnemyColor, true);

        WriteLine("A SKELETON WARRIOR and an ORC BRUTE block your path!\n", EnemyColor);

        Enemy skeleton = new Enemy("Skeleton Warrior", 20, 4, 15);
        Enemy orc = new Enemy("Orc Brute", 25, 5, 20);

        DisplayMenu("1. Fight them both!", "2. Try to sneak past");
        DisplayPrompt("Choose: ");

        if ((Console.ReadLine() ?? "") == "1")
        {
            DoCombat(skeleton);
            if (player.CurrentHP > 0)
            {
                WriteLine("\nSkeleton defeated!\n", SuccessColor);
                DoCombat(orc);

                if (player.CurrentHP > 0)
                {
                    WriteLine("\nYou defeated both enemies!", SuccessColor);
                    player.GainGold(100);
                    player.GainExperience(50);
                    WriteLine("You gained 100 gold and 50 experience!", GoldColor);
                }
            }
        }
        else
        {
            if (random.Next(0, 2) == 0)
            {
                WriteLine("You successfully sneak past them!", SuccessColor);
                WriteLine("You find a hidden treasure room!", InfoColor);
                player.GainGold(75);
                WriteLine("You gained 75 gold!", GoldColor);
            }
            else
            {
                WriteLine("They spot you!", ErrorColor);
                DoCombat(skeleton);
            }
        }

        PressAnyKey();
    }

    private void DisplayHP(Enemy enemy)
    {
        WriteLine($"{player.Name} HP: {player.CurrentHP}/{player.MaxHP}", PlayerColor);
        WriteLine($"{enemy.Name} HP: {enemy.CurrentHP}/{enemy.MaxHP}\n", EnemyColor);
    }

    private void DoCombat(Enemy enemy)
    {
        DrawHeader("COMBAT STARTED!");

        while (player.CurrentHP > 0 && enemy.CurrentHP > 0)
        {
            DisplayHP(enemy);

            DisplayMenu("1. Attack", "2. Use Potion", "3. Defend");
            DisplayPrompt("Choose: ");

            string action = Console.ReadLine() ?? "1";

            switch (action)
            {
                case "1":
                    PlayerAttack(enemy);
                    break;
                case "2":
                    UsePotion();
                    break;
                case "3":
                    PlayerDefend(enemy);
                    break;
                default:
                    WriteLine("Invalid action!", ErrorColor);
                    continue;
            }

            if (enemy.CurrentHP > 0)
            {
                EnemyAttack(enemy);
            }

            Console.WriteLine();
        }

        if (player.CurrentHP <= 0)
        {
            WriteLine("\n💀 You were defeated! 💀", ErrorColor);
            WriteLine("You lost half your gold!", GoldColor);
            player.SpendGold(player.Gold / 2);
            player.CurrentHP = 1;
        }
        else
        {
            WriteLine($"\n✓ Victory! {enemy.Name} defeated!", SuccessColor);
            int goldReward = enemy.GoldReward;
            player.GainGold(goldReward);
            WriteLine($"You gained {goldReward} gold!", GoldColor);
        }
    }

    private void PlayerAttack(Enemy enemy)
    {
        int damage = player.GetDamage() + random.Next(-2, 3);
        damage = Math.Max(1, damage);
        enemy.TakeDamage(damage);
        WriteLine($"You attack {enemy.Name} for {damage} damage!", ConsoleColor.Yellow);
    }

    private void PlayerDefend(Enemy enemy)
    {
        WriteLine($"You take a defensive stance!", ConsoleColor.Cyan);
        int enemyDamage = enemy.GetDamage() / 2 + random.Next(-1, 2);
        enemyDamage = Math.Max(0, enemyDamage);
        player.TakeDamage(enemyDamage);
        WriteLine($"{enemy.Name} attacks but you block half the damage ({enemyDamage})!", ConsoleColor.Green);
    }

    private void UsePotion()
    {
        var potions = player.Inventory.Where(i => i.Type == ItemType.Potion).FirstOrDefault();
        if (potions != null)
        {
            player.Heal(25);
            player.Inventory.Remove(potions);
            WriteLine("You used a Health Potion and recovered 25 HP!", ConsoleColor.Green);
        }
        else
        {
            WriteLine("You don't have any potions!", ConsoleColor.Red);
        }
    }

    private void EnemyAttack(Enemy enemy)
    {
        int damage = enemy.GetDamage() + random.Next(-1, 2);
        damage = Math.Max(1, damage);
        player.TakeDamage(damage);
        WriteLine($"{enemy.Name} attacks you for {damage} damage!", ConsoleColor.Red);
    }

    private void VisitStore()
    {
        DrawHeader("WELCOME TO THE STORE");
        WriteLine($"Your Gold: {player.Gold}\n", GoldColor);

        bool shopping = true;
        while (shopping)
        {
            DisplayMenu(
                "1. Buy Health Potion (25 gold)",
                "2. Buy Mana Potion (20 gold)",
                "3. Buy Iron Sword (50 gold)",
                "4. Buy Leather Armor (40 gold)",
                "5. Sell items",
                "6. Leave store"
            );
            DisplayPrompt("Choose: ");

            string choice = Console.ReadLine() ?? "6";

            switch (choice)
            {
                case "1":
                    if (player.Gold >= 25)
                    {
                        player.SpendGold(25);
                        player.AddItem(new Item("Health Potion", ItemType.Potion, 25));
                        WriteLine("✓ Purchased Health Potion!", SuccessColor);
                    }
                    else
                        WriteLine("✗ Not enough gold!", ErrorColor);
                    break;
                case "2":
                    if (player.Gold >= 20)
                    {
                        player.SpendGold(20);
                        player.AddItem(new Item("Mana Potion", ItemType.Potion, 15));
                        WriteLine("✓ Purchased Mana Potion!", SuccessColor);
                    }
                    else
                        WriteLine("✗ Not enough gold!", ErrorColor);
                    break;
                case "3":
                    if (player.Gold >= 50)
                    {
                        player.SpendGold(50);
                        player.AddItem(new Item("Iron Sword", ItemType.Weapon, 8));
                        WriteLine("✓ Purchased Iron Sword!", SuccessColor);
                    }
                    else
                        WriteLine("✗ Not enough gold!", ErrorColor);
                    break;
                case "4":
                    if (player.Gold >= 40)
                    {
                        player.SpendGold(40);
                        player.AddItem(new Item("Leather Armor", ItemType.Armor, 5));
                        WriteLine("✓ Purchased Leather Armor!", SuccessColor);
                    }
                    else
                        WriteLine("✗ Not enough gold!", ErrorColor);
                    break;
                case "5":
                    SellItems();
                    break;
                case "6":
                    shopping = false;
                    break;
                default:
                    WriteLine("Invalid choice!", ErrorColor);
                    break;
            }

            if (choice != "6")
            {
                WriteLine($"Gold: {player.Gold}\n", GoldColor);
            }
        }
    }

    private void SellItems()
    {
        WriteLine("\nItems to sell:", ConsoleColor.Magenta);
        if (player.Inventory.Count == 0)
        {
            WriteLine("You have no items!", ConsoleColor.Yellow);
            return;
        }

        for (int i = 0; i < player.Inventory.Count; i++)
        {
            WriteLine($"{i + 1}. {player.Inventory[i].Name} (Worth {player.Inventory[i].Value / 2} gold)", ConsoleColor.Cyan);
        }

        Write("Choose item to sell (number): ", ConsoleColor.Yellow);
        if (int.TryParse(Console.ReadLine() ?? "", out int index) && index > 0 && index <= player.Inventory.Count)
        {
            Item item = player.Inventory[index - 1];
            int salePrice = item.Value / 2;
            player.GainGold(salePrice);
            player.Inventory.RemoveAt(index - 1);
            WriteLine($"Sold {item.Name} for {salePrice} gold!", ConsoleColor.Green);
        }
    }

    private void ViewInventory()
    {
        DrawHeader("YOUR INVENTORY");

        if (player.Inventory.Count == 0)
        {
            WriteLine("Your inventory is empty!", InfoColor);
        }
        else
        {
            for (int i = 0; i < player.Inventory.Count; i++)
            {
                Item item = player.Inventory[i];
                WriteLine($"{i + 1}. {item.Name} ({item.Type})", MenuColor);
            }
        }

        WriteLine($"\nGold: {player.Gold}", GoldColor);
        PressAnyKey();
    }

    private void ViewStats()
    {
        DrawHeader("CHARACTER STATS");
        DisplayStats();
        PressAnyKey();
    }

    private void RestAndHeal()
    {
        Console.Clear();
        WriteLine("You rest at the tavern...\n", ConsoleColor.DarkCyan, true);
        player.Heal(player.MaxHP);
        WriteLine("You are fully healed!", ConsoleColor.Green);
        PressAnyKey();
    }

    private void ExitGame()
    {
        Console.Clear();
        WriteLine("═══════════════════════════════════════", ConsoleColor.DarkYellow);
        WriteLine($"Thanks for playing, {player.Name}!", ConsoleColor.Yellow);
        WriteLine($"Final Level: {player.Level}", ConsoleColor.Yellow);
        WriteLine($"Total Gold: {player.Gold}", ConsoleColor.Yellow);
        WriteLine("═══════════════════════════════════════", ConsoleColor.DarkYellow);
        gameRunning = false;
    }

    private void PressAnyKey()
    {
        Console.WriteLine("\nPress any key to continue...");
        Console.ReadKey();
    }

    private void WriteLine(string text, ConsoleColor color = ConsoleColor.White, bool slow = false)
    {
        Console.ForegroundColor = color;
        if (slow)
        {
            foreach (char c in text)
            {
                Console.Write(c);
                Thread.Sleep(15);
            }
            Console.WriteLine();
        }
        else
        {
            Console.WriteLine(text);
        }
        Console.ResetColor();
    }

    private void Write(string text, ConsoleColor color = ConsoleColor.White)
    {
        Console.ForegroundColor = color;
        Console.Write(text);
        Console.ResetColor();
    }
}

enum PlayerClass
{
    Warrior,
    Mage,
    Rogue
}

enum ItemType
{
    Weapon,
    Armor,
    Potion
}

class Player
{
    public string Name { get; set; }
    public PlayerClass Class { get; set; }
    public int Level { get; set; } = 1;
    public int Experience { get; set; } = 0;
    public int MaxHP { get; set; }
    public int CurrentHP { get; set; }
    public int Attack { get; set; }
    public int Defense { get; set; }
    public int Gold { get; set; } = 50;
    public List<Item> Inventory { get; set; } = new List<Item>();

    public Player(string name, PlayerClass playerClass)
    {
        Name = name;
        Class = playerClass;

        switch (playerClass)
        {
            case PlayerClass.Warrior:
                MaxHP = 100;
                Attack = 8;
                Defense = 5;
                break;
            case PlayerClass.Mage:
                MaxHP = 60;
                Attack = 12;
                Defense = 2;
                break;
            case PlayerClass.Rogue:
                MaxHP = 75;
                Attack = 10;
                Defense = 3;
                break;
        }

        CurrentHP = MaxHP;
    }

    public void TakeDamage(int damage)
    {
        damage = Math.Max(0, damage - Defense / 2);
        CurrentHP -= damage;
    }

    public void Heal(int amount)
    {
        CurrentHP = Math.Min(MaxHP, CurrentHP + amount);
    }

    public void GainGold(int amount)
    {
        Gold += amount;
    }

    public void SpendGold(int amount)
    {
        Gold = Math.Max(0, Gold - amount);
    }

    public void GainExperience(int amount)
    {
        Experience += amount;
        if (Experience >= 100)
        {
            LevelUp();
        }
    }

    public void LevelUp()
    {
        Level++;
        Experience = 0;
        MaxHP += 10;
        CurrentHP = MaxHP;
        Attack += 2;
        Defense += 1;
        Console.WriteLine($"\n⭐ LEVEL UP! You are now level {Level}!");
    }

    public void AddItem(Item item)
    {
        Inventory.Add(item);
    }

    public int GetDamage()
    {
        return Attack + random.Next(0, 3);
    }

    private Random random = new Random();

    public string GetStats()
    {
        return $"Name: {Name}\n" +
               $"Class: {Class}\n" +
               $"Level: {Level}\n" +
               $"Experience: {Experience}/100\n" +
               $"HP: {CurrentHP}/{MaxHP}\n" +
               $"Attack: {Attack}\n" +
               $"Defense: {Defense}\n" +
               $"Gold: {Gold}\n" +
               $"Items: {Inventory.Count}";
    }
}

class Enemy
{
    public string Name { get; set; }
    public int CurrentHP { get; set; }
    public int MaxHP { get; set; }
    public int Attack { get; set; }
    public int GoldReward { get; set; }
    private Random random = new Random();

    public Enemy(string name, int hp, int attack, int goldReward)
    {
        Name = name;
        MaxHP = hp;
        CurrentHP = hp;
        Attack = attack;
        GoldReward = goldReward;
    }

    public void TakeDamage(int damage)
    {
        CurrentHP -= damage;
    }

    public int GetDamage()
    {
        return Attack + random.Next(0, 3);
    }
}

class Item
{
    public string Name { get; set; }
    public ItemType Type { get; set; }
    public int Value { get; set; }

    public Item(string name, ItemType type, int value)
    {
        Name = name;
        Type = type;
        Value = value;
    }
}

class Store
{
    public List<Item> Items { get; set; }

    public Store()
    {
        Items = new List<Item>
        {
            new Item("Health Potion", ItemType.Potion, 25),
            new Item("Mana Potion", ItemType.Potion, 20),
            new Item("Iron Sword", ItemType.Weapon, 50),
            new Item("Leather Armor", ItemType.Armor, 40)
        };
    }
}
