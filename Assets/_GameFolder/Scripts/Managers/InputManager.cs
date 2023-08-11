using DG.Tweening;
using System;
using UnityEngine;

namespace ChainCube.Managers
{
	public class InputManager : MonoBehaviour
	{
		public static InputManager Instance { get; private set; }
		public bool isInputEnabled { get; private set; } = true;


		private void Awake()
		{
			if (Instance != null && Instance != this)
			{
				Destroy(this);
			}
			else
			{
				Instance = this;
			}
		}

	}
}