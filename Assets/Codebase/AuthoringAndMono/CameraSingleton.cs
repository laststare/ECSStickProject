using UnityEngine;

namespace Codebase.AuthoringAndMono
{
    public class CameraSingleton : MonoBehaviour
    {
        public static CameraSingleton Instance;
        public float moveSpeed;
        public float xOffset;
        public float startXPosition;
        
        private void Awake()
        {
            if (Instance != null)
            {
                Destroy(gameObject);
                return;
            }

            Instance = this;
        }

        public void MoveToSTart()
        {
            var cameraTransform = transform;
            cameraTransform.position = new Vector3(startXPosition, cameraTransform.position.y, cameraTransform.position.z);
        }
    }
    
}