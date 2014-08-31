using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NineTileGame_481
{
    public class Heuristics
    {
        public enum HeuristicTypes { SwapAdjecentTiles, SumTileMoves, NumberTileOutOfPlace, AllTogether }
        HeuristicTypes HeuristicType;

        public Heuristics (HeuristicTypes type)
        {
            HeuristicType = type;
        }

        public int GetHeuristicValue(NineTileGameBoard board, NineTileGameBoard goalBoard)
        {
            int heuristicValue = -1;
            switch(this.HeuristicType)
            {
                case HeuristicTypes.SwapAdjecentTiles: heuristicValue = SwapAdjecentTiles(board, goalBoard);
                    break;
                case HeuristicTypes.SumTileMoves: heuristicValue = SumTileMoves(board, goalBoard);
                    break;
                case HeuristicTypes.NumberTileOutOfPlace: heuristicValue = NumberTileOutOfPlace(board, goalBoard);
                    break;
                case HeuristicTypes.AllTogether: heuristicValue = AllTogether(board, goalBoard);
                    break;
            }
            return heuristicValue;
        }

        public int SwapAdjecentTiles(NineTileGameBoard board, NineTileGameBoard goalBoard)
        {
            int response = 0;
            // for each value in current board
            for (int row = 0; row < 3; row++)
            {
                for (int col = 0; col < 3; col++)
                {
                    // skip empty
                    if (board.board[row, col] != 0)
                    {
                        // find value in goal board
                        for (int goalrow = 0; goalrow < 3; goalrow++)
                        {
                            for (int goalcol = 0; goalcol < 3; goalcol++)
                            {
                                if (board.board[row, col] == goalBoard.board[goalrow, goalcol])
                                {
                                    // if tiles are adjcent
                                    if ((Math.Abs(row - goalrow) == 1 && Math.Abs(col - goalcol) == 0)
                                        || (Math.Abs(col - goalcol) == 1 && Math.Abs(row - goalrow) == 0))
                                    {
                                        // if values are adjcent
                                        if (Math.Abs(board.board[goalrow, goalcol] - board.board[row, col]) == 1)
                                            response = response + 2;
                                    }
                                }
                            }
                        }
                    }
                }
            }
            return response;
        }

        public int SumTileMoves(NineTileGameBoard board, NineTileGameBoard goalBoard)
        {
            int response = 0;
            // for each value in current board
            for (int row = 0; row < 3; row++)
            {
                for (int col = 0; col < 3; col++)
                {
                    // find value in goal board
                    for (int goalrow = 0; goalrow < 3; goalrow++)
                    {
                        for (int goalcol = 0; goalcol < 3; goalcol++)
                        {
                            if (board.board[row, col] == goalBoard.board[goalrow, goalcol])
                                response = response + Math.Abs(row - goalrow) + Math.Abs(col - goalcol);
                        }
                    }

                }
            }
            return response;
        }

        public int NumberTileOutOfPlace(NineTileGameBoard board, NineTileGameBoard goalBoard)
        {
            int response = 0;
            // for each value in current board
            for (int row = 0; row < 3; row++)
            {
                for (int col = 0; col < 3; col++)
                {
                    // find value in goal board
                    for (int goalrow = 0; goalrow < 3; goalrow++)
                    {
                        for (int goalcol = 0; goalcol < 3; goalcol++)
                        {
                            if (board.board[row,col] == goalBoard.board[goalrow,goalcol] && (row != goalrow || col != goalcol))
                                response++;
                        }
                    }

                }
            }
            return response;
        }

        public int AllTogether(NineTileGameBoard board, NineTileGameBoard goalBoard)
        {
            int response = 0;
            response = SwapAdjecentTiles(board, goalBoard);
            response = response + SumTileMoves(board, goalBoard);
            response = response + NumberTileOutOfPlace(board, goalBoard);
            return response;
        }
    }
}
