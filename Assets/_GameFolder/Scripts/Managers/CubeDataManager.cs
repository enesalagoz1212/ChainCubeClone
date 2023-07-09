using ChainCube.ScriptableObjects;
using System.Collections.Generic;
using UnityEngine;

namespace ChainCube.Managers
{
	public class CubeDataManager : MonoBehaviour
	{
		public CubeData[] cubeDataArray;
		public List<CubeData> cubeDataList;
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

		public CubeData ReturnTargetNumberCubeData(int number)
		{
			for (var i = 0; i < cubeDataList.Count; i++)
			{
				var cubeData = cubeDataList[i];
				if (cubeData.number == number)
				{
					return cubeData;
				}
			}

			return null;
		}
    }
}

