using System;
using UnityEngine;

namespace GameBoard
{
    public class Line : MonoBehaviour
    {
        [SerializeField] private Cell[] _cells;

        private int _lineNumber;

        public int LineNumber
        {
            get
            {
                return _lineNumber;
            }
            set
            {
                if (value < 0)
                {
                    Debug.LogError("You are trying set negative line number to Line");
                    return;
                }
                _lineNumber = value;
            }
        }

        public Cell[] Cells => _cells;

        private void OnValidate()
        {
            _cells ??= GetComponentsInChildren<Cell>();   
        }

        public void Validate()
        {
            Array.Sort(_cells, new CellComparer());
            for (int i = 0; i < _cells.Length; i++)
            {
                _cells[i].LineNumber = _lineNumber;
                _cells[i].Index = i;
            }
        }

        public Cell GetCell(int index)
        {
            return _cells[index];
        }
    }

}
