namespace Mapbox.Examples
{
	using UnityEngine;

	public class CameraBillboard : MonoBehaviour
	{
		public Camera _camera;

		public void Start()
		{
			//If a camera doesn't exists, find the main camera in the scene and use that
			if(_camera == null)
			{
				GameObject cameraGO = GameObject.Find("Main Camera");
				_camera = cameraGO.GetComponent<Camera>();
			}
			_camera = Camera.main;
		}

		void Update()
		{
			transform.LookAt(transform.position + _camera.transform.rotation * Vector3.forward, _camera.transform.rotation * Vector3.up);
		}
	}
}