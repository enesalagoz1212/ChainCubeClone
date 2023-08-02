using UnityEngine;
using TMPro;
using ChainCube.Managers;
using ChainCube.ScriptableObjects;

namespace ChainCube.Canvases
{
	public class GameCanvas : MonoBehaviour
	{
		public TextMeshProUGUI scoreText;
		public TextMeshProUGUI recordText;

		public GameSettings gameSettings;
		private SettingsCanvas _settingCanvas;
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

		public void Initialize(SettingsCanvas settingCanvas)
		{
			_settingCanvas = settingCanvas;
		}
		
		public void OnSettingButtonClick()
		{
			if (_settingCanvas != null)
			{
				_settingCanvas.ChangeSettingButtonInteractable();
			}
		}
		
		void Start()
		{
			UpdateScoreText();
			UpdateRecordText();
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
	}
}
