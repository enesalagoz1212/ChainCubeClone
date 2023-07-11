using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

namespace ChainCube.Managers
{
    public class UIManager : MonoBehaviour
    {
        public static UIManager Instance { get; private set; }

        public TextMeshProUGUI scoreText;
        public TextMeshProUGUI recordText;
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
        }

		private void OnEnable()
		{
            GameManager.OnGameScoreIncreased += OnGameScoreIncreased;
            GameManager.OnRecordScoreIncreased += OnRecordScoreIncreased;
        }
		private void OnDisable()
		{
            GameManager.OnGameScoreIncreased -= OnGameScoreIncreased;
            GameManager.OnRecordScoreIncreased -= OnRecordScoreIncreased;
        }

		
		void Start()
        {
            UpdateScoreText();
        }

  
        void Update()
        {

        }
        private void UpdateScoreText()
        {
            scoreText.text = " " + GameManager.Instance.gameScore.ToString();
        }

        private void UptadeRecordText()
		{
            recordText.text = "Rekor:  " + GameManager.Instance.recordScore.ToString();
		}
        private void OnGameScoreIncreased(int score)
        {
            UpdateScoreText();
        }

        private void OnRecordScoreIncreased(int score)
        {
            UptadeRecordText();
        }

    }
}

