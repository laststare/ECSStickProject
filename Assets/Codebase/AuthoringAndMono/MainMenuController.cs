using Codebase.Systems;
using Unity.Entities;
using UnityEngine;
using UnityEngine.UI;

namespace Codebase.AuthoringAndMono
{
    public class MainMenuController : MonoBehaviour
    {
        
        [SerializeField] private Button startGameBtn, restartGameBtn, backStartScreenBtn;
        [SerializeField] private GameObject gameTitle, backImage;
    
        private void Start()
        {
            var levelFlowSystem = World.DefaultGameObjectInjectionWorld.GetExistingSystemManaged<LevelFlowSystem>();
            levelFlowSystem.OnStateUpdate += GetFlowState;
            
            startGameBtn.onClick.AddListener(() =>
            {
                HideStartScreen();
                levelFlowSystem.SetState(LevelFlowState.PlayerIdle);
            });
            
            backStartScreenBtn.onClick.AddListener(() =>
            {
                ShowStartScreen();
                levelFlowSystem.SetState(LevelFlowState.StartMenu);
            });
            
            restartGameBtn.onClick.AddListener(() =>
            {
                HideFinishScreen();
                backImage.SetActive(false);
                levelFlowSystem.SetState(LevelFlowState.PlayerIdle);
            });
        }

        private void GetFlowState(LevelFlowState state)
        {
            switch (state)
            {
                case LevelFlowState.FinishMenu:
                    ShowFinishScreen();
                    break;
            }
        }

        private void ShowStartScreen()
        {
            backImage.SetActive(true);
            startGameBtn.gameObject.SetActive(true);
            gameTitle.SetActive(true);
            HideFinishScreen();
        }

        private void ShowFinishScreen()
        {
            backImage.SetActive(true);
            restartGameBtn.gameObject.SetActive(true); 
            backStartScreenBtn.gameObject.SetActive(true);
        }

        private void HideStartScreen()
        {
            backImage.SetActive(false);
            gameTitle.SetActive(false);
            startGameBtn.gameObject.SetActive(false);
        }

        private void HideFinishScreen()
        {
            restartGameBtn.gameObject.SetActive(false); 
            backStartScreenBtn.gameObject.SetActive(false);
        }
        
    }
}