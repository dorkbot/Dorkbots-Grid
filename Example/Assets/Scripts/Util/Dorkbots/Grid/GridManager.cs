/*
* Author: Dayvid jones
* http://www.dayvid.com
* Copyright (c) Superhero Robot 2016
* http://www.superherorobot.com
* Version: 1.0.0
* 
* Licence Agreement
*
* You may distribute and modify this class freely, provided that you leave this header intact,
* and add appropriate headers to indicate your changes. Credit is appreciated in applications
* that use this code, but is not required.
* 
* Permission is hereby granted, free of charge, to any person obtaining a copy
* of this software and associated documentation files (the "Software"), to deal
* in the Software without restriction, including without limitation the rights
* to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
* copies of the Software, and to permit persons to whom the Software is
* furnished to do so, subject to the following conditions:
* 
* The above copyright notice and this permission notice shall be included in
* all copies or substantial portions of the Software.
* 
* THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
* IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
* FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
* AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
* LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
* OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
* THE SOFTWARE.
*/

namespace Dorkbots.Grid
{
    public class GridManager : IGridManager
    {
		protected IGrid _grid;
        public IGrid grid{ get{ return _grid; } }
        public uint height{ get{ return _grid.height; } }
        public uint width{ get{ return _grid.width; } }
		public float cellWorldHeight{ get { return _grid.cellWorldHeight; } }
		public float cellWorldWidth{ get { return _grid.cellWorldWidth; } }
		public float worldHeight{ get { return _grid.worldHeight; } }
		public float worldWidth{ get { return _grid.worldWidth; } }

        public GridManager()
        {

        }

        public virtual void CreateGrid(IGrid grid)
        {
            _grid = grid;
        }

		public virtual void CreateGrid(uint height, uint width, float worldStartXPosition = 0, float worldStartYPosition = 0, float worldHeight = 0, float worldWidth = 0)
        {
			_grid = new Grid(height, width, new GridCellFactory(), worldStartXPosition, worldStartYPosition, worldHeight, worldWidth);
        }

        public IGridCell GetGridCell(uint x, uint y)
        {
            return _grid.GetGridCell(x, y);
        }

		public IGridCell GetGridCellFromWorldPosition(float x, float y)
		{
			return _grid.GetGridCellFromWorldPosition (x, y);
		}

        public IGridCell[] GetAllGridCells()
        {
            return _grid.allGridCells;
        }

		public IGridCell[] GetAdjacentCellsFromWorldDistance(float distance, IGridCell cell)
		{
			return _grid.GetAdjacentCellsFromWorldDistance(distance, cell);
		}

        public IGrid GetCopyOfGrid(IGridCellFactory cellFactory)
        {
			IGrid newGrid = new Grid(_grid.height, _grid.width, cellFactory);

            CopyGridToAnother(newGrid);

            return newGrid;
        }

        public void CopyGridToAnother(IGrid gridCopy)
        {
            if (_grid.width != gridCopy.width || _grid.height != gridCopy.height)
            {
                throw new System.ArgumentException("Grid height and width don't match!!!", "original");
            }

            uint y = 0;
            uint x = 0;

            for (x = 0; x < _grid.width; x++)
            {
                for (y = 0; y < _grid.height; y++)
                {
                    _grid.GetGridCell(x, y).Copy(gridCopy.GetGridCell(x, y));
                }
            }
        }

        public void FlipGridOnYAxis(IGrid gridCopy)
        {
            if (_grid.width != gridCopy.width || _grid.height != gridCopy.height)
            {
                throw new System.ArgumentException("Grid height and width don't match!!!", "original");
            }

            uint y = 0;
            uint x = 0;

            for (x = 0; x < _grid.width; x++)
            {
                for (y = 0; y < _grid.height; y++)
                {
                    _grid.GetGridCell(x, y).Copy(gridCopy.GetGridCell( (_grid.width - 1) - x, y));
                }
            }
        }
    }
}