using NineTileGame_481;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HillClimbing_481
{
    interface Reporting<T>
    {
        void PrintBoard(T obj, int count);
        void PrintHeaderBFS(String AlgorithmName);
        void PrintHeader(String name, Heuristics.HeuristicTypes heuristicType);
    }
}
