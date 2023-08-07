using System;
using ChainCube.ScriptableObjects;
using ChainCube.Managers;
using UnityEngine;
using DG.Tweening;
using TMPro;
using Random = UnityEngine.Random;

namespace ChainCube.Controllers
{
	public class CubeController : MainCubeController
	{
		public TextMeshPro[] cubeTexts;
		public bool IsEndTriggerAvailable { get; set; }

		public CubeData CubeData => _cubeData;
		private CubeData _cubeData;

		private MeshRenderer _meshRenderer;

		protected override void Awake()
		{
			base.Awake();
			
			_meshRenderer = GetComponent<MeshRenderer>();
		}

		public void CubeCreated(CubeData createdCubeData, bool isForThrow)
		{
			_cubeData = createdCubeData;
			UpdateCubeText();
			IsCollisionAvailable = true;
			IsEndTriggerAvailable = false;
			
			throwHighlighter.SetActive(isForThrow);
		}
		
		public void OnMergeCubeCreatedCheckSameCube()
		{
			var nearestCubeController = LevelManager.Instance.ReturnClosestCubeControllerWithSameNumber(this);
			if (nearestCubeController != null)
			{
				Vector3 direction = nearestCubeController.transform.position - transform.position;
				direction.Normalize();

				var velocity = direction * GameSettingManager.Instance.gameSettings.cubeSpeedMagnitude;
				velocity.y = GameSettingManager.Instance.gameSettings.cubeUpwardVelocity;

				SetVelocity(velocity);
			}
			else
			{
				Rigidbody.AddForce(Vector3.up * gameSettings.mergeUpwardForce, ForceMode.VelocityChange);
			}
			IsEndTriggerAvailable = true;
		}

		public void RotationOfMergingCube()
		{			
			var torque = new Vector3(Random.Range(gameSettings.minRotationOfMergingCube, gameSettings.maxRotationOfMergingCube), Random.Range(gameSettings.minRotationOfMergingCube, gameSettings.maxRotationOfMergingCube), Random.Range(gameSettings.minRotationOfMergingCube, gameSettings.maxRotationOfMergingCube)).normalized;
			
			Rigidbody.AddTorque(torque * gameSettings.cubeTorqueStrength, ForceMode.Impulse);
		}

		private void UpdateCubeText()
		{
			_meshRenderer.material.color = _cubeData.color;
			foreach (TextMeshPro item in cubeTexts)
			{
				item.text = _cubeData.number.ToString();
			}
		}

		public override void ThrowCube()
		{
			base.ThrowCube();
			
			DOVirtual.DelayedCall(1f, () =>
			{
				IsEndTriggerAvailable = true;
			});			
		}

		protected override void OnCollisionEnter(Collision collision)
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
							if (otherCubeController != null && _cubeData.number == otherCubeController._cubeData.number)
							{
								IsCollisionAvailable = false;

								var hitPoint = collision.contacts[0].point;
								LevelManager.Instance.OnCubesCollided(this, hitPoint);

								int scoreIncrease = _cubeData.number;
								GameManager.Instance.IncreaseGameScore(scoreIncrease);
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
		

		public override void DestroyObject()
		{
			base.DestroyObject();
		}
	}
}