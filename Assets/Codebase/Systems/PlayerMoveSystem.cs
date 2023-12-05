using Codebase.ComponentsAndTags;
using Unity.Burst;
using Unity.Entities;
using UnityEngine;

namespace Codebase.Systems
{
    [BurstCompile]
    [UpdateInGroup(typeof(SimulationSystemGroup))]
    public partial struct PlayerMoveSystem : ISystem
    {
        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
           
            
            //Debug.Log(levelFlow._levelFlowProperties.ValueRO.flowState);
        }
    }
}