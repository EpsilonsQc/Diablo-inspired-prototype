using UnityEngine;
using UnityEngine.UI; // used for "Button"
using TMPro; // used for "TextMeshProGUI"

namespace HeroicFantasy.UI.MerchantMenu
{
    public class DestroySelf : MerchantMenu
    {
        [Header("DESTROY SELF")]
        [SerializeField] private Button DestroySelfButton; // Added manually in the inspector
        [SerializeField] private TextMeshProUGUI selfItemPrice; // Added manually in the inspector

        private void Awake()
        {
            DestroySelfButton.onClick.AddListener(Destroy);
        }

        private void Destroy()
            {
                int selfItemPriceInt = int.Parse(selfItemPrice.text);
                
                totalCost = totalCost - selfItemPriceInt;
                totalCostText.text = totalCost.ToString();

                instanceCount = instanceCount - 1;

                Destroy(gameObject);
            }
    }
}