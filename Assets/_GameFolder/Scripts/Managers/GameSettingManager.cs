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
		
		private void Start()
		{
		
			
		}

		private void Update()
		{
			//if (Input.GetKeyDown(KeyCode.A))
			//{
			//	Debug.Log($"Music on: {GetSetting(MusicKey)}");
			//	Debug.Log($"Sound on: {GetSetting(SoundKey)}");
			//	Debug.Log($"Vibration on: {GetSetting(VibrationKey)}");
			//}
		}

	
		
	}
}