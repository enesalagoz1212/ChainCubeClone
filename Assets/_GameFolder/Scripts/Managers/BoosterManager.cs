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

		public GameObject coloredCubePrefab;
		
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
	}
}

