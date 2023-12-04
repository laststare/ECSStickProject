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
            //state.RequireForUpdate<LevelBuilderProperties>();
      
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
            ecb.Playback(state.EntityManager);
        }
    }

    [BurstCompile]
    public partial struct SpawnColumnJob : IJobEntity
    {
        public EntityCommandBuffer ECB;

        [BurstCompile]
        private void Execute(LevelBuilderAspect levelbuilder)
        {
            
            
            Debug.Log("update");
            var column1 = ECB.Instantiate(levelbuilder.ColumnPrefab);
            if (!levelbuilder.StartColumnSpawned)
            {
                var column = ECB.Instantiate(levelbuilder.ColumnPrefab);
                levelbuilder.StartColumnReady();
            }
        }
    }
}