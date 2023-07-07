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


		public int CalculateCubeDataSum()
		{
			int sum = 0;

			
			foreach (var cubeData in cubeDataList)
			{
				sum += cubeData.number;
			}

			return sum;
		}
		public CubeData ReturnCubeDataList(int sum)
        {
            

            switch (sum)
            {
                case 4:
                    Debug.Log("Toplamlarý 4");
                   
                    return cubeDataList[0];
                case 8:
					Debug.Log("Toplamlarý 8");
					return cubeDataList[1]; 
                case 16:
					Debug.Log("Toplamlarý 16");
					return cubeDataList[2]; 
                case 32:
					Debug.Log("Toplamlarý 32");
					return cubeDataList[3];
                case 64:
					Debug.Log("Toplamlarý 64");
					return cubeDataList[4]; 
                default:
           
                    return cubeDataList[5];
            }
        }
        
    }
}

