using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System;
using ChainCube.Managers;
using ChainCube.ScriptableObjects;

namespace ChainCube.Canvases
{
	public class SettingsCanvas : MonoBehaviour
	{

		public Button settingButton; // SETTINGS CANVAS
		public Button closeButton; // SETTINGS CANVAS
		public Button musicButton; // SETTINGS CANVAS
		public Button soundButton; // SETTINGS CANVAS
		public Button vibrationButton; // SETTINGS CANVAS

		public GameObject settingsPanel; // SETTINGS CANVAS
		public GameObject trueVibrationImage;// SETTINGS CANVAS
		public GameObject falseVibrationImage; // SETTINGS CANVAS
		public GameObject trueSoundImage;// SETTINGS CANVAS
		public GameObject falseSoundImage; // SETTINGS CANVAS
		public GameObject trueMusicImage;// SETTINGS CANVAS
		public GameObject falseMusicImage; // SETTINGS CANVAS

		public RectTransform _backGroundSettinsPanel;
		public RectTransform _closeButton;
		public RectTransform _vibrationButton;
		public RectTransform _soundButton;
		public RectTransform _musicButton;

		private InputManager _inputManager;
		private GameSettings gameSettings;

		private bool _isVibrationOn;
		private bool _isSoundOn;
		private bool _isMusicOn;

		private void Awake()
		{
			settingButton.onClick.AddListener(OnSettingsButton);
			closeButton.onClick.AddListener(OnCloseSettingButtonClick);
			_inputManager = FindObjectOfType<InputManager>();
		}
		public void OnSettingsButton()
		{
			settingsPanel.SetActive(true);
			if (_inputManager != null)
			{
				_inputManager.DisableInput();
				GameCanvas gameCanvas = FindObjectOfType<GameCanvas>();
				if (gameCanvas != null)
				{
					gameCanvas.OnSettingButtonClick();
				}
			}
			else
			{
				Debug.Log("_inputMaanager == null");
			}
		}
		public void ChangeSettingButtonInteractable()
		{
			settingButton.interactable = !settingButton.interactable;
			settingsPanel.SetActive(!settingButton.interactable);
		}

		public void OnCloseSettingButtonClick() // SETTINGS CANVAS
		{
			GameCanvas gameCanvas = FindObjectOfType<GameCanvas>();
			if (gameCanvas != null)
			{
				gameCanvas.OnSettingButtonClick();
			}
			DOVirtual.DelayedCall(0.5f, () =>
			{
				_inputManager.EnabledInput();
			});
		}

		public void OnVibrationButtonClick() // SETTINGS CANVAS
		{
			_isVibrationOn = !_isVibrationOn;
			GameSettingManager.IsVibrationOn = _isVibrationOn;
			UpdateVisualsVibration();
		}

		public void OnSoundButtonClick() // SETTINGS CANVAS
		{
			_isSoundOn = !_isSoundOn;
			GameSettingManager.IsSoundOn = _isSoundOn;
			UpdateVisualsSound();
		}
		public void OnMusicButtonClick() // SETTINGS CANVAS
		{
			_isMusicOn = !_isMusicOn;
			GameSettingManager.IsMusicOn = _isMusicOn;
			UpdateVisualsMusic();
		}
		private void UpdateVisualsMusic() // SETTINGS CANVAS
		{
			falseMusicImage.SetActive(!_isMusicOn);
			trueMusicImage.SetActive(_isMusicOn);
		}
		private void UpdateVisualsSound() // SETTINGS CANVAS
		{
			falseSoundImage.SetActive(!_isSoundOn);
			trueSoundImage.SetActive(_isSoundOn);
		}
		private void UpdateVisualsVibration() // SETTINGS CANVAS
		{
			falseVibrationImage.SetActive(!_isVibrationOn);
			trueVibrationImage.SetActive(_isVibrationOn);
		}
		public void SettingsTween()
		{
			_backGroundSettinsPanel.DOScale(Vector3.zero, gameSettings.revealDurationTween).From();
			_closeButton.DOScale(Vector3.zero, gameSettings.revealDurationTween).SetDelay(gameSettings.revealDurationTween / 2).From();
			_vibrationButton.DOScale(Vector3.zero, gameSettings.revealDurationTween).From();
			_musicButton.DOScale(Vector3.zero, gameSettings.revealDurationTween).From();
			_soundButton.DOScale(Vector3.zero, gameSettings.revealDurationTween).From();
		}
	}
}