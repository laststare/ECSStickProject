using Unity.Entities;

namespace Codebase.ComponentsAndTags
{
    public struct LevelBuilderProperties : IComponentData
    {
        public Entity columnPrefab;
        public Entity stickPrefab;
        public float playerYPosition;
    }
}