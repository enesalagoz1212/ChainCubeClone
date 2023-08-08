using ChainCube.ScriptableObjects;
using ChainCube.Managers;
using UnityEngine;
using DG.Tweening;
using System;
using TMPro;
using System.Collections;

namespace ChainCube.Controllers
{
	public enum CubeType
	{
		Cube = 1,
		ColoredCube = 2,
		BombCube = 3,
	}
	public class MainCubeController : MonoBehaviour
	{
		public CubeType cubeType;
		public GameObject throwHighlighter;
		public GameSettings gameSettings;


		private GameManager gameManager;



		protected Rigidbody Rigidbody;

		protected bool IsCollisionAvailable;

		protected virtual void Awake()
		{
			Rigidbody = GetComponent<Rigidbody>();
		}


		public void Initialize(GameManager gameManager)
		{
			this.gameManager = gameManager;
		}
		public virtual void ThrowCube()
		{

			SetVelocity(gameSettings.throwDirection);
			throwHighlighter.SetActive(false);

		}



		protected virtual void SetVelocity(Vector3 velocity)
		{
			if (Rigidbody != null)
			{
				Rigidbody.velocity = velocity;
			}
		}

		protected virtual void OnCollisionEnter(Collision collision)
		{
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
							var otherCubeController = collision.gameObject.GetComponent<CubeController>();
							if (otherCubeController != null)
							{
								IsCollisionAvailable = false;
								var hitPoint = collision.contacts[0].point;
								LevelManager.Instance.OnColoredCubesCollided((ColoredCubeController)this, otherCubeController, hitPoint);

								TextMeshProUGUI scoreText = collision.gameObject.GetComponent<TextMeshProUGUI>();
								Debug.Log("scoreText: " + scoreText);
								if (scoreText != null)
								{
									int scoreValue = int.Parse(scoreText.text);
									Debug.Log("scoreValue: " + scoreValue);
									int scoreColored = scoreValue * 2;

									if (gameManager != null)
									{
										Debug.Log("gameManager: " + gameManager);
										Debug.Log("GameManager.Instance: " + GameManager.Instance);
										gameManager.IncreaseGameScore(scoreColored);
									}
								}

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
		public void RemoveFromActiveCubeList()
		{
			if (LevelManager.Instance != null)
			{
				LevelManager.Instance._activeMainCubes.Remove(this);
			}
		}
		public virtual void DestroyObject()
		{
			Destroy(gameObject);
		}
	}
}