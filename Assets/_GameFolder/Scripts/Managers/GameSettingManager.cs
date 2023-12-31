using ChainCube.ScriptableObjects;
using UnityEngine;


namespace ChainCube.Managers
{
	public class GameSettingManager : MonoBehaviour
	{
		public static GameSettingManager Instance { get; private set; }

		public GameSettings gameSettings;
		
		private const string VibrationKey = "IsVibrationOn";
		private const string SoundKey = "IsSoundOn";
		private const string MusicKey = "IsMusicOn";
		public const string ColoredCubePrefsString = "ColoredCount";
		public const string BombCubePrefsString = "BombCount";
	
		public static int ColoredCount
		{
			get
			{
				return PlayerPrefs.GetInt(ColoredCubePrefsString, 5);
			}
			set
			{
				PlayerPrefs.SetInt(ColoredCubePrefsString, value);
			}
		}

		public static int BombCount
		{
			get
			{
				return PlayerPrefs.GetInt(BombCubePrefsString, 5);
			}
			set
			{
				PlayerPrefs.SetInt(BombCubePrefsString, value);
			}
		}

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
	}
}