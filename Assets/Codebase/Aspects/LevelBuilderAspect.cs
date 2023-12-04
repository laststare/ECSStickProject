using Codebase.ComponentsAndTags;
using Unity.Entities;

namespace Codebase.Aspects
{
    public readonly partial struct LevelBuilderAspect : IAspect
    {
        private readonly RefRO<LevelBuilderProperties> _levelBuilderProperties;
        private readonly RefRW<ColumnsState> _columnsState;

        public Entity ColumnPrefab => _levelBuilderProperties.ValueRO.columnPrefab;

        public bool StartColumnSpawned => _columnsState.ValueRW.startColumnSpawned;
        
        public void StartColumnReady() => _columnsState.ValueRW.startColumnSpawned = true;
    }
}