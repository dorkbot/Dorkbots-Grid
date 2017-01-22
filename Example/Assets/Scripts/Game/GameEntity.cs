using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Dorkbots.WorldGrid;
using System;
using Dorkbots.Broadcasters;

namespace Game
{
	[RequireComponent (typeof (IGridCellEntity))]
	public class GameEntity : MonoBehaviour 
	{
		[SerializeField] private string _greetingMessage = "";
		[SerializeField] protected Messaging messaging;

		protected IGridCellEntity _entity;
		public IGridCellEntity entity{ get{ return entity; } }
		protected bool interacting = false;
		protected int actionCnt = 0;

		// Use this for initialization
		void Start () 
		{
			InternalStart ();
		}

		protected virtual void InternalStart()
		{
			_entity = GetComponent<IGridCellEntity> ();
			_entity.broadcaster.AddEventHandler (GridCellEntity.INTERACTION, EntityInteractionEvent);
			_entity.broadcaster.AddEventHandler (GridCellEntity.NO_INTERACTION, EntityNoInteractionEvent);
		}

		void Update()
		{
			InternalUpdate ();
		}

		protected virtual void InternalUpdate()
		{
			if (interacting)
			{
				if (Input.GetKeyDown (KeyCode.E))
				{
					PerformAction ();
				}
			}
		}

		protected virtual void StartInteraction()
		{
			if (!interacting)
			{
				actionCnt = 0;
				interacting = true;
				messaging.DisplayMessage (_greetingMessage);
			}
		}

		protected virtual void EndInteraction()
		{
			interacting = false;
			messaging.HideMessage ();
		}

		protected virtual void PerformAction()
		{
			Debug.Log ("PerformAction needs concrete logic added!!!");
		}

		// EVENTS
		private void EntityInteractionEvent(object sender, EventArgs argument)
		{
			Debug.Log ("INTERACTION!!!");
			BroadcasterEvent eventObject = (BroadcasterEvent)argument;
			EntityInteractionEventObject dataObject = (EntityInteractionEventObject)eventObject.dataObject;
			IGridCellEntity interactionEntity = (IGridCellEntity)dataObject.interactingEntity;
			Debug.Log ("interactionEntity.entityName = " + interactionEntity.entityName);
			if (interactionEntity.entityName == EntityNames.PLAYER)
			{
				StartInteraction ();
			}
		}

		private void EntityNoInteractionEvent(object sender, EventArgs argument)
		{
			Debug.Log ("NO INTERACTION!!!");
			BroadcasterEvent eventObject = (BroadcasterEvent)argument;
			EntityInteractionEventObject dataObject = (EntityInteractionEventObject)eventObject.dataObject;
			IGridCellEntity interactionEntity = (IGridCellEntity)dataObject.interactingEntity;
			Debug.Log ("interactionEntity.entityName = " + interactionEntity.entityName);
			if (interactionEntity.entityName == EntityNames.PLAYER)
			{
				EndInteraction ();
			}
		}
	}
}