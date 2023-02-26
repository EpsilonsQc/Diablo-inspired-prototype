using UnityEngine;

namespace HeroicFantasy.Instanciation
{
    public class Instanciation_Prefab : MonoBehaviour
    {
        [Header("CLOUDS")]
        [SerializeField] private GameObject[] clouds;

        private void Awake()
        {
            InstantiateCloud();
        }

        private void InstantiateCloud()
        {
            GameObject cloudsParent = new GameObject(); // Create parent for instanciated objects
            cloudsParent.name = "Clouds";
            cloudsParent.transform.SetParent(gameObject.transform, false);

            for(int i = 0; i < 24; i++)
            {
                Instantiate(clouds[i], transform.parent = cloudsParent.transform, false);
                clouds[i].name = "Cloud " + i + " ";
            }
        }
    }
}