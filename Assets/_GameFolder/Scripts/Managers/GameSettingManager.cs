using System;
using ChainCube.ScriptableObjects;
using UnityEngine;
using ChainCube.Canvases;

namespace ChainCube.Managers
{
	public class GameSettingManager : MonoBehaviour
	{
		public static GameSettingManager Instance { get; private set; }

		public GameSettings gameSettings;
		public SettingsCanvas settingsCanvas;

		private const string VibrationKey = "IsVibrationOn";
		private const string SoundKey = "IsSoundOn";
		private const string MusicKey = "IsMusicOn";

		public static bool IsVibrationOn
		{
			get
			{
				if (PlayerPrefs.HasKey(VibrationKey))
				{
					return bool.Parse(PlayerPrefs.GetString(VibrationKey));
				}
				return true;
			}
			set => PlayerPrefs.SetString(VibrationKey, value.ToString());
			
		}

		public static bool IsSoundOn
		{
			get
			{
				if (PlayerPrefs.HasKey(SoundKey))
				{
					return bool.Parse(PlayerPrefs.GetString(SoundKey));
				}
				return true;
			}
			set => PlayerPrefs.SetString(SoundKey, value.ToString());
			
		}

		public static bool IsMusicOn
		{
			get
			{
				if (PlayerPrefs.HasKey(MusicKey))
				{
					return bool.Parse(PlayerPrefs.GetString(MusicKey));
				}
				return true;
			}
			set => PlayerPrefs.SetString(MusicKey, value.ToString());
		}

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
		}

		private void Update()
		{
			if (Input.GetKeyDown(KeyCode.A))
			{
				Debug.Log($"Music on: {GameSettingManager.IsMusicOn}");
				Debug.Log($"Sound on: {GameSettingManager.IsSoundOn}");
				Debug.Log($"Vibration on: {GameSettingManager.IsVibrationOn}");
			}
		}
	}
}