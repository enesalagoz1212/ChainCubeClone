using UnityEngine;

namespace ChainCube.ScriptableObjects
{
	[CreateAssetMenu(fileName ="GameSettings",menuName ="Data/Game Settings")]
	public class GameSettings :ScriptableObject
	{	
		public Vector3 CubeSpawnPos;
		public Vector3 throwDirection;
		public float mergeUpwardForce;
		public float cubeSpeedMagnitude;
		public float cubeUpwardVelocity;
		public float depthSpeedZ;
		public float horizontalMaxX;
		public float horizontalMinX;
		public int minRotationOfMergingCube;
		public int maxRotationOfMergingCube;
		public float cubeTorqueStrength;
		public float revealDurationTween;

	}
}

