using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

namespace MergeGame
{
    public class Board : MonoBehaviour
    {
        private const string FreeCellsBeOverError = "There are no free cells";

        private readonly List<Cell> _freeCells = new();

        [SerializeField] private GridLayoutGroup _layout = null;

        private Cell[,] _cells = null;

        public bool HaveEmptyCells => _freeCells.Count > 0;

        private void Awake()
        {
            Initialize();
        }

        public void PutIntoRandomEmptyCell(BaseMergeable mergeable)
        {
            if (mergeable == null)
            {
                throw new ArgumentNullException(nameof(mergeable));
            }

            if (HaveEmptyCells == false)
            {
                Debug.LogError(FreeCellsBeOverError);
                return;
            }

            Cell cell = _freeCells[Random.Range(0, _freeCells.Count)];
            cell.Set(mergeable, false);

            _freeCells.Remove(cell);
        }

        private void Initialize()
        {
            int childCount = transform.childCount;
            int columns = _layout.constraintCount;

            int rows = Mathf.CeilToInt(childCount / (float)columns);

            _cells = new Cell[rows, columns];

            int index = 0;

            for (int row = 0; row < rows; row++)
            {
                for (int column = 0; column < columns; column++)
                {
                    if (index >= childCount)
                    {
                        return;
                    }

                    Transform child = transform.GetChild(index);
                    Cell cell = child.GetComponent<Cell>();

                    if (cell == null)
                    {
                        continue;
                    }

                    _cells[row, column] = cell;
                    _freeCells.Add(cell);

                    index++;
                }
            }
        }
    }
}
