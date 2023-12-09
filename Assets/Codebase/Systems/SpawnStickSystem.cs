using Codebase.Aspects;
using Codebase.ComponentsAndTags;
using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

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
                ecb.AddComponent(stick, new StickMovementComponent
                {
                    growSpeed = 10,
                    rotationSpeed = 90
                });
                ecb.AddComponent(stick, new PostTransformMatrix
                {
                    Value = float4x4.Scale(1, 0.25f, 1f)
                });
                levelFlow._levelFlowProperties.ValueRW.stickIsSpawned = true;
            }
            ecb.Playback(state.EntityManager);
        }
    }
}