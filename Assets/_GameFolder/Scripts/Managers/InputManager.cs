using DG.Tweening;
using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace ChainCube.Managers
{
	public class InputManager : MonoBehaviour
	{
		public static InputManager Instance { get; private set; }
		public bool isInputEnabled { get; private set; } = true;

		private float _firstTouchX;
		private bool _isDragging;

		
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
			_isDragging = false;
		}

		public void OnScreenTouch(PointerEventData eventData)
		{
			if (!_isDragging)
			{
				_firstTouchX = Input.mousePosition.x;
				_isDragging = true;
			}
		}
		
		public void OnScreenDrag(PointerEventData eventData)
		{
			if (_isDragging && isInputEnabled)
			{
				var cubeTransform = LevelManager.Instance.CurrentCubeTransform;
				float lastTouch = Input.mousePosition.x;
				float diff = lastTouch - _firstTouchX;

				var targetPosX = cubeTransform.position.x + diff * GameSettingManager.Instance.gameSettings.depthSpeedZ * Time.deltaTime;
				targetPosX = Mathf.Clamp(targetPosX, GameSettingManager.Instance.gameSettings.horizontalMinX, GameSettingManager.Instance.gameSettings.horizontalMaxX);

				var cubePos = cubeTransform.position;
				cubePos.x = targetPosX;
				cubeTransform.position = cubePos;

				_firstTouchX = lastTouch;
			}
		}

		public void OnScreenUp(PointerEventData eventData)
		{
			if (_isDragging && isInputEnabled)
			{
				LevelManager.Instance.ThrowCube();
			}

			_isDragging = false;
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