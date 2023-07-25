using RPG.Shops;
using UnityEngine.UI;
using UnityEngine;
using TMPro;

namespace RPG.UI.Shops
{
    public class RowUI : MonoBehaviour
    {
        [SerializeField] Image iconField;
        [SerializeField] TextMeshProUGUI nameField;
        [SerializeField] TextMeshProUGUI availabilityField;
        [SerializeField] TextMeshProUGUI priceField;
        [SerializeField] TextMeshProUGUI quantityField;
        [SerializeField] AudioClip plusMinusClip;

        Shop currentShop = null;
        ShopItem item = null;

        public void Setup(Shop currentShop, ShopItem item)
        {
            this.currentShop = currentShop;
            this.item = item;
            iconField.sprite = item.GetIcon();
            nameField.text = item.GetName();
            availabilityField.text = $"{item.GetAvailability()}";
            priceField.text = $"${item.GetPrice():N2}";
            quantityField.text = $"{item.GetQuantityInTransaction()}";
        }

        public void Add()
        {
            GetComponent<AudioSource>().PlayOneShot(plusMinusClip);
            currentShop.AddToTransaction(item.GetInventoryItem(), 1);
        }

        public void Remove()
        {
            GetComponent<AudioSource>().PlayOneShot(plusMinusClip);
            currentShop.AddToTransaction(item.GetInventoryItem(), -1);
        }
    }
}