using System;
using System.Collections.Generic;
using System.Linq;

class Program
{
    static void Main()
    {
        Console.Clear();
        Console.WriteLine("╔════════════════════════════════════════╗");
        Console.WriteLine("║     WELCOME TO DUNGEON RPG QUEST       ║");
        Console.WriteLine("╚════════════════════════════════════════╝\n");

        Game game = new Game();
        game.Start();
    }
}

class Game
{
    private Player player;
    private Store store;
    private Random random = new Random();
    private bool gameRunning = true;

    public void Start()
    {
        CreateCharacter();
        MainMenu();
    }

    private void CreateCharacter()
    {
        Console.WriteLine("=== CHARACTER CREATION ===\n");
        Console.Write("Enter your character name: ");
        string name = Console.ReadLine() ?? "Hero";

        Console.WriteLine("\nChoose your class:");
        Console.WriteLine("1. Warrior (High HP, Medium Damage)");
        Console.WriteLine("2. Mage (Low HP, High Magic Damage)");
        Console.WriteLine("3. Rogue (Medium HP, High Physical Damage)");
        Console.Write("Choose (1-3): ");

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
        Console.WriteLine($"Welcome {player?.Name}, the {playerClass}!\n");
        Console.WriteLine(player?.GetStats());
        PressAnyKey();
    }

