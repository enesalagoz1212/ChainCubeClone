using ChainCube.ScriptableObjects;
using DG.Tweening;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace ChainCube.Managers
{
	public class GameSettingManager : MonoBehaviour
	{
		public static GameSettingManager Instance { get; private set; }
		public GameSettings gameSettings;


		public GameObject settingsPanel;
		public GameObject trueVibrationImage, falseVibrationImage;
		public GameObject trueSoundImage, falseSoundImage;
		public GameObject trueMusicImage, falseMusicImage;

		private bool _isVibrationOn;
		private bool _isSoundOn;
		private bool _isMusicOn;

		private const string VibrationKey = "VibrationSetting";
		private const string SoundKey = "SoundSetting";
		private const string MusicKey = "MusicSetting";

		private InputManager _inputManager;
		public Button settingButton;
		private void Awake()
		{
			if (Instance != null && Instance != this)
			{
				Destroy(gameObject);
			}
			else
			{
				Instance = this;
				DontDestroyOnLoad(gameObject);
			}
			_inputManager = FindObjectOfType<InputManager>();
		}
		private void Start()
		{
			_isMusicOn = GetSetting(MusicKey, true);
			_isSoundOn = GetSetting(SoundKey, true);
			_isVibrationOn = GetSetting(VibrationKey, true);
		}
	

		private bool GetSetting(string key, bool defaultValue)
		{
			if (PlayerPrefs.HasKey(key))
			{
				return bool.Parse(PlayerPrefs.GetString(key));
			}
			return defaultValue;
		}

		private void SaveSetting(string key, bool value)
		{
			PlayerPrefs.SetString(key, value.ToString());
		}

		public void SetVibration(bool value)
		{
			_isVibrationOn = value;
			SaveSetting(VibrationKey, value);
			UpdateVisuals();
		}

		public void SetSound(bool value)
		{
			_isSoundOn = value;
			SaveSetting(SoundKey, value);
			UpdateVisuals();
		}

		public void SetMusic(bool value)
		{
			_isMusicOn = value;
			SaveSetting(MusicKey, value);
			UpdateVisuals();
		}


		public void SettingsButton()
		{
			_inputManager.DisableInput();
			settingsPanel.gameObject.SetActive(true);
			OnButtonClick();
		}
		public void OnButtonClick()
		{
			settingButton.interactable = false;
		}
		public void EnableButton()
		{
			settingButton.interactable = true;
		}
		public void CloseSettingsButton()
		{
			settingsPanel.gameObject.SetActive(false);
			DOVirtual.DelayedCall(1f, () =>
			{
				_inputManager.EnabledInput();
			});
			EnableButton();
		}

		public void VibrationButton()
		{
			_isVibrationOn = !_isVibrationOn;
		

			UpdateVisuals();
		}
		public void SoundButton()
		{
			_isSoundOn = !_isSoundOn;
	
			UpdateVisuals();
		}
		public void MusicButton()
		{
			_isMusicOn = !_isMusicOn;

			UpdateVisuals();
		}
		private void UpdateVisuals()
		{
			trueVibrationImage.SetActive(_isVibrationOn);
			falseVibrationImage.SetActive(!_isVibrationOn);

			trueSoundImage.SetActive(_isSoundOn);
			falseSoundImage.SetActive(!_isSoundOn);

			trueMusicImage.SetActive(_isMusicOn);
			falseMusicImage.SetActive(!_isMusicOn);
		}
	}
}

