using System;

namespace BobsCarPark
{
    internal class Program
    {
        // Procedure to print a welcome screen
        static void WelcomeScreen()
        {
            Console.WriteLine("###################################################");
            Console.WriteLine();
            Console.WriteLine(" W E L C O M E   T O   B O B ' S   C A R   P A R K ");
            Console.WriteLine();
            Console.WriteLine("###################################################");
        }

        // Procedure to ask for the user's number plate and verify it is correct.
        static void GetCarReg(ref string CarReg, ref bool ExitCode)
        {
            string IsUserCarRegCorrect;
            Console.Write("Enter your car's registration: ");
            CarReg = Console.ReadLine().ToUpper();
            Console.Write($"Is the registration {CarReg} correct? (y/n) ");
            IsUserCarRegCorrect = Console.ReadLine().ToUpper();
            if (IsUserCarRegCorrect != "Y")
            {
                ExitCode = true;
            }
        }

        // Procedure to ask the user for the amount of time they would like to stay
        static void TimeToStay(double[] Payments, TimeSpan[] Times, ref double PaymentDue, ref bool ExitCode, ref int TimeSelected)
        {
            int UserSelection = 0;
            Console.WriteLine("###################################################");
            Console.WriteLine();
            for (int i = 0; i < Payments.Length; i++)
            {
                Console.Write(i + 1);
                FillSpace(17, i.ToString());
                Console.Write($"Up to {Times[i]}");
                FillSpace(28, Times[i].ToString(), Payments[i].ToString());
                Console.Write($"£{Payments[i]}\n");
            }

            Console.WriteLine($"{Payments.Length + 1}                Exit");
            Console.WriteLine();
            Console.WriteLine("###################################################");

            bool ValidSelection = false;
            do
            {
                Console.Write("Enter the time you would like to stay (1-8): ");
                try
                {
                    UserSelection = int.Parse(Console.ReadLine());
                    if (UserSelection >= 1 && UserSelection <= 8)
                    {
                        ValidSelection = true;
                    }
                    else
                    {
                        Console.WriteLine("That is not a valid selection.");
                    }
                }
                catch
                {
                    Console.WriteLine("That is not a number");
                }
            } while (!ValidSelection);
            if (UserSelection == 8)
            {
                ExitCode = true;
            }
            else
            {
                PaymentDue = Payments[UserSelection - 1];
                TimeSelected = UserSelection - 1;
            }
        }

        // Procedure to ask the user to pay their money
        static double PayMoney(double PaymentDue, ref bool ExitCode)
        {
            double[] Coins = { 0.01, 0.02, 0.05, 0.10, 0.20, 0.50, 1.00, 2.00 };
            double UserCoin = 0;
            Console.WriteLine();
            while (PaymentDue > 0 && !ExitCode)
            {
                bool ValidCoin = false;
                do
                {
                    Console.SetCursorPosition(0,Console.CursorTop -1);
                    Console.WriteLine("                                                        ");
                    Console.SetCursorPosition(0, Console.CursorTop - 1);
                    Console.Write($"£{PaymentDue} due. Input a coin (or c to cancel): ");
                    string UserInput = Console.ReadLine();
                    if (UserInput == "c")
                    {
                        ExitCode = true;
                        ValidCoin = true;
                    }
                    else
                    {
                        try
                        {
                            UserCoin = double.Parse(UserInput);
                            for (int i = 0; i < Coins.Length && ValidCoin == false; i++)
                            {
                                if (Coins[i] == UserCoin)
                                {
                                    ValidCoin = true;
                                }
                            }
                            if (ValidCoin == false)
                            {
                                Console.WriteLine("Invalid coin.\n");
                            }
                        }
                        catch
                        {
                            Console.WriteLine("Please enter your coin as a decimal.\n");
                        }
                    }
                } while (!ValidCoin);
                if (!ExitCode)
                {
                    PaymentDue -= UserCoin;
                }
            }
            Console.SetCursorPosition(0, Console.CursorTop - 1);
            Console.WriteLine("                                                        ");
            Console.SetCursorPosition(0, Console.CursorTop - 1);
            Console.WriteLine("£0 due. Input a coin (or c to cancel): ");
            return PaymentDue * -1;
        }
        // Prints a ticket
        static void DispenseChange(double ChangeDue)
        {
            double[] Coins = { 0.01, 0.02, 0.05, 0.10, 0.20, 0.50, 1.00, 2.00 };
            if (ChangeDue != 0)
            {
                Console.WriteLine($"Dispensing £{ChangeDue} change...");
                while (ChangeDue > 0)
                {
                    ChangeDue = Math.Round(ChangeDue, 2);
                    bool Subtracted = false;
                    for (int i = Coins.Length - 1; i >= 0 && !Subtracted; i--)
                    {
                        if (ChangeDue - Coins[i] >= 0)
                        {
                            Console.WriteLine(Coins[i]);
                            ChangeDue -= Coins[i];
                        }
                    }
                }
            }
        }


