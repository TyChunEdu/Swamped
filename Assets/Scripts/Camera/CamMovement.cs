using Cinemachine;
using UnityEngine;

namespace GameCamera
{
    public class CamMovement : MonoBehaviour
    {
        public GameObject Ground;
        public CinemachineCameraOffset cameraOffset;
        public CinemachineVirtualCamera virtualCamera;
    
        // Start is called before the first frame update
        void Start()
        {
        
        }

        // Update is called once per frame
        void Update()
        {
            Debug.Log(transform.position.y + ", " + transform.localScale.y);
            // virtualCamera.m_Lens.
            // transform.position.Set(transform.position.x, 5f, transform.position.z);
            // cameraOffset.
        }
    }
}
