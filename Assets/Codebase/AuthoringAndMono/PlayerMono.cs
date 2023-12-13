using Codebase.ComponentsAndTags;
using Unity.Entities;
using UnityEngine;

namespace Codebase.AuthoringAndMono
{
    public class PlayerMono : MonoBehaviour
    {
        public float moveSpeed;
        public float startXPosition;
    }
    
    public class PlayerMonoBaker : Baker<PlayerMono>
    {
        public override void Bake(PlayerMono authoring)
        {
            var player = GetEntity(TransformUsageFlags.Dynamic);
            AddComponent(player, new PlayerProperties
            {
                moveSpeed = authoring.moveSpeed,
                startXPosition = authoring.startXPosition
            });
        }
    }
}