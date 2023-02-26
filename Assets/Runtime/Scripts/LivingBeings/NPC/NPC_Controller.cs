using UnityEngine;
using UnityEditor; // used for "Handles"

// RANDOM NPC's MOVEMENTS

namespace HeroicFantasy.LivingBeings
{
    public class NPC_Controller : MonoBehaviour
    {
        [Header("VECTORS")]
        [SerializeField] private Vector3 initialPosition;
        [SerializeField] private Vector3 actualDirection;
        [SerializeField] private Vector3 newDirection;
        [Space(5)]

        [Header("QUATERNIONS")]
        [SerializeField] private Quaternion actualRotation;
        [SerializeField] private Quaternion newRotation;
        [Space(5)]

        [Header("SPEED")]
        [SerializeField] private float movementSpeed;
        [SerializeField] private float rotationSpeed;
        [Space(5)]

        [Header("MOVEMENT RANGE")]
        [SerializeField] private float movementRangeMin;
        [SerializeField] private float movementRangeMax;
        [Space(5)]

        [Header("INVOKE RATE")]
        [SerializeField] private float invokeStartTime;
        [SerializeField] private float invokeRateMin;
        [SerializeField] private float invokeRateMax;
        [Space(5)]

        [Header("LERP INTERPOLATION RATIO")]
        [SerializeField] private float lerpInterpolationRatio;

        [Header("VARIABLES")]
        [SerializeField] private bool invoke;        
        [SerializeField] private int rayLength; // used for drawing a ray in the scene view
        
        #if UNITY_EDITOR // compilation directive (exclude from build)  

        private void OnDrawGizmos()
        {
            Handles.color = Color.red;
            Handles.DrawWireDisc(initialPosition, Vector3.up, (movementRangeMax - movementRangeMin));
            Debug.DrawRay(transform.position, actualDirection * rayLength, Color.red);
        }
        #endif // compilation directive (exclude from build)

        private void Awake()
        {
            initialPosition = transform.position;

            newRotation = Quaternion.identity; // initialize the quaternion (prevent error message)

            movementSpeed = 100f;
            rotationSpeed = 3f;

            movementRangeMin = -75f;
            movementRangeMax = 75f;

            invokeStartTime = 0f;
            invokeRateMin = 3f;
            invokeRateMax = 10f;

            lerpInterpolationRatio = 0f;

            invoke = true;
            rayLength = 100;
        }

        private void Update()
        {
            if(invoke == true)
            {
                if(IsInvoking("ChangePosition"))
                {
                    CancelInvoke("ChangePosition");
                    invoke = true;
                }
                else
                {
                    InvokeRepeating("ChangePosition", invokeStartTime, Random.Range(invokeRateMin, invokeRateMax));
                    invoke = false;
                }
            }

            if (transform.rotation != newRotation)
            {
                lerpInterpolationRatio += rotationSpeed * Time.deltaTime; 
                transform.rotation = Quaternion.Lerp(actualRotation, newRotation, lerpInterpolationRatio);
            }

            if (transform.position != newDirection)
            {
                transform.position = Vector3.MoveTowards(transform.position, newDirection, Time.deltaTime * movementSpeed);
            }
        }

        private void ChangePosition()
        {
            newDirection = new Vector3(initialPosition.x + Random.Range(movementRangeMin, movementRangeMax), 0f, initialPosition.z + Random.Range(movementRangeMin, movementRangeMax));
            actualDirection = Vector3.Normalize(newDirection - transform.position);

            newRotation = Quaternion.LookRotation(actualDirection, Vector3.up);  
            actualRotation = transform.rotation;
            lerpInterpolationRatio = 0f;
        }
    }
}