using System;
using Codebase.Aspects;
using Codebase.ComponentsAndTags;
using Unity.Burst;
using Unity.Entities;
using Unity.Mathematics;

namespace Codebase.Systems
{
    [BurstCompile]
    [UpdateInGroup(typeof(SimulationSystemGroup))]
    public partial class PlayerMoveSystem : SystemBase
    {
        private float _playerMoveDistance;
        private bool _getReward;
        private int _currentScore;
        public Action<int, float3> OnColumnIsReachable;


        [BurstCompile]
        protected override void OnUpdate()
        {
            var levelFlowEntity = SystemAPI.GetSingletonEntity<LevelFlowProperties>();
            var levelFlow = SystemAPI.GetAspect<LevelFlowAspect>(levelFlowEntity);
            
            var playerEntity = SystemAPI.GetSingletonEntity<PlayerProperties>();
            var player = SystemAPI.GetAspect<PlayerAspect>(playerEntity);
            
            if (levelFlow.GetState == LevelFlowState.Start)
            {
                player.MoveToStart();
                levelFlow.SetStickSpawned(false);
            }

            if (levelFlow.GetState != LevelFlowState.PlayerRun) return;
            
            var levelBuilderEntity = SystemAPI.GetSingletonEntity<LevelBuilderProperties>();
            var levelBuilder = SystemAPI.GetAspect<LevelBuilderAspect>(levelBuilderEntity);


            if (_playerMoveDistance == 0)
                _playerMoveDistance = levelBuilder.GetPlayerMoveDistance(levelFlow.GetStickLength);
            
            if (levelBuilder.ColumnIsReachable && !_getReward)
            {
                _currentScore += player.GetColumnReward;
                OnColumnIsReachable?.Invoke(player.GetColumnReward,
                    new float3(levelBuilder.GetNextColumnXPosition, levelBuilder.GetPlayerYPosition, 0));
                _getReward = true;
            }

            if( player.TransformRO.Position.x < _playerMoveDistance)
                player.Walk(SystemAPI.Time.DeltaTime);
            else
            {
                levelFlow.SetState(levelBuilder.ColumnIsReachable ? LevelFlowState.CameraRun : LevelFlowState.GameOver);
                if (levelBuilder.ColumnIsReachable)
                {
                    levelBuilder.UpdateActualColumnPosition();
                    levelFlow.SetStickSpawned(false);
                }
                _getReward = false;
                _playerMoveDistance = 0;
            }

            
        }
    }
}