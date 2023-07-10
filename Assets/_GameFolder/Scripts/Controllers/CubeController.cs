using ChainCube.ScriptableObjects;
using ChainCube.Managers;
using UnityEngine;
using TMPro;

namespace ChainCube.Controllers
{
	public class CubeController : MonoBehaviour
	{
		public TextMeshPro[] cubeTexts;
		public CubeData cubeData;

		private Rigidbody _rigidbody;
		private MeshRenderer _meshRenderer;

		private bool _isCollisionAvailable;

		private void Awake()
		{
			_rigidbody = GetComponent<Rigidbody>();
			_meshRenderer = GetComponent<MeshRenderer>();
		}

		public void CubeCreated(CubeData createdCubeData)
		{
			cubeData = createdCubeData;
			UpdateCubeText();
			_isCollisionAvailable = true;
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
		}

		private void OnCollisionEnter(Collision collision)
		{
			if (!_isCollisionAvailable)
			{
				return;
			}

			if (collision.gameObject.CompareTag("Cube"))
			{
				// Debug.Log("Cubes collided");
				var otherCubeController = collision.gameObject.GetComponent<CubeController>();
				if (otherCubeController != null)
				{
					if (cubeData.number == otherCubeController.cubeData.number)
					{
						_isCollisionAvailable = false;

						var hitPoint = collision.contacts[0].point;

						LevelManager.Instance.OnCubesCollided(this, hitPoint);

						// LEVEL MANAGER ICINDE MERGE METHODU OLACAK
						// BIRLESEN KUPLER DESTROY OLCAK, YENISI OLUSACAK
						// OLUSACAK KUP BIR KADEME USTTEN OLCAK
					}
				}
			}
		}

		public void DestroyObject()
		{
			Destroy(gameObject);
		}
	}
}

