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
            var ecb = new EntityCommandBuffer(Allocator.Temp);
            var levelFlowEntity = SystemAPI.GetSingletonEntity<LevelFlowProperties>();
            var levelFlow = SystemAPI.GetAspect<LevelFlowAspect>(levelFlowEntity);
            
            if(levelFlow._levelFlowProperties.ValueRO.flowState == LevelFlowState.PlayerRun)
                _destroyWave = true;

            if (_destroyWave && levelFlow._levelFlowProperties.ValueRO.flowState == LevelFlowState.PlayerIdle)
            {

                var stickCount = 0;
                var victim = new Entity();
                var lastPosition = Mathf.Infinity;
                foreach (var stick in SystemAPI.Query<StickAspect>().WithAll<OldStickTag>())
                {
                    stickCount++;
                    var stickPosition = stick.GetXPosition();
                    if (!(stickPosition < lastPosition)) continue;
                    lastPosition = stickPosition;
                    victim = stick.Entity;
                }
                if(stickCount > 1)
                    state.EntityManager.DestroyEntity(victim);
                
                _destroyWave = false;
            }
        }

       
    }
}