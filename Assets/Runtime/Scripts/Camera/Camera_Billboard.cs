using UnityEngine;

namespace HeroicFantasy.Camera
{
    public class Camera_Billboard : MonoBehaviour
    {
        [SerializeField] private Transform playerCamera;

        private void LateUpdate()
        {
            transform.LookAt(transform.position + playerCamera.forward); // Make the object attached to this script always face the camera
        }
    }
}