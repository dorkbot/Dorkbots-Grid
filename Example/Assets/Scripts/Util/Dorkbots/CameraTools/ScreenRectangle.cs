using System;
using UnityEngine;
using System.Diagnostics;

namespace Dorkbots.CameraTools
{
	public class ScreenRectangle
	{
		public static Rect GetScreenRectangle(Camera camera, Vector3 position)
        {
            var dist = (position - camera.transform.position).z;

			Vector3 topRight = camera.ViewportToWorldPoint (new Vector3 (1, 1, dist));
			Vector3 bottomleft = camera.ViewportToWorldPoint (new Vector3 (0, 0, dist));

			Rect rect = new Rect ();
			rect.xMin = bottomleft.x;
			rect.yMin = topRight.y;
			rect.xMax = topRight.x;
			rect.yMax = bottomleft.y;

			return rect;
		}
	}
}