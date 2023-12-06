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
    public partial struct  StickGrowSystem :  ISystem
    {
        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            var ecb = new EntityCommandBuffer(Allocator.Temp);
            var levelFlowEntity = SystemAPI.GetSingletonEntity<LevelFlowProperties>();
            var levelFlow = SystemAPI.GetAspect<LevelFlowAspect>(levelFlowEntity);

            if (levelFlow._levelFlowProperties.ValueRO.flowState == LevelFlowState.StickGrowsUp){
                // foreach (var stick in SystemAPI.Query<StickAspect>().WithAll<NewStickTag>())
                // {
                //     ecb.SetComponent(stick.Entity, new PostTransformMatrix
                //     {
                //         Value = float4x4.Scale(0.25f, 0f, 0.25f)
                //     });
                // }
            }

            ecb.Playback(state.EntityManager);
        }
    }
}