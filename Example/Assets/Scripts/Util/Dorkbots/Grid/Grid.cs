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
using System;
using UnityEngine;

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
		private float _cellWorldHeight = 0;
		public float cellWorldHeight{ get { return _cellWorldHeight; } }
		private float _cellWorldWidth = 0;
		public float cellWorldWidth{ get { return _cellWorldWidth; } }
		private float _worldHeight = 0;
		public float worldHeight{ get { return _worldHeight; } }
		private float _worldWidth = 0;
		public float worldWidth{ get { return _worldWidth; } }
		private float _worldStartXPosition = 0;
		public float worldStartXPosition{ get { return _worldStartXPosition; } }
		private float _worldStartYPosition = 0;
		public float worldStartYPosition{ get { return _worldStartYPosition; } }

		public Grid(uint height, uint width, IGridCellFactory factory, float worldStartXPosition = 0, float worldStartYPosition = 0, float worldHeight = 0, float worldWidth = 0)
        {
			List<IGridCell> gridCells = new List<IGridCell>();

            gridArray = new IGridCell[width][];

            int y = 0;
            int x = 0;

			_height = height;
			_width = width;

			_worldStartXPosition = worldStartXPosition;
			_worldStartYPosition = worldStartYPosition;

			_worldHeight = worldHeight;
			_worldWidth = worldWidth;

			_cellWorldHeight = worldHeight / _height;
			_cellWorldWidth = worldWidth / _width;

            for (x = 0; x < _width; x++)
            {
                gridArray[x] = new IGridCell[_height];

                for (y = 0; y < _height; y++)
                {
                    gridArray[x][y] = factory.CreateGridCell((uint)x, (uint)y, this);
                    gridCells.Add(gridArray[x][y]);
                }
            }

            _allGridCells = gridCells.ToArray();
        }

        public IGridCell GetGridCell(uint x, uint y)
        {
            return gridArray[(int)x][(int)y];
        }

		public IGridCell GetGridCellFromWorldPosition(float x, float y)
		{
			Double offsetX = x - _worldStartXPosition;
			Double offsetY = y - _worldStartYPosition;
			
			Double possibleX = Math.Floor (offsetX / _cellWorldHeight);
			Double possibleY = Math.Floor (offsetY / _cellWorldWidth);

			float gridX = Mathf.Clamp( (float)possibleX, 0, (_width - 1));
			float gridY = Mathf.Clamp( (float)possibleY, 0, (_height - 1));

//			Debug.Log ( string.Format("offsetX = {0} | offsetY = {1}", offsetX, offsetY) );
//			Debug.Log ( string.Format("_worldStartXPosition = {0} | _worldStartYPosition = {1}", _worldStartXPosition, _worldStartYPosition) );
//			Debug.Log ( string.Format("x = {0} | y = {1} | possibleX = {2} | possibleX = {3} | gridX = {4} | gridX = {5}", x, y, possibleX, possibleY, gridX, gridY) );

			return GetGridCell((uint)gridX, (uint)gridY);
		}

		public IGridCell[] GetAdjacentCellsFromWorldDistance(float distance, IGridCell cell)
		{
			double gridXDistance = Math.Ceiling (distance / _cellWorldWidth);
			double gridYDistance = Math.Ceiling (distance / _cellWorldHeight);

			uint xStart = (uint)Mathf.Clamp ( (float)(cell.x - gridXDistance), 0, (_width - 1) );
			uint xEnd = (uint)Mathf.Clamp ( (float)(cell.x + gridXDistance), 0, (_width - 1) );

			uint yStart = (uint)Mathf.Clamp ( (float)(cell.y - gridYDistance), 0, (_height - 1) );
			uint yEnd = (uint)Mathf.Clamp ( (float)(cell.y + gridYDistance), 0, (_height - 1) );

			List<IGridCell> cells = new List<IGridCell> ();
			for (uint x = xStart; x <= xEnd; x++)
			{
				for (uint y = yStart; y <= yEnd; y++)
				{
					cells.Add (GetGridCell (x, y));
				}
			}

			//Debug.Log ( string.Format("gridXDistance = {0} | gridYDistance = {1} | xStart = {2} | yStart = {3} | yEnd = {4} | xEnd = {5} | cells.Count = {6}", gridXDistance, gridYDistance, xStart, yStart, yEnd, xEnd, cells.Count) );

			return cells.ToArray ();
		}
	}
}