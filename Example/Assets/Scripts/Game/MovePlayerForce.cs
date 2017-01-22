using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
	public class MovePlayerForce : MonoBehaviour 
	{
		[SerializeField] private float speed = 16;
		[SerializeField] private float rotationSpeed = 16;

		private Rigidbody rb;

		void Start()
		{
			rb = GetComponent<Rigidbody> ();
		}

		void FixedUpdate ()
		{
			if (States.movePlayer)
			{
				float forAndAft = Input.GetAxis ("Vertical") * speed;
				float rotation = Input.GetAxis ("Horizontal") * rotationSpeed;
				rb.AddRelativeForce (0, 0, forAndAft);
				rb.AddTorque (0, rotation, 0);
			}
		}
	}
}