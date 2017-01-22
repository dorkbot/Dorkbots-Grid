using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Game
{
	public class Messaging : MonoBehaviour 
	{
		[SerializeField] private Text txt_message;

		// Use this for initialization
		void Start () 
		{
			HideMessage();
		}

		public void DisplayMessage(string message)
		{
			txt_message.text = message;
			this.gameObject.SetActive (true);
		}

		public void HideMessage()
		{
			this.gameObject.SetActive (false);
		}

		// BUTTONS
		public void BtnClose()
		{
			HideMessage ();
			States.movePlayer = true;
		}
	}
}