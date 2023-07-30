using ChainCube.ScriptableObjects;
using ChainCube.Managers;
using UnityEngine;
using TMPro;
using ChainCube.Controllers;
using System.Collections;
using DG.Tweening;

namespace ChainCube.Controllers
{
	public class CubeController : MonoBehaviour
	{
		public TextMeshPro[] cubeTexts;
		public GameObject throwHighlighter;

		
		public GameSettings gameSettings;
		public bool IsEndTriggerAvailable { get; set; }

		public CubeData CubeData => _cubeData;

		private Rigidbody _rigidbody;
		private MeshRenderer _meshRenderer;
		private bool _isCollisionAvailable;

		private CubeData _cubeData;

		private void Awake()
		{
			_rigidbody = GetComponent<Rigidbody>();
			_meshRenderer = GetComponent<MeshRenderer>();
		}

		public void CubeCreated(CubeData createdCubeData, bool isForThrow)
		{
			_cubeData = createdCubeData;
			UpdateCubeText();
			_isCollisionAvailable = true;
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
				_rigidbody.AddForce(Vector3.up *gameSettings.mergeUpwardForce, ForceMode.VelocityChange);
			}
			IsEndTriggerAvailable = true;
		}
		public void RotationOfMergingCube()
		{			
			var torque = new Vector3(Random.Range(gameSettings.minRotationOfMergingCube, gameSettings.maxRotationOfMergingCube), Random.Range(gameSettings.minRotationOfMergingCube, gameSettings.maxRotationOfMergingCube), Random.Range(gameSettings.minRotationOfMergingCube, gameSettings.maxRotationOfMergingCube)).normalized;
			
			_rigidbody.AddTorque(torque * gameSettings.cubeTorqueStrength, ForceMode.Impulse);
		}
		private void UpdateCubeText()
		{
			_meshRenderer.material.color = _cubeData.color;
			foreach (TextMeshPro item in cubeTexts)
			{
				item.text = _cubeData.number.ToString();
			}
		}

		private void SetVelocity(Vector3 velocity)
		{
			if (_rigidbody != null)
			{
				_rigidbody.velocity = velocity;
			}
		}

		public void ThrowCube()
		{
			_rigidbody.velocity = GameSettingManager.Instance.gameSettings.throwDirection;
			throwHighlighter.SetActive(false);
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
			if (collision.gameObject.CompareTag("Cube"))
			{
				var otherCubeController = collision.gameObject.GetComponent<CubeController>();
				if (otherCubeController != null && _cubeData.number == otherCubeController._cubeData.number)
				{
					_isCollisionAvailable = false;

					var hitPoint = collision.contacts[0].point;
					LevelManager.Instance.OnCubesCollided(this, hitPoint);

					int scoreIncrease = _cubeData.number;
					GameManager.Instance.IncreaseGameScore(scoreIncrease);
				}
			}
		}
		
		public void DestroyObject()
		{
			Destroy(gameObject);
		}
	}
}