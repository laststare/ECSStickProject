using Unity.Entities;

namespace Codebase.ComponentsAndTags
{
    public readonly partial struct LevelFlowAspect : IAspect
    {
        public readonly RefRW<LevelFlowProperties> _levelFlowProperties;
    }
}