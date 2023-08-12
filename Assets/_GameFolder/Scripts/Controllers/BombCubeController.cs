using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ChainCube.Managers;
using System;

namespace ChainCube.Controllers
{
	public class BombCubeController : MainCubeController
	{
		private bool _hasCollidedWithCube = false;
		public ParticleSystem particleEffectPrefab;

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
			if (collision.gameObject.CompareTag("Cube"))
			{
				if (!_hasCollidedWithCube)
				{
					var mainCubeController = collision.gameObject.GetComponent<MainCubeController>();
					if (mainCubeController != null)
					{
						switch (mainCubeController.cubeType)
						{
							case CubeType.Cube:
								var otherCubeController = collision.gameObject.GetComponent<CubeController>();
								if (otherCubeController != null)
								{
									Debug.Log("aa");
									LevelManager.Instance.BombParticleEffects(transform.position);
									LevelManager.Instance.DestroyBombCubeAndCube(this, otherCubeController, transform.position, GameSettingManager.Instance.gameSettings.bombDestroyRadius);
									Debug.Log("bb");
								}
								break;

							case CubeType.ColoredCube:
								break;
							case CubeType.BombCube:
								break;

							default:
								throw new ArgumentOutOfRangeException();
						}
					}
				}
			}
		}

	}
}

