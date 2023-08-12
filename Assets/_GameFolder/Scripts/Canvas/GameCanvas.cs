using UnityEngine;
using UnityEngine.UI;
using TMPro;
using ChainCube.Managers;
using ChainCube.ScriptableObjects;
using System.Collections;
using DG.Tweening;

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

		public int _coloredCubeCount = 5;
		public int _bombCubeCount = 5;
		private void OnEnable()
		{
			GameManager.OnGameScoreIncreased += OnGameScoreIncreased;
			GameManager.OnRecordScoreTexted += OnRecordScoreIncreased;
		}

		private void OnDisable()
		{
			GameManager.OnGameScoreIncreased -= OnGameScoreIncreased;
			GameManager.OnRecordScoreTexted -= OnRecordScoreIncreased;
		}
		void Start()
		{
			coloredButton.onClick.AddListener(OnColoredButtonClick);
			bombButton.onClick.AddListener(OnBombButtonClick);
			UpdateScoreText();
			UpdateRecordText();
		}


		public void Initialize(SettingsCanvas settingCanvas, BoosterManager boosterManager)
		{
			_settingCanvas = settingCanvas;
			_boosterManager = boosterManager;
		}

		private void OnColoredButtonClick()
		{
			if (LevelManager.Instance != null)
			{
				LevelManager.Instance.OnColoredCubeRequested();
			}
		}

		private void OnBombButtonClick()
		{
			if (LevelManager.Instance != null)
			{
				LevelManager.Instance.OnBombCubeRequsted();
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

		public void UseColoredCube()
		{
			if (_coloredCubeCount > 0)
			{
				_coloredCubeCount--;
				UpdateColoredCubeButtonText();
			}
		}
		public void UseBombCube()
		{
			if (_bombCubeCount>0)
			{
				_bombCubeCount--;
				UpdateBombCubeButtonText();
			}
		}

		private void UpdateColoredCubeButtonText()
		{
			coloredButton.GetComponentInChildren<TextMeshProUGUI>().text = $"{_coloredCubeCount}";
		}

		private void UpdateBombCubeButtonText()
		{
			bombButton.GetComponentInChildren<TextMeshProUGUI>().text = $"{_bombCubeCount}";
		}
	}
}
