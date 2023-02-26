using UnityEngine;

// HOTKEYS
// Space        : <DEBUG> Skip intro for faster testing

namespace HeroicFantasy.Camera
{
    public class Camera_Switch : MonoBehaviour
    {
        [Header("CAMERA")]
        [SerializeField] private GameObject mainCamera;
        [SerializeField] private GameObject playerCamera;
        [Space(10)]

        [Header("CANVAS")]
        [SerializeField] private GameObject CanvasIntro;
        [SerializeField] private GameObject CanvasHUD;
        [SerializeField] private GameObject CanvasMerchantMenu;
        [Space(10)]

        [Header("GAME OBJECT")]
        [SerializeField] private GameObject IntroWelcomeText;

        private void Awake()
        {
            // CAMERA
            playerCamera = GameObject.Find("Camera");
            playerCamera.SetActive(false);

            // CANVAS
            CanvasIntro = GameObject.Find("Canvas (Intro)");
            CanvasHUD = GameObject.Find("Canvas (HUD)");
            CanvasMerchantMenu = GameObject.Find("Canvas (Merchant menu)");

            // GAME OBJECT
            IntroWelcomeText = GameObject.Find("Canvas (Intro)/Welcome adventurer (Text)");
        }

        private void Start()
        {        
            // CANVAS
            CanvasIntro.SetActive(true);
            CanvasHUD.SetActive(false);
            CanvasMerchantMenu.SetActive(false);
        }

        private void Update()
        {
            SkipIntro(); // DEBUG ONLY
            CameraSwitch();
        }

        // <DEBUG> Skip intro for faster testing (if intro is playing)
        private void SkipIntro()
        {
            if(Input.GetKeyDown(KeyCode.Space))
            {
                IntroWelcomeText.SetActive(false);
            }
        }

        private void CameraSwitch()
        {
            if(IntroWelcomeText.activeSelf == false)
            {
                // CANVAS
                CanvasIntro.SetActive(false);
                CanvasHUD.SetActive(true); 

                // CAMERA
                mainCamera.SetActive(false);
                playerCamera.SetActive(true);
            }
        }
    }
}