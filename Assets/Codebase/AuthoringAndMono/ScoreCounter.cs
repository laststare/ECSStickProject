using System;
using Codebase.Systems;
using TMPro;
using Unity.Entities;
using UnityEngine;

namespace Codebase.AuthoringAndMono
{
    public class ScoreCounter : MonoBehaviour
    {
        [SerializeField] private TMP_Text bestScoreText, actualScoreText;
        
        private void Start()
        {
            var playerMoveSystem = World.DefaultGameObjectInjectionWorld.GetExistingSystemManaged<PlayerMoveSystem>();
            var levelFlowSystem = World.DefaultGameObjectInjectionWorld.GetExistingSystemManaged<LevelFlowSystem>();
            playerMoveSystem.OnScoreAdded += ShowScore;
            levelFlowSystem.OnStateUpdate += GetFlowState;
            ShowScore(playerMoveSystem.BestScore.ToString(), String.Empty);
            ShowStart();
        }

        private void GetFlowState(LevelFlowState state)
        {
            switch (state)
            {
                case LevelFlowState.StartMenu:
                    HideAll();
                    ShowStart();
                    break;
                case LevelFlowState.FinishMenu:
                    ShowFinish();
                    break;
                case LevelFlowState.PlayerIdle:
                    HideAll();
                    break;
            }
        }

        private void ShowScore(string best, string actual)
        {
            bestScoreText.text = "Best score: " + best;
            actualScoreText.text = "Your score: " + actual;
        }

        private void ShowStart()
        {
            bestScoreText.gameObject.SetActive(true);
        }

        private void ShowFinish()
        {
            ShowStart();
            actualScoreText.gameObject.SetActive(true);
        }

        private void HideAll()
        {
            bestScoreText.gameObject.SetActive(false);
            actualScoreText.gameObject.SetActive(false);
        }
    }
}