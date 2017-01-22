using UnityEngine;
using System.Collections;

namespace Dorkbots.CameraTools
{
	// This script was ported to C# from 'Smooth Follow' found in Unity's Standard Assets. 'Smooth Follow' was originally in Uniscript.
	public class SmoothCameraFollow : MonoBehaviour 
	{
		// The target we are following
		[SerializeField] private Transform target;
		// The distance in the x-z plane to the target
		[SerializeField] private float distance = 10.0f;
		// the height we want the camera to be above the target
		[SerializeField] private float height = 5.0f;
		// alternatively ignore positions variables and get them instead from the scene.
		[SerializeField] private bool getPositionVarsFromWorld = false;

		[SerializeField] private float lookAtXRotationModifier = 0;

		[SerializeField] private float heightDamping = 2.0f;
		[SerializeField] private float rotationDamping = 3.0f;

		// Place the script in the Camera-Control group in the component menu
		[AddComponentMenu("Camera-Control/Smooth Camera Follow")]

		void Start()
		{
			if (getPositionVarsFromWorld)
			{
				distance = Vector3.Distance (transform.position, target.position);
				height = transform.position.y - target.position.y;
			}
		}

		void LateUpdate () 
		{
			// Calculate the current rotation angles
			float wantedRotationAngle = target.eulerAngles.y;
			float wantedHeight = target.position.y + height;

			float currentRotationAngle = transform.eulerAngles.y;
			float currentHeight = transform.position.y;

			// Damp the rotation around the y-axis
			currentRotationAngle = Mathf.LerpAngle(currentRotationAngle, wantedRotationAngle, rotationDamping * Time.deltaTime);

			// Damp the height
			currentHeight = Mathf.Lerp(currentHeight, wantedHeight, heightDamping * Time.deltaTime);

			// Convert the angle into a rotation
			var currentRotation = Quaternion.Euler(0, currentRotationAngle, 0);

			// Set the position of the camera on the x-z plane to:
			// distance meters behind the target
			transform.position = target.position;
			transform.position -= currentRotation * Vector3.forward * distance;

			// Set the height of the camera
			transform.position = new Vector3(transform.position.x,currentHeight,transform.position.z);

			Vector3 lookAtTarget = target.position;
			lookAtTarget.y += lookAtXRotationModifier;

			// Always look at the target
			transform.LookAt(lookAtTarget);
		}
	}
}