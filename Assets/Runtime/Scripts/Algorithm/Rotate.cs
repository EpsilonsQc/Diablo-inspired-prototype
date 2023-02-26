using UnityEngine;

namespace HeroicFantasy.Algorithm
{
    public class Rotate : MonoBehaviour
    {
        [SerializeField] private float rotationSpeed;

        private void Awake()
        {
            rotationSpeed = 20f;
        }

        private void Update()
        {
            transform.Rotate(new Vector3(0, rotationSpeed, 0) * Time.deltaTime);
        }
    }
}