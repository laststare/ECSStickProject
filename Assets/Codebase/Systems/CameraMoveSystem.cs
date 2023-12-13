using System.Numerics;
using Codebase.Aspects;
using Codebase.AuthoringAndMono;
using Codebase.ComponentsAndTags;
using Unity.Burst;
using Unity.Entities;
using Vector3 = UnityEngine.Vector3;

namespace Codebase.Systems
{
    [BurstCompile]
    public partial class CameraMoveSystem: SystemBase
    {

        private float _cameraMoveDistance;
        
        [BurstCompile]
        protected override void OnUpdate()
        {
            var levelFlowEntity = SystemAPI.GetSingletonEntity<LevelFlowProperties>();
            var levelFlow = SystemAPI.GetAspect<LevelFlowAspect>(levelFlowEntity);
            
            var cameraSingleton = CameraSingleton.Instance;
            if (cameraSingleton == null) return;
            
            if (levelFlow._levelFlowProperties.ValueRO.flowState == LevelFlowState.Start)
            {
                cameraSingleton.MoveToSTart();
            }

            if (levelFlow._levelFlowProperties.ValueRO.flowState != LevelFlowState.CameraRun) return;
            
            var levelBuilderEntity = SystemAPI.GetSingletonEntity<LevelBuilderProperties>();
            var levelBuilder = SystemAPI.GetAspect<LevelBuilderAspect>(levelBuilderEntity);

           
            
            if (_cameraMoveDistance == 0)
                _cameraMoveDistance = levelBuilder.GetActualColumnXPosition + cameraSingleton.xOffset;
            
            if (cameraSingleton.transform.position.x < _cameraMoveDistance)
            {
                cameraSingleton.transform.position += Vector3.right * cameraSingleton.moveSpeed * SystemAPI.Time.DeltaTime;
            }
            else
            {
                levelFlow._levelFlowProperties.ValueRW.flowState = LevelFlowState.PlayerIdle;
                _cameraMoveDistance = 0;
            }
        }
    }
}