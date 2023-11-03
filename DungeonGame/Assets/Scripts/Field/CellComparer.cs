using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameBoard
{
    public class CellComparer : IComparer<Cell>
    {
        public int Compare(Cell x, Cell y)
        {
            var leftPosition = x.transform.position;
            var rightPosition = y.transform.position;

            if (leftPosition.x < rightPosition.x)
                return -1;
            else if (leftPosition.x > rightPosition.x)
                return 1;
            return 0;
        }
    }
}