using Codebase.ComponentsAndTags;
using Unity.Entities;
using UnityEngine;

namespace Codebase.AuthoringAndMono
{
    public class LevelFlowMono : MonoBehaviour
    {
        public LevelFlowState levelFlowState;

    }
    
    public class LevelFlowBaker : Baker<LevelFlowMono>
    {
        public override void Bake(LevelFlowMono authoring)
        {
            var levelFlowEntity = GetEntity(TransformUsageFlags.Dynamic);
            
            AddComponent(levelFlowEntity, new LevelFlowProperties
            {
                flowState =  authoring.levelFlowState
            });
        }
    }
}