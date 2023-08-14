using UnityEngine;
using ChainCube.Managers;
using System;

namespace ChainCube.Controllers
{
	public class BombCubeController : MainCubeController
	{
		CubeController cubeController;
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
				
				return;
			}		
			 LevelManager.Instance.DestroyBombCubeAndCube(this,transform.position, GameSettingManager.Instance.gameSettings.bombDestroyRadius);
		}
	}
}

