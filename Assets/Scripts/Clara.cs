using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Clara : MonoBehaviour
{
    public float max_health = 100;
    public float current_health;
    public float max_hunger = 100;
    public float current_hunger;
    public float max_thirst = 100;
    public float current_thirst;
    
    public HealthBar health_bar;
    public HungerBar hunger_bar;
    public ThirstBar thirst_bar;

    private Inventory inventory;

    [SerializeField] private InventoryUI inventoryUI;

    private Item ItemToPickUp = null;
    private bool InWater = false;
    private GameObject ItemToDestory = null;
    private GameObject MessageBox;
    private GameObject MessageBoxText;
    private GameObject ThirdPersonCamera;
    private GameObject SelectMode;
    // Start is called before the first frame update
    void Start()
    {
        ThirdPersonCamera = GameObject.Find("vThirdPersonCamera");
        SelectMode = GameObject.Find("SelectModePanel");
        MessageBox = GameObject.Find("GameMessage");
        MessageBoxText = GameObject.Find("GameMessageText");
        SelectMode.SetActive(false);
        health_bar.SetMaxHealth(max_health);
        hunger_bar.SetMaxHunger(max_hunger);
        thirst_bar.SetMaxThirst(max_thirst);
        current_health = max_health;
        current_hunger = 75;
        current_thirst = 25;
        hunger_bar.SetHunger(current_hunger);
        thirst_bar.SetThirst(current_thirst);
        inventory = new Inventory();
        inventoryUI.UpdateInventory(inventory);
        inventory.AddItem(new Item { itemName = Item.ItemName.EmptyJar, number = 1 });
        MessageBox.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void FixedUpdate()
    {
        current_hunger -= 0.001f;
        current_thirst -= 0.001f;
        hunger_bar.SetHunger(current_hunger);
        thirst_bar.SetThirst(current_thirst);
    }

    private void SetMessageBoxAndSetActive(string message)
    {
        MessageBoxText.GetComponent<TextMeshProUGUI>().SetText(message);
        MessageBox.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E)){
            ThirdPersonCamera.GetComponent<Invector.vCamera.vThirdPersonCamera>().LockCamera = !ThirdPersonCamera.GetComponent<Invector.vCamera.vThirdPersonCamera>().LockCamera;
            if(ThirdPersonCamera.GetComponent<Invector.vCamera.vThirdPersonCamera>().LockCamera == true)
            {
                SelectMode.SetActive(true);
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
            }
            else
            {
                SelectMode.SetActive(false);
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
            }
        }
        if (Input.GetKeyDown(KeyCode.F))
        {
            if(ItemToPickUp != null)
            {
                inventory.AddItem(ItemToPickUp);
                ItemToPickUp = null;
                if(ItemToDestory != null)
                {
                    Destroy(ItemToDestory);
                    ItemToDestory = null;
                }
                MessageBox.SetActive(false);
            }
        }
        if (Input.GetKeyDown(KeyCode.I))
        {
            if(InWater == true)
            {
                if (inventory.HasItem(new Item { itemName = Item.ItemName.EmptyJar, number = 1 }))
                {
                    inventory.UseItem(new Item { itemName = Item.ItemName.EmptyJar, number = 1 }, "fill_water");
                    if (!inventory.HasItem(new Item { itemName = Item.ItemName.EmptyJar, number = 1 }))
                    {
                        MessageBox.SetActive(false);
                    }
                }
            }
        }
    }

    public void ChangeHealthByDiff(int diff)
    {
        current_health += diff;
        hunger_bar.SetHunger(current_health);
    }

    public void ChangeHungerByDiff(int diff)
    {
        current_hunger += diff;
        hunger_bar.SetHunger(current_hunger);
    }

    public void ChangeThirstByDiff(int diff)
    {
        current_thirst += diff;
        hunger_bar.SetHunger(current_thirst);
    }

    /*
    private void OnTriggerEnter(Collider other)
    {
      
    }
    */


    private void OnTriggerStay(Collider other)
    {
        if (other.name.Contains("Jar_Empty"))
        {
            SetMessageBoxAndSetActive("Press F to pick up");
            ItemToPickUp = new Item { itemName = Item.ItemName.EmptyJar, number = 1 };
            ItemToDestory = other.gameObject;
        }
        if (other.name.Contains("Water"))
        {
            if (inventory.HasItem(new Item { itemName = Item.ItemName.EmptyJar, number = 1 }))
            {
                SetMessageBoxAndSetActive("Press I to fill water");
                InWater = true;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.name.Contains("Jar_Empty"))
        {
            MessageBox.SetActive(false);
            ItemToPickUp = null;
            ItemToDestory = null;
        }
        if (other.name.Contains("Water"))
        {
            MessageBox.SetActive(false);
            InWater = false;
        }
    }
}