        // Procedure to print a ticket
        static void PrintTicket(string CarReg, double PaymentDue, int TimeSelected, double[] Payments, TimeSpan[] Times)
        {
            Console.BackgroundColor = ConsoleColor.White;
            Console.ForegroundColor = ConsoleColor.Black;
            // 35 spaces
            FillSpace(35);
            Console.WriteLine("\n    B O B ' S   C A R   P A R K    ");
            FillSpace(35);
            Console.Write($"\n   Registration: {CarReg}");
            FillSpace(35, $"   Registration: {CarReg}");
            Console.WriteLine();
            FillSpace(35);
            DateTime NowTime = DateTime.Now;
            Console.Write($"\n   Entry: {NowTime}");
            FillSpace(35, $"   Entry: {NowTime}");
            Console.WriteLine();
            FillSpace(35);
            Console.Write($"\n   Fee:           £{PaymentDue}");
            FillSpace(35, $"   Fee:           £{PaymentDue}");
            Console.WriteLine();
            FillSpace(35);

            Console.Write($"\n   Expiry: {NowTime + Times[TimeSelected]}");
            FillSpace(35, $"   Expiry: {NowTime + Times[TimeSelected]}");
            Console.WriteLine();
            FillSpace(35);
            System.Threading.Thread.Sleep(10000);
        }

        // Nifty procedure to help with text formatting. Fills a line with spaces until the desired length is met

        // TextBefore is the text that occurs before the spaces,
        // TextAfter is the text that occurs after the spaces
        // DesiredLength is the desired final length of the total space to be occupied by both TextBefore, TextAfter AND the empty space combined
        static void FillSpace(int DesiredLength, string TextBefore = "", string TextAfter = "")
        {
            for (int i = 0; i < DesiredLength - TextBefore.Length - TextAfter.Length; i++)
            {
                Console.Write(" ");
            }
        }

        // Main program
        static void Main(string[] args)
        {
            double[] Payments = { 1, 2, 3, 4, 5, 6, 10 };
            TimeSpan[] Times = { TimeSpan.FromMinutes(30), TimeSpan.FromMinutes(60), TimeSpan.FromHours(2), TimeSpan.FromHours(3), TimeSpan.FromHours(4), TimeSpan.FromHours(6), TimeSpan.FromHours(12) };
            bool ExitCode = false;
            double PaymentDue = 0.00;
            double ChangeDue;
            int TimeSelected = 0;
            while (true)
            {
                ExitCode = false;
                while (true)
                {
                    Console.BackgroundColor = ConsoleColor.DarkBlue;
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.Clear();
                    string CarReg = "";
                    WelcomeScreen();
                    GetCarReg(ref CarReg, ref ExitCode);
                    if (ExitCode)
                    {
                        break;
                    }
                    TimeToStay(Payments, Times, ref PaymentDue, ref ExitCode, ref TimeSelected);
                    if (ExitCode)
                    {
                        break;
                    }
                    ChangeDue = Math.Round(PayMoney(PaymentDue, ref ExitCode), 2);
                    if (ExitCode)
                    {
                        break;
                    }
                    DispenseChange(ChangeDue);
                    PrintTicket(CarReg, PaymentDue, TimeSelected, Payments, Times);
                }
            }
            
        }

    }
}
