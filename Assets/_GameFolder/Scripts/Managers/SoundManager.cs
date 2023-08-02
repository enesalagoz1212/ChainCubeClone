using UnityEngine;

namespace ChainCube.Managers
{
	public class SoundManager : MonoBehaviour
	{
		public static SoundManager Instance { get; private set; }
		private AudioSource _audioSourceBackground;

		private bool _isMusicOn;

		private void Awake()
		{
			if (Instance == null)
			{
				Instance = this;
			}
			else
			{
				Destroy(gameObject);
			}			
			_audioSourceBackground = GetComponent<AudioSource>();
		}
		
		private void Start()
		{
			CheckBackgroundMusic();
		}
		
		public void CheckBackgroundMusic()
		{
			if (GameSettingManager.IsMusicOn)
			{
				_isMusicOn = true;
				_audioSourceBackground.Play();
			}
			else
			{
				if (_isMusicOn)
				{
					_audioSourceBackground.Stop();
				}
				_isMusicOn = false;
			}
		}
	}
}