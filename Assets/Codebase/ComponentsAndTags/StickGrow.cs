using Unity.Entities;

namespace Codebase.ComponentsAndTags
{
    public struct StickGrow : IComponentData
    {
        public float growSpeed;
        public float yScale;
    }
}