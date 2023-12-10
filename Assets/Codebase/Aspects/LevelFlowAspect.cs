using Unity.Entities;

namespace Codebase.ComponentsAndTags
{
    public readonly partial struct LevelFlowAspect : IAspect
    {
        public readonly RefRW<LevelFlowProperties> _levelFlowProperties;

        public void SetStickLength(float length) => _levelFlowProperties.ValueRW.stickLength = length;
        public float GetStickLength => _levelFlowProperties.ValueRO.stickLength;
    }
}