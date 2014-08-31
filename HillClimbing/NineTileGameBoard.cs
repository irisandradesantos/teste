using HillClimbing_481;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NineTileGame_481
{
    public class NineTileGameBoard : Reporting<bool>
    {
        enum ParentEmptyPosition { up, right, down, left, root };
        const int NumRows = 3;
        const int NumCols = 3;
        ParentEmptyPosition parentEmptyPosition = ParentEmptyPosition.root;
                
        public int heuristicValue = -1;
        public int[,] board = new int[NumRows, NumCols]
            {
             {1,2,3},
             {8,0,4},
             {7,6,5}
            };
        

        public List<NineTileGameBoard> GetChildren()
        {
            List<NineTileGameBoard> response = new List<NineTileGameBoard>();
            int emptyRow = -1;
            int emptyCol = -1;

            // find empty space
            for (int row = 0; row < NumRows; row++)
            {
                for(int col=0;col<NumCols;col++)
                {
                    if(this.board[row,col] == 0)
                    {
                        emptyRow = row;
                        emptyCol = col;
                        break;
                    }
                }
            }

            // create children by moving empty up, right, down, left if the move is valid
            //up
            if (IsValidRowCol(emptyRow - 1, emptyCol) && this.parentEmptyPosition!=ParentEmptyPosition.up)
            {
                NineTileGameBoard upBoard = new NineTileGameBoard();
                upBoard.Copy(this);
                upBoard.board[emptyRow, emptyCol] = upBoard.board[emptyRow - 1, emptyCol];
                upBoard.board[emptyRow - 1, emptyCol] = 0;
                upBoard.parentEmptyPosition = ParentEmptyPosition.down;
                response.Add(upBoard);
            }

            //right
            if (IsValidRowCol(emptyRow, emptyCol + 1) && this.parentEmptyPosition != ParentEmptyPosition.right)
            {
                NineTileGameBoard rightBoard = new NineTileGameBoard();
                rightBoard.Copy(this);
                rightBoard.board[emptyRow, emptyCol] = rightBoard.board[emptyRow, emptyCol + 1];
                rightBoard.board[emptyRow, emptyCol + 1] = 0;
                rightBoard.parentEmptyPosition = ParentEmptyPosition.left;
                response.Add(rightBoard);
            }

            //down
            if (IsValidRowCol(emptyRow + 1, emptyCol) && this.parentEmptyPosition != ParentEmptyPosition.down)
            {
                NineTileGameBoard downBoard = new NineTileGameBoard();
                downBoard.Copy(this);
                downBoard.board[emptyRow, emptyCol] = downBoard.board[emptyRow + 1, emptyCol];
                downBoard.board[emptyRow + 1, emptyCol] = 0;;
                downBoard.parentEmptyPosition = ParentEmptyPosition.up;
                response.Add(downBoard);
            }

            //left
            if (IsValidRowCol(emptyRow, emptyCol - 1) && this.parentEmptyPosition != ParentEmptyPosition.left)
            {
                NineTileGameBoard leftBoard = new NineTileGameBoard();
                leftBoard.Copy(this);
                leftBoard.board[emptyRow, emptyCol] = leftBoard.board[emptyRow, emptyCol - 1];
                leftBoard.board[emptyRow, emptyCol - 1] = 0;
                leftBoard.parentEmptyPosition = ParentEmptyPosition.right;
                response.Add(leftBoard);
            }

            return response;
        }

        public bool Equals(NineTileGameBoard goalBoard)
        {
            for (int row = 0; row < NumRows; row++)
            {
                for (int col = 0; col < NumCols; col++)
                {
                    if (this.board[row, col] != goalBoard.board[row,col])
                    {
                        return false;
                    }
                }
            }
            return true;
        }


        private bool IsValidRowCol(int row, int col)
        {
            if (row >= 0 && col >= 0 && row < NumRows && col < NumCols)
                return true;
            return false;
        }
        public void PrintHeader(String AlgorithmName, Heuristics.HeuristicTypes heuristicType)
        {
            //FileInfo fi = new FileInfo("OutPut.txt");
            using (StreamWriter writer = new StreamWriter("OutPut.txt", true))
            {
                writer.WriteLine("=========================================================");
                writer.WriteLine("");
                writer.WriteLine("Algorithm: " + AlgorithmName);
                writer.WriteLine("Heuristic function: "+heuristicType.ToString());
                writer.WriteLine("");
                writer.WriteLine("=========================================================");
                writer.WriteLine("");
            }
        }
        public void PrintHeaderBFS(String AlgorithmName)
        {
            //FileInfo fi = new FileInfo("OutPut.txt");
            using (StreamWriter writer = new StreamWriter("OutPut.txt", true))
            {
                writer.WriteLine("=========================================================");
                writer.WriteLine("");
                writer.WriteLine("Algorithm: " + AlgorithmName);
                writer.WriteLine("");
                writer.WriteLine("=========================================================");
                writer.WriteLine("");
            }
        }
        public void PrintBoard(bool isParent, int count )
        {
            using (StreamWriter writer = new StreamWriter("OutPut.txt", true))
            {
                if (count == 500) {
                    writer.WriteLine("------------------------------------------------------");
                    writer.WriteLine("");
                    writer.WriteLine("The algorithm got stuck at a local maximum");
                    writer.WriteLine("");
                    writer.WriteLine("------------------------------------------------------");
                }
                if (isParent)
                {
                    writer.WriteLine("Current Game Board:");
                    writer.WriteLine("Steps Since Start: " + count);
                }
                else
                {
                    writer.WriteLine("Child Board:");
                    writer.WriteLine("Heuristic: " + this.heuristicValue);
                }
                writer.WriteLine(this.board[0, 0] + " " + this.board[0, 1] + " " + this.board[0, 2]);
                writer.WriteLine(this.board[1, 0] + " " + this.board[1, 1] + " " + this.board[1, 2]);
                writer.WriteLine(this.board[2, 0] + " " + this.board[2, 1] + " " + this.board[2, 2]);
                writer.WriteLine("");
            }
        }

        public void Copy(NineTileGameBoard input)
        {
            for (int row = 0; row < NumRows; row++)
            {
                for (int col = 0; col < NumCols; col++)
                {
                    this.board[row, col] = input.board[row, col];
                }
            }
        }

    }
}
