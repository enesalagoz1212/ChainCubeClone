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

		private void OnEnable()
		{
			GameManager.OnGameStarted += OnGameStart;
			GameManager.OnGameEnd += OnGameEnd;
			GameManager.OnGameReset += OnGameReset;
		}

		private void OnDisable()
		{
			GameManager.OnGameStarted -= OnGameStart;
			GameManager.OnGameEnd -= OnGameEnd;
			GameManager.OnGameReset -= OnGameReset;
		}

		public void Initialize()
		{

		}

		private void OnGameStart()
		{
			DOVirtual.DelayedCall(1f, () =>
			{
				EnabledInput();
			});
		}

		private void OnGameEnd()
		{
		
			DisableInput();
		}

		private void OnGameReset()
		{
			DisableInput();
		}

		public void EnabledInput()
		{
			isInputEnabled = true;
		}

		public void DisableInput()
		{
			isInputEnabled = false;
		}
	}
}