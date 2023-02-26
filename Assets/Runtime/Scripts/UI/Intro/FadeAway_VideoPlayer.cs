using UnityEngine;

namespace HeroicFantasy.UI.Intro
{
    public class FadeAway_VideoPlayer : FadeAway
    {
        [Header("GAME OBJECT")]
        [SerializeField] private Material myMaterial; // Added manually in the inspector

        private void Awake()
        {
            waitFor = 6f; // Amount of seconds before gradually fading away;
            startTime = Time.time + waitFor;
            fadeSpeed = 0.15f;
        }

        private void Update()
        {
            myColor = myMaterial.color;
            myColor.a = Mathf.Lerp(1, 0, (Time.time - startTime) * fadeSpeed);
            myMaterial.color = myColor;

            if(myColor.a == 0)
            {
                gameObject.SetActive(false);
            }
        }
    }
}