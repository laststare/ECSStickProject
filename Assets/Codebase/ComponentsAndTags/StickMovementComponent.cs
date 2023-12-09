using Unity.Entities;

namespace Codebase.ComponentsAndTags
{
    public struct StickMovementComponent : IComponentData
    {
        public float growSpeed;
        public float rotationSpeed;
    }
}