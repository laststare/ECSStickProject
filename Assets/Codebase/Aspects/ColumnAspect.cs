using Codebase.ComponentsAndTags;
using Unity.Entities;

namespace Codebase.Aspects
{
    public readonly partial struct ColumnAspect : IAspect
    {
        public readonly Entity Entity;

        private readonly RefRW<ColumnProperties> _columnProperties;
        
        public float GetXPosition() => _columnProperties.ValueRO.xPosition;
    }
}