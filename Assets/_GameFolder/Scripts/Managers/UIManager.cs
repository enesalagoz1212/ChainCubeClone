using UnityEngine;
using TMPro;
using UnityEngine.UI;

namespace ChainCube.Managers
{
    public class UIManager : MonoBehaviour
    {
        public static UIManager Instance { get; private set; }

        public GameObject endPanel;
        public TextMeshProUGUI scoreText;
        public TextMeshProUGUI recordText;
        public TextMeshProUGUI endScoreText;
        public TextMeshProUGUI endRecordScore;
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
            UptadeRecordText();
            
        }

        public void UpdateScoreText()
        {
            scoreText.text = " " + GameManager.Instance.gameScore.ToString();
        }

        public void UptadeRecordText()
		{
            recordText.text = "Record:  " + GameManager.Instance.recordScore.ToString();
		}

        private void UpdateEndPanelScore()
		{
            endScoreText.text = "Score: " + GameManager.Instance.gameScore.ToString();
            endRecordScore.text = "Record Score: " + GameManager.Instance.recordScore.ToString();
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
            Debug.Log("OnGameStart cagirildi");
            colorImage.gameObject.SetActive(true);
            bombImage.gameObject.SetActive(true);
            endPanel.SetActive(false);
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
		}
    }
}

