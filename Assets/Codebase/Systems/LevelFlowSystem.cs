using Codebase.ComponentsAndTags;
using Unity.Entities;
using UnityEngine;

namespace Codebase.Systems
{
    public partial class LevelFlowSystem : SystemBase
    {
        protected override void OnUpdate()
        {
            var isClicked = Input.GetMouseButton(0);
            var levelFlowEntity = SystemAPI.GetSingletonEntity<LevelFlowProperties>();
            var levelFlow = SystemAPI.GetAspect<LevelFlowAspect>(levelFlowEntity);
            
            levelFlow._levelFlowProperties.ValueRW.flowState = levelFlow._levelFlowProperties.ValueRO.flowState switch
            {
                LevelFlowState.PlayerIdle when isClicked => LevelFlowState.StickGrowsUp,
                LevelFlowState.StickGrowsUp when !isClicked => LevelFlowState.StickFalls,
                LevelFlowState.GameOver => LevelFlowState.Start,
                LevelFlowState.Start => LevelFlowState.PlayerIdle,
                _ => levelFlow._levelFlowProperties.ValueRW.flowState
            };
            
            
        }
        
    }
}