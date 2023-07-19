using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using UnityEngine.UI;

namespace ChainCube.Managers
{
    public class UIManager : MonoBehaviour
    {
        public static UIManager Instance { get; private set; }

        public GameObject endPanel;
        public TextMeshProUGUI scoreText;
        public TextMeshProUGUI recordText;
        public Image colorImage;
        public Image bombImage;
		

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
            GameManager.OnGameStarted += OnGameStart;
            GameManager.OnGameScoreIncreased += OnGameScoreIncreased;
            GameManager.OnRecordScoreIncreased += OnRecordScoreIncreased;
            GameManager.OnGameEnd += OnGameEnd;
           
        }
		private void OnDisable()
		{
            GameManager.OnGameStarted -= OnGameStart;
            GameManager.OnGameScoreIncreased -= OnGameScoreIncreased;
            GameManager.OnRecordScoreIncreased -= OnRecordScoreIncreased;
            GameManager.OnGameEnd -= OnGameEnd;
           
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
            scoreText.text = "Score: " + GameManager.Instance.gameScore.ToString();
        }

        private void UptadeRecordText()
		{
            recordText.text = "Record Score:  " + GameManager.Instance.recordScore.ToString();
		}
        private void OnGameScoreIncreased(int score)
        {
            UpdateScoreText();
        }

        private void OnRecordScoreIncreased(int score)
        {
            UptadeRecordText();
        }
        private void OnGameStart()
		{
            Debug.Log("OnGameStart cagirild");
            colorImage.gameObject.SetActive(true);
            bombImage.gameObject.SetActive(true);
        }
        public void OnCubeCollidedWithReset()
        {
            endPanel.SetActive(true);
           
        }
        private void OnGameEnd()
		{
            Debug.Log("OnGameEnd cagirild");
            OnCubeCollidedWithReset();
            colorImage.gameObject.SetActive(false);
            bombImage.gameObject.SetActive(false);
		}
        
       

    }
}

