using UnityEngine;
using TMPro;
using UnityEngine.UI;
using DG.Tweening;
using ChainCube.Canvases;

namespace ChainCube.Managers
{
    public class UIManager : MonoBehaviour
    {
        public static UIManager Instance { get; private set; }

        public GameObject endPanel; // END CANVAS
        public TextMeshProUGUI scoreText; // GAME CANVAS
        public TextMeshProUGUI recordText; // GAME CANVAS
        public TextMeshProUGUI endScoreText; // END CANVAS
        public TextMeshProUGUI endRecordScore; // END CANVAS
        public Image colorImage; // DELETE
        public Image bombImage; // DELETE

        public EndCanvas endCanvas;
		
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
            GameManager.OnRecordScoreTexted += OnRecordScoreIncreased;
            GameManager.OnGameEnd += OnGameEnd;           
        }
		private void OnDisable()
		{
            GameManager.OnGameStarted -= OnGameStart;
            GameManager.OnGameScoreIncreased -= OnGameScoreIncreased;
            GameManager.OnRecordScoreTexted -= OnRecordScoreIncreased;
            GameManager.OnGameEnd -= OnGameEnd;          
        }
		
		void Start()
        {
            UpdateScoreText();
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

        private void UpdateEndPanelScore() // END CANVAS
		{
            endScoreText.text = "Score: " + GameManager.Instance.gameScore.ToString();
            endRecordScore.text = "Record Score: " + GameManager.RecordScore.ToString();
		}
        
        private void OnGameScoreIncreased(int score) // GAME CANVAS
        {
            UpdateScoreText();
        }

        private void OnRecordScoreIncreased(int score) // GAME CANVAS
        {
            UpdateRecordText();
        }

   
        private void OnGameStart()
		{
            Debug.Log("OnGameStart cagirildi");
           // colorImage.gameObject.SetActive(true);
           // bombImage.gameObject.SetActive(true);
            endPanel.SetActive(false);
            UpdateRecordText();
        }

        public void OnCubeCollidedWithReset()
        {
            endPanel.SetActive(true);

            UpdateEndPanelScore();
        }

        private void OnGameEnd()
		{
            Debug.Log("OnGameEnd cagirildi");
            OnCubeCollidedWithReset();
            colorImage.gameObject.SetActive(false);
            bombImage.gameObject.SetActive(false);
            endCanvas.UiEndTween();
		}
    }
}

