using Codebase.ComponentsAndTags;
using Unity.Entities;

namespace Codebase.Aspects
{
    public readonly partial struct StickAspect : IAspect
    {
        public readonly Entity Entity;

        public readonly RefRW<StickMovementComponent> _stickMovement;
        
        public void SetStickLength(float length) => _stickMovement.ValueRW.stickLength = length;
        public float GetStickLength() => _stickMovement.ValueRO.stickLength;

    }
}