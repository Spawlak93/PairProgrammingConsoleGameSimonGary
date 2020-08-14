using System;
using System.Collections.Generic;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PairProgramGame
{
    class ProgramUI
    {
        private bool _isPlaying = true;
        private bool _isRunning = true;
        private int _strike = 6;
        int _lengthOfOriginalPhrase = 0;
        char[] _maskedPhrase = null;
        string _listOfGuesses = "";
        public void Start()
        {
            RunMenu();
        }
        public void RunMenu()
        {
            while (_isRunning)
            {
                DisplayMenu();
                GetMenuSelection();
            }
        }
        public void DisplayMenu()
        {
            Console.WriteLine("Welcome to Hangman!\n" +
                "1. Play Game\n" +
                "2. Exit\n");
        }
        private void GetMenuSelection()
        {
            string userInput = Console.ReadLine();
            switch (userInput.ToLower())
            {
                case "1":
                case "play":
                    Play();
                    break;
                case "2":
                case "exit":
                    _isRunning = false;
                    return;
                default:
                    Console.Clear();
                    return;
            }
        }
        //Play method
        private void Play()
        {
            Console.Clear();
            _strike = 6;
            _listOfGuesses = "";
            string originalPhrase = GetPhrase();
            _maskedPhrase = MaskPhrase(originalPhrase);
            Console.Clear();
            while (_isPlaying)
            {
                DisplayBoard(_maskedPhrase);
                string guessedChar = GetCharacter();
                Console.Clear();
                bool gary = StrikeCounter(guessedChar, originalPhrase);
                if (gary)
                {
                    CharContainedInString(guessedChar, originalPhrase);
                }
                CheckIfGameOver(originalPhrase);
            }
            SystemSounds.Beep.Play();
            PlayAgain();
        }
        private string GetPhrase()
        {
            string phrase;
            while (true)
            {

                Console.WriteLine("Rules:\n\n" +
                    "The phrase must start with a letter.\n" +
                    "The phrase must have more than 3 letters.\n" +
                    "The phrase cannot contain numbers.\n\n" +
                    "Please enter a word or phrase.\n");
                phrase = Console.ReadLine().ToUpper();
                int counter = 0;
                int counterTwo = 0;
                foreach (char c in phrase)
                {
                    if (counter != 0 && (c == ' ' || c == '\'' || c == '.' || c == ',' || c == '?' || c == '!' || c == '-'))
                    {
                        char cSharp = phrase[counter - 1];
                        if (cSharp == ' ' || cSharp == '\'' || cSharp == '.' || cSharp == ',' || cSharp == '?' || cSharp == '!' || cSharp == '-')
                        {
                            break;
                        }
                        counter++;
                    }
                    else if (c >= 65 && c <= 90)
                    {
                        counter++;
                        counterTwo++;
                    }
                }
                if (counter == phrase.Length && phrase.Length > 3 && counterTwo > 3)
                {
                    break;
                }
                Console.Clear();
                Console.WriteLine("No cheating! You have to enter something longer than 3 letters,\n" +
                    "must not contain numbers, and cannot start with any puncuation!\n");
            }
            _lengthOfOriginalPhrase = phrase.Length;
            return phrase;
        }
        private void DisplayBoard(char[] maskedPhrase)
        {
            Console.WriteLine(maskedPhrase);
            Console.WriteLine($"\nYou currently have {_strike} strikes left.");
            if (_listOfGuesses != null)
            {
                Console.WriteLine($"You have guessed these letters: {_listOfGuesses}");
            }
            Console.WriteLine();
        }
        private char[] MaskPhrase(string originalPhrase)
        {
            char[] phraseAsChars = originalPhrase.ToCharArray();
            for (int i = 0; i < _lengthOfOriginalPhrase; i++)
            {
                if (phraseAsChars[i] == ' ')
                {
                    phraseAsChars[i] = '\n';
                }
                else if (phraseAsChars[i] == '\'' || phraseAsChars[i] == '?' || phraseAsChars[i] == '.' || phraseAsChars[i] == '-' || phraseAsChars[i] == ',' || phraseAsChars[i] == '!')
                {
                    continue;
                }
                else
                {
                    phraseAsChars[i] = (char)127;
                }
            }
            return phraseAsChars;
        }
        private string GetCharacter()
        {
            while (true)
            {
                Console.Write("Please enter a character to guess: ");
                string charGuess = Console.ReadLine().ToUpper();
                if (_listOfGuesses.Contains(charGuess))
                {
                    Console.WriteLine("Letter already guessed, please try again.");
                    continue;
                }
                if (charGuess.Length == 1 && charGuess[0] >= 65 && charGuess[0] <= 90)
                {
                    _listOfGuesses = _listOfGuesses + " " + charGuess;
                    return charGuess;
                }
                Console.WriteLine("Please enter a single letter.\n");
            }
        }
        private bool StrikeCounter(string charGuess, string originalWord)
        {
            if (originalWord.Contains(charGuess))
            {
                Console.WriteLine($"{charGuess} is found in the phrase.\n");
                return true;
            }
            _strike--;
            Console.WriteLine($"{charGuess} is not found in the phrase.\n");
            return false;
        }
        private void CharContainedInString(string charGuess, string originalWord)
        {
            for (int i = 0; i < _lengthOfOriginalPhrase; i++)
                if (charGuess == originalWord.Substring(i, 1))
                {
                    _maskedPhrase[i] = charGuess[0];
                }
            //int space = originalWord.Contains(charGuess);
        }
        private void CheckIfGameOver(string originalPhrase)
        {
            if (_strike == 0)
            {
                Console.Clear();
                Console.WriteLine($"You Lost!\n" +
                    $"The answer was {originalPhrase}.");
                _isPlaying = false;
            }
            else if (!_maskedPhrase.Contains((char)127))
            {
                Console.Clear();
                Console.WriteLine($"You Won!\n" +
                    $"The answer was {originalPhrase}.");
                _isPlaying = false;
            }
        }
        private void PlayAgain()
        {
            while (true)
            {
                Console.WriteLine("Play Again?(Y/N)");
                switch (Console.ReadLine().ToLower())
                {
                    case "y":
                    case "yes":
                        _isPlaying = true;
                        Play();
                        return;
                    case "n":
                    case "no":
                        _isRunning = false;
                        return;
                }
                Console.Clear();
            }
        }




        //Welcome screen/ Menu
        //User input of phrase
        //Break phrase up
        //Change phrase into boxes
        //Display phrase as boxes ---
        //User guesses a letter 
        //if right phrase reveals a letter -----
        //if wrong strike counter goes up
        //If all letters revealed user wins
        //If all strikes used up user loses
        //option to try again
    }
}
