using System.Linq;
using UnityEngine;
using Camera = UnityEngine.Camera;

namespace BricksAndBalls.Util.CameraExtensions
{
	public static class CameraExtensions
	{
		public static Vector2 GetCameraExtent(this Camera camera)
		{
			if(!camera.orthographic)
				throw new System.Exception("Camera needs to be in orthographic view");

			return new Vector2(camera.orthographicSize * Screen.width / Screen.height, camera.orthographicSize);
		}

		public static Vector2 BoundsMin(this Camera camera)
		{
			return camera.transform.position.ToVector2() - camera.GetCameraExtent();
		}

		public static Vector2 BoundsMax(this Camera camera)
		{
			return camera.transform.position.ToVector2() + camera.GetCameraExtent();
		}

		private static Vector2 ToVector2(this Vector3 vector)
		{
			return new Vector2(vector.x, vector.y);
		}

		public static Vector2 GetMinCameraExtent(this Camera cam)
      {
			var supportedResolutions = GetResolutions();
			var minimumAspect = GetResolutions().Min(x => x.x / x.y);

			return new Vector2(cam.orthographicSize * minimumAspect, cam.orthographicSize);
      }

		private static Vector2[] GetResolutions()
      {
			return new[]
			{
				new Vector2(1920,1080),
				new Vector2(2732, 2047),
				new Vector2(2400,1080)
			};
      }
	}
}