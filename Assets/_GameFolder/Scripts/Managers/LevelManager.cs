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
		public Transform CurrentCubeTransform { get; private set; }
		
		private CubeController _currentCubeController;

		private int _collisionCounter;
		
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
		}

		private void OnDisable()
		{
			GameManager.OnGameStarted -= OnGameStarted;
		}

		private void OnGameStarted()
		{
			_collisionCounter = 0;
			SpawnCube();
		}

		private void SpawnCube()
		{
			var cubeObject = Instantiate(cubePrefab, CubeSpawnPos, Quaternion.identity, cubes.transform);
			CurrentCubeTransform = cubeObject.transform;
			_currentCubeController = cubeObject.GetComponent<CubeController>();

			var cubeData = CubeDataManager.Instance.ReturnRandomCubeData();
			_currentCubeController.CubeCreated(cubeData);
		}

		public void ThrowCube()
		{
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
			
			cubeController.DestroyObject();
			_collisionCounter++;
			
			if (_collisionCounter % 2 == 0)
			{
				MergeCubes(hitPoint, mergeCubeNumber);
			}
		}
		
		private void MergeCubes(Vector3 hitPos, int cubeNumber)
		{
			var cubeObject = Instantiate(cubePrefab, hitPos, Quaternion.identity, cubes.transform);
			var cubeController = cubeObject.GetComponent<CubeController>();

			var cubeData = CubeDataManager.Instance.ReturnTargetNumberCubeData(cubeNumber);
			cubeController.CubeCreated(cubeData);
		}
	}
}