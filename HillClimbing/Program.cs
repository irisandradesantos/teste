using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NineTileGame_481
{
    class Program
    {
        static void Main(string[] args)
        {
            BreadthFirstSearch bfs = new BreadthFirstSearch();
            NineTileGameBoard goalBoard = new NineTileGameBoard();
            NineTileGameBoard startingBoard = new NineTileGameBoard();
            SteepestDescent steepest = new SteepestDescent();
            startingBoard.board = new int[,]
            {
                {2,8,3},
                {1,6,4},
                {7,0,5}
            };

            // ui title
            Console.WriteLine("Heuristic Search:");
            Console.WriteLine("");
            // choose to define game board
            Console.WriteLine("Use default game board (2,8,3  1,6,4  7,0,5)?(y/n)");
            string inputString = Console.ReadLine();
            if(inputString.IndexOf("n",StringComparison.CurrentCultureIgnoreCase) >= 0)
            {
                // define game board
                Console.WriteLine("Enter Game Board (R0C0,R0C1,R0C2,R1C0,R1C1,R1C2,R2C0,R2C1,R2C2,): ");
                inputString = Console.ReadLine();
                startingBoard = StringToNineTileBoard(inputString);
            }

            // choose algorithm to use

            Console.WriteLine("Pick an algoritm to use");
            Console.WriteLine("For Steepest Descent Climbing - Enter 1");
            Console.WriteLine("For Breadth First Search - Enter 2");
            Console.WriteLine("Exit - Enter 3");

            inputString = Console.ReadLine();
            int algorithm_selection = int.Parse(inputString);
            if (algorithm_selection == 2)
            {
                bfs.Search(startingBoard, goalBoard);
                Console.WriteLine("Done! Output is found at: " + Directory.GetCurrentDirectory() + @"\Output.txt");
            }
            else
            {
                if (algorithm_selection == 1)
                {
                    // choose heuristic to use
                    Console.WriteLine("Pick Heuristic to use:");
                    Console.WriteLine("For Swap Adjecent Tiles - Enter 0");
                    Console.WriteLine("For Sum Tile Moves - Enter 1");
                    Console.WriteLine("For Number Tiles Out Of Place - Enter 2");
                    Console.WriteLine("For All Together - Enter 3");
                    Console.WriteLine("Run All Heuristics - Enter 4");
                    Console.WriteLine("Exit - Enter 5");

                    inputString = Console.ReadLine();
                    int heuristicInt = int.Parse(inputString);

                    if (heuristicInt == 4)
                    {
                        foreach (Heuristics.HeuristicTypes type in Enum.GetValues(typeof(Heuristics.HeuristicTypes)))
                        {
                            steepest.Search(startingBoard, goalBoard, type);
                        }
                        Console.WriteLine("Done! Output is found at: " + Directory.GetCurrentDirectory() + @"\Output.txt");
                    }

                    else if (heuristicInt > -1 && heuristicInt < 4)
                    {
                        steepest.Search(startingBoard, goalBoard, (Heuristics.HeuristicTypes)heuristicInt);
                        Console.WriteLine("Done! Output is found at: " + Directory.GetCurrentDirectory() + @"\Output.txt");
                    }
                }
            }
        
            
            Console.WriteLine("Hit Enter to Exit...");
            Console.ReadLine();
        }

        static public NineTileGameBoard StringToNineTileBoard(string input)
        {
            NineTileGameBoard response = new NineTileGameBoard();
            int[] inputArray = input.Split(',').Select(x => int.Parse(x)).ToArray();
            int i = 0;
            for (int row = 0; row < 3; row++)
            {
                for (int col = 0; col < 3; col++)
                {
                    response.board[row, col] = inputArray[i];
                    i++;
                }
            }
            return response;
        }
    }
}
