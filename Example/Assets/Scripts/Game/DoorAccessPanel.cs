using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Dorkbots.WorldGrid;

namespace Game
{
	public class DoorAccessPanel : GameEntity
	{
		[SerializeField] private Animator hangarDoorAnimator;
		private bool doorOpen = false;

		protected override void PerformAction()
		{
			messaging.HideMessage ();
			if (doorOpen)
			{
				hangarDoorAnimator.SetTrigger ("close");
				doorOpen = false;
			} 
			else
			{
				hangarDoorAnimator.SetTrigger ("open");
				doorOpen = true;
			}
		}
	}
}