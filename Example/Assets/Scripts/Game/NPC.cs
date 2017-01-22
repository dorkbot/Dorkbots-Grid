using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Dorkbots.WorldGrid;
using System;
using Dorkbots.Broadcasters;

namespace Game
{
	public class NPC : GameEntity 
	{
		[SerializeField] private string[] conversationMessage;

		protected override void PerformAction()
		{
			if (actionCnt >= conversationMessage.Length)
			{
				messaging.HideMessage ();
				actionCnt = 0;
				States.movePlayer = true;
			} 
			else
			{
				this.messaging.DisplayMessage (conversationMessage[actionCnt]);
				actionCnt++;
				States.movePlayer = false;
			}
		}
	}
}