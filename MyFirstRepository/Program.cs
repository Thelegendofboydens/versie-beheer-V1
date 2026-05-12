using System;

Console.WriteLine("=== Welcome to the Dice Guessing Game! ===\n");

bool playAgain = true;
int gamesWon = 0;

while (playAgain)
{
    Random random = new Random();
    int secretNumber = random.Next(1, 7); // Random number between 1 and 6
    int guess = 0;
    int attempts = 0;

    Console.WriteLine("I'm thinking of a number between 1 and 6. Can you guess it?\n");

    while (guess != secretNumber)
    {
        Console.Write("Enter your guess: ");
        string input = Console.ReadLine();

        if (!int.TryParse(input, out guess))
        {
            Console.WriteLine("❌ Please enter a valid number!\n");
            continue;
        }

        if (guess < 1 || guess > 6)
        {
            Console.WriteLine("❌ Please guess a number between 1 and 6!\n");
            continue;
        }

        attempts++;

        if (guess < secretNumber)
        {
            Console.WriteLine("📈 Too low! Try again.\n");
        }
        else if (guess > secretNumber)
        {
            Console.WriteLine("📉 Too high! Try again.\n");
        }
        else
        {
            Console.WriteLine($"\n🎉 You got it! The number was {secretNumber}!");
            Console.WriteLine($"You guessed it in {attempts} attempt(s)!\n");
            gamesWon++;
        }
    }

    Console.Write("Do you want to play again? (yes/no): ");
    string response = Console.ReadLine().ToLower();
    playAgain = response == "yes" || response == "y";
    Console.WriteLine();
}

Console.WriteLine($"Thanks for playing! You won {gamesWon} game(s). Goodbye!");

