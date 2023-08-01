using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ChainCube.Managers
{
	public class SoundManager : MonoBehaviour
	{
		public static SoundManager Instance { get; private set; }
		public AudioClip backgroundMusic;
		//public AudioClip mergeCubeMusic;
		private AudioSource _audioSourceBackground;
		//private AudioSource _audioSourceMergeCube;

		private bool _isMusicEnabled = true;
		private void Awake()
		{
			if (Instance == null)
			{
				Instance = this;
				DontDestroyOnLoad(gameObject);				
				_audioSourceBackground = GetComponent<AudioSource>();
			}
			else
			{
				Destroy(gameObject);
			}
		}

		private void OnEnable()
		{
			GameManager.OnGameStarted += OnGameStart;
			GameManager.OnGameEnd += OnGameEnd;
		}
		private void OnDisable()
		{
			GameManager.OnGameStarted -= OnGameStart;
			GameManager.OnGameEnd -= OnGameEnd;
		}
		void Start()
		{
			Debug.Log("music caldi");
			BackgroundPlayMusic();
		}


		public void BackgroundPlayMusic()
		{
			
			if (backgroundMusic != null)
			{
				_audioSourceBackground.clip = backgroundMusic;
				_audioSourceBackground.Play();
			}
		}

		public void BackgroundMusicEnabled(bool enabled)
		{
			_isMusicEnabled = enabled;
			if (enabled)
			{
				BackgroundPlayMusic();
			}
			else
			{
				StopBackgroundMusic();
			}
		}
		//public void MergeCubePlayMusic()
		//{

		//	if (mergeCubeMusic != null)
		//	{
		//		_audioSourceMergeCube.clip = mergeCubeMusic;
		//		_audioSourceMergeCube.Play();
		//	}
		//}
		public void StopBackgroundMusic()
		{
			_audioSourceBackground.Stop();
		}
		private void OnGameStart()
		{
			BackgroundPlayMusic();
		}
		private void OnGameEnd()
		{
			StopBackgroundMusic();
		}
	}
}

