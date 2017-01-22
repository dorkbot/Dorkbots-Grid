using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Dorkbots.WorldGrid;
using Dorkbots.Broadcasters;
using System;

namespace Game
{
	public class Controller : MonoBehaviour 
	{
		[SerializeField] private GameObject go_world;
		[SerializeField] private GridCellEntity playerEntity;

		private IWorldGridManager grid;
		private IGridCellEntity[] eligibleEntities;

		// Use this for initialization
		void Start () 
		{
			grid = new WorldGridManager ();
			grid.CreateGrid (20, 20, -10, -10, 20, 20);

			grid.AddAllEntities (go_world);
			var test = new {test = "test"};
			Debug.Log (test.test);
			//playersTransform = playerEntity.gameObject.transform;

			grid.PrintEntities (grid.GetAllEntities ());

			playerEntity.broadcaster.AddEventHandler (GridCellEntity.ENTITY_MOVED, PlayerMovedEvent);
		}

		// Update is called once per frame
		void Update () 
		{
			grid.CheckAllMovingEntities ();
		}

		// Events
		private void PlayerMovedEvent(object sender, EventArgs argument)
		{
			// check if any entities can no longer be interacted with
			if (eligibleEntities != null)
			{
				List<IGridCellEntity> failedEntities;
				List<IGridCellEntity> passedEntities;
				grid.ConfirmEntitiesWithinDistance (playerEntity.interactDistance, playerEntity, eligibleEntities, out failedEntities, out passedEntities);
				grid.NoInteractionWithEntities (failedEntities.ToArray(), playerEntity);
			}

			eligibleEntities = grid.GetEntitiesWithinDistance (playerEntity.interactDistance, playerEntity);
			grid.PrintEntities(eligibleEntities);
			grid.InteractWithEntities (eligibleEntities, playerEntity);
		}

		// BUTTONS
		public void BtnClose()
		{
			grid.NoInteractionWithEntities (eligibleEntities, playerEntity);
		}
	}
}