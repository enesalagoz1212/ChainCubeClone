using UnityEngine;
using ChainCube.Canvases;

namespace ChainCube.Managers
{
    public class UIManager : MonoBehaviour
    {
        public static UIManager Instance { get; private set; }
        public GameCanvas GameCanvas => gameCanvas;

        [SerializeField] private GameCanvas gameCanvas;
        [SerializeField] private SettingsCanvas settingsCanvas;
        [SerializeField] private EndCanvas endCanvas;
        [SerializeField] private BoosterManager boosterManager;
        [SerializeField] private InputCanvas inputCanvas;
		
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
 
        public void Initialize(LevelManager levelManager, InputManager inputManager)
        {
            inputCanvas.Initialize(inputManager);
			gameCanvas.Initialize(levelManager, boosterManager, settingsCanvas);
            settingsCanvas.Initialize(gameCanvas, inputCanvas);
            endCanvas.Initialize(gameCanvas);
        }
    }
}

