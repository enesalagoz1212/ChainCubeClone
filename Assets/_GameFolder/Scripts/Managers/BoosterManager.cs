using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ChainCube.ScriptableObjects;
using ChainCube.Canvases;
using ChainCube.Controllers;


namespace ChainCube.Managers
{
	public class BoosterManager : MonoBehaviour
	{
		public static BoosterManager Instance { get; private set; }

		CubeController _coloredCubeController;

		public GameObject coloredCubePrefab;
		public GameObject coloredCubes;
		public Transform ColoredCubeTransform { get; private set; }

		
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
		
		
		public void CreateColoredCube()
		{
			var newColoredCube = Instantiate(coloredCubePrefab, GameSettingManager.Instance.gameSettings.CubeSpawnPos, Quaternion.identity, coloredCubes.transform);
			ColoredCubeTransform = newColoredCube.transform;
			_coloredCubeController = newColoredCube.GetComponent<CubeController>();
		
		}

	}
}

