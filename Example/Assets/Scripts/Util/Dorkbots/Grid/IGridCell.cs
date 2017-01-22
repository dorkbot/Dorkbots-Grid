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

using System;

namespace Dorkbots.Grid
{
    public interface IGridCell
    {
        uint x{ get; }
        uint y{ get; }
		float worldHeight { get; }
		float worldWidth { get; }
        IGrid grid{ get; }
        /// <summary>
        /// "type' is intended to be a non changing value. Use "state" for a value that changes.
        /// </summary>
        int type{ get; }
        /// <summary>
        /// Use "state" for values that need to change.
        /// </summary>
        int state{ get; }

        void Copy(IGridCell gridCell);

		/// <summary>
		/// "type' is intended to be a non changing value. Use "state" for a value that changes.
		/// </summary>
		void ChangeType (int newType, bool sendEvent = true);
		/// <summary>
		/// Use "state" for values that need to change.
		/// </summary>
		void ChangeState (int newState, bool sendEvent = true);

        void SendMessage(string message);

        EventHandler TypeChangedEvent{ get; set; }
        EventHandler StateChangedEvent{ get; set; }
        EventHandler MessageEvent{ get; set; }
    }
}