using System;
using Codebase.Systems;
using Unity.Entities;
using UnityEngine;
using UnityEngine.UI;

namespace Codebase.AuthoringAndMono
{
    public class MainMenuController : MonoBehaviour
    {
        
        [SerializeField] private Button startGameBtn, restartGameBtn, backStartScreenBtn;
        [SerializeField] private GameObject gameTitle;
        private void OnEnable()
        {
            var playerMoveSystem = World.DefaultGameObjectInjectionWorld.GetExistingSystemManaged<PlayerMoveSystem>();
            playerMoveSystem.OnGameOver += ShowFinishScreen;
        }

        private void ShowStartScreen()
        {
            startGameBtn.gameObject.SetActive(true);
            gameTitle.SetActive(true);
            HideFinishScreen();
        }

        private void ShowFinishScreen(string best, string actual)
        {
            restartGameBtn.gameObject.SetActive(true); 
            backStartScreenBtn.gameObject.SetActive(true);
        }

        private void HideStartScreen()
        {
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