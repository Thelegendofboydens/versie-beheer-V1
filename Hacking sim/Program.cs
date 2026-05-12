using System;
using System.Threading;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Collections.Generic;

class HackingSimulator
{
    private static Random random = new Random();
    private static bool isHacking = true;
    private static object consoleLock = new object();

    static void Main()
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;
        
        // Start the hacking simulation with extra windows
        StartHackingSimulation();
    }

    static void StartHackingSimulation()
    {
        Thread.Sleep(500);

        // Run multiple hacking processes in parallel
        Task.Run(() => MainHackingDisplay());
        Task.Run(() => NetworkActivityDisplay());
        Task.Run(() => FileSystemScanDisplay());
        Task.Run(() => PasswordCrackingDisplay());
        Task.Run(() => SystemLogsDisplay());
        Task.Run(() => EmailInterceptionDisplay());

        // Keep the program running while the simulation continues
        while (isHacking)
        {
            Thread.Sleep(100);
        }
    }

    static void MainHackingDisplay()
    {
        Thread.Sleep(500);
        Console.Clear();
        
        // Initial hack screen
        PrintHackingHeader();
        
        Thread.Sleep(2000);
        SimulateSystemAccess();
        Thread.Sleep(3000);
        SimulateFileDownload();
        Thread.Sleep(2000);
        SimulateCameraHack();
        Thread.Sleep(1500);
        SimulateRegistryModification();
        Thread.Sleep(1000);
        SimulatePersistence();
        Thread.Sleep(800);
        SimulateRootkitInjection();
        Thread.Sleep(800);
        SimulateDataExfiltration();
        Thread.Sleep(800);
        SimulateShellHijack();
        Thread.Sleep(800);
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine("\n[!] Finalizing compromise and preparing final shutdown...");
        Thread.Sleep(800);
        Console.WriteLine("[!] Clean-up operations complete");
        Console.ForegroundColor = ConsoleColor.White;
        Thread.Sleep(800);
        SimulateAlarmAndShutdown();
    }

    static void PrintHackingHeader()
    {
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine();
        Console.WriteLine("  ╔════════════════════════════════════════════════════════════╗");
        Console.WriteLine("  ║                    UNAUTHORIZED ACCESS DETECTED             ║");
        Console.WriteLine("  ║                   ⚠️  SYSTEM COMPROMISED  ⚠️                 ║");
        Console.WriteLine("  ╚════════════════════════════════════════════════════════════╝");
        Console.WriteLine();
        Console.ForegroundColor = ConsoleColor.White;
    }

    static void SimulateSystemAccess()
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine("[!] Remote access established");
        Console.WriteLine("[!] Bypassing firewall...");
        Thread.Sleep(800);
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine("[✓] Firewall disabled");
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine("[!] Accessing system files...");
        Thread.Sleep(800);
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine("[✓] System files accessible");
        Console.ForegroundColor = ConsoleColor.White;
    }

    static void SimulateFileDownload()
    {
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine("\n[*] Downloading sensitive files...\n");
        
        string[] files = new[]
        {
            "Documents/passwords.txt",
            "Documents/BankInfo.xlsx",
            "Pictures/PersonalPhotos/",
            "AppData/Chrome/cookies.db",
            "Desktop/crypto_wallet.key",
            "Users/PrivateFiles/secrets.pdf"
        };

        foreach (var file in files)
        {
            int fileSize = random.Next(100, 5000);
            Console.Write($"    [→] {file,-45} ");
            Console.ForegroundColor = ConsoleColor.Cyan;
            
            for (int i = 0; i <= 100; i += 10)
            {
                Console.Write($"{i}% ");
                Thread.Sleep(150);
            }
            Console.WriteLine("✓");
            Console.ForegroundColor = ConsoleColor.Yellow;
        }
        
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine("\n[✓] All files transferred to remote server\n");
        Console.ForegroundColor = ConsoleColor.White;
    }

    static void SimulateCameraHack()
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine("[!] CRITICAL: Webcam access compromised!");
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine("    └─ Capturing video stream...");
        Thread.Sleep(1000);
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine("    └─ Video stream active ✓");
        Console.ForegroundColor = ConsoleColor.White;
    }

    static void SimulateRegistryModification()
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine("[!] Modifying system registry...");
        Thread.Sleep(500);
        Console.WriteLine("[!] HKEY_LOCAL_MACHINE\\Software\\Microsoft\\Windows\\CurrentVersion");
        Thread.Sleep(300);
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine("     → InstallDate: 2024-01-15");
        Console.WriteLine("     → RegisteredOwner: HACKER_BOT");
        Console.WriteLine("     → ProductName: Windows (Compromised)");
        Thread.Sleep(500);
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine("[✓] Registry modified successfully");
        Console.ForegroundColor = ConsoleColor.White;
    }

    static void SimulatePersistence()
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine("[!] Installing persistence modules...");
        Thread.Sleep(400);
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine("    └─ Malware scheduled task: WinUpdateService");
        Thread.Sleep(400);
        Console.WriteLine("    └─ Driver loaded: remote_net.sys");
        Thread.Sleep(400);
        Console.WriteLine("    └─ Auto-start registry key created");
        Thread.Sleep(400);
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine("[✓] Persistence installed");
        Console.ForegroundColor = ConsoleColor.White;
    }

    static void SimulateRootkitInjection()
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine("[!] Injecting stealth rootkit...");
        Thread.Sleep(500);
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine("    └─ Kernel module: stealth_hook.sys");
        Thread.Sleep(500);
        Console.WriteLine("    └─ Event monitor disabled");
        Thread.Sleep(500);
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine("[✓] Rootkit injected successfully");
        Console.ForegroundColor = ConsoleColor.White;
    }

    static void SimulateDataExfiltration()
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine("[!] Launching data exfiltration...");
        Thread.Sleep(400);
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine("    └─ Opening encrypted tunnel to remote server");
        Thread.Sleep(400);
        Console.WriteLine("    └─ Streaming stolen archives: payroll.zip");
        Thread.Sleep(400);
        Console.WriteLine("    └─ Streaming stolen archives: credentials.db");
        Thread.Sleep(400);
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine("[✓] Data exfiltration in progress");
        Console.ForegroundColor = ConsoleColor.White;
    }

    static void SimulateShellHijack()
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine("[!] Hijacking user shell...");
        Thread.Sleep(400);
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine("    └─ Replacing command prompt with remote backdoor");
        Thread.Sleep(400);
        Console.WriteLine("    └─ Executing stealth commands in background");
        Thread.Sleep(400);
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine("[✓] Shell hijack complete");
        Console.ForegroundColor = ConsoleColor.White;
    }

    static void NetworkActivityDisplay()
    {
        Thread.Sleep(1000);
        Console.SetCursorPosition(0, 12);
        Console.ForegroundColor = ConsoleColor.Cyan;
        
        Console.WriteLine("  ┌─ NETWORK ACTIVITY ─────────────────────────────────────┐");
        
        for (int i = 0; i < 8; i++)
        {
            string ip = $"{random.Next(0, 256)}.{random.Next(0, 256)}.{random.Next(0, 256)}.{random.Next(0, 256)}";
            string port = random.Next(1000, 65000).ToString();
            int dataTransferred = random.Next(1024, 10240);
            
            Console.SetCursorPosition(0, 13 + i);
            Console.WriteLine($"  │ ↔ {ip,-15} : {port,-6} │ {dataTransferred} MB/s");
            Thread.Sleep(600);
        }
        
        Console.SetCursorPosition(0, 21);
        Console.WriteLine("  └─────────────────────────────────────────────────────────┘");
        Console.ForegroundColor = ConsoleColor.White;
    }

    static void SystemLogsDisplay()
    {
        Thread.Sleep(2500);
        Console.SetCursorPosition(60, 12);
        Console.ForegroundColor = ConsoleColor.Yellow;
        
        Console.WriteLine("┌─ SYSTEM LOGS ───────────────────┐");
        
        string[] logs = new[]
        {
            "[14:23:15] User login detected",
            "[14:23:18] Administrator access",
            "[14:23:22] Malware scan disabled",
            "[14:23:25] Firewall rules modified",
            "[14:23:28] BitLocker encryption",
            "[14:23:31] Recovery disabled",
            "[14:23:35] Backdoor installed",
            "[14:23:38] Remote access enabled"
        };

        for (int i = 0; i < logs.Length; i++)
        {
            Console.SetCursorPosition(60, 13 + i);
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"│ {logs[i],-33}│");
            Thread.Sleep(400);
        }
        
        Console.SetCursorPosition(60, 21);
        Console.WriteLine("└──────────────────────────────────┘");
        Console.ForegroundColor = ConsoleColor.White;
    }

    static void EmailInterceptionDisplay()
    {
        Thread.Sleep(3500);
        Console.SetCursorPosition(0, 35);
        Console.ForegroundColor = ConsoleColor.Magenta;
        
        Console.WriteLine("┌─ EMAIL INTERCEPTION ────────────────────────────────────┐");
        
        string[] emails = new[]
        {
            "FROM: admin@company.com",
            "SUBJECT: Q2 Financial Reports - CONFIDENTIAL",
            "ENCRYPTED: AES-256",
            "STATUS: DECRYPTED ✓",
            "",
            "FROM: hr@company.com",
            "SUBJECT: Salary Information - 2024",
            "ENCRYPTED: RSA-2048",
            "STATUS: DECRYPTED ✓"
        };

        for (int i = 0; i < emails.Length; i++)
        {
            Console.SetCursorPosition(0, 36 + i);
            Console.Write($"│ {emails[i],-58}│");
            Thread.Sleep(300);
        }
        
        Console.SetCursorPosition(0, 45);
        Console.WriteLine("└──────────────────────────────────────────────────────────┘");
        Console.ForegroundColor = ConsoleColor.White;
    }

    static void FileSystemScanDisplay()
    {
        Thread.Sleep(2000);
        Console.SetCursorPosition(0, 23);
        Console.ForegroundColor = ConsoleColor.Magenta;
        
        Console.WriteLine("  ┌─ FILE SYSTEM SCAN ──────────────────────────────────────┐");
        
        string[] directories = new[]
        {
            "C:\\Windows\\System32",
            "C:\\Users\\Documents",
            "C:\\Users\\AppData\\Local",
            "C:\\Users\\Pictures",
            "C:\\Users\\Desktop",
            "C:\\ProgramFiles",
            "C:\\Users\\Downloads",
            "C:\\Users\\Music"
        };

        for (int i = 0; i < directories.Length; i++)
        {
            Console.SetCursorPosition(0, 24 + i);
            Console.Write($"  │ Scanning: {directories[i],-40}");
            
            for (int j = 0; j <= 100; j += 25)
            {
                Console.Write($"\r  │ Scanning: {directories[i],-40} {j}%");
                Thread.Sleep(300);
            }
            Console.WriteLine(" ✓ │");
        }
        
        Console.SetCursorPosition(0, 32);
        Console.WriteLine("  └─────────────────────────────────────────────────────────┘");
        Console.ForegroundColor = ConsoleColor.White;
    }

    static void PasswordCrackingDisplay()
    {
        Thread.Sleep(3000);
        Console.SetCursorPosition(0, 34);
        Console.ForegroundColor = ConsoleColor.Yellow;
        
        Console.WriteLine("  ┌─ PASSWORD CRACKING ────────────────────────────────────┐");
        
        string[] passwordAttempts = new[]
        {
            "admin123",
            "password",
            "123456",
            "letmein",
            "monkey",
            "1qwerty",
            "Aa1234567890",
            "P@ssw0rd",
            "qwerty123"
        };

        for (int i = 0; i < passwordAttempts.Length - 1; i++)
        {
            Console.SetCursorPosition(0, 35 + i);
            Console.Write($"  │ Trying: {passwordAttempts[i],-35} ✗");
            Thread.Sleep(500);
        }
        
        Console.SetCursorPosition(0, 43);
        Console.ForegroundColor = ConsoleColor.Green;
        Console.Write($"  │ Trying: {passwordAttempts[passwordAttempts.Length - 1],-35}");
        Thread.Sleep(1000);
        Console.WriteLine(" ✓ PASSWORD CRACKED!");
        
        Console.ForegroundColor = ConsoleColor.White;
        Console.SetCursorPosition(0, 44);
        Console.WriteLine("  └─────────────────────────────────────────────────────────┘");
        
        Thread.Sleep(1000);
        SimulateAlarmAndShutdown();
    }

    static void SimulateAlarmAndShutdown()
    {
        lock (consoleLock)
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Red;

            for (int i = 0; i < 5; i++)
            {
                Console.WriteLine("\n  ⚠️  SYSTEM ALERT - INITIATING EMERGENCY SHUTDOWN ⚠️");
                Console.WriteLine("  ╔════════════════════════════════════════════════════════╗");
                Console.WriteLine("  ║  ALL YOUR DATA HAS BEEN COMPROMISED AND BACKED UP TO   ║");
                Console.WriteLine("  ║  REMOTE SERVERS. YOUR PRIVACY HAS BEEN VIOLATED.       ║");
                Console.WriteLine("  ║                                                        ║");
                Console.WriteLine("  ║  HACKER: \"Thanks for playing! This was a prank! 😄\"  ║");
                Console.WriteLine("  ╚════════════════════════════════════════════════════════╝");
                Thread.Sleep(500);
                Console.Clear();
                Thread.Sleep(500);
            }

            Console.ForegroundColor = ConsoleColor.Green;
            Console.Clear();
            PrintFinalMessage();
        }

        isHacking = false;
    }

    static void PrintFinalMessage()
    {
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine("\n");
        Console.WriteLine("  ███╗   ███╗ █████╗ ██████╗ ██╗   ██╗███████╗███████╗██╗");
        Console.WriteLine("  ████╗ ████║██╔══██╗██╔══██╗██║   ██║██╔════╝██╔════╝██║");
        Console.WriteLine("  ██╔████╔██║███████║██║  ██║██║   ██║█████╗  █████╗  ██║");
        Console.WriteLine("  ██║╚██╔╝██║██╔══██║██║  ██║██║   ██║██╔══╝  ██╔══╝  ╚═╝");
        Console.WriteLine("  ██║ ╚═╝ ██║██║  ██║██████╔╝╚██████╔╝███████╗███████╗██╗");
        Console.WriteLine("  ╚═╝     ╚═╝╚═╝  ╚═╝╚═════╝  ╚═════╝ ╚══════╝╚══════╝╚═╝");
        Console.WriteLine();
        Console.WriteLine("  🎉 Congratulations! You've been hacked! (Just kidding 😉)");
        Console.WriteLine();
        Console.WriteLine("  This was a harmless prank simulation.");
        Console.WriteLine("  No actual hacking has occurred!");
        Console.WriteLine();
        Console.WriteLine("  Press any key to exit...");
        Console.ForegroundColor = ConsoleColor.White;
        Console.ReadKey();
    }
}
