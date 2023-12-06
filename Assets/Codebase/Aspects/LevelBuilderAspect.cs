using Codebase.ComponentsAndTags;
using Unity.Entities;
using Unity.Transforms;
using UnityEngine;

namespace Codebase.Aspects
{
    public readonly partial struct LevelBuilderAspect : IAspect
    {
        private readonly RefRO<LevelBuilderProperties> _levelBuilderProperties;
        private readonly RefRW<ColumnsState> _columnsState;

        public Entity ColumnPrefab => _levelBuilderProperties.ValueRO.columnPrefab;
        public Entity StickPrefab => _levelBuilderProperties.ValueRO.stickPrefab;

        public bool StartColumnSpawned => _columnsState.ValueRW.startColumnSpawned;

        public bool NeedNextColumn => _columnsState.ValueRW.needNextColumn;
        
        public void StartColumnReady() => _columnsState.ValueRW.startColumnSpawned = true;
        
        public void NextColumnReady() => _columnsState.ValueRW.needNextColumn = false;

        public LocalTransform GetNextColumnPosition()
        {
            var offset = _columnsState.ValueRO.actualColumnPositionOffset +
                         Random.Range(_columnsState.ValueRO.minSpawnDistance,
                             _columnsState.ValueRO.maxSpawnDistance + 1);
            
           // _columnsState.ValueRW.actualColumnPositionOffset = offset;
            return new LocalTransform
            {
                Position = new Vector3(offset, -1),
                Rotation = Quaternion.identity,
                Scale = 1f
            };
        }
        
        public LocalTransform GetStickSpawnPosition()
        {
            var xOffset = _columnsState.ValueRO.actualColumnPositionOffset + 1;
            var yOffset = _levelBuilderProperties.ValueRO.playerYPosition -0.5f;

            new LocalToWorld
            {
                
            };
            
            return new LocalTransform
            {
                Position = new Vector3(xOffset, yOffset),
                Rotation = Quaternion.identity,
                Scale = 0.25f
            };
        }
        
    }
}