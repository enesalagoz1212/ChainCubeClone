using System.Collections.Generic;
using ChainCube.Controllers;
using UnityEngine;
using DG.Tweening;

namespace ChainCube.Managers
{
	public class LevelManager : MonoBehaviour
	{
		public static LevelManager Instance { get; private set; }
		public Transform CurrentCubeTransform { get; private set; }
		public GameObject cubePrefab;
		public GameObject cubes;
		public GameObject endCube;
		public ParticleSystem mergeParticlePrefab;

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
		}

		private void OnDisable()
		{
			GameManager.OnGameStarted -= OnGameStarted;
			GameManager.OnGameReset -= OnGameReseted;
		}

		public void Initialize()
		{
			
		}

		private void OnGameStarted()
		{
			_collisionCounter = 0;
			SpawnCube();
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
			var cubeObject = Instantiate(cubePrefab, GameSettingManager.Instance.gameSettings.CubeSpawnPos, Quaternion.identity, cubes.transform);
			CurrentCubeTransform = cubeObject.transform;
			_currentCubeController = cubeObject.GetComponent<CubeController>();

			var cubeData = CubeDataManager.Instance.ReturnRandomCubeData();
			_currentCubeController.CubeCreated(cubeData, true);
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
			var cubeData = cubeController.CubeData;
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
				GameManager.Instance.EndGame();
			}
		}

		private void MergeCubes(Vector3 hitPos, int cubeNumber)
		{
			var cubeObject = Instantiate(cubePrefab, hitPos, Quaternion.identity, cubes.transform);
			var cubeController = cubeObject.GetComponent<CubeController>();




			var cubeData = CubeDataManager.Instance.ReturnTargetNumberCubeData(cubeNumber);
			cubeController.CubeCreated(cubeData, false);
			cubeController.OnMergeCubeCreatedCheckSameCube();


			GameObject mergeParticleObject = Instantiate(mergeParticlePrefab.gameObject, cubeObject.transform.position, Quaternion.identity, cubeController.transform);
			ParticleSystem mergeParticle = mergeParticleObject.GetComponent<ParticleSystem>();
			mergeParticle.Play();



			_activeCubes.Add(cubeController);

			cubeController.RotationOfMergingCube();

		}


		public CubeController ReturnClosestCubeControllerWithSameNumber(CubeController cubeController)
		{
			CubeController closestCubeController = null;
			float closestDistance = float.MaxValue;
			foreach (var activeCube in _activeCubes)
			{
				if (activeCube != cubeController && activeCube.CubeData.number == cubeController.CubeData.number)
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
