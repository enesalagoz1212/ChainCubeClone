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

		protected Rigidbody Rigidbody;

		protected bool IsCollisionAvailable;

		protected virtual void Awake()
		{
			Rigidbody = GetComponent<Rigidbody>();
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