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
using UnityEngine;
using Dorkbots.Grid;
using System;

namespace Dorkbots.WorldGrid
{
	public class WorldGridManager : GridManager, IWorldGridManager
	{
		public enum GridOrientationTypes
		{
			x_and_z,
			x_and_y,
			y_and_z
		}
		private GridOrientationTypes _gridOrientation = GridOrientationTypes.x_and_z;
		public GridOrientationTypes gridOrientation{ get { return _gridOrientation; } set{ _gridOrientation = value; } }

		private List<IGridCellEntity> _movingEntities;

		public WorldGridManager()
		{
			_movingEntities = new List<IGridCellEntity> ();
		}

		public override void CreateGrid(uint height, uint width, float worldStartXPosition, float worldStartYPosition, float worldHeight, float worldWidth)
		{
			_grid = new Dorkbots.Grid.Grid(height, width, new WorldGridCellFactory(), worldStartXPosition, worldStartYPosition, worldHeight, worldWidth);
		}

		// add all IGridCellEntities
		public void AddAllEntities(GameObject parentGameObject)
		{
			IGridCellEntity[] enties = parentGameObject.GetComponentsInChildren<IGridCellEntity> ();
			IGridCellEntity entity;
			for (int i = 0; i < enties.Length; i++)
			{
				entity = enties [i];
				IntializeEntity (entity);
			}
		}

		public void IntializeEntity(IGridCellEntity entity)
		{
			if (entity.movingEntity && _movingEntities.IndexOf(entity) < 0)
			{
				_movingEntities.Add (entity);
				entity.broadcaster.AddEventHandler (GridCellEntity.ENTITY_MOVED, EntityMovedEvent);
			}

			UpdateEntityPosition (entity);
		}

		public void RemoveEntityFromGrid(IGridCellEntity entity)
		{
			entity.currentGridCell.RemoveEntity (entity);
			if (entity.movingEntity)
			{
				_movingEntities.Remove (entity);
			}
		}

		public void UpdateEntityPosition(IGridCellEntity entity)
		{
			switch(_gridOrientation)
			{
				case GridOrientationTypes.x_and_y:
					AddEntity(entity, entity.gameObject.transform.position.x, entity.gameObject.transform.position.y);
					break;

				case GridOrientationTypes.x_and_z:
					AddEntity(entity, entity.gameObject.transform.position.x, entity.gameObject.transform.position.z);
					break;
			}
		}

		private void AddEntity(IGridCellEntity  entity, float x, float y)
		{
			IWorldGridCell gridCell = _grid.GetGridCellFromWorldPosition(x, y) as IWorldGridCell;
			gridCell.AddEntity (entity);
		}

		public void CheckAllMovingEntities()
		{
			for (int i = 0; i < _movingEntities.Count; i++)
			{
				_movingEntities [i].Check ();
			}
		}
			
		public void InteractWithEntities(IGridCellEntity[] entities, IGridCellEntity entity)
		{
			for (int i = 0; i < entities.Length; i++)
			{
				entities [i].Interact (entity);
			}
		}

		public void NoInteractionWithEntities(IGridCellEntity[] entities, IGridCellEntity entity)
		{
			for (int i = 0; i < entities.Length; i++)
			{
				entities [i].NoInteraction (entity);
			}
		}

		public IGridCellEntity[] GetAllEntities()
		{
			return GetEntitiesFromCells(GetAllGridCells());
		}

		public IGridCellEntity[] GetAdjacentEntities(float distance, IGridCell cell, IGridCellEntity entityToExclude = null)
		{
			return GetEntitiesFromCells( GetAdjacentCellsFromWorldDistance( distance, cell ), entityToExclude );
		}

		public IGridCellEntity[] GetEntitiesWithinDistance(float distance, IGridCellEntity entity)
		{
			IGridCellEntity[] adjacentEntities = GetAdjacentEntities(distance, entity.currentGridCell, entity);
			List<IGridCellEntity> eligibleEntities = new List<IGridCellEntity> ();
			IGridCellEntity adjacentEntity;
			float distanceFromEntity;
			for (int i = 0; i < adjacentEntities.Length; i++)
			{
				adjacentEntity = adjacentEntities [i];
				distanceFromEntity = Vector3.Distance (adjacentEntity.gameObject.transform.position, entity.gameObject.transform.position);
				if (distanceFromEntity <= distance)
				{
					eligibleEntities.Add (adjacentEntity);
				}
			}

			return eligibleEntities.ToArray ();
		}

		public void ConfirmEntitiesWithinDistance(float distance, IGridCellEntity entity, IGridCellEntity[] checkEntities, out List<IGridCellEntity> failedEntities, out List<IGridCellEntity> passedEntities)
		{
			failedEntities = new List<IGridCellEntity> ();
			passedEntities = new List<IGridCellEntity> ();
			IGridCellEntity testingEntity;
			for (int i = 0; i < checkEntities.Length; i++)
			{
				testingEntity = checkEntities [i];
				if (Vector3.Distance (testingEntity.gameObject.transform.position, entity.gameObject.transform.position) <= distance)
				{
					passedEntities.Add (testingEntity);
				} 
				else
				{
					failedEntities.Add (testingEntity);
				}
			}
		}

		public IGridCellEntity[] GetEntitiesFromCells(IGridCell[] gridCells, IGridCellEntity entityToExclude = null)
		{
			List<IGridCellEntity> allEntities = new List<IGridCellEntity> ();
			IWorldGridCell gridCell;
			IGridCellEntity[] entities;
			IGridCellEntity entity;

			for (int i = 0; i < gridCells.Length; i++)
			{
				gridCell = gridCells [i] as IWorldGridCell;
				entities = gridCell.GetEntities ();
				for (int j = 0; j < entities.Length; j++)
				{
					entity = entities [j];
					allEntities.Add (entity);
				}
			}

			if (entityToExclude != null) allEntities.Remove (entityToExclude);

			return allEntities.ToArray ();
		}

		// Events
		private void EntityMovedEvent(object sender, EventArgs argument)
		{
			UpdateEntityPosition ((IGridCellEntity)sender);
		}

		// Debug/Print all Entities
		public void PrintEntities(IGridCellEntity[] entities)
		{
			IGridCellEntity entity;
			GameObject entityGameObject;

			for (int j = 0; j < entities.Length; j++)
			{
				entity = entities [j];
				entityGameObject = entity.gameObject;
				Debug.Log( string.Format("Entity Name = {0} ||| Entity Postition = {1} ||| Cell Position -> x = {2} | y = {3}", entity.entityName, entityGameObject.transform.position, entity.currentGridCell.x, entity.currentGridCell.y) );
			}
		}
	}
}