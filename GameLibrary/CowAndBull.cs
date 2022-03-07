using BLLibrary;
using ErrorException;
using ModelsLibrary;
using System.Diagnostics;

namespace GamesLibrary
{
    public class CowAndBull
    {
        Queue<HiddenWord> hiddenWordsQueue = new Queue<HiddenWord>();
        WordsBL wordsBL = new WordsBL();
        readonly int wordLength =4;
        public CowAndBull()
        {
            try
            {
                hiddenWordsQueue = wordsBL.CreateWordQueueFromDB();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                Debug.WriteLine(ex.StackTrace);
                Console.WriteLine("Sorry, program can't get words from DB");
            }
        }
        void setWord()
        {
            Console.WriteLine($"Please enter the word ({wordLength} chars only): ");
            string hiddenWord = takeNCharFromConsole(wordLength).ToLower();
            HiddenWord newWord = wordsBL.AddWord(hiddenWord);
            if (newWord == null)
            {
                Console.WriteLine("This word is already in queue");
                return;
            }
            hiddenWordsQueue.Enqueue(newWord);
            Console.WriteLine("Thank you, the word has added to the queue");
        }
        void guessWord()
        {
            if (hiddenWordsQueue.Count == 0)
            {
                Console.WriteLine("No hidden words");
                return;
            }
            HiddenWord hiddenWord = hiddenWordsQueue.Dequeue();
            wordsBL.RemoveWord(hiddenWord.Id);            
            Console.WriteLine($"Game has started, enter the word ({wordLength} chars only):");
            int cow = 0, bull = 0;
            string inputString = takeNCharFromConsole(wordLength).ToLower();
            
            while (inputString != hiddenWord.Word)
            {
                for (int i = 0; i < inputString.Length; i++)
                {
                    if (inputString[i] == hiddenWord.Word[i])
                    {
                        cow += 1;
                    }
                    for (int j = 0; j < inputString.Length; j++)
                    {
                        if (inputString[i] == hiddenWord.Word[j] && i != j)
                        {
                            bull += 1;
                        }
                    }
                }
                Console.Write($"cow: {cow} bull: {bull} ");
                cow = 0;
                bull = 0;
                Console.WriteLine();
                inputString = takeNCharFromConsole(wordLength).ToLower();
            }
            Console.WriteLine("Congratulations, you won");
            Console.WriteLine();
        }
        void PrintGameMenu()
        {
            Console.WriteLine("1) Give Word" +
                "\n2) Guess word" +
                "\n3) Exit\n");
        }
        string takeNCharFromConsole(int nChar)
        {
            string input = "";
            while (input.Length != nChar)
            {
                try
                {
                    input = Console.ReadLine();
                    if (input.Length != nChar)
                    {
                        throw new InvalidWordLength(nChar);
                    }
                }
                catch (InvalidWordLength e)
                {
                    Console.WriteLine(e.errorMassage);
                }
                catch (Exception e)
                {
                    Debug.WriteLine(e.Message);
                    Debug.WriteLine(e.StackTrace);
                    Console.WriteLine("Invalid input, please try again...");
                }
            }
            return input;
        }
        public void StartGame()
        {
            string switchChoice;

            do
            {
                Console.WriteLine("Welcome to the game");
                PrintGameMenu();
                switchChoice = Console.ReadLine();
                switch (switchChoice)
                {
                    case "1":
                        setWord();
                        break;
                    case "2":
                        guessWord();
                        break;
                    case "3":
                        Console.WriteLine("Bye...\n");
                        break;
                    default:
                        Console.WriteLine("Sorry I dont understand you, try again\n");
                        break;
                }
            }
            while (switchChoice != "3");
        }
    }
}