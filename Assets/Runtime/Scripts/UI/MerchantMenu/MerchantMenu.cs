using UnityEngine;
using UnityEngine.UI; // used for "Button"
using TMPro; // used for "TextMeshProGUI"

// HOTKEYS
// ESC          : Close merchant menu (if open)

namespace HeroicFantasy.UI.MerchantMenu
{
    public class MerchantMenu : MonoBehaviour
    {
        [Header("CANVAS")]
        [SerializeField] private GameObject CanvasHUD;
        [SerializeField] private GameObject CanvasMerchantMenu;
        [Space(10)]

        [Header("BUTTONS")]
        [SerializeField] private Button openMenuButton; // used to open the merchant menu
        [SerializeField] private Button closeMenuButton; // used to close the merchant menu
        [SerializeField] private Button buyItemsButton; // used to buy items from the merchant
        [Space(10)]

        [Header("GAME OBJECTS")]
        [SerializeField] private GameObject objectTemplate; // used to create the items in the merchant menu
        [SerializeField] private GameObject merchantMenu; // used to display the merchant menu items (grid layout)
        [SerializeField] private SpriteRenderer spriteRenderer; // used to display the merchant menu items (image)
        [Space(10)]

        [Header("RESOURCES")]
        [SerializeField] private Sprite[] sprites;
        [Space(10)]

        [Header("TEXT FIELDS")] 
        [SerializeField] private TextMeshProUGUI itemNameText;
        [SerializeField] private TextMeshProUGUI itemPriceText;

        // DestroySelf data member inheritance
        static protected TextMeshProUGUI totalCostText;
        static protected int totalCost = 0;
        static protected int instanceCount = 0;

        // Clone of prefab for each item in the menu
        private GameObject clone;
        
        private int minPrice = 1;
        private int maxPrice = 2500;
        private int itemPrice = 0;

        private int minSprites = 0;
        private int maxSprites = 21;
        private int currentSprites = 0;

        private void Awake()
        {
            CanvasHUD = GameObject.Find("Canvas (HUD)");
            CanvasMerchantMenu = GameObject.Find("Canvas (Merchant menu)");

            openMenuButton = GameObject.Find("Canvas (Griswold, the blacksmith)/Merchant menu (Button)").GetComponent<Button>();
            openMenuButton.onClick.AddListener(OpenMenu);

            closeMenuButton = GameObject.Find("Canvas (Merchant menu)/Close menu (Button)").GetComponent<Button>();
            closeMenuButton.onClick.AddListener(CloseMenu);

            buyItemsButton = GameObject.Find("Canvas (Merchant menu)/Buy items (Button)").GetComponent<Button>();
            buyItemsButton.onClick.AddListener(BuyItems);

            objectTemplate = GameObject.Find("Canvas (Merchant menu)/Object template (Empty)");
            merchantMenu = GameObject.Find("Canvas (Merchant menu)/Merchant menu (Image)");
            spriteRenderer = GameObject.Find("Canvas (Merchant menu)/Object template (Empty)/Object (Button)/Object icon (Sprite Renderer)").GetComponent<SpriteRenderer>();
            sprites = Resources.LoadAll<Sprite>("Sprites");

            itemNameText = GameObject.Find("Canvas (Merchant menu)/Object template (Empty)/Object (Button)/Object name (Text)").GetComponent<TextMeshProUGUI>();
            itemPriceText = GameObject.Find("Canvas (Merchant menu)/Object template (Empty)/Object (Button)/Price parent (Empty)/Price (Text)").GetComponent<TextMeshProUGUI>();
            totalCostText = GameObject.Find("Canvas (Merchant menu)/Total cost (Image)/Total cost (Text)").GetComponent<TextMeshProUGUI>();
        }

        private void Update()
        {
            CloseMenuEsc();
        }

        // Open merchant menu
        private void OpenMenu()
        {
            if(!CanvasMerchantMenu.activeSelf) // If merchant menu is closed, open it
            {
                CanvasHUD.SetActive(false);
                CanvasMerchantMenu.SetActive(true);
            }
        }

        // Close merchant menu
        private void CloseMenu()
        {
            if(CanvasMerchantMenu.activeSelf) // If merchant menu is open, close it
            {
                CanvasHUD.SetActive(true);
                CanvasMerchantMenu.SetActive(false);

                for(int i = 0; i < merchantMenu.transform.childCount; i++)
                {
                    Destroy(merchantMenu.transform.GetChild(i).gameObject); // Destroy all children of "merchant menu" (instanciation)
                }

                instanceCount = 0; // Reset instance count to 0
                totalCost = 0; // Reset total cost (int) to 0
                totalCostText.text = "0"; // Reset total cost (string) to 0$
            }
        }

        private void CloseMenuEsc()
        {
            // Close merchant menu if "Escape" key is pressed
            if(Input.GetKeyDown(KeyCode.Escape))
            {
                CloseMenu();
            }
        }

        // Buy items
        private void BuyItems()
        {
            if(instanceCount <= 7)
            {
                instanceCount++;

                currentSprites = Random.Range(minSprites, maxSprites);
                spriteRenderer.sprite = sprites[currentSprites];

                itemNameText.text = spriteRenderer.sprite.name;

                itemPrice = Random.Range(minPrice, maxPrice);
                itemPriceText.text =  itemPrice.ToString();

                totalCost = totalCost + itemPrice;
                totalCostText.text = totalCost.ToString();

                clone = Instantiate(objectTemplate);
                clone.transform.SetParent(merchantMenu.transform, false);
                clone.name = spriteRenderer.sprite.name + " (" + itemPrice.ToString() + "$)";
            }
        }
    }
}