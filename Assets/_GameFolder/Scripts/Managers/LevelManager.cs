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

		public GameObject cubePrefab;
		public GameObject cubes;

		public Transform CurrentCubeTransform { get; private set; }

		private CubeController _currentCubeController;
		//private CubeData _cubeData;
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
			SpawnCube();
		}

		public void SpawnCube()
		{
			
			Vector3 position = new Vector3(0f, 0.35f, -3f);

			var cubeObject = Instantiate(cubePrefab, position, Quaternion.identity, cubes.transform);
			CurrentCubeTransform = cubeObject.transform;
			_currentCubeController = cubeObject.GetComponent<CubeController>();
			_currentCubeController.CubeCreated();
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
		public void MergeCubes(CubeController cube1,CubeController cube2, Vector3 mergePosition)
		{
			int newNumber = cube1.cubeData.number + cube2.cubeData.number;
			CubeData newCubeData = CubeDataManager.Instance.ReturnCubeDataList(newNumber);

			GameObject newCube = Instantiate(cubePrefab, mergePosition + new Vector3(0f, 1f, 0f), Quaternion.identity, cubes.transform);


			CubeController newCubeController = newCube.GetComponent<CubeController>();
			newCubeController.cubeData = newCubeData;
			newCubeController.CubeCreated();

		}
	}
}