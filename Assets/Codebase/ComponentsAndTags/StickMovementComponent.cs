using Unity.Entities;

namespace Codebase.ComponentsAndTags
{
    public struct StickMovementComponent : IComponentData
    {
        public float scaleСoefficient;
        public float growSpeed;
        public float rotationSpeed;
        public float stickLength;
        public float xPosition;
    }
}