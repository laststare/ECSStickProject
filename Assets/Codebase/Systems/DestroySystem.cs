using Codebase.Aspects;
using Codebase.ComponentsAndTags;
using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using UnityEngine;

namespace Codebase.Systems
{
    [BurstCompile]
    public partial struct DestroySystem :  ISystem
    {
        private bool _destroyWave;
        private int entityCount;
        private Entity victim;
        private float lastPosition;

        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            var levelFlowEntity = SystemAPI.GetSingletonEntity<LevelFlowProperties>();
            var levelFlow = SystemAPI.GetAspect<LevelFlowAspect>(levelFlowEntity);
            
            if(levelFlow.GetState == LevelFlowState.PlayerRun)
                _destroyWave = true;

            if (_destroyWave && levelFlow.GetState == LevelFlowState.PlayerIdle)
            {

                DestroySticks(ref state, 1);
                DestroyColumns(ref state, 3);
                _destroyWave = false;
            }

            if (_destroyWave && levelFlow.GetState == LevelFlowState.GameOver)
            {
                DestroyAllSticks(ref state);
                DestroyAllColumns(ref state);
                _destroyWave = false;
            }
            
        }

        private void ClearVars()
        {
            entityCount = 0;
            victim = Entity.Null;
            lastPosition = Mathf.Infinity;
        }

        private void DestroySticks(ref SystemState state, int limit)
        {
            ClearVars();
            foreach (var stick in SystemAPI.Query<StickAspect>())
            {
                entityCount++;
                var position = stick.GetXPosition();
                if (!(position < lastPosition)) continue;
                lastPosition = position;
                victim = stick.Entity;
            }
            if(entityCount > limit)
                state.EntityManager.DestroyEntity(victim);
        }

        private void DestroyColumns(ref SystemState state, int limit)
        {
            ClearVars();
            foreach (var column in SystemAPI.Query<ColumnAspect>())
            {
                entityCount++;
                var position = column.GetXPosition();
                if (!(position < lastPosition)) continue;
                lastPosition = position;
                victim = column.Entity;
            }
            if(entityCount > limit)
                state.EntityManager.DestroyEntity(victim);
        }
        
        private void DestroyAllSticks(ref SystemState state)
        {
            var group = state.GetEntityQuery(ComponentType.ReadOnly<StickMovementComponent>());
            state.EntityManager.DestroyEntity(group);
        }
        
        private void DestroyAllColumns(ref SystemState state)
        {
            var group = state.GetEntityQuery(ComponentType.ReadOnly<ColumnProperties>());
            state.EntityManager.DestroyEntity(group);
        }


    }
}