using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using ChainCube.Managers;
using ChainCube.ScriptableObjects;

namespace ChainCube.Canvases
{
	public class SettingsCanvas : MonoBehaviour
	{

		public Button settingButton; // SETTINGS CANVAS
		public Button closeButton; // SETTINGS CANVAS
		public Button musicButton; // SETTINGS CANVAS
		public Button soundButton; // SETTINGS CANVAS
		public Button vibrationButton; // SETTINGS CANVAS

		public GameObject settingsPanel; // SETTINGS CANVAS


		public RectTransform _backGroundSettinsPanel;
		public RectTransform _closeButton;
		public RectTransform _vibrationButton;
		public RectTransform _soundButton;
		public RectTransform _musicButton;

		private GameSettings gameSettings;

		private InputManager _inputManager;
		private GameCanvas _gameCanvas;


		private void Awake()
		{
			settingButton.onClick.AddListener(OnSettingsButton);
			closeButton.onClick.AddListener(OnCloseSettingButtonClick);
		}
		
		public void Initialize(InputManager inputManager, GameCanvas gameCanvas)
		{
			_inputManager = inputManager;
			_gameCanvas = gameCanvas;
		}
		
		public void OnSettingsButton()
		{
			settingsPanel.SetActive(true);
			_inputManager.DisableInput();
			_gameCanvas.OnSettingButtonClick();
		}
		
		public void ChangeSettingButtonInteractable()
		{
			settingButton.interactable = !settingButton.interactable;
			settingsPanel.SetActive(!settingButton.interactable);
		}

		public void OnCloseSettingButtonClick() // SETTINGS CANVAS
		{
			GameCanvas gameCanvas = FindObjectOfType<GameCanvas>(); // REMOVE
			if (gameCanvas != null)
			{
				gameCanvas.OnSettingButtonClick();
			}
			DOVirtual.DelayedCall(0.5f, () =>
			{
				_inputManager.EnabledInput();
			});
		}

		public void OnVibrationButtonClick() 
		{
			GameSettingManager.IsVibrationOn = !GameSettingManager.IsVibrationOn;
			GameSettingManager.Instance.UpdateVisualsVibration();
		}

		public void OnSoundButtonClick() 
		{
			GameSettingManager.IsSoundOn = !GameSettingManager.IsSoundOn;
			GameSettingManager.Instance.UpdateVisualsSound();
		}
		public void OnMusicButtonClick() 
		{
			GameSettingManager.IsMusicOn = !GameSettingManager.IsMusicOn;
			SoundManager.Instance.CheckBackgroundMusic();
			GameSettingManager.Instance.UpdateVisualsMusic();
		}
		
		public void SettingsTween()
		{
			_backGroundSettinsPanel.DOScale(Vector3.zero, gameSettings.revealDurationTween).From();
			_closeButton.DOScale(Vector3.zero, gameSettings.revealDurationTween).SetDelay(gameSettings.revealDurationTween / 2).From();
			_vibrationButton.DOScale(Vector3.zero, gameSettings.revealDurationTween).From();
			_musicButton.DOScale(Vector3.zero, gameSettings.revealDurationTween).From();
			_soundButton.DOScale(Vector3.zero, gameSettings.revealDurationTween).From();
		}
	}
}