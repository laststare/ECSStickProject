using Codebase.Aspects;
using Codebase.ComponentsAndTags;
using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using UnityEngine;

namespace Codebase.Systems
{
    [BurstCompile]
    [UpdateInGroup(typeof(SimulationSystemGroup))]
    public partial struct SpawnColumnSystem : ISystem
    {

        [BurstCompile]
        public void OnCreate(ref SystemState state)
        {
      
        }
        
        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            var levelBuilderEntity = SystemAPI.GetSingletonEntity<LevelBuilderProperties>();
            var levelBuilder = SystemAPI.GetAspect<LevelBuilderAspect>(levelBuilderEntity);
            
            var ecb = new EntityCommandBuffer(Allocator.Temp);
            
            if (!levelBuilder.StartColumnSpawned)
            {
                ecb.Instantiate(levelBuilder.ColumnPrefab);
                levelBuilder.StartColumnReady();
            }

            if (levelBuilder.NeedNextColumn)
            {
                var nextColumn = ecb.Instantiate(levelBuilder.ColumnPrefab);
                var nextColumnTransform = levelBuilder.GetNextColumnPosition();
                ecb.SetComponent(nextColumn, nextColumnTransform);
                levelBuilder.NextColumnReady();
            }

            ecb.Playback(state.EntityManager);
        }
    }
    
}