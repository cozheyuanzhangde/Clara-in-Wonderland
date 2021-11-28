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
    private GameObject MeleeWeaponMetalSword;
    private GameObject SelectMode;
    private GameObject InGameMenu;
    private GameObject InGameMenuDeathInfo;
    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1;
        ThirdPersonCamera = GameObject.Find("vThirdPersonCamera");
        MeleeWeaponMetalSword = GameObject.Find("MeleeWeaponMetalSword");
        MeleeWeaponMetalSword.SetActive(false);
        SelectMode = GameObject.Find("SelectModePanel");
        MessageBox = GameObject.Find("GameMessage");
        MessageBoxText = GameObject.Find("GameMessageText");
        InGameMenu = GameObject.Find("InGameMenu");
        InGameMenuDeathInfo = GameObject.Find("InGameMenuDeathInfo");
        InGameMenuDeathInfo.SetActive(false);
        InGameMenu.SetActive(false);
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
        //inventory.AddItem(new Item { itemName = Item.ItemName.EmptyJar, number = 1 });
        MessageBox.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void FixedUpdate()
    {
        if (current_health <= 0)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            GetComponent<Invector.vHealthController>().ChangeHealth(-200);
            InGameMenuDeathInfo.SetActive(true);
        }
        if (current_hunger > 0)
        {
            ChangeHungerByDiff(-0.005f);
        }
        if (current_thirst > 0)
        {
            ChangeThirstByDiff(-0.005f);
        }
        if (current_hunger <= 0.005f)
        {
            ChangeHealthByDiff(-0.01f);
        }
        if (current_thirst <= 0.005f)
        {
            ChangeHealthByDiff(-0.01f);
        }
        if ((current_hunger > 85) && (current_thirst > 85))
        {
            ChangeHealthByDiff(0.005f);
        }
    }

    private void SetMessageBoxAndSetActive(string message)
    {
        MessageBoxText.GetComponent<TextMeshProUGUI>().SetText(message);
        MessageBox.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            Time.timeScale = 0;
            InGameMenu.SetActive(true);
        }
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
                if (ItemToPickUp.itemName == Item.ItemName.MetalSword)
                {
                    MeleeWeaponMetalSword.SetActive(true);
                    //GetComponent<Invector.vMelee.vMeleeManager>().rightWeapon = MeleeWeaponMetalSword.GetComponent<Invector.vMelee.vMeleeWeapon>();
                }
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

    public void ChangeHealthByDiff(float diff)
    {
        current_health += diff;
        if (current_health > max_health)
        {
            current_health = max_health;
        }
        if (current_health < 0)
        {
            current_health = 0;
        }
        health_bar.SetHealth(current_health);
    }

    public void ChangeHungerByDiff(float diff)
    {
        current_hunger += diff;
        if (current_hunger > max_hunger)
        {
            current_hunger = max_hunger;
        }
        if (current_hunger < 0)
        {
            current_hunger = 0;
        }
        hunger_bar.SetHunger(current_hunger);
    }

    public void ChangeThirstByDiff(float diff)
    {
        current_thirst += diff;
        if (current_thirst > max_thirst)
        {
            current_thirst = max_thirst;
        }
        if (current_thirst < 0)
        {
            current_thirst = 0;
        }
        thirst_bar.SetThirst(current_thirst);
    }

    
    private void OnTriggerEnter(Collider other)
    {
        
    }
    


    private void OnTriggerStay(Collider other)
    {
        if (other.name.Contains("Jar_Empty"))
        {
            SetMessageBoxAndSetActive("Press F to pick up");
            ItemToPickUp = new Item { itemName = Item.ItemName.EmptyJar, number = 1 };
            ItemToDestory = other.gameObject;
        }
        if (other.name.Contains("Jar_Full_Water"))
        {
            SetMessageBoxAndSetActive("Press F to pick up");
            ItemToPickUp = new Item { itemName = Item.ItemName.WaterJar, number = 1 };
            ItemToDestory = other.gameObject;
        }
        if (other.name.Contains("Jar_Full_Blood"))
        {
            SetMessageBoxAndSetActive("Press F to pick up");
            ItemToPickUp = new Item { itemName = Item.ItemName.BloodJar, number = 1 };
            ItemToDestory = other.gameObject;
        }
        if (other.name.Contains("Metal Sword"))
        {
            SetMessageBoxAndSetActive("Press F to pick up");
            ItemToPickUp = new Item { itemName = Item.ItemName.MetalSword, number = 1 };
            ItemToDestory = other.gameObject;
        }
        if (other.name.Contains("Slime_Meat"))
        {
            SetMessageBoxAndSetActive("Press F to pick up");
            ItemToPickUp = new Item { itemName = Item.ItemName.SlimeMeat, number = 1 };
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
        if (other.name.Contains("Jar_Full_Water"))
        {
            MessageBox.SetActive(false);
            ItemToPickUp = null;
            ItemToDestory = null;
        }
        if (other.name.Contains("Jar_Full_Blood"))
        {
            MessageBox.SetActive(false);
            ItemToPickUp = null;
            ItemToDestory = null;
        }
        if (other.name.Contains("Metal Sword"))
        {
            MessageBox.SetActive(false);
            ItemToPickUp = null;
            ItemToDestory = null;
        }
        if (other.name.Contains("Slime_Meat"))
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
