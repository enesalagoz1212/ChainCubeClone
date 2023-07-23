using UnityEngine;

namespace ChainCube.ScriptableObjects
{
	[CreateAssetMenu(fileName ="GameSettings",menuName ="Data/Game Settings")]
	public class GameSettings :ScriptableObject
	{	
		public Vector3 CubeSpawnPos;
		public Vector3 throwDirection;
		public float mergeUpwardForce;
		public float velocityMagnitude;
		public float upwardVelocity;
		public float speed;
		public float maxX;
		public float minX;
		public float delay;
	}
}

