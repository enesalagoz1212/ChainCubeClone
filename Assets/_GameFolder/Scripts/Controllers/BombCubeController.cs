using System.Collections;
using System.Collections.Generic;
using UnityEngine;


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
			base.OnCollisionEnter(collision);
			if (!IsCollisionAvailable)
			{
				return;
			}
			if (collision.gameObject.CompareTag("Cube"))
			{
				var mainCubeController = collision.gameObject.GetComponent<MainCubeController>();
				if (mainCubeController != null)
				{
					switch (mainCubeController.cubeType)
					{
						case CubeType.Cube:
							Destroy(collision.gameObject);
							Destroy(gameObject);
							RemoveFromActiveCubeList();
							break;
						case CubeType.ColoredCube:
							break;
						case CubeType.BombCube:

							break;
						default:
							break;
					}
				}
			}
		}
	}
}

