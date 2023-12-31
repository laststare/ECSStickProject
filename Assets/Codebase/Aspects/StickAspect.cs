﻿using Codebase.ComponentsAndTags;
using Unity.Entities;

namespace Codebase.Aspects
{
    public readonly partial struct StickAspect : IAspect
    {
        public readonly Entity Entity;

        public readonly RefRW<StickMovementComponent> _stickMovement;

        public float GetXPosition() => _stickMovement.ValueRO.xPosition;


    }
}