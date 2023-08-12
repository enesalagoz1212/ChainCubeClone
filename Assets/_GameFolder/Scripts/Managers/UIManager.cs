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

        public GameCanvas gameCanvas;
        public SettingsCanvas settingsCanvas;
        public EndCanvas endCanvas;
        public BoosterManager boosterManager;
        public InputCanvas inputCanvas;
		
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
 
        public void Initialize(InputManager inputManager)
        {
            inputCanvas.Initialize(inputManager);
			gameCanvas.Initialize(settingsCanvas ,boosterManager);
            settingsCanvas.Initialize(inputManager, gameCanvas, inputCanvas);
            endCanvas.Initialize(gameCanvas);
        }
    }
}

