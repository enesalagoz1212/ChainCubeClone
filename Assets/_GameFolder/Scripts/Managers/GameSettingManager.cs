using System;
using ChainCube.ScriptableObjects;
using UnityEngine.UI;
using UnityEngine;
using DG.Tweening;

namespace ChainCube.Managers
{
	public class GameSettingManager : MonoBehaviour
	{
		public static GameSettingManager Instance { get; private set; }
		public GameSettings gameSettings;
		
		public GameObject settingsPanel; // SETTINGS CANVAS
		public GameObject trueVibrationImage, falseVibrationImage; // SETTINGS CANVAS
		public GameObject trueSoundImage, falseSoundImage; // SETTINGS CANVAS
		public GameObject trueMusicImage, falseMusicImage; // SETTINGS CANVAS

		private bool _isVibrationOn;
		private bool _isSoundOn;
		private bool _isMusicOn;
		
		// private static bool IsMusicOn
		// {
		// 	get
		// 	{
		// 		if (PlayerPrefs.HasKey(MusicPlayerPrefs))
		// 		{
		// 			return bool.Parse(PlayerPrefs.GetString(MusicPlayerPrefs));
		// 		}
		// 		return true;
		// 	}
		// 	set => PlayerPrefs.SetString(MusicPlayerPrefs, value.ToString());
		// }

		private const string VibrationKey = "VibrationSetting";
		private const string SoundKey = "SoundSetting";
		private const string MusicKey = "MusicSetting";

		private InputManager _inputManager;
		public Button settingButton; // SETTINGS CANVAS
		
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
			_isMusicOn = GetSetting(MusicKey);
			_isSoundOn = GetSetting(SoundKey);
			_isVibrationOn = GetSetting(VibrationKey);
			
			UpdateVisuals();
		}

		private void Update()
		{
			if (Input.GetKeyDown(KeyCode.A))
			{
				Debug.Log($"Music on: {GetSetting(MusicKey)}");
				Debug.Log($"Sound on: {GetSetting(SoundKey)}");
				Debug.Log($"Vibration on: {GetSetting(VibrationKey)}");
			}
		}

		private bool GetSetting(string key)
		{
			if (PlayerPrefs.HasKey(key))
			{
				return bool.Parse(PlayerPrefs.GetString(key));
			}
			return true;
		}
		
		private void SaveSetting(string key, bool value)
		{
			PlayerPrefs.SetString(key, value.ToString());
		}
		
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
			SaveSetting(VibrationKey, _isVibrationOn);
			UpdateVisuals();
		}
		
		public void SoundButton() // SETTINGS CANVAS
		{
			_isSoundOn = !_isSoundOn;
			SaveSetting(SoundKey, _isSoundOn);
			UpdateVisuals();
		}
		
		public void MusicButton() // SETTINGS CANVAS
		{
			_isMusicOn = !_isMusicOn;
			SaveSetting(MusicKey, _isMusicOn);
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
	}
}