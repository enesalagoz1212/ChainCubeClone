using ChainCube.Managers;
using ChainCube.ScriptableObjects;
using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace ChainCube.Controllers
{
	public class ColoredCubeController : MainCubeController
	{

		public override void ThrowCube()
		{
		

			base.ThrowCube();
			IsCollisionAvailable = true;


		}


		public void OnColorCubeCreated()
		{
			
			IsCollisionAvailable = false;

			throwHighlighter.SetActive(true);
			
		}

		protected override void OnCollisionEnter(Collision collision)
		{
			base.OnCollisionEnter(collision);
		}
	}
}

