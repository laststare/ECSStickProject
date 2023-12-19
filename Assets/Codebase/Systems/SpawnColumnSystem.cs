using Codebase.Aspects;
using Codebase.ComponentsAndTags;
using Unity.Burst;
using Unity.Collections;
using Unity.Entities;

namespace Codebase.Systems
{
    [BurstCompile]
    [UpdateInGroup(typeof(SimulationSystemGroup))]
    public partial struct SpawnColumnSystem : ISystem
    {
        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            var levelBuilderEntity = SystemAPI.GetSingletonEntity<LevelBuilderProperties>();
            var levelBuilder = SystemAPI.GetAspect<LevelBuilderAspect>(levelBuilderEntity);
            
            var levelFlowEntity = SystemAPI.GetSingletonEntity<LevelFlowProperties>();
            var levelFlow = SystemAPI.GetAspect<LevelFlowAspect>(levelFlowEntity);
            
            var ecb = new EntityCommandBuffer(Allocator.Temp);

            if (levelFlow.GetState == LevelFlowState.Restart) 
                levelBuilder.Restart();

            if (!levelBuilder.StartColumnSpawned)
            {
                var startColumn = ecb.Instantiate(levelBuilder.ColumnPrefab);
                ecb.AddComponent(startColumn, new ColumnProperties
                {
                    xPosition = 0
                });
                levelBuilder.StartColumnReady();
            }

            if (levelBuilder.NeedNextColumn)
            {
                var nextColumn = ecb.Instantiate(levelBuilder.ColumnPrefab);
                var nextColumnTransform = levelBuilder.GetNextColumnPosition();
                ecb.SetComponent(nextColumn, nextColumnTransform);
                ecb.AddComponent(nextColumn, new ColumnProperties
                {
                    xPosition = nextColumnTransform.Position.x
                });
                levelBuilder.NextColumnReady();
            }

            ecb.Playback(state.EntityManager);
        }
    }
    
}