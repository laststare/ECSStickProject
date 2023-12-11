using System;
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

        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            var levelFlowEntity = SystemAPI.GetSingletonEntity<LevelFlowProperties>();
            var levelFlow = SystemAPI.GetAspect<LevelFlowAspect>(levelFlowEntity);
            
            if(levelFlow._levelFlowProperties.ValueRO.flowState == LevelFlowState.PlayerRun)
                _destroyWave = true;

            if (_destroyWave && levelFlow._levelFlowProperties.ValueRO.flowState == LevelFlowState.PlayerIdle)
            {

                var entityCount = 0;
                var victim = new Entity();
                var lastPosition = Mathf.Infinity;
                
                foreach (var stick in SystemAPI.Query<StickAspect>())
                {
                    entityCount++;
                    var position = stick.GetXPosition();
                    if (!(position < lastPosition)) continue;
                    lastPosition = position;
                    victim = stick.Entity;
                }
                if(entityCount > 1)
                    state.EntityManager.DestroyEntity(victim);
                
                entityCount = 0;
                victim = new Entity();
                lastPosition = Mathf.Infinity;

                foreach (var column in SystemAPI.Query<ColumnAspect>())
                {
                    entityCount++;
                    var position = column.GetXPosition();
                    if (!(position < lastPosition)) continue;
                    lastPosition = position;
                    victim = column.Entity;
                }
                if(entityCount > 3)
                    state.EntityManager.DestroyEntity(victim);
                
                _destroyWave = false;
            }
        }

       
    }
}