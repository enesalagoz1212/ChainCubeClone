using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ChainCube.Managers;
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
			
		}
	}
}

