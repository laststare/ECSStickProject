using Unity.Entities;

namespace Codebase.ComponentsAndTags
{
    public readonly partial struct LevelFlowAspect : IAspect
    {
        private readonly RefRW<LevelFlowProperties> _levelFlowProperties;
        public void SetState(LevelFlowState state) => _levelFlowProperties.ValueRW.flowState = state;
        public LevelFlowState GetState => _levelFlowProperties.ValueRO.flowState;

        public bool IsStickSpawned => _levelFlowProperties.ValueRO.stickIsSpawned;

        public void SetStickSpawned(bool state) => _levelFlowProperties.ValueRW.stickIsSpawned = state;
        
        public void SetStickLength(float length) => _levelFlowProperties.ValueRW.stickLength = length;
        public float GetStickLength => _levelFlowProperties.ValueRO.stickLength;
    }
}