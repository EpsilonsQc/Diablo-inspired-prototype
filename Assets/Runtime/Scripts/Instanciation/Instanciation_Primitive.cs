using UnityEngine;
using UnityEngine.UI; // used for "Button"
using TMPro; // used for "TextMeshProGUI"

namespace HeroicFantasy.Instanciation
{
    public class Instanciation_Primitive : MonoBehaviour
    {
        [Header("GAME OBJECTS")]
        [SerializeField] private Button pushToInstanciateButton;
        [SerializeField] private TextMeshProUGUI pushToInstanciateButtonText;
        [SerializeField] private GameObject PrimitiveParent;
        [Space(10)]

        [Header("PRIMITIVES")]
        private GameObject cube;
        private GameObject sphere;
        private GameObject capsule;
        private GameObject cylinder;
        [Space(10)]

        [Header("VARIABLE")]
        private bool isActive;
        private bool hasBeenEnabled;

        private void Awake()
        {
            isActive = false;
            hasBeenEnabled = false;

            pushToInstanciateButton = GameObject.Find("World Space/Canvas (Primitive)/Instanciate (Button)").GetComponent<Button>();
            pushToInstanciateButton.onClick.AddListener(InstanciatePrimitive);

            pushToInstanciateButtonText = GameObject.Find("World Space/Canvas (Primitive)/Instanciate (Button)/Instanciate (Text)").GetComponent<TextMeshProUGUI>();
        }

        private void InstanciatePrimitive()
        {
            if(isActive == false)
            {
                if(!hasBeenEnabled)
                {
                    PrimitiveParent = new GameObject(); // Create parent for instanciated objects
                    PrimitiveParent.name = "Primitives";
                    PrimitiveParent.transform.position = new Vector3(-720, 0, 620); // set position
                    PrimitiveParent.transform.SetParent(gameObject.transform, false);
                    hasBeenEnabled = true;
                }

                cube = GameObject.CreatePrimitive(PrimitiveType.Cube); // create primitive
                cube.name = "Cube (Primitive)"; // set name
                cube.transform.position = new Vector3(0, 50, 335); // set position
                cube.transform.localScale = new Vector3(100, 100, 100); // set scale
                cube.transform.SetParent(PrimitiveParent.transform, false); // set as a child of the gameObject attached to this script

                sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                sphere.name = "Sphere (Primitive)";
                sphere.transform.position = new Vector3(0, 50, 135);
                sphere.transform.localScale = new Vector3(100, 100, 100);
                sphere.transform.SetParent(PrimitiveParent.transform, false);

                capsule = GameObject.CreatePrimitive(PrimitiveType.Capsule);
                capsule.name = "Capsule (Primitive)";
                capsule.transform.position = new Vector3(0, 100, -65);
                capsule.transform.localScale = new Vector3(100, 100, 100);
                capsule.transform.SetParent(PrimitiveParent.transform, false);

                cylinder = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
                cylinder.name = "Cylinder (Primitive)";
                cylinder.transform.position = new Vector3(0, 100, -265);
                cylinder.transform.localScale = new Vector3(100, 100, 100);
                cylinder.transform.SetParent(PrimitiveParent.transform, false);

                pushToInstanciateButtonText.text = "Destroy";
                isActive = true;
            }
            else
            {
                Destroy(PrimitiveParent); // will also destroy all primitives children (cube, sphere, capsule, cylinder)

                pushToInstanciateButtonText.text = "Instanciate";
                isActive = false;
                hasBeenEnabled = false;
            }
        }
    }
}