using UnityEngine;

namespace HeroicFantasy.Algorithm
{
    public class WindSimulation : MonoBehaviour
    {
        [SerializeField] private float windSpeed;

        private void Awake()
        {
            windSpeed = 0.09f; // Define wind intensity
        }

        private void FixedUpdate()
        {
            windSimulation();
        }

        private void windSimulation()
        {
            float windEffect = Mathf.Cos(Time.time) * windSpeed;

            transform.Rotate(windEffect, windEffect, 0, Space.Self);
        }
    }
}