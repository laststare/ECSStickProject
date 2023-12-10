using Codebase.Aspects;
using Codebase.ComponentsAndTags;
using Unity.Burst;
using Unity.Entities;
using UnityEngine;

namespace Codebase.Systems
{
    [BurstCompile]
    public partial class PlayerMoveSystem : SystemBase
    {
        private float _playerMoveDistance;
        
        [BurstCompile]
        protected override void OnUpdate()
        {
            var levelFlowEntity = SystemAPI.GetSingletonEntity<LevelFlowProperties>();
            var levelFlow = SystemAPI.GetAspect<LevelFlowAspect>(levelFlowEntity);

            if (levelFlow._levelFlowProperties.ValueRO.flowState != LevelFlowState.PlayerRun) return;
            
            var levelBuilderEntity = SystemAPI.GetSingletonEntity<LevelBuilderProperties>();
            var levelBuilder = SystemAPI.GetAspect<LevelBuilderAspect>(levelBuilderEntity);

            var playerEntity = SystemAPI.GetSingletonEntity<PlayerProperties>();
            var player = SystemAPI.GetAspect<PlayerAspect>(playerEntity);
                
            if (_playerMoveDistance == 0)
                _playerMoveDistance = levelBuilder.GetPlayerMoveDistance(levelFlow.GetStickLength);

            if( player.TransformRO.Position.x < _playerMoveDistance)
                player.Walk(SystemAPI.Time.DeltaTime);
            else
            {
                levelBuilder.UpdateActualColumnPosition();
                levelFlow._levelFlowProperties.ValueRW.stickIsSpawned = false;
                levelFlow._levelFlowProperties.ValueRW.flowState = levelBuilder.ColumnIsReachable ? LevelFlowState.CameraRun : LevelFlowState.GameOver;
                _playerMoveDistance = 0;
            }
        }
    }
}