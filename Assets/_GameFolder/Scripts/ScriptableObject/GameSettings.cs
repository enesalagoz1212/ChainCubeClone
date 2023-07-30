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
		public float horizontalSpeed;
		public float horizontalMaxX;
		public float horizontalMinX;
		public int minRotation;
		public int maxRotation;
		public float cubeTorqueStrength;
		public float revealDurationTween;

	}
}

