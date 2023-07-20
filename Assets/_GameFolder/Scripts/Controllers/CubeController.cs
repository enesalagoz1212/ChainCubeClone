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

		private Rigidbody _rigidbody;
		private MeshRenderer _meshRenderer;

		public bool IsEndTriggerAvailable { get; set; }
		private bool _isCollisionAvailable;

		public Light cubeLight;
		
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

				float velocityMagnitude = 2f;
				var velocity = direction * velocityMagnitude;
				velocity.y = 5f;

				SetVelocity(velocity);
			}
			else
			{
				_rigidbody.AddForce(Vector3.up * 7, ForceMode.VelocityChange);
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
			_rigidbody.velocity = new Vector3(0, 0, 13f);
			
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

