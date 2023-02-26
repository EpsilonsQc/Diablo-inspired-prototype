using UnityEngine;
using UnityEngine.UI; // used for "Button"
using TMPro; // used for "TextMeshProUGUI"

namespace HeroicFantasy.UI.WorldSpace
{
    public class GraveyardStory : MonoBehaviour
    {
        [SerializeField] private Button graveyardButton; // Button to start the story
        [SerializeField] private GameObject storyObject; // Empty game object that will holds the story text
        [SerializeField] private TextMeshProUGUI storyText; // Text that will display the story
        [SerializeField] private TMP_FontAsset T4CBeaulieuxFont; // Font used in the story text

        private bool[] isActive; // Array that will hold the state of each part of the story

        private void Awake()
        {
            graveyardButton = GameObject.Find("Canvas (Graveyard)/Untold prophecy (Button)").GetComponent<Button>();
            graveyardButton.onClick.AddListener(TellStory);

            storyObject = GameObject.Find("Canvas (Graveyard)/Story (Empty)");
            T4CBeaulieuxFont = Resources.Load<TMP_FontAsset>("Font/t4cbeaulieux SDF");

            isActive = new bool[7]; // set the array size to 7 elements

            for (int i = 0; i < isActive.Length; i++)
            {
                isActive[i] = false;
            }
        }

        private void AddText()
        {
            storyText = storyObject.AddComponent<TextMeshProUGUI>();
            storyText.GetComponent<TextMeshProUGUI>().font = T4CBeaulieuxFont;
            storyText.GetComponent<TextMeshProUGUI>().fontSize = 24;
            storyText.GetComponent<TextMeshProUGUI>().color = new Color(0.8588235f, 0.8156863f, 0.7058824f, 1.0f);
        }

        private void DestroyText()
        {
            storyText = storyObject.GetComponent<TextMeshProUGUI>();
            Destroy(storyText);

            for(int i = 0; i < isActive.Length; i++)
            {
                isActive[i] = false;
            }
        }

        // On-Click, tell a story.
        private void TellStory()
        {
            if (!isActive[0])
            {
                AddText();

                storyText.text = "Hello, my friend !";
                isActive[0] = true;
            }
            else if (!isActive[1])
            {
                storyText.text = "Stay awhile and listen...";
                isActive[1] = true;
            }
            else if(!isActive[2])
            {
                storyText.text = "The sanctity of this place has been fouled.";
                isActive[2] = true;
            }
            else if(!isActive[3])
            {
                storyText.text = "For soon, nothing of it shall remain...";
                isActive[3] = true;
            }
            else if(!isActive[4])
            {
                storyText.text = "Hell is coming, brother. Hell is coming.";
                isActive[4] = true;
            }
            else if(!isActive[5])
            {
                storyText.text = "But, it has been said that in the end of all things...";
                isActive[5] = true;
            }
            else if(!isActive[6])
            {
                storyText.text = "...you would find a new beginning.";
                isActive[6] = true;
            }
            else
            {
                DestroyText();
            }
        }
    }
}