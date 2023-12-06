using Codebase.Aspects;
using Codebase.ComponentsAndTags;
using Unity.Burst;
using Unity.Collections;
using Unity.Entities;

namespace Codebase.Systems
{
    [BurstCompile]
    [UpdateInGroup(typeof(SimulationSystemGroup))]
    public partial struct SpawnStickSystem : ISystem
    {
        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            var levelBuilderEntity = SystemAPI.GetSingletonEntity<LevelBuilderProperties>();
            var levelBuilder = SystemAPI.GetAspect<LevelBuilderAspect>(levelBuilderEntity);
            var levelFlowEntity = SystemAPI.GetSingletonEntity<LevelFlowProperties>();
            var levelFlow = SystemAPI.GetAspect<LevelFlowAspect>(levelFlowEntity);
            
            var ecb = new EntityCommandBuffer(Allocator.Temp);
            
            if (levelFlow._levelFlowProperties.ValueRO.flowState == LevelFlowState.StickGrowsUp && !levelFlow._levelFlowProperties.ValueRO.stickIsSpawned)
            {
                var stick = ecb.Instantiate(levelBuilder.StickPrefab);
                var stickTransform = levelBuilder.GetStickSpawnPosition();
                ecb.SetComponent(stick, stickTransform);
              //  ecb.SetComponent(stick, new StickGrow());
               // ecb.SetComponent(stick, new NewStickTag());
                levelFlow._levelFlowProperties.ValueRW.stickIsSpawned = true;
            }
            ecb.Playback(state.EntityManager);
        }
    }
}