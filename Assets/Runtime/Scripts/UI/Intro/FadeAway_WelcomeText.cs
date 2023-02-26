using UnityEngine;
using TMPro; // used for "TextMeshProGUI"

namespace HeroicFantasy.UI.Intro
{
    public class FadeAway_WelcomeText : FadeAway
    {
        [Header("GAME OBJECT")]
        [SerializeField] private TextMeshProUGUI welcomeText;

        private void Awake()
        {
            welcomeText = GameObject.Find("Canvas (Intro)/Welcome adventurer (Text)").GetComponent<TextMeshProUGUI>();

            waitFor = 4f; // Amount of seconds before gradually fading away;
            startTime = Time.time + waitFor;
            fadeSpeed = 0.15f;
        }

        private void Update()
        {
            myColor = welcomeText.color;
            myColor.a = Mathf.Lerp(1, 0, (Time.time - startTime) * fadeSpeed);
            welcomeText.color = myColor;

            if(myColor.a == 0)
            {
                gameObject.SetActive(false);
            }
        }
    }
}