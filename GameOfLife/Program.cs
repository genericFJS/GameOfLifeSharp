using System;
using System.Threading;

namespace GameOfLife
{
    class Program
    {
        static void Main(string[] args)
        {
            // Visualisierung vom Test
            // ARRANGE
            var universe = new Universe();
            universe.VitalizeCell(2, 1);
            universe.VitalizeCell(3, 2);
            universe.VitalizeCell(4, 3);
            universe.VitalizeCell(2, 4);
            Console.WriteLine("Original Universum:");
            PrintUniverse(universe, 0, 4, 0, 4);

            // ACT
            var nextUniverse = universe.Next();
            Console.WriteLine("Nächstes Universum:");
            PrintUniverse(nextUniverse, 0, 4, 0, 4);

            // ASSERT
            var expectedUniverse = new Universe();
            expectedUniverse.VitalizeCell(3, 2);
            expectedUniverse.VitalizeCell(3, 3);
            Console.WriteLine("Erwartetes Universum:");
            PrintUniverse(expectedUniverse, 0, 4, 0, 4);

            // "Glider" Spaceship
            Console.WriteLine("=====================");
            universe = new Universe();
            universe.VitalizeCell(0, 1);
            universe.VitalizeCell(1, 2);
            universe.VitalizeCell(2, 0);
            universe.VitalizeCell(2, 1);
            universe.VitalizeCell(2, 2);
            while (true)
            {
                Thread.Sleep(500);
                Console.WriteLine("Nächstes Universum:");
                if (!PrintUniverse(universe, 0, 4, 0, 4, false))
                {
                    Console.WriteLine("Universum lässt sich nicht weiter beobachten…");
                    break;
                }
                universe = universe.Next();
            }
        }

        static bool PrintUniverse(Universe universe, int minX, int maxX, int minY, int maxY, bool printHeader = true)
        {
            var hasObservableContent = false;
            for (var y = minY - 1; y <= maxY; y++)
            {
                for (var x = minX - 1; x <= maxX; x++)
                {
                    if (y < minY && printHeader)
                    {
                        if (x < minX)
                            Console.Write("  ");
                        else
                            Console.Write($"{x} ");
                    }
                    else
                    {
                        if (x < minX && printHeader)
                            Console.Write($"{y} ");
                        else
                        {
                            if (universe.AliveCells.Contains(new Tuple<int, int>(x, y)))
                            {
                                Console.Write("x ");
                                hasObservableContent = true;
                            }
                            else
                                Console.Write("  ");

                        }
                    }

                }
                Console.WriteLine("");
            }
            return hasObservableContent;
        }
    }
}
