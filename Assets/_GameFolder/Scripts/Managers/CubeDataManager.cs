using ChainCube.ScriptableObjects;
using UnityEngine;

namespace ChainCube.Managers
{
	public class CubeDataManager : MonoBehaviour
	{
		public CubeData[] cubeDataArray;
		public static CubeDataManager Instance { get; private set; }

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

		public CubeData ReturnRandomCubeData()
		{
			var randomIndex = Random.Range(0, cubeDataArray.Length);
			return cubeDataArray[randomIndex];
		}
	}
}

