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

using UnityEngine;
using System;

namespace Dorkbots.Grid
{
    public class GridCell : IGridCell
    {
        private uint _x;
        public uint x{ get{return _x;} }
        private uint _y;
        public uint y{ get{return _y;} }
        private IGrid _grid;
        public IGrid grid{ get{return _grid;} }
        private int _type = 0;
        public int type{ get{ return _type; } set{ _type = value; OnTypeChangedEvent(EventArgs.Empty); } }
        private int _state = 0;
        public int state{ get{ return _state; } set{ _state = value; OnStateChangedEvent(EventArgs.Empty); } }

        public GridCell(uint x, uint y, IGrid grid)
        {
            _x = x;
            _y = y;
            _grid = grid;
        }

        public void Copy(IGridCell gridCell)
        {
            gridCell.type = this._type;
            gridCell.state = this._state;
        }

        public void SendMessage(string message)
        {
            OnMessageEvent(new GridCellMessageEventArgs(message));
        }

        // Events
        public event EventHandler _TypeChangedEvent;
        public EventHandler TypeChangedEvent{ get{ return _TypeChangedEvent; } set{ _TypeChangedEvent = value; } }
        private void OnTypeChangedEvent(EventArgs e)
        {
            EventHandler handler = _TypeChangedEvent;
            if (handler != null)
            {
                handler(this, e);
            }
        }

        private event EventHandler _StateChangedEvent;
        public EventHandler StateChangedEvent{ get{ return _StateChangedEvent; } set{ _StateChangedEvent = value; } }
        private void OnStateChangedEvent(EventArgs e)
        {
            EventHandler handler = _StateChangedEvent;
            if (handler != null)
            {
                handler(this, e);
            }
        }

        private event EventHandler _MessageEvent;
        public EventHandler MessageEvent{ get{ return _MessageEvent; } set{ _MessageEvent = value; } }
        private void OnMessageEvent(GridCellMessageEventArgs e)
        {
            EventHandler handler = _MessageEvent;
            if (handler != null)
            {
                handler(this, e);
            }
        }
    }
}