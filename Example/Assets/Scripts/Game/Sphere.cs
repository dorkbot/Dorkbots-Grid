using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
	public class Sphere : GameEntity 
	{
		[SerializeField] private GameObject spherePrefab;
		[SerializeField] private GameObject effectPrefab;

		private GameObject sphere;
		private GameObject effect;

		protected override void StartInteraction()
		{
			if (!interacting)
			{
				interacting = true;

				StopCoroutine ("RunDestroyEffect");

				foreach (Transform child in transform) 
				{
					GameObject.Destroy(child.gameObject);
				}

				StartCoroutine ("RunSphere");
			}
		}

		protected override void EndInteraction()
		{
//			interacting = false;
//			StopCoroutine ("RunSphere");
//			if (sphere != null)
//			{
//				StartCoroutine ("RunDestroyEffect");
//			} 
//			else
//			{
//				Destroy (effect);
//				effect = null;
//			}
		}


		protected override void PerformAction()
		{
			// no player ineraction
		}

		private IEnumerator RunSphere()
		{
			sphere = Instantiate (spherePrefab, this.transform.position, Quaternion.identity) as GameObject;
			sphere.transform.parent = this.transform;

			sphere.transform.localScale = new Vector3 (.1f, .1f, .1f);

			while(sphere.transform.localScale.x < 1)
			{
				sphere.transform.localScale = new Vector3 (sphere.transform.localScale.x + .01f, sphere.transform.localScale.y + .01f, sphere.transform.localScale.z + .01f);
				yield return new WaitForSeconds (.05f);
			}

			yield return new WaitForSeconds (1f);

			effect = Instantiate (effectPrefab, this.transform.position, Quaternion.identity) as GameObject;
			effect.transform.parent = this.transform;

			Destroy (sphere);
			sphere = null;

			yield return new WaitForSeconds (1);

			Destroy (effect);
			effect = null;

			yield return new WaitForSeconds (3);

			StartCoroutine ("RunSphere");
		}

		private IEnumerator RunDestroyEffect()
		{
			Destroy (sphere);

			effect = Instantiate (effectPrefab, this.transform.position, Quaternion.identity) as GameObject;
			effect.transform.parent = this.transform;

			yield return new WaitForSeconds (1);

			Destroy (effect);
		}
	}
}