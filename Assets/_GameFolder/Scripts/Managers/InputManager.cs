using DG.Tweening;
using System;
using UnityEngine;

namespace ChainCube.Managers
{
	public class InputManager : MonoBehaviour, IInputHandler
	{
		public static InputManager Instance { get; private set; }

		private IInputHandler inputHandler;
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
			inputHandler = this;
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
			if (isInputEnabled)
			{
				HandleInput();
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

		private void HandleInput()
		{
			switch (GameManager.Instance.GameState)
			{
				case GameState.Start:
					break;
				case GameState.ThrowAvailable:
					if (LevelManager.Instance.CurrentCubeTransform != null && isInputEnabled)
					{
						HandleTouchInput();
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

		public void HandleTouchInput()
		{
			if (Input.touchCount > 0)
			{
				Touch touch = Input.GetTouch(0);
				Vector2 touchPosition = touch.position;

				switch (touch.phase)
				{
					case TouchPhase.Began:
						inputHandler.HandleTouchDown(touchPosition);
						break;

					case TouchPhase.Moved:
						inputHandler.HandleTouchMove(touchPosition);
						break;

					case TouchPhase.Stationary:
						break;

					case TouchPhase.Ended:
						inputHandler.HandleTouchUp();
						break;

					case TouchPhase.Canceled:
						break;
					default:
						break;
				}

			}
		}


		public void HandleMouseInput()
		{
			if (Input.GetMouseButtonDown(0))
			{
				Vector2 mousePosition = Input.mousePosition;
				inputHandler.HandleInputStart(mousePosition);
			}
			else if (Input.GetMouseButton(0))
			{
				Vector2 mousePosition = Input.mousePosition;
				inputHandler.HandleInputMove(mousePosition);
			}
			else if (Input.GetMouseButtonUp(0))
			{
				inputHandler.HandleInputEnd();
			}
		}

		public void HandleTouchDown(Vector2 position)
		{
			_firstTouchX = position.x;
		}

		public void HandleTouchMove(Vector2 position)
		{
			if (LevelManager.Instance.CurrentCubeTransform != null)
			{
				Transform cubeTransform = LevelManager.Instance.CurrentCubeTransform;

				float lastTouch = position.x;
				float diff = lastTouch - _firstTouchX;

				var targetPosX = cubeTransform.position.x + diff * GameSettingManager.Instance.gameSettings.depthSpeedZ * Time.deltaTime;
				targetPosX = Mathf.Clamp(targetPosX, GameSettingManager.Instance.gameSettings.horizontalMinX, GameSettingManager.Instance.gameSettings.horizontalMaxX);

				var cubePos = cubeTransform.position;
				cubePos.x = targetPosX;
				cubeTransform.position = cubePos;

				_firstTouchX = lastTouch;
			}

		}

		public void HandleTouchUp()
		{
			LevelManager.Instance.ThrowCube();
		}



		public void EnabledInput()
		{
			isInputEnabled = true;
		}

		public void DisableInput()
		{
			isInputEnabled = false;
		}

		public void HandleInputStart(Vector2 mousePosition)
		{
			_firstTouchX = mousePosition.x;
		}

		public void HandleInputMove(Vector2 mousePosition)
		{
			if (LevelManager.Instance.CurrentCubeTransform != null)
			{
				Transform cubeTransform = LevelManager.Instance.CurrentCubeTransform;

				float lastTouch = mousePosition.x;
				float diff = lastTouch - _firstTouchX;

				var targetPosX = cubeTransform.position.x + diff * GameSettingManager.Instance.gameSettings.depthSpeedZ * Time.deltaTime;
				targetPosX = Mathf.Clamp(targetPosX, GameSettingManager.Instance.gameSettings.horizontalMinX, GameSettingManager.Instance.gameSettings.horizontalMaxX);

				var cubePos = cubeTransform.position;
				cubePos.x = targetPosX;
				cubeTransform.position = cubePos;

				_firstTouchX = lastTouch;
			}

		}

		public void HandleInputEnd()
		{
			LevelManager.Instance.ThrowCube();

		}
	}
}