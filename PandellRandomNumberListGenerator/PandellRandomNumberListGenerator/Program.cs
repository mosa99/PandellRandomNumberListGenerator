using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

//*********************************************************************************************************
// Name:        PandellRandomNumberListGenerator
// Date:        November 2, 2012
// Author:      Mark Richardson
// Description: A small program to display an interactive menu to a user to allow them to generate a dynamic length array of inclusive, randomized numbers.
//              The users would also be able to display this list to the screen or output it to a text file.
//
//*********************************************************************************************************


namespace PandellRandomNumberListGenerator
{
    class Program
    {
        static void Main(string[] args)
        {
            int[] RandomNumberArray = new int[0];
            
            Console.WriteLine("Welcome to the Pandell Random Number List Generator");
            //loop until a user chooses to exit
            while (true)
            {
                string MenuResponse = DisplayMenu().ToLower();
            
                switch (MenuResponse)
                {
                    //generate List
                    case "a":
                        int MaxNumber = GetMaxNumberFromUser();
                        RandomNumberArray = GenerateRandomNumberArray(MaxNumber);
                        break;

                    //Display List to Screen
                    case "b":
                        DisplayRandomNumberList(RandomNumberArray);
                        break;
                    
                    //Save List to File
                    case "c":
                        SaveListToFile(RandomNumberArray);
                        break;

                    //Help
                    case "d":
                        DisplayHelp();
                        break;

                    case "e":
                        Environment.Exit(0);
                        break;

                    default:
                        Console.WriteLine("Error - We can't match your input to an appropriate menu option. Please try again by chosing a letter that matches with the menu options.");
                        Console.WriteLine("");
                        break;

                }//switch (MenuResponse)
            }//while (true)

        }//static void Main(string[] args)

        public static string DisplayMenu()
        {
            Console.WriteLine("");
            Console.WriteLine(" Pandell Random Number List Generator Menu ");
            Console.WriteLine(" -----------------------------------------");
            Console.WriteLine(" A) Produce a new List of Random Numbers");
            Console.WriteLine(" B) Display List of Random Numbers");
            Console.WriteLine(" C) Save List of Random Numbers to File");
            Console.WriteLine(" D) Help");
            Console.WriteLine(" E) Exit");
            Console.WriteLine("");
            Console.Write("What would you like to do? ");
            string MenuResponse = Console.ReadLine();

            Console.Clear();
            return MenuResponse;

        }//public static string DisplayMenu()

        public static void DisplayRandomNumberList(int[] RandomNumberArray)
        {
            if (RandomNumberArray.Length == 0)
            {
                Console.WriteLine("");
                Console.WriteLine("The list is currently empty");
            }
            else
            {
                Console.WriteLine("");
                Console.WriteLine("The current list of inclusive numbers between 1 and " + RandomNumberArray.Length);
                Console.WriteLine("");

                for (int x = 0; x < RandomNumberArray.Length; x++)
                {
                    Console.WriteLine(RandomNumberArray[x]);
                }//for 
            }//esle

        }//public static void DisplayRandomNumberList()

        public static void DisplayHelp()
        {
            Console.WriteLine("");
            Console.WriteLine("This Program will produce a list of numbers between 1 and a positive number you will be asked to input.");
            Console.WriteLine("");
            Console.WriteLine("Menu Option \"A\" will generate a new list of numbers between 1 and the positive number you enter.");
            Console.WriteLine("Menu Option \"B\" will display the list of numbers on the screen.");
            Console.WriteLine("Menu Option \"C\" will save a text file in the same location as this executable conatianing your list of random numbers, one per line.");
            Console.WriteLine("Menu Option \"D\" will bring up this help screen.");
            Console.WriteLine("Menu Option \"E\" will exit the program.");
            Console.WriteLine("");
            Console.WriteLine("");
            Console.WriteLine("For any other questions please contact PandellRandomNumberListGenerator Support.");
            Console.WriteLine("Phone: (555) 123 4567");
            Console.WriteLine("Email: support@MarkRichardsonInc.com");
            Console.WriteLine("");

        }//public static void DisplayHelp()

