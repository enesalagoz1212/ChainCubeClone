using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ChainCube.ScriptableObjects
{
	public class CubeDataManager : MonoBehaviour
	{
		public CubeData cubeData;
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
		public int GetNumberIndex(int index)
		{
			if (cubeData != null && index >= 0 && index < cubeData.numbers.Length)
			{
				return cubeData.numbers[index];
			}
			return 0;
		}
		public Color GetColorIndex(int index)
		{
			if (cubeData!=null && index>=0&&index<cubeData.colors.Length)
			{
				return cubeData.colors[index];
			}
			return Color.red;
		}
	}
}

