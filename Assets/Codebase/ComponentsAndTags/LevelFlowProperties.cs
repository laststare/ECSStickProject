using Unity.Entities;

namespace Codebase.ComponentsAndTags
{
    public struct LevelFlowProperties : IComponentData
    {
        public LevelFlowState flowState;
        public bool stickIsSpawned;
        public float stickLength;
    }
}