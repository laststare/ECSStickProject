using Codebase.Systems;
using TMPro;
using Unity.Entities;
using UnityEngine;

namespace Codebase.AuthoringAndMono
{
    public class ScoreCounter : MonoBehaviour
    {
        [SerializeField] private TMP_Text bestScoreText, actualScoreText;
        
        private void OnEnable()
        {
            var playerMoveSystem = World.DefaultGameObjectInjectionWorld.GetExistingSystemManaged<PlayerMoveSystem>();
            playerMoveSystem.OnGameOver += ShowScore;
        }

        private void ShowScore(string best, string actual)
        {
            bestScoreText.gameObject.SetActive(true);
            actualScoreText.gameObject.SetActive(true);
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