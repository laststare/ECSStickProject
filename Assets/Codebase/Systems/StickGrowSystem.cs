using System;
using Codebase.Aspects;
using Codebase.ComponentsAndTags;
using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

namespace Codebase.Systems
{
    [BurstCompile]
    public partial struct  StickGrowSystem :  ISystem
    {
        private float _stickLength;
        private float _zAxisRotation;

        
        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            var ecb = new EntityCommandBuffer(Allocator.Temp);
            var levelFlowEntity = SystemAPI.GetSingletonEntity<LevelFlowProperties>();
            var levelFlow = SystemAPI.GetAspect<LevelFlowAspect>(levelFlowEntity);
            var deltaTime = SystemAPI.Time.DeltaTime;

            switch (levelFlow.GetState)
            {
                case LevelFlowState.StickGrowsUp:
                {
                    foreach (var stick in SystemAPI.Query<StickAspect>().WithAll<NewStickTag>())
                    {
                        _stickLength += deltaTime * stick._stickMovement.ValueRO.growSpeed;
                        var matrix = float4x4.TRS(new float3(0,0,0), Quaternion.Euler(0,0,_zAxisRotation), new float3(1,_stickLength,1));
                        ecb.SetComponent(stick.Entity, new PostTransformMatrix
                        {
                            Value = matrix
                        });
                    }
                    break;
                }
                case LevelFlowState.StickFalls:
                {
                    foreach (var stick in SystemAPI.Query<StickAspect>().WithAll<NewStickTag>())
                    {
                        _zAxisRotation -= deltaTime * stick._stickMovement.ValueRO.rotationSpeed;
                        var matrix = float4x4.TRS(new float3(0,0,0), Quaternion.Euler(0,0,_zAxisRotation), new float3(1,_stickLength,1));
                        ecb.SetComponent(stick.Entity, new PostTransformMatrix
                        {
                            Value = matrix
                        });
                    }

                    if (_zAxisRotation <= -90)
                    {
                        levelFlow.SetStickLength(_stickLength * 0.25f);
                        levelFlow.SetState(LevelFlowState.PlayerRun);
                        _stickLength = 0;
                        _zAxisRotation = 0;
                        var query = state.GetEntityQuery(typeof(NewStickTag));
                        ecb.RemoveComponent<NewStickTag>(query);
                        ecb.AddComponent<OldStickTag>(query);
                    }
                    break;
                }
            }

            ecb.Playback(state.EntityManager);
        }
        
    }
}