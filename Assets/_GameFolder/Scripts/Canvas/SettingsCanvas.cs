using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System;
using ChainCube.Managers;

namespace ChainCube.Canvases
{
	public class SettingsCanvas : MonoBehaviour
	{

		public GameObject settingsPanel; // SETTINGS CANVAS
		public GameObject trueVibrationImage, falseVibrationImage; // SETTINGS CANVAS
		public GameObject trueSoundImage, falseSoundImage; // SETTINGS CANVAS
		public GameObject trueMusicImage, falseMusicImage; // SETTINGS CANVAS

		public GameObject settingPanel;
		public RectTransform backGroundSettinsPanel;
		public RectTransform closeButton;
		public RectTransform vibrationButton, soundButton, musicButton;
		float revealDurationTween = 0.5f;

		public Button settingButton; // SETTINGS CANVAS

		private InputManager _inputManager;

		private bool _isVibrationOn;
		private bool _isSoundOn;
		private bool _isMusicOn;
		public void SettingsButton()
		{
			_inputManager.DisableInput();
			settingsPanel.gameObject.SetActive(true);
			OnButtonClick();
		}

		public void OnButtonClick() // SETTINGS CANVAS
		{
			settingButton.interactable = false;
		}

		public void EnableButton() // SETTINGS CANVAS
		{
			settingButton.interactable = true;
		}

		public void CloseSettingsButton() // SETTINGS CANVAS
		{
			settingsPanel.gameObject.SetActive(false);
			DOVirtual.DelayedCall(1f, () =>
			{
				_inputManager.EnabledInput();
			});
			EnableButton();
		}

		public void VibrationButton() // SETTINGS CANVAS
		{
			_isVibrationOn = !_isVibrationOn;
		
			UpdateVisuals();
		}

		public void SoundButton() // SETTINGS CANVAS
		{
			_isSoundOn = !_isSoundOn;
		
			UpdateVisuals();
		}

		public void MusicButton() // SETTINGS CANVAS
		{
			
			UpdateVisuals();
		}

		private void UpdateVisuals() // SETTINGS CANVAS
		{
			trueVibrationImage.SetActive(_isVibrationOn);
			falseVibrationImage.SetActive(!_isVibrationOn);

			trueSoundImage.SetActive(_isSoundOn);
			falseSoundImage.SetActive(!_isSoundOn);

			trueMusicImage.SetActive(_isMusicOn);
			falseMusicImage.SetActive(!_isMusicOn);
		}
		public void UiSettingsTween()
		{
			backGroundSettinsPanel.DOScale(Vector3.zero, revealDurationTween).From();
			closeButton.DOScale(Vector3.zero,revealDurationTween).SetDelay(revealDurationTween).From();
			vibrationButton.DOScale(Vector3.zero, revealDurationTween).From();
			musicButton.DOScale(Vector3.zero, revealDurationTween).From();
			soundButton.DOScale(Vector3.zero, revealDurationTween).From();
		}
	}
}