using System.Collections; // used for "IEnumerator"
using UnityEngine;
using UnityEngine.SceneManagement; // used for "SceneManager"
using UnityEngine.UI; // used for "Button" and "Image"
using TMPro; // used for "TextMeshProGUI"

namespace HeroicFantasy.UI.MainMenu
{
    public class LoadScene : MonoBehaviour
    {
        [Header("GAME OBJECTS")]
        [SerializeField] private Button loadGameButton;
        [SerializeField] private Image textLoadingPercentageImg;
        [SerializeField] private Image loadingBarImg;
        [SerializeField] private Image loadingBarProgressImg;
        [SerializeField] private TextMeshProUGUI loadGameButtonText;
        [SerializeField] private TextMeshProUGUI loadingPercentageText;
        [Space(10)]

        [Header("BOOLEAN PARAMETERS")]
        [SerializeField] private bool startLoadingScene = false;
        [SerializeField] private bool sceneIsLoaded = false;
        [SerializeField] private bool enterScene = false;
        [Space(10)]

        [Header("VARIABLES")]
        [SerializeField] private float textLoadingProgress = 0f;
        [SerializeField] private float changePerSecond = 20f;

        private void Awake()
        {
            loadGameButton = GameObject.Find("Canvas (Main Menu)/Load Game (Button)").GetComponent<Button>();
            loadGameButton.onClick.AddListener(StartLoadingScene);
            loadGameButton.onClick.AddListener(EnterTheScene);

            textLoadingPercentageImg = GameObject.Find("Canvas (Main Menu)/Loading Percentage (Image)").GetComponent<Image>();
            loadingBarImg = GameObject.Find("Canvas (Main Menu)/Loading Bar (Image)").GetComponent<Image>();
            loadingBarProgressImg = GameObject.Find("Canvas (Main Menu)/Loading Bar (Image)/Loading Bar Progress (Image)").GetComponent<Image>();

            loadGameButtonText = GameObject.Find("Canvas (Main Menu)/Load Game (Button)/Load Game (Text)").GetComponent<TextMeshProUGUI>();
            loadingPercentageText = GameObject.Find("Canvas (Main Menu)/Loading Percentage (Image)/Loading Percentage (Text)").GetComponent<TextMeshProUGUI>();
        }

        private void Update()
        {
            if (startLoadingScene == true && textLoadingProgress <= 100)
            {
                textLoadingPercentageImg.enabled = true;
                loadingBarImg.enabled = true;
                loadingBarProgressImg.enabled = true;

                textLoadingProgress += changePerSecond * Time.deltaTime;
                loadingPercentageText.text = ((int)textLoadingProgress + "%");

                loadingBarProgressImg.fillAmount = (float)textLoadingProgress / 100f;
            }
        }

        // Start loading scene
        private void StartLoadingScene()
        {
            StartCoroutine(LoadMyScene());
        }

        // Load my scene (coroutine)
        private IEnumerator LoadMyScene()
        {
            if (startLoadingScene == false)
            {
                AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("InGame");
                asyncLoad.allowSceneActivation = false;
            
                while (asyncLoad.isDone == false)
                {
                    startLoadingScene = true;

                    if(textLoadingProgress >= 100)
                    {
                        loadGameButtonText.text = "Start";
                        sceneIsLoaded = true;
                        loadGameButton.onClick.RemoveListener(StartLoadingScene);

                        if(enterScene == true)
                        {
                            asyncLoad.allowSceneActivation = true;
                            loadGameButton.onClick.RemoveListener(EnterTheScene);
                        }
                    }
                    yield return null; // wait until the next frame before continuing
                }
            }
        }

        // Enter the scene
        private void EnterTheScene()
        {
            if(sceneIsLoaded == true)
            {
                enterScene = true;
            }
        }
    }
}