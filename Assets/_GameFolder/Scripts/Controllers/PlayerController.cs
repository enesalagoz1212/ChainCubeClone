using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ChainCube.Managers;


namespace ChainCube.Controllers
{
	public class PlayerController : MonoBehaviour
	{
		public GameObject cubePrefab;
		public GameObject cubes;
		private GameObject currentCube;

		private bool isCubeThrown;
		private float _firstTouchX;
		public float velocityZ;
		public float speed;
		public float _maxX;
		public float _minX;


		private void OnEnable()
		{
			GameManager.OnGameStarted += OnGameStarted;
			GameManager.OnGamePlaying += OnGamePlaying;
		}
		private void OnDisable()
		{
			GameManager.OnGameStarted -= OnGameStarted;
			GameManager.OnGamePlaying -= OnGamePlaying;
		}
		private void Start()
		{
			
		}


		void Update()
		{

			if (isCubeThrown && currentCube == null)
			{
				SpawnCube();
			}

			HorizontalMovement();


		}
		public void SpawnCube()
		{


			Vector3 position = new Vector3(0f, 0.35f, -3f);
			currentCube = Instantiate(cubePrefab, position, Quaternion.identity, cubes.transform);
			isCubeThrown = false;
		}


		public void HorizontalMovement()
		{
			if (Input.GetMouseButtonDown(0))
			{

				_firstTouchX = Input.mousePosition.x;
			}
			if (Input.GetMouseButton(0))

			{
				float lastTouch = Input.mousePosition.x;
				float diff = lastTouch - _firstTouchX;

				currentCube.transform.Translate(diff * Time.deltaTime * speed, 0, 0);

				Vector3 clampedPosition = currentCube.transform.position;
				clampedPosition.x = Mathf.Clamp(clampedPosition.x, _minX, _maxX);
				currentCube.transform.position = clampedPosition;

				_firstTouchX = lastTouch;
			}
			if (Input.GetMouseButtonUp(0))
			{
				ThrowCube(velocityZ);
			}

		}
		public void ThrowCube(float velocityZ)
		{
			if (currentCube != null && !isCubeThrown)
			{
				Rigidbody rb = currentCube.GetComponent<Rigidbody>();
				rb.velocity = new Vector3(0, 0, velocityZ);
				isCubeThrown = true;


			}
		}
		private void OnGameStarted()
		{
			SpawnCube();

		}
		private void OnGamePlaying()
		{
			//SpawnCube();
		}

	}
}

