using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using ChainCube.Managers;
using ChainCube.ScriptableObjects;

namespace ChainCube.Canvases
{
	public class SettingsCanvas : MonoBehaviour
	{
		private GameSettings _gameSettings;
		private InputManager _inputManager;
		private GameCanvas _gameCanvas;

		public Button settingButton;
		public Button closeButton;
		public Button musicButton;
		public Button soundButton;
		public Button vibrationButton;

		public GameObject settingsPanel;
		public GameObject trueVibrationImage;
		public GameObject falseVibrationImage;
		public GameObject trueSoundImage;
		public GameObject falseSoundImage;
		public GameObject trueMusicImage;
		public GameObject falseMusicImage;

		public RectTransform backGroundSettinsPanelRectTransform;
		public RectTransform closeButtonRectTransform;
		public RectTransform vibrationButtonRectTransform;
		public RectTransform soundButtonRectTransform;
		public RectTransform musicButtonRectTransform;

		private void Awake()
		{
			settingButton.onClick.AddListener(OnSettingsButton);
			closeButton.onClick.AddListener(OnCloseSettingButtonClick);
		}

		private void OnEnable()
		{
			UpdateVisualsMusic();
			UpdateVisualsSound();
			UpdateVisualsVibration();
		}
		public void Initialize(InputManager inputManager, GameCanvas gameCanvas)
		{
			_inputManager = inputManager;
			_gameCanvas = gameCanvas;
			_gameSettings = gameCanvas.gameSettings;
		}
		
		public void OnSettingsButton()
		{
			settingsPanel.SetActive(true);
			_inputManager.DisableInput();
			_gameCanvas.OnSettingButtonClick();
			SettingsTween();
		}
		
		public void ChangeSettingButtonInteractable()
		{
			settingButton.interactable = !settingButton.interactable;
			settingsPanel.SetActive(!settingButton.interactable);			
		}

		public void OnCloseSettingButtonClick()
		{
		
			if (_gameCanvas != null)
			{
				_gameCanvas.OnSettingButtonClick();
			}
			DOVirtual.DelayedCall(0.5f, () =>
			{
				_inputManager.EnabledInput();
			});
		}

		public void OnVibrationButtonClick() 
		{
			GameSettingManager.IsVibrationOn = !GameSettingManager.IsVibrationOn;
			UpdateVisualsVibration();
		}

		public void OnSoundButtonClick() 
		{
			GameSettingManager.IsSoundOn = !GameSettingManager.IsSoundOn;
			UpdateVisualsSound();
		}

		public void OnMusicButtonClick() 
		{
			GameSettingManager.IsMusicOn = !GameSettingManager.IsMusicOn;
			SoundManager.Instance.CheckBackgroundMusic();
			UpdateVisualsMusic();
		}

		public void UpdateVisualsMusic()
		{
			
			falseMusicImage.SetActive(!GameSettingManager.IsMusicOn);
			trueMusicImage.SetActive(GameSettingManager.IsMusicOn);
		}

		public void UpdateVisualsSound()
		{
			falseSoundImage.SetActive(!GameSettingManager.IsSoundOn);
			trueSoundImage.SetActive(GameSettingManager.IsSoundOn);
		}

		public void UpdateVisualsVibration()
		{
			falseVibrationImage.SetActive(!GameSettingManager.IsVibrationOn);
			trueVibrationImage.SetActive(GameSettingManager.IsVibrationOn);
		}

		public void SettingsTween()
		{
			backGroundSettinsPanelRectTransform.DOScale(Vector3.zero, _gameSettings.revealDurationTween).From();
			closeButtonRectTransform.DOScale(Vector3.zero, _gameSettings.revealDurationTween).SetDelay(_gameSettings.revealDurationTween / 2).From();
			vibrationButtonRectTransform.DOScale(Vector3.zero, _gameSettings.revealDurationTween).From();
			musicButtonRectTransform.DOScale(Vector3.zero, _gameSettings.revealDurationTween).From();
			soundButtonRectTransform.DOScale(Vector3.zero, _gameSettings.revealDurationTween).From();
		}
	}
}