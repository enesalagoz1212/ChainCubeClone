using DG.Tweening;
using System;
using UnityEngine;

namespace ChainCube.Managers
{
	public class InputManager : MonoBehaviour
	{
		public static InputManager Instance { get; private set; }
		public bool isInputEnabled { get; private set; } = true;

		private float _firstTouchX;

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

		private void Update()
		{
			switch (GameManager.Instance.GameState)
			{
				case GameState.Start:
					break;
				case GameState.ThrowAvailable:
					if (LevelManager.Instance.CurrentCubeTransform != null && isInputEnabled )//|| BoosterManager.Instance.ColoredCubeTransform != null)
					{
						HorizontalMovement();
					}
					break;

				case GameState.ThrowWaiting:
					break;

				case GameState.GameEnd:
					break;

				case GameState.Reset:
					break;

				default:
					throw new ArgumentOutOfRangeException();
			}
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
			Debug.Log("isInputEnabled=false");
			DisableInput();
		}

		private void OnGameReset()
		{
			DisableInput();
		}


		private void HorizontalMovement()
		{
			var cubeTransform = LevelManager.Instance.CurrentCubeTransform;
			if (Input.GetMouseButtonDown(0))
			{
				_firstTouchX = Input.mousePosition.x;
			}
			else if (Input.GetMouseButton(0))
			{
				float lastTouch = Input.mousePosition.x;
				float diff = lastTouch - _firstTouchX;

				var targetPosX = cubeTransform.position.x + diff * GameSettingManager.Instance.gameSettings.depthSpeedZ * Time.deltaTime;
				targetPosX = Mathf.Clamp(targetPosX, GameSettingManager.Instance.gameSettings.horizontalMinX, GameSettingManager.Instance.gameSettings.horizontalMaxX);

				var cubePos = cubeTransform.position;
				cubePos.x = targetPosX;
				cubeTransform.position = cubePos;

				_firstTouchX = lastTouch;
			}
			else if (Input.GetMouseButtonUp(0))
			{
				LevelManager.Instance.ThrowCube();
			}
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