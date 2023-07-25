using UnityEngine;
using Cinemachine;
using RPG.Control;

namespace RPG.Core
{
    public class FreeLookCameraController : MonoBehaviour
    {
        [SerializeField] GameObject freeLookCamera;
        CinemachineFreeLook freeLookComponent;
        PlayerController playerControllerScript;

        private void Awake()
        {
            freeLookComponent = freeLookCamera.GetComponent<CinemachineFreeLook>();
            playerControllerScript = GetComponent<PlayerController>();
        }

        private void Update()
        {          
            if (Input.GetMouseButtonDown(1))
            {
                if (playerControllerScript.GetIsDraggingUI()) return;

                // use the following line for mouse control of zoom instead of mouse wheel
                // be sure to change Input Axis Name on the Y axis to "Mouse Y"

                freeLookComponent.m_YAxis.m_MaxSpeed = 0.5f;
                freeLookComponent.m_XAxis.m_MaxSpeed = 500;
            }
            if (Input.GetMouseButtonUp(1))
            {
                // use the following line for mouse control of zoom instead of mouse wheel
                // be sure to change Input Axis Name on the Y axis from to "Mouse Y"

                freeLookComponent.m_YAxis.m_MaxSpeed = 0;
                freeLookComponent.m_XAxis.m_MaxSpeed = 0;
            }

            // wheel zoom //
            // comment out the below if condition if you are using mouse control for zoom
            if (Input.mouseScrollDelta.y != 0)
            {
                freeLookComponent.m_YAxis.m_MaxSpeed = 0.5f;
            }
        }
    }
}