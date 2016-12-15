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

using System.Collections.Generic;

namespace Dorkbots.Grid
{
    public class Grid : IGrid
    {
        private IGridCell[][] gridArray;
        private IGridCell[] _allGridCells;
        public IGridCell[] allGridCells{ get{ return _allGridCells; } }
        private uint _height;
        public uint height{ get{return _height;} }
        private uint _width;
        public uint width{ get{return _width;} }

        public Grid(uint height, uint width, IGridCellFactory factory)
        {
            List<IGridCell> gridCells = new List<IGridCell>();

            gridArray = new IGridCell[width][];

            int y = 0;
            int x = 0;

            for (x = 0; x < width; x++)
            {
                gridArray[x] = new IGridCell[height];

                for (y = 0; y < height; y++)
                {
                    gridArray[x][y] = factory.CreateGridCell((uint)x, (uint)y, this);
                    gridCells.Add(gridArray[x][y]);
                }
            }

            _allGridCells = gridCells.ToArray();

            _height = height;
            _width = width;
        }

        public IGridCell GetGridCell(uint x, uint y)
        {
            return gridArray[(int)x][(int)y];
        }
    }
}