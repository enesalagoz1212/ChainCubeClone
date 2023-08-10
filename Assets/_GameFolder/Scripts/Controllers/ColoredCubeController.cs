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
		bool hasCollidedWithCube = false;

		private void Update()
		{
			HandleMovement();
		}
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

		private void HandleMovement()
		{
			if (InputManager.Instance.isInputEnabled)
			{
				Vector2 inputPosition = Vector2.zero;

				if (Application.isMobilePlatform)
				{
					if (Input.touchCount > 0)
					{
						Touch touch = Input.GetTouch(0);
						inputPosition = touch.position;
					}
				}
				else
				{
					inputPosition = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
				}

				InputManager.Instance.HandleInputMove(inputPosition);
			}
		}

		protected override void OnCollisionEnter(Collision collision)
		{
			base.OnCollisionEnter(collision);
			if (!IsCollisionAvailable || hasCollidedWithCube)
			{
				return;
			}
			if (collision.gameObject.CompareTag("Cube"))
			{
				Collider[] hitColliders = Physics.OverlapSphere(transform.position, 0.5f);
				Debug.Log($"hitColliders length: {hitColliders.Length}");
				foreach (var hitCollider in hitColliders) // HIT COLLIDER IS NOT USED!
				{
					if (!hasCollidedWithCube)
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
										IsCollisionAvailable = false;
										var hitPoint = collision.contacts[0].point;
										LevelManager.Instance.OnColoredCubesCollided(this, otherCubeController, hitPoint);
										hasCollidedWithCube = true;
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
}