        public static int GetMaxNumberFromUser()
        {
            bool UserDidNotEnterValidNumber = true;
            Int32 MaxNumber = 0;

            while (UserDidNotEnterValidNumber)
            {
                Console.WriteLine("");
                Console.WriteLine("What is the maxium(positive) number in your inclusive list of random numbers?? ");                
                Console.WriteLine("");
                Console.Write("Number: ");

                string UserInput = Console.ReadLine();

                try
                {
                    MaxNumber = Convert.ToInt32(UserInput);

                    //test for a negative value
                    if (MaxNumber <= 0 || MaxNumber > 1000000)
                    {
                        //test to provide appropriate feedback to the user
                        if (MaxNumber <= 0){
                            Console.Clear();
                            Console.WriteLine("");
                            Console.WriteLine("Error - The number was negative! Please enter a Positive number between 1 and 1,000,000");
                        }//if
                        else if (MaxNumber > 1000000)
                        {
                            Console.Clear();
                            Console.WriteLine("");
                            Console.WriteLine("Error - The number was to big! Please enter a Positive number between 1 and 1,000,000");
                        }
                    }
                    else { UserDidNotEnterValidNumber = false; }
                   
                }
                catch (OverflowException)
                {
                    Console.Clear();
                    Console.WriteLine();
                    Console.WriteLine("Error - That number was too big! Please enter a postive number between 1 and 1,000,000");
                }
                catch
                {
                    Console.Clear();
                    Console.WriteLine();
                    Console.WriteLine("Error - There was an error with the number entered. Please enter a postive number between 1 and 1,000,000");
                }
            }//while


            Console.Clear();
            return MaxNumber; ;
        }//public static int GetMaxNumberFromUser()

        public static int[] GenerateRandomNumberArray(int MaxNumber)
        {
            int[] RandomNumberArray = new int[0];
            try
            {
                RandomNumberArray = new int[MaxNumber];

                //seed the array with the numbers between 1 and MaxNumber
                for (int x = 1; x <= MaxNumber; x++)
                {
                    RandomNumberArray[x - 1] = x; //care as array starts at 0, and numbers are between 1 and MaxNumber
                }//for

                //preform Fisher-Yates shuffle
                //http://en.wikipedia.org/wiki/Fisher-Yates_shuffle
                //C# implementation http://www.dotnetperls.com/fisher-yates-shuffle
                Random rnd = new Random();
                for (int x = RandomNumberArray.Length; x > 1; x--)
                {
                    //pick a random element to swap
                    int r = rnd.Next(x); // 0 <= r <= x-1
                    //Perform Swap
                    int temp = RandomNumberArray[r];
                    RandomNumberArray[r] = RandomNumberArray[x - 1]; //offset due to array starting at 0
                    RandomNumberArray[x - 1] = temp;
                }//for 

                Console.WriteLine("");
                Console.WriteLine("Inclusive list of numbers between 1 and " + RandomNumberArray.Length.ToString("N0") + " generated.");
            }
            catch (OutOfMemoryException)
            {
                Console.WriteLine("");
                Console.WriteLine(MaxNumber + " was to large for your system to process. Please pick a smaller number.");
            }

            return RandomNumberArray;
        }//public static int[] GenerateRandomNumberArray()

        public static void SaveListToFile(int[] RandomNumberArray)
        {
            if (RandomNumberArray.Length == 0)
            {
                Console.WriteLine("");
                Console.WriteLine("The list is currently empty");
            }
            else
            {
                using (System.IO.StreamWriter file = new System.IO.StreamWriter(@"PandellRandomNumberList.txt", false))
                {

                    //write the file
                    Console.WriteLine("");
                    Console.WriteLine("Writing Random Number List to file \"PandellRandomNumberList.txt\"");

                    for (int x = 0; x < RandomNumberArray.Length; x++)
                    {
                        file.WriteLine(RandomNumberArray[x]);

                        //not necessary but could be worth keeping in for future impementations if we were to move into very large numbers
                        ////update the user we are still working
                        //if (x % 100000 == 0) //write an update every 100000 results so the user doesn't think we are locked
                        //{
                        //    Console.WriteLine("");
                        //    Console.WriteLine("Writing Random Number List to file \"PandellRandomNumberList.txt\"");
                        //}//
                    }//for
                }//using
            }//else
        }//public static void SaveListToFile(int[] RandomNumberArray)

    }//class Program
}//namespace PandellRandomNumberListGenerator
