using Unity.Entities;

namespace Codebase.ComponentsAndTags
{
    public struct ColumnsState : IComponentData
    {
        public bool startColumnSpawned;
        public bool needNewColumn;
    }
}