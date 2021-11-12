using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using CodeMonkey.Utils;

public class InventoryUI : MonoBehaviour
{
    private Inventory inventory;
    private Transform inventory_panel;
    private Transform slot;

    private void Awake()
    {
        inventory_panel = transform.Find("Inventory");
        slot = inventory_panel.Find("Slot");
    }


    public void UpdateInventory(Inventory inventory)
    {
        this.inventory = inventory;
        inventory.OnInventoryUpdated += Inventory_OnInventoryUpdated;
        RefreshInventory();
    }

    private void Inventory_OnInventoryUpdated(object sender, System.EventArgs e)
    {
        RefreshInventory();
    }

    public void RefreshInventory()
    {
        foreach(Transform child in inventory_panel)
        {
            if(child == slot)
            {
                continue;
            }
            else
            {
                Destroy(child.gameObject);
            }
        }
        foreach(Item item in inventory.GetInventory())
        {
            RectTransform SlotRectTransform = Instantiate(slot, inventory_panel).GetComponent<RectTransform>();
            SlotRectTransform.gameObject.SetActive(true);
            TextMeshProUGUI number_text = SlotRectTransform.Find("NumberText").GetComponent<TextMeshProUGUI>();
            if (item.number > 1)
            {
                number_text.SetText(item.number.ToString());
            }
            else
            {
                number_text.SetText("");
            }
            Transform Border = SlotRectTransform.Find("Border");
            SlotRectTransform.Find("Border").GetComponent<Button_UI>().ClickFunc = () =>
            {
                inventory.UseItem(item);
            };
            SlotRectTransform.Find("Border").GetComponent<Button_UI>().MouseRightClickFunc = () =>
            {
                inventory.DropItem(item);
            };
            Image image = Border.Find("ItemImage").GetComponent<Image>();
            image.sprite = item.GetSprite();
        }
    }
}
