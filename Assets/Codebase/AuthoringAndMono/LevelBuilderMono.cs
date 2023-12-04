using Codebase.ComponentsAndTags;
using Unity.Entities;
using UnityEditor.SceneManagement;
using UnityEngine;

namespace Codebase.AuthoringAndMono
{
    public class LevelBuilderMono : MonoBehaviour
    {
        public GameObject columnPrefab;
    }

    public class LevelBuilderBaker : Baker<LevelBuilderMono>
    {
        public override void Bake(LevelBuilderMono authoring)
        {
            var levelBuilderEntity = GetEntity(TransformUsageFlags.Dynamic);
            
            AddComponent(levelBuilderEntity, new LevelBuilderProperties
            {
                columnPrefab = GetEntity(authoring.columnPrefab, TransformUsageFlags.Dynamic) //TODO проверить реально ли нужен dynamic
            });
            AddComponent(levelBuilderEntity, new ColumnsState());
        }
    }
}