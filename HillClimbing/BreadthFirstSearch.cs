using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NineTileGame_481
{
    public class BreadthFirstSearch
    {
        private int count = 0;
        private String name = "Breadth-First Search";
        public bool Search(NineTileGameBoard board, NineTileGameBoard goalBoard)
        {
            
            board.PrintHeaderBFS(this.name);
            List<NineTileGameBoard> open = new List<NineTileGameBoard>();
            open.Add(board);
            List<NineTileGameBoard> closed = new List<NineTileGameBoard>();
            
            while(open != null)
            {
                
                // remove leftmost state from open
                open.RemoveAt(0);

                //check if board is the goal
                if (board.Equals(goalBoard))
                {

                    board.PrintBoard(true, count);
                    
                    return true;
                }
                else
                {
                    // get childern
                    List<NineTileGameBoard> children = board.GetChildren();

                 
                    // put board on closed
                    closed.Add(board);

                    // discard children of X if already on open or closed
                    bool alreadyOnOpen = false;
                    bool alreadyOnClosed = false;

                    foreach (var child in children)
                    {
                        foreach (var node in open){
                        
                            if (child.Equals(node)) {
                                alreadyOnOpen = true;                                
                                break;
                            }
                        }
                        
                        foreach (var closeNode in closed)
                        {
                            if (child.Equals(closeNode))
                            {
                                alreadyOnClosed = true;
                                break;
                            }
                        }
                        
                        if (!alreadyOnOpen && !alreadyOnClosed)
                        {
                  
                            open.Add(child);
                        }
                        
                        
                    }
                    
                    
                    // use child with best heurisitic value for next iteration
                    
                    NineTileGameBoard nextBoard = open.First();

                    // print to file
                    board.PrintBoard(true,count);
                    foreach (var node in open)
                    {
                        node.PrintBoard(false,count);
                    }

                    //call board the next in the list
                    board = nextBoard;
                }
                //count the number of steps
                count += 1;
            }

            
            return false;
        }


    }
}
