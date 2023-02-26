using UnityEngine;

namespace HeroicFantasy.UI.Intro
{
    public class FadeAway : MonoBehaviour
    {
        [Header("PARAMETERS")]
        [SerializeField] protected float startTime;
        [SerializeField] protected float waitFor;
        [SerializeField] protected float fadeSpeed;
        [Space(10)]

        [Header("COLOR")]
        [SerializeField] protected Color myColor;
    }
}