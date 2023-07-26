using ChainCube.ScriptableObjects;
using DG.Tweening;
using System.Collections.Generic;
using UnityEngine;

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

		private bool _isVibrationOn = true;
		private bool _isSoundOn = true;
		private bool _isMusicOn = true;

		private const string VibrationKey = "VibrationSetting";
		private const string SoundKey = "SoundSetting";
		private const string MusicKey = "MusicSetting";

		private InputManager _inputManager;

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
			_isVibrationOn = GetSetting(VibrationKey, true);
			_isSoundOn = GetSetting(SoundKey, true);
			_isMusicOn = GetSetting(MusicKey, true);
		}

		private bool GetSetting(string key, bool defaultValue)
		{
			return PlayerPrefs.GetInt(key, defaultValue ? 1 : 0) == 1;
		}

		private void SetSetting(string key, bool value)
		{
			PlayerPrefs.SetInt(key, value ? 1 : 0);
		}

		public void SettingsButton()
		{
			_inputManager.DisableInput();
			settingsPanel.gameObject.SetActive(true);
		}
		public void CloseSettingsButton()
		{
			settingsPanel.gameObject.SetActive(false);
			DOVirtual.DelayedCall(1f, () =>
			{
				_inputManager.EnabledInput();
			});

		}

		public void VibrationButton()
		{
			_isVibrationOn = !_isVibrationOn;
			SetSetting(VibrationKey, _isVibrationOn);

			UpdateVisuals();
		}
		public void SoundButton()
		{
			_isSoundOn = !_isSoundOn;
			SetSetting(SoundKey, _isSoundOn);

			UpdateVisuals();
		}
		public void MusicButton()
		{
			_isMusicOn = !_isMusicOn;
			SetSetting(MusicKey, _isMusicOn);


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

