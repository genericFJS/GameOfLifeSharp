using System;
using System.Collections.Generic;

namespace GameOfLife
{
    public class Universe
    {
        HashSet<Tuple<int, int>> RelevantCells { get; } = new HashSet<Tuple<int, int>>();
        public HashSet<Tuple<int, int>> AliveCells { get; } = new HashSet<Tuple<int, int>>();

        delegate void CellAction(Tuple<int, int> position);

        // Universum soll nach Spezifikation immutable sein => neues Universum ausgeben.
        public Universe Next()
        {
            var nextUniverse = new Universe();

            foreach (var cellPosition in RelevantCells)
            {
                // Zähle Nachbarn
                var neighborCount = 0;
                ProcessNeighbors(cellPosition, pos =>
                {
                    if (!cellPosition.Equals(pos) && AliveCells.Contains(pos))
                    {
                        neighborCount++;
                    }
                });
                //  Lebende mit 2-3 Nachbarn überleben
                if (AliveCells.Contains(cellPosition))
                {
                    if (neighborCount == 2 || neighborCount == 3)
                        nextUniverse.VitalizeCell(cellPosition);
                }
                // Tote mit 3 Nachbarn werden lebend
                else
                {
                    if (neighborCount == 3)
                        nextUniverse.VitalizeCell(cellPosition);
                }
                // Der Rest ist nicht am Leben
            }

            return nextUniverse;
        }

        public void VitalizeCell(Tuple<int, int> position)
        {
            AliveCells.Add(position);
            // Relevante Zellen speichern, um nicht gesamtes Grid durchlaufen zu müssen
            ProcessNeighbors(position, pos => RelevantCells.Add(pos));
        }

        public void VitalizeCell(int x, int y)
        {
            VitalizeCell(new Tuple<int, int>(x, y));
        }

        void ProcessNeighbors(Tuple<int, int> position, CellAction action)
        {
            for (var x = position.Item1 - 1; x <= position.Item1 + 1; x++)
                for (var y = position.Item2 - 1; y <= position.Item2 + 1; y++)
                {
                    action(new Tuple<int, int>(x, y));
                }
        }
    }
}
