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
			if (!IsCollisionAvailable)
			{
				return;
			}
			if (collision.gameObject.CompareTag("Cube"))
			{
				var mainCubeController = collision.gameObject.GetComponent<MainCubeController>();
				if (collision.gameObject.CompareTag("Cube"))
				{
					TextMeshProUGUI scoreText = collision.gameObject.GetComponent<TextMeshProUGUI>();

					if (scoreText != null)
					{
						int scoreValue = int.Parse(scoreText.text);
						IncreaseScore(scoreValue);
					}
				}
				if (mainCubeController != null)
				{
					switch (mainCubeController.cubeType)
					{
						case CubeType.Cube:
							var otherCubeController = collision.gameObject.GetComponent<CubeController>();
							if (otherCubeController != null)
							{
								IsCollisionAvailable = false;
								var hitPoint = collision.contacts[0].point;
								LevelManager.Instance.OnColoredCubesCollided(this, otherCubeController, hitPoint);

								

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

