using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace NineTileGame_481
{
    class SteepestDescent
    {
        private int count = 0;
        private String name = "Steepest-descent";
        public bool Search(NineTileGameBoard board, NineTileGameBoard goalBoard, Heuristics.HeuristicTypes heuristicType)
        {
                        
            Heuristics Heuristic = new Heuristics(heuristicType);
            board.PrintHeader(this.name, heuristicType);
            while (true)
            {
                if (board.Equals(goalBoard) || count == 500)
                {
                    board.PrintBoard(true, count);
                    
                    return true;
                }

                // get childern
                List<NineTileGameBoard> children = board.GetChildren();

                // get heuristic of each child
                foreach (var child in children)
                {
                    child.heuristicValue = Heuristic.GetHeuristicValue(child, goalBoard);
                }

                // use child with best heurisitic value for next iteration
                NineTileGameBoard nextBoard = children.OrderBy(x => x.heuristicValue).First();
                
                // print to file
                board.PrintBoard(true, count);
                foreach (var child in children)
                {
                    child.PrintBoard(false, count);
                }

                count +=1;
                board = nextBoard;
            }


            return false;
        }


    }
}
