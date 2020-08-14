using System;
using System.Collections.Generic;
using System.Text;

namespace MockPairGame
{
    class ProgramUI
    {
        private bool _isRunning = true;
        private int _strike = 0;
        private Phrase phrase = new Phrase("Baloon");
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
            switch (userInput)
            {
                case "1":
                    Play();
                    break;
                case "2":
                    _isRunning = false;
                    return;
                default:
                    return;
            }
        }
        //Play method
        private void Play()
        {
            string originalPhrase = GetPhrase();
            Console.Clear();
            DisplayBoard(originalPhrase);
            string guessedChar = GetCharacter();
            StrikeCounter(guessedChar, originalPhrase);
            Console.WriteLine(guessedChar);
            Console.ReadLine();

        }

        private string GetPhrase()
        {
            Console.WriteLine("Please enter a word or phrase:");
            string phrase = Console.ReadLine();
            return phrase;
        }

        private void DisplayBoard(string originalPhrase)
        {
            foreach (char c in originalPhrase)
            {
                if (c == ' ')
                {
                    Console.Write("\n");
                }
                else if (c == '\'')
                {
                    Console.Write("'");
                }
                else
                {
                    Console.Write((char)127);
                }

            }
            Console.WriteLine();
        }

        private string GetCharacter()
        {
            Console.WriteLine("Please enter a character to guess");
            string charGuess = Console.ReadLine();
            return charGuess;
        }

        private void StrikeCounter(string charGuess, string originalWord)
        {
            if (originalWord.Contains(charGuess))
            {
                Console.WriteLine($"{charGuess} is found in the phrase.");
                return;
            }

            _strike++;
            Console.WriteLine($"{charGuess} is not found in the phrase.{_strike}");
            return;
        }


        //Welcome screen/ Menu
        //User input of phrase
        //Break phrase up
        //Change phrase into boxes
        //Display phrase as boxes ---
        //User guesses a letter 
        //if right phrase reveals a letter ---
        //if wrong strike counter goes up
        //If all letters revealed user wins
        //If all strikes used up user loses
        //option to try again
    }
}
