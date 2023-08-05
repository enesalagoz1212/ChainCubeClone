using UnityEngine;
using UnityEngine.UI;
using TMPro;
using ChainCube.Managers;
using ChainCube.ScriptableObjects;
using System.Collections;

namespace ChainCube.Canvases
{
	public class GameCanvas : MonoBehaviour
	{
		public TextMeshProUGUI scoreText;
		public TextMeshProUGUI recordText;

		public Button coloredButton;
		public Button bombButton;

		public GameSettings gameSettings;
		private SettingsCanvas _settingCanvas;
		private BoosterManager _boosterManager;
		private LevelManager _levelManager;
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
			UpdateScoreText();
			UpdateRecordText();
		}

		public void Initialize(SettingsCanvas settingCanvas, BoosterManager boosterManager)
		{
			_settingCanvas = settingCanvas;
			_boosterManager = boosterManager;
		}
		public void OnColoredButtonClick()
		{
			if (LevelManager.Instance!=null)
			{
				LevelManager.Instance.DestroyCurrentCube();
			}

			if (_boosterManager!=null)
			{
				
				_boosterManager.CreateColoredCube();
			}
			InputManager.Instance.DisableInput();
			StartCoroutine(EnabledInputAfterDelay());
		}
		private IEnumerator EnabledInputAfterDelay()
		{
			yield return new WaitForSeconds(1f); // Ýsteðe baðlý bir gecikme süresi

			// Ekrana týklama iþlemini tekrar etkinleþtir:
			InputManager.Instance.EnabledInput();
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
	}
}
