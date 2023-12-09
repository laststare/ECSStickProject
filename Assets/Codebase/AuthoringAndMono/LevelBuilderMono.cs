using Codebase.ComponentsAndTags;
using Unity.Entities;
using UnityEngine;

namespace Codebase.AuthoringAndMono
{
    public class LevelBuilderMono : MonoBehaviour
    {
        public GameObject columnPrefab;
        public int minSpawnDistance;
        public int maxSpawnDistance;
        public GameObject stickPrefab;
        public float playerYPosition;
        public float destinationOffset;
        public float columnOffset;
    }

    public class LevelBuilderBaker : Baker<LevelBuilderMono>
    {
        public override void Bake(LevelBuilderMono authoring)
        {
            var levelBuilderEntity = GetEntity(TransformUsageFlags.Dynamic);
            
            AddComponent(levelBuilderEntity, new LevelBuilderProperties
            {
                columnPrefab = GetEntity(authoring.columnPrefab, TransformUsageFlags.Dynamic), //TODO проверить реально ли нужен dynamic
                stickPrefab = GetEntity(authoring.stickPrefab, TransformUsageFlags.Dynamic),
                playerYPosition = authoring.playerYPosition

            });
            AddComponent(levelBuilderEntity, new ColumnsState
            {
                needNextColumn = true,
                minSpawnDistance = authoring.minSpawnDistance,
                maxSpawnDistance = authoring.maxSpawnDistance,
                destinationOffset = authoring.destinationOffset,
                columnOffset = authoring.columnOffset
            });
        }
    }
}