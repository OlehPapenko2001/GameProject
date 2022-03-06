using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLLibrary;
using GamesLibrary;
using ModelsLibrary;

namespace ConsoleApp
{
    class Program
    {
        UserBL userBL = new UserBL();
        static void Main(string[] args)
        {
            Program program = new Program();
            program.MenuLogin();
        }

        void PrintLoginMenu()
        {
            Console.WriteLine("1) Register" +
                "\n2) Login\n");
        }
        void MenuLogin()
        {
            Program program = new Program();
            string switchChoice;
            CowAndBull game = new CowAndBull();
            while (true)
            {
                program.PrintLoginMenu();
                switchChoice = Console.ReadLine();
                switch (switchChoice)
                {
                    case "1":
                        if(program.Register())
                            game.StartGame();
                        break;
                    case "2":
                        if (program.Login())
                            game.StartGame();
                        break;
                    case "3":
                        Console.WriteLine("Byeeee\n");
                        break;
                    default:
                        Console.WriteLine("Sorry I dont understand you, try again\n");
                        break;
                }
            }
        }
        public bool Login()
        {
            Console.WriteLine("Enter your id:");
            int inputId;
            while (!int.TryParse(Console.ReadLine(), out inputId))
            {
                Console.WriteLine("Wrong input, id should be a number");
            }
            Console.WriteLine("Enter your password:");
            string inputPassword = Console.ReadLine();
            string userName = userBL.LoginUser(inputId, inputPassword);
            if (userName != null && userName!="")
            {
                Console.WriteLine($"Welcome {userName}");
                return true;
            }
            Console.WriteLine("Invalid username or password\n");
            return false;
        }
        public bool Register()
        {
            Console.WriteLine("Enter your name:");
            string inputName = Console.ReadLine();
            int InputAge;
            Console.WriteLine("Enter your Age:");
            while (!int.TryParse(Console.ReadLine(), out InputAge))
            {
                Console.WriteLine("Wrong input, age should be a number");
            }
            User user = userBL.RegisterUser(inputName, InputAge);
            if (user != null)
            {
                Console.WriteLine($"Welcome {user.Name}, your password is {user.Password}");
                Console.WriteLine($"Your id is {user.Id}");
                return true;
            }
            Console.WriteLine("Invalid username or password\n");
            return false;
        }
    }
}