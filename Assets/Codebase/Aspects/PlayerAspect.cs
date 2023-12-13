using Codebase.ComponentsAndTags;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

namespace Codebase.Aspects
{
    public readonly partial struct PlayerAspect : IAspect
    {
        private readonly RefRW<LocalTransform> _transform;
        private readonly RefRW<PlayerProperties> _playerProperties;
        public LocalTransform TransformRO => _transform.ValueRO;
        
        public void Walk(float deltaTime) => _transform.ValueRW.Position += _transform.ValueRO.Right() * _playerProperties.ValueRO.moveSpeed * deltaTime;

        public void MoveToStart() =>
            _transform.ValueRW.Position =
                new float3(_playerProperties.ValueRO.startXPosition, _transform.ValueRW.Position.y, _transform.ValueRW.Position.z);
    }
}