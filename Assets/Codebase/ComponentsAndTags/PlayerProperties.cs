using Unity.Entities;

namespace Codebase.ComponentsAndTags
{
    public struct PlayerProperties : IComponentData
    {
        public float moveSpeed;
        public float startXPosition;
    }
}