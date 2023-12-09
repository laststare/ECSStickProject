using Unity.Entities;
using UnityEngine;

namespace Codebase.ComponentsAndTags
{
    public struct ColumnsState : IComponentData
    {
        public bool startColumnSpawned;
        public bool needNextColumn;
        public int minSpawnDistance;
        public int maxSpawnDistance;
        public float actualColumnXPosition;
        public float nextColumnXPosition;
        public float destinationOffset;
        public float columnOffset;
        public bool columnIsReachable;

    }
}