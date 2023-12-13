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

        public void Restart()
        {
            _columnsState.ValueRW.actualColumnXPosition = 0;
            _columnsState.ValueRW.startColumnSpawned = false;
            _columnsState.ValueRW.needNextColumn = true;
        }

        public bool NeedNextColumn => _columnsState.ValueRW.needNextColumn;
        
        public bool ColumnIsReachable => _columnsState.ValueRO.columnIsReachable;
        
        public void StartColumnReady() => _columnsState.ValueRW.startColumnSpawned = true;
        
        public void NextColumnReady() => _columnsState.ValueRW.needNextColumn = false;

        public LocalTransform GetNextColumnPosition()
        {
            var offset = GetActualColumnXPosition +
                         Random.Range(_columnsState.ValueRO.minSpawnDistance,
                             _columnsState.ValueRO.maxSpawnDistance + 1);
            
            _columnsState.ValueRW.nextColumnXPosition = offset;
            return new LocalTransform
            {
                Position = new Vector3(offset, -1),
                Rotation = Quaternion.identity,
                Scale = 1f
            };
        }
        
        public LocalTransform GetStickSpawnPosition()
        {
            var xOffset = _columnsState.ValueRO.actualColumnXPosition + 1;
            var yOffset = _levelBuilderProperties.ValueRO.playerYPosition - 1.5f;
            
            return new LocalTransform
            {
                Position = new Vector3(xOffset, yOffset),
                Rotation = Quaternion.identity,
                Scale = 0.25f
            };
        }

        public float GetPlayerMoveDistance(float stickLength)
        {
            var moveDistance =  GetActualColumnXPosition + _columnsState.ValueRO.destinationOffset +
                                stickLength;

            var offset = _columnsState.ValueRO.columnOffset;
            var reachable = moveDistance >= GetNextColumnXPosition - offset &&
                            moveDistance <= GetNextColumnXPosition + offset;
            _columnsState.ValueRW.columnIsReachable = reachable;
            return reachable ? GetNextColumnXPosition : moveDistance;
        }
        
        public void UpdateActualColumnPosition()
        {
            _columnsState.ValueRW.actualColumnXPosition = GetNextColumnXPosition;
            _columnsState.ValueRW.needNextColumn = true;
        }
        
        public float GetActualColumnXPosition => _columnsState.ValueRO.actualColumnXPosition;

        private float GetNextColumnXPosition => _columnsState.ValueRO.nextColumnXPosition;

    }
}