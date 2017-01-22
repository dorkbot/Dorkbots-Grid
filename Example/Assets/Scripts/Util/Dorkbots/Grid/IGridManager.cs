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
    public interface IGridManager
    {
        IGrid grid{ get; }
        void CreateGrid(IGrid grid);
		void CreateGrid (uint height, uint width, float worldStartXPosition = 0, float worldStartYPosition = 0, float worldHeight = 0, float worldWidth = 0);
        IGridCell GetGridCell(uint x, uint y);
		IGridCell GetGridCellFromWorldPosition (float x, float y);
		IGridCell[] GetAdjacentCellsFromWorldDistance (float distance, IGridCell cell);
        /// <summary>
        /// This reveals and gives access to all “GridCells”. Their position in the array is irrelevant.
        /// </summary>
        IGridCell[] GetAllGridCells();
        uint height{ get; }
        uint width{ get; }
		float cellWorldHeight{ get; }
		float cellWorldWidth { get; }
		IGrid GetCopyOfGrid(IGridCellFactory cellFactory);
        void CopyGridToAnother(IGrid gridCopy);
        void FlipGridOnYAxis(IGrid gridCopy);
    }
}