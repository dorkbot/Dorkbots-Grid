
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Dorkbots.Broadcasters;

namespace Dorkbots.WorldGrid
{
	public class GridCellEntity : MonoBehaviour, IGridCellEntity
	{
		public const string ENTITY_MOVED = "entity moved";
		public const string INTERACTION = "interaction";
		public const string NO_INTERACTION = "no interaction";

		[SerializeField] private string _entityName = "";
		public string entityName{ get{ return _entityName; } }
		[SerializeField] private string _entityType = "";
		public string entityType{ get { return _entityType; } }
		[SerializeField] private bool _movingEntity = false;
		public bool movingEntity{ get { return _movingEntity; } }
		[SerializeField] private float _interactDistance = 0;
		public float interactDistance{ get { return _interactDistance; } }

		private IWorldGridCell _currentGridCell;
		public IWorldGridCell currentGridCell{ get { return _currentGridCell; } set{ _currentGridCell = value; } }

		private Vector3 lastPosition;
		private Transform entityTransform;

		private IBroadcasterManager _broadcaster;
		public IBroadcasterManager broadcaster{ get{ return _broadcaster; } }

		void Awake()
		{
			_broadcaster = new BroadcasterManager ();
		}

		void Start () 
		{
			entityTransform = this.gameObject.transform;
			lastPosition = entityTransform.position;
		}

		public void Check () 
		{
			if (lastPosition != entityTransform.position)
			{
				lastPosition = entityTransform.position;

				_broadcaster.BroadcastEvent (ENTITY_MOVED, this);
			}
		}

		public void Interact(IGridCellEntity entity)
		{
			_broadcaster.BroadcastEvent (INTERACTION, this, new EntityInteractionEventObject(entity));
		}

		public void NoInteraction(IGridCellEntity entity)
		{
			_broadcaster.BroadcastEvent (NO_INTERACTION, this, new EntityInteractionEventObject(entity));
		}
	}

	public struct EntityInteractionEventObject
	{
		private object _interactingEntity;
		public object interactingEntity{ get { return _interactingEntity; } }

		public EntityInteractionEventObject(object interactingEntity)
		{
			_interactingEntity = interactingEntity;
		}
	}
}