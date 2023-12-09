using Codebase.ComponentsAndTags;
using Unity.Entities;
using Unity.Transforms;

namespace Codebase.Aspects
{
    public readonly partial struct PlayerAspect : IAspect
    {
        public readonly Entity Entity;
        
        private readonly RefRW<LocalTransform> _transform;
        private readonly RefRW<PlayerProperties> _playerProperties;
        public LocalTransform TransformRO => _transform.ValueRO;
        
        public void Walk(float deltaTime)
        {
            _transform.ValueRW.Position += _transform.ValueRO.Right() * _playerProperties.ValueRO.moveSpeed * deltaTime;
        }
    }
}