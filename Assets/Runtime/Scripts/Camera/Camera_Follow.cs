using UnityEngine;

namespace HeroicFantasy.Camera
{
    public class Camera_Follow : MonoBehaviour
    {
        [Header("CAMERA SETTINGS")]
        [SerializeField] private Transform thisTransform; // Camera transform
        [SerializeField] private Transform target; // Player transform
        [Space(15)]
        [SerializeField] private Vector3 offset = new Vector3(0f, 45f, -125f); // default offset from target
        [SerializeField] private float positionDamping = 1.0f; // smoothing out the position (camera)

        private void Start()
        {
            thisTransform = transform; // cache 'transform' component of the camera so it doesn't need to be looked up each frame.
        }

        private void LateUpdate()
        {
            // Early out if we don't have a target
            if (!target) // if target == null
            {
                Debug.Log("[CAMERA] No target detected. Please add a target in the inspector.");
                return;
            }
            
            // control camera position in the world
            Vector3 wantedPosition = target.position + (target.rotation * offset);
            Vector3 currentPosition = Vector3.Lerp(thisTransform.position, wantedPosition, positionDamping * Time.deltaTime);
            thisTransform.position = currentPosition;

            // control camera rotation in the world
            thisTransform.LookAt(target.position, target.up);
        }
    }
}