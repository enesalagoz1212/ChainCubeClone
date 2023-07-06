using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ChainCube.Managers;
using ChainCube.ScriptableObjects;
using TMPro;

namespace ChainCube.Controllers
{
	public class PlayerController : MonoBehaviour
	{
		public GameObject cubePrefab;
		public GameObject cubes;
		private GameObject currentCube;
		private GameObject lastThrownCube;

	

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
				Invoke("SpawnCube", 1f);
			}

			HorizontalMovement();


		}
		public void SpawnCube()
		{
			if (isCubeThrown)
			{
				StartCoroutine(DelayedSpawnCube());
			}
			else
			{
				Vector3 position = new Vector3(0f, 0.35f, -3f);
				currentCube = Instantiate(cubePrefab, position, Quaternion.identity, cubes.transform);
				lastThrownCube = currentCube;
				isCubeThrown = false;

				UpdateCubeText();
			}
		
		}
		private IEnumerator DelayedSpawnCube()
		{
			yield return new WaitForSeconds(0.5f);
			Vector3 position = new Vector3(0f, 0.35f, -3f);
			currentCube = Instantiate(cubePrefab, position, Quaternion.identity, cubes.transform);
			lastThrownCube = currentCube;
			isCubeThrown = false;

			UpdateCubeText();
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
			if (lastThrownCube != null && !isCubeThrown)
			{
				Rigidbody rb = currentCube.GetComponent<Rigidbody>();
				rb.velocity = new Vector3(0, 0, velocityZ);
				isCubeThrown = true;
				SpawnCube();

			}
		}
		private void OnGameStarted()
		{
			SpawnCube();
			UpdateCubeText();
		}
		private void OnGamePlaying()
		{
			
		}
		private void UpdateCubeText()
		{
			TextMeshPro[] textMesh = currentCube.GetComponentsInChildren<TextMeshPro>();
			if (textMesh != null&& textMesh.Length>0)
			{
				int cubeIndex = Random.Range(0, CubeDataManager.Instance.cubeData.numbers.Length);
				int number = CubeDataManager.Instance.GetNumberIndex(cubeIndex);
				Color color = CubeDataManager.Instance.GetColorIndex(cubeIndex);
				foreach (TextMeshPro item in textMesh)
				{

				    item.text = number.ToString();
					item.color = Color.white;
				}
			}
		}
	}
}

