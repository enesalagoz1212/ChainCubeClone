using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using ChainCube.Managers;
using DG.Tweening;


namespace ChainCube.Canvases
{
	public class GameCanvas : MonoBehaviour
	{
		public TextMeshProUGUI scoreText; // GAME CANVAS
		public TextMeshProUGUI recordText; // GAME CANVAS

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
		
		public void Initialize()
		{
			
		}
		
		public void OnSettingButtonClick()
		{
			SettingsCanvas settingCanvas = FindObjectOfType<SettingsCanvas>(); // REMOVE
			if (settingCanvas != null)
			{
				settingCanvas.ChangeSettingButtonInteractable();
			}
		}
		
		void Start()
		{
			UpdateScoreText();
			UpdateRecordText();
		}

		private void OnGameScoreIncreased(int score) // GAME CANVAS
		{
			UpdateScoreText();
		}

		private void OnRecordScoreIncreased(int score) // GAME CANVAS
		{
			UpdateRecordText();
		}
		public void UpdateScoreText() // GAME CANVAS
		{
			scoreText.text = " " + GameManager.Instance.gameScore.ToString();
		}

		public void UpdateRecordText() // GAME CANVAS
		{
			recordText.text = "Record:  " + GameManager.RecordScore;
		}
	}
}
