using System.Collections.Generic;
using ChainCube.Controllers;
using UnityEngine;
using DG.Tweening;
using ChainCube.Pooling;
using System.Collections;
using ChainCube.Canvases;
using TMPro;

namespace ChainCube.Managers
{
	public class LevelManager : MonoBehaviour
	{
		public static LevelManager Instance { get; private set; }
		public Transform CurrentCubeTransform { get; private set; }
		public GameObject cubePrefab;
		public GameObject cubes;
		public GameObject endCube;
		public ParticleSystem bombParticlePrefab;

		private UIManager _uiManager;
		private ParticlePool _particlePool;

		private MainCubeController _currentMainCubeController;
		private int _collisionCounter;
		private List<MainCubeController> _activeMainCubes = new List<MainCubeController>();

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

		public void Initialize(UIManager uiManager, InputManager inputManager,ParticlePool particlePool)
		{
			_uiManager = uiManager;
			_particlePool = particlePool;
		}

		private void OnGameStarted()
		{
			_collisionCounter = 0;
			SpawnCube();
		}

		private void OnGameReseted()
		{
			foreach (var cube in _activeMainCubes)
			{
				Destroy(cube.gameObject);
			}

			_activeMainCubes.Clear();

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
			_currentMainCubeController = cubeObject.GetComponent<CubeController>();

			var cubeData = CubeDataManager.Instance.ReturnRandomCubeData();

			var cubeController = (CubeController)_currentMainCubeController; // MainCubeController => CubeController
			if (cubeController != null)
			{
				cubeController.CubeCreated(cubeData, true);
			}
		}

		public void ThrowCube()
		{
			if (_currentMainCubeController != null)
			{
				_activeMainCubes.Add(_currentMainCubeController);
			}

			GameManager.Instance.ChangeState(GameState.ThrowWaiting);
			_currentMainCubeController.ThrowCube();

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

		public void OnColoredCubesCollided(ColoredCubeController coloredCubeController, CubeController hitCubeController, Vector3 hitPoint)
		{
			var cubeData = hitCubeController.CubeData;
			var coloredMergeCubeNumber = cubeData.number * 2;

			DestroyCube(coloredCubeController);
			DestroyCube(hitCubeController);

			MergeCubes(hitPoint, coloredMergeCubeNumber);
		}

		public void OnCubeCollidedWithReset(CubeController cubeController)
		{
			if (_activeMainCubes.Contains(cubeController))
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
			
			GameObject mergeParticleEffect=_particlePool.GetParticle(cubeController.transform.position+Vector3.up);
		
			StartCoroutine(ReturnParticleEffect(mergeParticleEffect));

			cubeController.RotationOfMergingCube();

			int scoreIncrease = cubeNumber;
			GameManager.Instance.IncreaseGameScore(scoreIncrease);
			_activeMainCubes.Add(cubeController);
		}


		public CubeController ReturnClosestCubeControllerWithSameNumber(CubeController cubeController)
		{
			CubeController closestCubeController = null;
			float closestDistance = float.MaxValue;
			foreach (var activeCube in _activeMainCubes)
			{
				if (activeCube is CubeController activeCubeController && activeCubeController.CubeData.number == cubeController.CubeData.number)
				{
					float distance = Vector3.Distance(cubeController.transform.position, activeCubeController.transform.position);
					if (distance < closestDistance)
					{
						closestDistance = distance;
						closestCubeController = activeCubeController;
					}
				}
			}
			return closestCubeController;
		}

		public void OnColoredCubeRequested()
		{		
			DestroyCurrentCube();	
			
			if (BoosterManager.Instance != null)
			{
				var coloredCubeObject = Instantiate(BoosterManager.Instance.coloredCubePrefab, GameSettingManager.Instance.gameSettings.CubeSpawnPos, Quaternion.identity, cubes.transform);
				CurrentCubeTransform = coloredCubeObject.transform;
				_currentMainCubeController = coloredCubeObject.GetComponent<ColoredCubeController>();

				var coloredCubeController = (ColoredCubeController)_currentMainCubeController; // MainCubeController => CubeController

				if (coloredCubeController != null)
				{
					coloredCubeController.OnColorCubeCreated();
				}
			}
		}

		public void OnBombCubeRequested()
		{
			DestroyCurrentCube();

			if (BoosterManager.Instance != null)
			{
				var bombCubeObject = Instantiate(BoosterManager.Instance.bombCubePrefab, GameSettingManager.Instance.gameSettings.CubeSpawnPos, Quaternion.identity, cubes.transform);
				CurrentCubeTransform = bombCubeObject.transform;
				_currentMainCubeController = bombCubeObject.GetComponent<BombCubeController>();

				var bombCubeController = (BombCubeController)_currentMainCubeController; // MainCubeController => CubeController
				if (bombCubeController != null)
				{
					bombCubeController.OnBombCubeCreated();
				}
			}
		}

		public void DestroyBombCubeAndCube(BombCubeController bombCubeController, Vector3 center, float radius)
		{
			Collider[] hitColliders = Physics.OverlapSphere(center, radius);

			bool cubeDestroyed = false;
			int totalScore = 0;

			foreach (var hitCollider in hitColliders)
			{
				CubeController cubeController = hitCollider.GetComponent<CubeController>();
				if (cubeController != null)
				{
					totalScore += cubeController.CubeData.number;
					DestroyCube(cubeController);

					cubeDestroyed = true;
				}
			}

			if (cubeDestroyed && bombParticlePrefab != null)
			{
				Instantiate(bombParticlePrefab, center, Quaternion.identity);
			}

			if (bombCubeController != null)
			{
				Destroy(bombCubeController.gameObject);
			}
			if (totalScore > 0)
			{
				GameManager.Instance.IncreaseGameScore(totalScore);
			}
		}

		public void DestroyCurrentCube()
		{
			if (CurrentCubeTransform != null)
			{
				Destroy(CurrentCubeTransform.gameObject);
				CurrentCubeTransform = null;
			}
		}

		private void DestroyCube(MainCubeController mainCubeController)
		{
			if (_activeMainCubes.Contains(mainCubeController))
			{
				_activeMainCubes.Remove(mainCubeController);
			}
			mainCubeController.DestroyObject();
		}

		private IEnumerator ReturnParticleEffect(GameObject particle)
		{
			yield return new WaitForSeconds(1f);
			_particlePool.ReturnParticle(particle);
		}
	}
}
