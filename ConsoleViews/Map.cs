using BestiLeikurinn.Cells;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestiLeikurinn
{
    public class Map
    {
        public int Width { get; set; }
        public int Height { get; set; }
        public IReadOnlyList<Cell> CellList => cellList.AsReadOnly();

        private List<Cell> cellList;
        private Cell[,] cellMap;

        public Map(int width, int height)
        {
            Width = width;
            Height = height;
            cellList = new List<Cell>();
            cellMap = new Cell[width, height];
        }

        public bool AddCell(Cell cell, bool overwrite = false)
        {
            if(cellMap[cell.X, cell.Y] != null)
            {
                if(overwrite)
                {
                    cellList.Remove(cellMap[cell.X, cell.Y]);
                    cellList.Add(cell);
                    cellMap[cell.X, cell.Y] = cell;
                    return true;
                }
            }
            else
            {
                cellList.Add(cell);
                cellMap[cell.X, cell.Y] = cell;
                return true;
            }

            return false;
        }

        public void RemoveCell(Cell cell)
        {
            if(cellList.Contains(cell))
            {
                cellMap[cell.X, cell.Y] = null;
                cellList.Remove(cell);
            }
        }

        public bool MoveCell(Cell cell, int newX, int newY)
        {
            if(cellMap[newX, newY] == null)
            {
                cellMap[cell.X, cell.Y] = null;
                cellMap[newX, newY] = cell;
                return true;
            }

            return false;
        }
    }
}
