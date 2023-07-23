using ChainCube.ScriptableObjects;
using ChainCube.Managers;
using UnityEngine;
using TMPro;
using ChainCube.Controllers;
using System;
using DG.Tweening;

namespace ChainCube.Controllers
{
	public class CubeController : MonoBehaviour
	{
		public TextMeshPro[] cubeTexts;
		public CubeData cubeData;
		public GameSettings gameSettings;
		public bool IsEndTriggerAvailable { get; set; }
		public Light cubeLight;

		private Rigidbody _rigidbody;
		private MeshRenderer _meshRenderer;
		private bool _isCollisionAvailable;
	
		private void Awake()
		{
			_rigidbody = GetComponent<Rigidbody>();
			_meshRenderer = GetComponent<MeshRenderer>();
			cubeLight = GetComponentInChildren<Light>();
			cubeLight.enabled = true;
		}

		public void CubeCreated(CubeData createdCubeData)
		{
			cubeData = createdCubeData;
			UpdateCubeText();
			_isCollisionAvailable = true;
			IsEndTriggerAvailable = false;
		}

		public void OnMergeCubeCreatedCheckSameCube()
		{
			var nearestCubeController = LevelManager.Instance.ReturnClosestCubeControllerWithSameNumber(this);
			if (nearestCubeController != null)
			{
				Vector3 direction = nearestCubeController.transform.position - transform.position;
				direction.Normalize();
				
				var velocity = direction * GameSettingManager.Instance.VariablesGameSettingsList[0].velocityMagnitude;
				velocity.y = GameSettingManager.Instance.VariablesGameSettingsList[0].upwardVelocity;

				SetVelocity(velocity);
			}
			else
			{
				_rigidbody.AddForce(Vector3.up * GameSettingManager.Instance.VariablesGameSettingsList[0].mergeUpwardForce, ForceMode.VelocityChange);
			}
			IsEndTriggerAvailable = true;
		}

		public void UpdateCubeText()
		{
			_meshRenderer.material.color = cubeData.color;
			foreach (TextMeshPro item in cubeTexts)
			{
				item.text = cubeData.number.ToString();
			}
		}

		public void SetVelocity(Vector3 velocity)
		{
			if (_rigidbody != null)
			{
				_rigidbody.velocity = velocity;
			}
		}

		public void ThrowCube()
		{
			_rigidbody.velocity = GameSettingManager.Instance.VariablesGameSettingsList[0].throwDirection;
			
			DOVirtual.DelayedCall(1f, () =>
			{
				IsEndTriggerAvailable = true;
			});
		}

		private void OnCollisionEnter(Collision collision)
		{
			if (!_isCollisionAvailable)
			{
				return;
			}
			if (collision.gameObject.CompareTag("Cube") || collision.gameObject.CompareTag("Platform"))
			{
				cubeLight.enabled = false;

				var otherCubeController = collision.gameObject.GetComponent<CubeController>();
				if (otherCubeController != null && cubeData.number == otherCubeController.cubeData.number)
				{
					_isCollisionAvailable = false;

					var hitPoint = collision.contacts[0].point;
					LevelManager.Instance.OnCubesCollided(this, hitPoint);

					int scoreIncrease = cubeData.number;
					GameManager.Instance.IncreaseGameScore(scoreIncrease);
					GameManager.Instance.IncreaseRecordScore(scoreIncrease);					
				}
			}
		}
		
		public void DestroyObject()
		{
			cubeLight.enabled = false;
			Destroy(gameObject);
		}
	}
}

