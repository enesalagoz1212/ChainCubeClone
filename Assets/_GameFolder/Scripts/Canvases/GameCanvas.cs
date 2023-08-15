using UnityEngine;
using UnityEngine.UI;
using TMPro;
using ChainCube.Managers;
using ChainCube.ScriptableObjects;
using System.Collections;
using DG.Tweening;
using TMPro;

namespace ChainCube.Canvases
{
	public class GameCanvas : MonoBehaviour
	{
		public TextMeshProUGUI scoreText;
		public TextMeshProUGUI recordText;
		public TextMeshProUGUI coloredCubeText;
		public TextMeshProUGUI bombCubeText;

		public Button coloredButton;
		public Button bombButton;

		public GameSettings gameSettings;
		private SettingsCanvas _settingCanvas;
		private BoosterManager _boosterManager;
		private LevelManager _levelManager;

		private void Awake()
		{

		}
		private void OnEnable()
		{
			GameManager.OnGameScoreIncreased += OnGameScoreIncreased;
			GameManager.OnRecordScoreTexted += OnRecordScoreIncreased;
			GameManager.OnColoredCubeCountChanged += ColoredCubeCountChanged;
			GameManager.OnBombCubeCountChanged += BombCubeCountedChanged;
		}
		private void OnDisable()
		{
			GameManager.OnGameScoreIncreased -= OnGameScoreIncreased;
			GameManager.OnRecordScoreTexted -= OnRecordScoreIncreased;
			GameManager.OnColoredCubeCountChanged -= ColoredCubeCountChanged;
			GameManager.OnBombCubeCountChanged -= BombCubeCountedChanged;
		}
		void Start()
		{
			coloredButton.onClick.AddListener(OnColoredButtonClick);
			bombButton.onClick.AddListener(OnBombButtonClick);
			UpdateScoreText();
			UpdateRecordText();

			UpdateColoredCubeButtonText();
			UpdateBombCubeButtonText();
		}

		public void Initialize(LevelManager levelManager, BoosterManager boosterManager, SettingsCanvas settingCanvas)
		{
			_levelManager = levelManager;
			_boosterManager = boosterManager;
			_settingCanvas = settingCanvas;
		}

		private void OnColoredButtonClick()
		{
			if (GameSettingManager.ColoredCount > 0)
			{
				if (GameManager.Instance != null)
				{
					GameManager.Instance.DecreaseColoredCount(1);
					_levelManager.OnColoredCubeRequested();
				}
			}
		}

		private void OnBombButtonClick()
		{
			if (GameSettingManager.BombCount > 0)
			{
				if (GameManager.Instance != null)
				{

					GameManager.Instance.DecreaseBombCount(1);
					_levelManager.OnBombCubeRequested();
				}
			}
		}

		public void OnSettingButtonClick()
		{
			if (_settingCanvas != null)
			{
				_settingCanvas.ChangeSettingButtonInteractable();
			}
		}

		private void OnGameScoreIncreased(int score)
		{
			UpdateScoreText();
		}

		private void OnRecordScoreIncreased(int score)
		{
			UpdateRecordText();
		}

		public void UpdateScoreText()
		{
			scoreText.text = " " + GameManager.Instance.gameScore.ToString();
		}

		public void UpdateRecordText()
		{
			recordText.text = "Record:  " + GameManager.RecordScore;
		}

		private void ColoredCubeCountChanged(int count)
		{
			UpdateColoredCubeButtonText();
		}

		private void BombCubeCountedChanged(int count)
		{
			UpdateBombCubeButtonText();
		}


		private void UpdateColoredCubeButtonText()
		{
			coloredCubeText.text = " " + GameSettingManager.ColoredCount;
		}

		private void UpdateBombCubeButtonText()
		{
			bombCubeText.text = " " + GameSettingManager.BombCount;
		}

		public void DisableCubeButtons() 
		{
			coloredButton.interactable = false;
			bombButton.interactable = false;
		}

		public void EnableCubeButtons()
		{
			coloredButton.interactable = true;
			bombButton.interactable = true;
		}
	}
}
