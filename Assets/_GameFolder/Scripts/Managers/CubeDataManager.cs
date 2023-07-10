using ChainCube.ScriptableObjects;
using System.Collections.Generic;
using UnityEngine;

namespace ChainCube.Managers
{
	public class CubeDataManager : MonoBehaviour
	{
		public CubeData[] spawnedCubeDataArrayRandom;//isim deðiþ

		public List<CubeData> regeneratedCubeDataList;
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
			var randomIndex = Random.Range(0, spawnedCubeDataArrayRandom.Length);
			return spawnedCubeDataArrayRandom[randomIndex];
		}

		public CubeData ReturnTargetNumberCubeData(int number)
		{
			for (var i = 0; i < regeneratedCubeDataList.Count; i++)
			{
				var cubeData = regeneratedCubeDataList[i];
				if (cubeData.number == number)
				{
					return cubeData;
				}
			}

			return null;
		}
    }
}

