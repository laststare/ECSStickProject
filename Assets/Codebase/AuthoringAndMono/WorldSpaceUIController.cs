using System;
using Codebase.Systems;
using DG.Tweening;
using TMPro;
using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

namespace Codebase.AuthoringAndMono
{
    public class WorldSpaceUIController : MonoBehaviour
    {
        [SerializeField] private GameObject rewardPref;
        
        private void OnEnable()
        {
            var playerMoveSystem = World.DefaultGameObjectInjectionWorld.GetExistingSystemManaged<PlayerMoveSystem>();
            playerMoveSystem.OnColumnIsReachable += ShowReward;
        }

        private void OnDisable()
        {
            if(World.DefaultGameObjectInjectionWorld == null)return;
            var playerMoveSystem = World.DefaultGameObjectInjectionWorld.GetExistingSystemManaged<PlayerMoveSystem>();
            playerMoveSystem.OnColumnIsReachable -= ShowReward;
            
        }

        private void ShowReward(int score, float3 position)
        {
            var reward = Instantiate(rewardPref, position, Quaternion.identity, transform);
            reward.transform.DOMoveY(reward.transform.position.y + 3, 2);
            var rewardText = reward.transform.GetChild(0).GetComponent<TMP_Text>();
            rewardText.text = $"+{score}";
            rewardText.DOFade(0, 2).OnComplete(() => {Destroy(reward.gameObject);});
        }
    }
}