using System.Collections.Generic;
using ChainCube.Controllers;
using UnityEngine;
using DG.Tweening;
using ChainCube.ScriptableObjects;
using ChainCube.Managers;

namespace ChainCube.Managers
{
	public class LevelManager : MonoBehaviour
	{

		public static LevelManager Instance { get; private set; }

		private static readonly Vector3 CubeSpawnPos = new Vector3(0f, 0.35f, -3f);

		public GameObject cubePrefab;
		public GameObject cubes;
		public GameObject endCube;
		public Transform CurrentCubeTransform { get; private set; }
		public CubeDataManager cubeDataManager;
		private CubeController _currentCubeController;

		
		private int _collisionCounter;
		private List<CubeController> _activeCubes = new List<CubeController>();

		private void Awake()
		{
			if (Instance != null && Instance != this)
			{
				Destroy(this);
			}
			else
			{
				Instance = this;
			}
		}

		private void OnEnable()
		{
			GameManager.OnGameStarted += OnGameStarted;
			GameManager.OnGameReset += OnGameReseted;
			GameManager.OnGameEnd += OnGameEnd;

		}

		private void OnDisable()
		{
			GameManager.OnGameStarted -= OnGameStarted;
			GameManager.OnGameReset -= OnGameReseted;
			GameManager.OnGameEnd -= OnGameEnd;
		}

		private void OnGameStarted()
		{
			_collisionCounter = 0;
			SpawnCube();
		}
		private void OnGameEnd()
		{

		}
		private void OnGameReseted()
		{
			foreach (var cube in _activeCubes)
			{
				Destroy(cube.gameObject);
			}

			_activeCubes.Clear();

			if (CurrentCubeTransform != null)
			{
				Destroy(CurrentCubeTransform.gameObject);
				CurrentCubeTransform = null;
			}

		

		}

		public void SpawnCube()
		{
			var cubeObject = Instantiate(cubePrefab, CubeSpawnPos, Quaternion.identity, cubes.transform);
			CurrentCubeTransform = cubeObject.transform;
			_currentCubeController = cubeObject.GetComponent<CubeController>();

			var cubeData = CubeDataManager.Instance.ReturnRandomCubeData();
			_currentCubeController.CubeCreated(cubeData);
		}

		public void ThrowCube()
		{
			if (_currentCubeController != null)
			{
				_activeCubes.Add(_currentCubeController);
			}

			GameManager.Instance.ChangeState(GameState.ThrowWaiting);
			_currentCubeController.ThrowCube();

			DOVirtual.DelayedCall(0.5f, () =>
			{
				SpawnCube();
				GameManager.Instance.ChangeState(GameState.ThrowAvailable);
			});
		}

		public void OnCubesCollided(CubeController cubeController, Vector3 hitPoint)
		{
			var cubeData = cubeController.cubeData;
			var mergeCubeNumber = cubeData.number * 2;

			DestroyCube(cubeController);
			_collisionCounter++;

			if (_collisionCounter % 2 == 0)
			{
				MergeCubes(hitPoint, mergeCubeNumber);
			}
		}

		public void OnCubeCollidedWithReset(CubeController cubeController)
		{
			if (_activeCubes.Contains(cubeController))
			{
				GameManager.Instance.ChangeState(GameState.GameEnd);


			}
		}
		private void MergeCubes(Vector3 hitPos, int cubeNumber)
		{
			// REFACTOR THIS CODE

			var cubeObject = Instantiate(cubePrefab, hitPos, Quaternion.identity, cubes.transform);
			var cubeController = cubeObject.GetComponent<CubeController>();

			cubeController.cubeLight.enabled = false;
			var cubeData = CubeDataManager.Instance.ReturnTargetNumberCubeData(cubeNumber);
			cubeController.CubeCreated(cubeData);
			cubeController.OnMergeCubeCreatedCheckSameCube();


			_activeCubes.Add(cubeController);

			// REFACTOR THIS CODE
		}

		public CubeController ReturnClosestCubeControllerWithSameNumber(CubeController cubeController)
		{
			CubeController closestCubeController = null;
			float closestDistance = float.MaxValue;
			foreach (var activeCube in _activeCubes)
			{
				if (activeCube != cubeController && activeCube.cubeData.number == cubeController.cubeData.number)
				{
					float distance = Vector3.Distance(cubeController.transform.position, activeCube.transform.position);
					if (distance < closestDistance)
					{
						closestDistance = distance;
						closestCubeController = activeCube;
					}
				}
			}

			return closestCubeController;
		}

		private void DestroyCube(CubeController cubeController)
		{
			if (_activeCubes.Contains(cubeController))
			{
				_activeCubes.Remove(cubeController);
			}
			cubeController.DestroyObject();
		}
	}
}

//private Transform GetClosestEnemy(Transform[] enemies)
//{
//	Transform tMin = null;
//	float minDist = Mathf.Infinity;
//	Vector3 currentPos = transform.position;
//	foreach (Transform t in enemies)
//	{
//		float dist = Vector3.Distance(t.position, currentPos);
//		if (dist < minDist)
//		{
//			tMin = t;
//			minDist = dist;
//		}
//	}
//	return tMin;
//}