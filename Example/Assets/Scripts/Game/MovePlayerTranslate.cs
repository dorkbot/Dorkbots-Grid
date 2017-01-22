using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
	public class MovePlayerTranslate : MonoBehaviour 
	{
		[SerializeField] private float speed = 3;
		[SerializeField] private float rotationSpeed = 150;

		// Update is called once per frame
		void Update () 
		{
			if (States.movePlayer)
			{
				float moveZ = Input.GetAxis("Vertical") * Time.deltaTime * speed;
				float rotationX = Input.GetAxis("Horizontal") * Time.deltaTime * rotationSpeed;

				transform.Translate(0, 0, moveZ);
				transform.Rotate(0, rotationX, 0);
			}
		}
	}
}