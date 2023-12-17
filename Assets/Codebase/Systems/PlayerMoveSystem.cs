using System;
using Codebase.Aspects;
using Codebase.ComponentsAndTags;
using Unity.Burst;
using Unity.Burst.CompilerServices;
using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

namespace Codebase.Systems
{
    [BurstCompile]
    [UpdateInGroup(typeof(SimulationSystemGroup))]
    public partial class PlayerMoveSystem : SystemBase
    {
        private float _playerMoveDistance;
        private bool _getReward;
        private int _currentScore, _bestScore;
        public Action<int, float3> OnColumnIsReachable;
        public Action<string, string> OnGameOver;

        protected override void OnCreate()
        {
            base.OnCreate();
            _bestScore = PlayerPrefs.GetInt("saved_score");
        }


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
                if (_currentScore > _bestScore)
                {
                    _bestScore = _currentScore;
                    PlayerPrefs.SetInt("saved_score", _bestScore);
                }

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
                else
                    OnGameOver?.Invoke(_bestScore.ToString(), _currentScore.ToString());

                _getReward = false;
                _playerMoveDistance = 0;
            }

            
        }
    }
}