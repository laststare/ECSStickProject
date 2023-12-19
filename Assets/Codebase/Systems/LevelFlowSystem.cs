using System;
using Codebase.ComponentsAndTags;
using Unity.Entities;
using UnityEngine;

namespace Codebase.Systems
{
    public partial class LevelFlowSystem : SystemBase
    {
        
        private LevelFlowState _currentState = LevelFlowState.None;
        public Action<LevelFlowState> OnStateUpdate;

        public void SetState(LevelFlowState state)
        {
            var levelFlowEntity = SystemAPI.GetSingletonEntity<LevelFlowProperties>();
            var levelFlow = SystemAPI.GetAspect<LevelFlowAspect>(levelFlowEntity);
            levelFlow.SetState(state);
        }
        
        protected override void OnUpdate()
        {
            var isClicked = Input.GetMouseButton(0);
            var levelFlowEntity = SystemAPI.GetSingletonEntity<LevelFlowProperties>();
            var levelFlow = SystemAPI.GetAspect<LevelFlowAspect>(levelFlowEntity);


            var state = levelFlow.GetState switch
            {
                LevelFlowState.PlayerIdle when isClicked => LevelFlowState.StickGrowsUp,
                LevelFlowState.StickGrowsUp when !isClicked => LevelFlowState.StickFalls,
                LevelFlowState.GameOver => LevelFlowState.Restart,
                LevelFlowState.Restart => LevelFlowState.FinishMenu,
                _ => levelFlow.GetState
            };

            if (_currentState != state)
            {
                OnStateUpdate?.Invoke(state);
                _currentState = state;
            }

            levelFlow.SetState(_currentState); 
            
        }
    }
}