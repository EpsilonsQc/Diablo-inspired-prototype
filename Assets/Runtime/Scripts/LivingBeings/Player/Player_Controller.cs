using UnityEngine;
using UnityEditor; // used for "EditorUtility.DisplayDialog"

// HOTKEYS
// SHIFT + W    : Move forward (run)
// W            : Move forward (walk)
// A            : Rotate left
// S            : Move backward
// D            : Rotate right

namespace HeroicFantasy.LivingBeings
{
    public class Player_Controller : MonoBehaviour
    {
        [Header("TRANSFORM")]
        [SerializeField] private Transform thisTransform;
        [Space(10)]

        [Header("MOVEMENTS SPEED")]
        [SerializeField] private float walkSpeed = 250f;
        [SerializeField] private float runSpeed = 500f;
        [SerializeField] private float rotationSpeed = 100f;

        private void Awake()
        {
            thisTransform = this.transform;
        }

        private void LateUpdate()
        {
            float playerSpeed = walkSpeed;

            if (Input.GetKey(KeyCode.LeftShift))
            {
                if (Input.GetKey(KeyCode.W)) // SHIFT + W : Set move forward speed (run)
                {
                    playerSpeed = runSpeed;
                }
            }
            else if (Input.GetKey(KeyCode.W)) // W : Set move forward speed (walk)
            {
                playerSpeed = walkSpeed;
            }
            else if (Input.GetKey(KeyCode.S)) // S: Set move backward speed
            {
                playerSpeed = walkSpeed;
            }

            float verticalInput = Input.GetAxis("Vertical"); // W and S : move forward and backward
            thisTransform.Translate(new Vector3(0, 0, verticalInput) * playerSpeed * Time.deltaTime);

            float horizontalInput = Input.GetAxis("Horizontal"); // A and D : rotate left and right
            thisTransform.Rotate(new Vector3(0, horizontalInput, 0) * rotationSpeed * Time.deltaTime);
        }

        #if UNITY_EDITOR // compilation directive (exclude from build)
        #region ON VALIDATE
        private void OnValidate()
        {
            // WALK SPEED [Range (200, 300)]
            if (walkSpeed < 200)
            {
                walkSpeed = 200;
                EditorUtility.DisplayDialog("WARNING", "You can't reduce WALK speed below that threshold.", "OK");
            }

            if (walkSpeed > 300)
            {
                walkSpeed = 300;
                EditorUtility.DisplayDialog("WARNING", "You can't increase WALK speed above that threashold.", "OK");
            }

            // RUN SPEED [Range (450, 550)]
            if(runSpeed < 450)
            {
                runSpeed = 450;
                EditorUtility.DisplayDialog("WARNING", "You can't reduce RUN speed below that threshold.", "OK");
            }

            if(runSpeed > 550)
            {
                runSpeed = 550;
                EditorUtility.DisplayDialog("WARNING", "You can't increment RUN speed above that threshold.", "OK");
            }

            // ROTATION SPEED [Range (75, 125)]
            if(rotationSpeed < 75)
            {
                rotationSpeed = 75;
                EditorUtility.DisplayDialog("WARNING", "You can't reduce ROTATION speed below that threshold.", "OK");
            }

            if(rotationSpeed > 125)
            {
                rotationSpeed = 125;
                EditorUtility.DisplayDialog("WARNING", "You can't increment ROTATION speed above that threshold.", "OK");
            }
        }
        #endregion
        #endif // compilation directive (exclude from build)
    }
}