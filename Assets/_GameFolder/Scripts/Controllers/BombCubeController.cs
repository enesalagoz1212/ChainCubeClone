using UnityEngine;
using ChainCube.Managers;
using System;

namespace ChainCube.Controllers
{
	public class BombCubeController : MainCubeController
	{
		public override void ThrowCube()
		{
			base.ThrowCube();
			IsCollisionAvailable = true;
		}

		public void OnBombCubeCreated()
		{
			IsCollisionAvailable = false;
			throwHighlighter.SetActive(true);
		}

	
		protected override void OnCollisionEnter(Collision collision)
		{
			if (!IsCollisionAvailable)
			{
				return;
			}

			if (collision.gameObject.CompareTag("BodyGround"))
			{
				Debug.Log($"Hit body ground");
				return;
			}
			
			Debug.Log($"Collision game name: {collision.gameObject.name}");
			
			// LevelManager.Instance.DestroyBombCubeAndCube(this, transform.position, GameSettingManager.Instance.gameSettings.bombDestroyRadius);
		}
	}
}