    private void MainMenu()
    {
        while (gameRunning)
        {
            Console.Clear();
            Console.WriteLine($"╔════════════════════════════════════════╗");
            Console.WriteLine($"║ {player?.Name} - Level {player?.Level} | HP: {player?.CurrentHP}/{player?.MaxHP} | Gold: {player?.Gold}");
            Console.WriteLine($"╚════════════════════════════════════════╝\n");

            if (player?.CurrentHP <= 0)
            {
                Console.WriteLine("You have been defeated! Game Over.");
                gameRunning = false;
                break;
            }

            Console.WriteLine("What would you like to do?");
            Console.WriteLine("1. Enter the Dungeon");
            Console.WriteLine("2. Visit the Store");
            Console.WriteLine("3. Check Inventory");
            Console.WriteLine("4. View Stats");
            Console.WriteLine("5. Rest and Heal");
            Console.WriteLine("6. Exit Game");
            Console.Write("Choose (1-6): ");

            string choice = Console.ReadLine();
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
                    Console.WriteLine("Invalid choice. Try again.");
                    PressAnyKey();
                    break;
            }
        }
    }

    private void EnterDungeon()
    {
        Console.Clear();
        Console.WriteLine("═══════════════════════════════════════");
        Console.WriteLine("          ENTERING THE DUNGEON");
        Console.WriteLine("═══════════════════════════════════════\n");

        Console.WriteLine("You stand at the entrance of a dark, mysterious dungeon.");
        Console.WriteLine("The air is cold and you can hear strange sounds echoing...\n");

        Console.WriteLine("Choose your path:");
        Console.WriteLine("1. Go LEFT into the shadows");
        Console.WriteLine("2. Go RIGHT toward a faint light");
        Console.WriteLine("3. Go STRAIGHT into the darkness");
        Console.Write("Choose (1-3): ");

        string pathChoice = Console.ReadLine();

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
                Console.WriteLine("You freeze in fear and don't move.");
                break;
        }
    }

    private void LeftPath()
    {
        Console.Clear();
        Console.WriteLine("═══════════════════════════════════════");
        Console.WriteLine("            LEFT PATH - THE SHADOWS");
        Console.WriteLine("═══════════════════════════════════════\n");

        Console.WriteLine("You venture into the shadows...");
        Console.WriteLine("Suddenly, a GOBLIN jumps out!\n");

        Enemy goblin = new Enemy("Goblin", 15, 3, 5);
        DoCombat(goblin);

        if (player.CurrentHP > 0)
        {
            Console.Clear();
            Console.WriteLine("\nYou defeated the Goblin!");
            int goldReward = random.Next(10, 25);
            player.GainGold(goldReward);
            Console.WriteLine($"You gained {goldReward} gold!\n");

            Console.WriteLine("You find a treasure chest!");
            Console.WriteLine("1. Open it");
            Console.WriteLine("2. Leave it and continue");
            Console.Write("Choose: ");

            if ((Console.ReadLine() ?? "") == "1")
            {
                int potionReward = random.Next(1, 4);
                player?.AddItem(new Item("Health Potion", ItemType.Potion, 25));
                Console.WriteLine($"You found a Health Potion!\n");
            }
        }

        PressAnyKey();
    }

    private void RightPath()
    {
        Console.Clear();
        Console.WriteLine("═══════════════════════════════════════");
        Console.WriteLine("         RIGHT PATH - THE LIGHT");
        Console.WriteLine("═══════════════════════════════════════\n");

        Console.WriteLine("You follow the light and discover a peaceful chamber.");
        Console.WriteLine("An old merchant stands here with glowing eyes.\n");

        Console.WriteLine("The merchant speaks: 'I see the warrior spirit in you...'");
        Console.WriteLine("1. Buy rare items from the merchant");
        Console.WriteLine("2. Fight the merchant for his treasures");
        Console.WriteLine("3. Leave the chamber");
        Console.Write("Choose: ");

        string merchantChoice = Console.ReadLine() ?? "";

        if (merchantChoice == "1")
        {
            Console.Clear();
            Console.WriteLine("\nThe merchant's wares:");
            Console.WriteLine("- Magic Scroll (50 gold) - +10 Magic Damage");
            Console.WriteLine("- Enchanted Shield (75 gold) - +20 Defense");
            Console.WriteLine("- Elixir of Power (100 gold) - Full HP restore");

            Console.Write("What would you like to buy? (name or 0 to skip): ");
            string itemChoice = (Console.ReadLine() ?? "").ToLower();

            switch (itemChoice)
            {
                case "magic scroll":
                    if (player?.Gold >= 50)
                    {
                        player?.SpendGold(50);
                        player?.AddItem(new Item("Magic Scroll", ItemType.Weapon, 0));
                        Console.WriteLine("You purchased the Magic Scroll!");
                    }
                    else
                        Console.WriteLine("Not enough gold!");
                    break;
                case "enchanted shield":
                    if (player?.Gold >= 75)
                    {
                        player?.SpendGold(75);
                        player?.AddItem(new Item("Enchanted Shield", ItemType.Armor, 0));
                        Console.WriteLine("You purchased the Enchanted Shield!");
                    }
                    else
                        Console.WriteLine("Not enough gold!");
                    break;
                case "elixir of power":
                    if (player?.Gold >= 100)
                    {
                        player?.SpendGold(100);
                        player?.Heal(player?.MaxHP ?? 100);
                        Console.WriteLine("You drank the Elixir! Full health restored!");
                    }
                    else
                        Console.WriteLine("Not enough gold!");
                    break;
            }
        }
        else if (merchantChoice == "2")
        {
            Console.WriteLine("\nThe merchant's eyes glow red!");
            Enemy merchant = new Enemy("Dark Merchant", 30, 6, 50);
            DoCombat(merchant);

            if (player.CurrentHP > 0)
            {
                Console.WriteLine("\nYou defeated the merchant!");
                player.GainGold(100);
                Console.WriteLine("You gained 100 gold!");
            }
        }

        PressAnyKey();
    }

    private void StraightPath()
    {
        Console.Clear();
        Console.WriteLine("═══════════════════════════════════════");
        Console.WriteLine("         STRAIGHT PATH - DARKNESS");
        Console.WriteLine("═══════════════════════════════════════\n");

        Console.WriteLine("You walk deeper into absolute darkness...");
        Console.WriteLine("Your eyes adjust and you see TWO ENEMIES!\n");

        Console.WriteLine("A SKELETON WARRIOR and an ORC BRUTE block your path!\n");

        Enemy skeleton = new Enemy("Skeleton Warrior", 20, 4, 15);
        Enemy orc = new Enemy("Orc Brute", 25, 5, 20);

        Console.WriteLine("1. Fight them both!");
        Console.WriteLine("2. Try to sneak past");
        Console.Write("Choose: ");

        if (Console.ReadLine() == "1")
        {
            DoCombat(skeleton);
            if (player.CurrentHP > 0)
            {
                Console.WriteLine("\nSkeleton defeated!\n");
                DoCombat(orc);

                if (player.CurrentHP > 0)
                {
                    Console.WriteLine("\nYou defeated both enemies!");
                    player.GainGold(100);
                    player.GainExperience(50);
                    Console.WriteLine("You gained 100 gold and 50 experience!");
                }
            }
        }
        else
        {
            if (random.Next(0, 2) == 0)
            {
                Console.WriteLine("You successfully sneak past them!");
                Console.WriteLine("You find a hidden treasure room!");
                player.GainGold(75);
            }
            else
            {
                Console.WriteLine("They spot you!");
                DoCombat(skeleton);
            }
        }

        PressAnyKey();
    }

    private void DoCombat(Enemy enemy)
    {
        Console.Clear();
        Console.WriteLine($"╔════════════════════════════════════════╗");
        Console.WriteLine($"║           COMBAT STARTED!              ║");
        Console.WriteLine($"╚════════════════════════════════════════╝\n");

        while (player.CurrentHP > 0 && enemy.CurrentHP > 0)
        {
            Console.WriteLine($"{player.Name} HP: {player.CurrentHP}/{player.MaxHP}");
            Console.WriteLine($"{enemy.Name} HP: {enemy.CurrentHP}/{enemy.MaxHP}\n");

            Console.WriteLine("1. Attack");
            Console.WriteLine("2. Use Potion");
            Console.WriteLine("3. Defend");
            Console.Write("Choose: ");

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
                    Console.WriteLine("Invalid action!");
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
            Console.WriteLine("\n💀 You were defeated! 💀");
            Console.WriteLine("You lost half your gold!");
            player.SpendGold(player.Gold / 2);
            player.CurrentHP = 1;
        }
        else
        {
            Console.WriteLine($"\n✓ Victory! {enemy.Name} defeated!");
            int goldReward = enemy.GoldReward;
            player.GainGold(goldReward);
            Console.WriteLine($"You gained {goldReward} gold!");
        }
    }

    private void PlayerAttack(Enemy enemy)
    {
        int damage = player.GetDamage() + random.Next(-2, 3);
        damage = Math.Max(1, damage);
        enemy.TakeDamage(damage);
        Console.WriteLine($"You attack {enemy.Name} for {damage} damage!");
    }

    private void PlayerDefend(Enemy enemy)
    {
        Console.WriteLine($"You take a defensive stance!");
        int enemyDamage = enemy.GetDamage() / 2 + random.Next(-1, 2);
        enemyDamage = Math.Max(0, enemyDamage);
        player.TakeDamage(enemyDamage);
        Console.WriteLine($"{enemy.Name} attacks but you block half the damage ({enemyDamage})!");
    }

    private void UsePotion()
    {
        var potions = player.Inventory.Where(i => i.Type == ItemType.Potion).FirstOrDefault();
        if (potions != null)
        {
            player.Heal(25);
            player.Inventory.Remove(potions);
            Console.WriteLine("You used a Health Potion and recovered 25 HP!");
        }
        else
        {
            Console.WriteLine("You don't have any potions!");
        }
    }

    private void EnemyAttack(Enemy enemy)
    {
        int damage = enemy.GetDamage() + random.Next(-1, 2);
        damage = Math.Max(1, damage);
        player.TakeDamage(damage);
        Console.WriteLine($"{enemy.Name} attacks you for {damage} damage!");
    }

    private void VisitStore()
    {
        Console.Clear();
        Console.WriteLine("═══════════════════════════════════════");
        Console.WriteLine("            WELCOME TO THE STORE");
        Console.WriteLine("═══════════════════════════════════════\n");
        Console.WriteLine($"Your Gold: {player.Gold}\n");

        bool shopping = true;
        while (shopping)
        {
            Console.WriteLine("1. Buy Health Potion (25 gold)");
            Console.WriteLine("2. Buy Mana Potion (20 gold)");
            Console.WriteLine("3. Buy Iron Sword (50 gold)");
            Console.WriteLine("4. Buy Leather Armor (40 gold)");
            Console.WriteLine("5. Sell items");
            Console.WriteLine("6. Leave store");
            Console.Write("Choose: ");

            string choice = Console.ReadLine() ?? "6";

            switch (choice)
            {
                case "1":
                    if (player?.Gold >= 25)
                    {
                        player?.SpendGold(25);
                        player?.AddItem(new Item("Health Potion", ItemType.Potion, 25));
                        Console.WriteLine("✓ Purchased Health Potion!");
                    }
                    else
                        Console.WriteLine("✗ Not enough gold!");
                    break;
                case "2":
                    if (player?.Gold >= 20)
                    {
                        player?.SpendGold(20);
                        player?.AddItem(new Item("Mana Potion", ItemType.Potion, 15));
                        Console.WriteLine("✓ Purchased Mana Potion!");
                    }
                    else
                        Console.WriteLine("✗ Not enough gold!");
                    break;
                case "3":
                    if (player?.Gold >= 50)
                    {
                        player?.SpendGold(50);
                        player?.AddItem(new Item("Iron Sword", ItemType.Weapon, 8));
                        Console.WriteLine("✓ Purchased Iron Sword!");
                    }
                    else
                        Console.WriteLine("✗ Not enough gold!");
                    break;
                case "4":
                    if (player?.Gold >= 40)
                    {
                        player?.SpendGold(40);
                        player?.AddItem(new Item("Leather Armor", ItemType.Armor, 5));
                        Console.WriteLine("✓ Purchased Leather Armor!");
                    }
                    else
                        Console.WriteLine("✗ Not enough gold!");
                    break;
                case "5":
                    SellItems();
                    break;
                case "6":
                    shopping = false;
                    break;
                default:
                    Console.WriteLine("Invalid choice!");
                    break;
            }

            if (choice != "6")
            {
                Console.WriteLine($"Gold: {player?.Gold}\n");
            }
        }
    }

    private void SellItems()
    {
        Console.WriteLine("\nItems to sell:");
        if (player.Inventory.Count == 0)
        {
            Console.WriteLine("You have no items!");
            return;
        }

        for (int i = 0; i < player.Inventory.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {player.Inventory[i].Name} (Worth {player.Inventory[i].Value / 2} gold)");
        }

        Console.Write("Choose item to sell (number): ");
        if (int.TryParse(Console.ReadLine(), out int index) && index > 0 && index <= player?.Inventory.Count)
        {
            Item item = player?.Inventory[index - 1];
            int salePrice = item?.Value / 2 ?? 0;
            player?.GainGold(salePrice);
            player?.Inventory.RemoveAt(index - 1);
            Console.WriteLine($"Sold {item.Name} for {salePrice} gold!");
        }
    }

    private void ViewInventory()
    {
        Console.Clear();
        Console.WriteLine("═══════════════════════════════════════");
        Console.WriteLine("           YOUR INVENTORY");
        Console.WriteLine("═══════════════════════════════════════\n");

        if (player.Inventory.Count == 0)
        {
            Console.WriteLine("Your inventory is empty!");
        }
        else
        {
            for (int i = 0; i < player.Inventory.Count; i++)
            {
                Item item = player.Inventory[i];
                Console.WriteLine($"{i + 1}. {item.Name} ({item.Type})");
            }
        }

        Console.WriteLine($"\nGold: {player?.Gold}");
        PressAnyKey();
    }

    private void ViewStats()
    {
        Console.Clear();
        Console.WriteLine("═══════════════════════════════════════");
        Console.WriteLine("            CHARACTER STATS");
        Console.WriteLine("═══════════════════════════════════════\n");
        Console.WriteLine(player.GetStats());
        PressAnyKey();
    }

    private void RestAndHeal()
    {
        Console.Clear();
        Console.WriteLine("You rest at the tavern...\n");
        player.Heal(player.MaxHP);
        Console.WriteLine("You are fully healed!");
        PressAnyKey();
    }

    private void ExitGame()
    {
        Console.Clear();
        Console.WriteLine("═══════════════════════════════════════");
        Console.WriteLine($"Thanks for playing, {player?.Name}!");
        Console.WriteLine($"Final Level: {player?.Level}");
        Console.WriteLine($"Total Gold: {player?.Gold}");
        Console.WriteLine("═══════════════════════════════════════");
        gameRunning = false;
    }

    private void PressAnyKey()
    {
        Console.WriteLine("\nPress any key to continue...");
        Console.ReadKey();
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
