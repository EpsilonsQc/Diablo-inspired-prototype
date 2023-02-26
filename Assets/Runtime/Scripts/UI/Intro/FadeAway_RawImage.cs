using UnityEngine;
using UnityEngine.UI; // used for "RawImage"

namespace HeroicFantasy.UI.Intro
{
    public class FadeAway_RawImage : FadeAway
    {
        [Header("GAME OBJECT")]
        [SerializeField] private RawImage blackBackground;

        private void Awake()
        {
            blackBackground = GameObject.Find("Canvas (Intro)/Black background (Raw Image)").GetComponent<RawImage>();

            waitFor = 2f; // Amount of seconds before gradually fading away;
            startTime = Time.time + waitFor;
            fadeSpeed = 0.15f;
        }

        private void Update()
        {
            blackBackground.color = Color.Lerp(Color.black, Color.clear, (Time.time - startTime) * fadeSpeed);

            if(blackBackground.color == Color.clear)
            {
                gameObject.SetActive(false);
            }
        }
    }
}