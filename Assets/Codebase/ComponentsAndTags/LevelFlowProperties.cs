using Unity.Entities;

namespace Codebase.ComponentsAndTags
{
    public struct LevelFlowProperties : IComponentData
    {
        public LevelFlowState flowState;
    }
}