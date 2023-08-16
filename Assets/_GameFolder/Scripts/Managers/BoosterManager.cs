using UnityEngine;

namespace ChainCube.Managers
{
	public class BoosterManager : MonoBehaviour
	{
		public static BoosterManager Instance { get; private set; }

		public GameObject coloredCubePrefab;
		public GameObject bombCubePrefab;

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

