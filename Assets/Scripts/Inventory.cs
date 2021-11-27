using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Inventory
{
    public event EventHandler OnInventoryUpdated;
    private List<Item> inventory;

    GameObject items_spawner = GameObject.Find("ItemsSpawner");
    GameObject PlayerClara = GameObject.Find("Clara");

    public Inventory()
    {
        inventory = new List<Item>();
    }

    public List<Item> GetInventory()
    {
        return inventory;
    }

    public void AddItem(Item item)
    {
        if (item.CanStack())
        {
            bool itemNotInInventory = true;
            for (int i = 0; i < inventory.Count; i++)
            {
                if (inventory[i].itemName == item.itemName)
                {
                    inventory[i].number += item.number;
                    itemNotInInventory = false;
                }
            }
            if (itemNotInInventory == true)
            {
                inventory.Add(item);
            }
        }
        else
        {
            inventory.Add(item);
        }
        OnInventoryUpdated?.Invoke(this, EventArgs.Empty);
    }  

    public void DropItem(Item item)
    {
        if (item.itemName == Item.ItemName.MetalSword)
        {
            GameObject MeleeWeaponMetalSword = GameObject.Find("MeleeWeaponMetalSword");
            MeleeWeaponMetalSword.SetActive(false);
        }
        RemoveItem(item);
        items_spawner.GetComponent<SpawnPrefabs>().SpawnThis(item);
    }
    
    public void RemoveItem(Item item)
    {
        if (item.CanStack())
        {
            for (int i = 0; i < inventory.Count; i++)
            {
                if (inventory[i].itemName == item.itemName)
                {
                    inventory[i].number -= 1;
                    if(inventory[i].number == 0)
                    {
                        inventory.RemoveAt(i);
                    }
                    break;
                }
            }
        }
        else
        {
            inventory.Remove(item);
        }
        OnInventoryUpdated?.Invoke(this, EventArgs.Empty);
    }

    public bool HasItem(Item item)
    {
        for (int i = 0; i < inventory.Count; i++)
        {
            if (inventory[i].itemName == item.itemName)
            {
                return true;
            }
        }
        return false;
    }

    public void UseItem(Item item, string purpose=null)
    {
        switch (purpose)
        {
            default: break;
            case "fill_water":
                RemoveItem(item);
                AddItem(new Item { itemName = Item.ItemName.WaterJar, number = 1 });
                break;
            case null:
                switch (item.itemName)
                {
                    default: break;
                    case Item.ItemName.WaterJar:
                        PlayerClara.GetComponent<Clara>().ChangeThirstByDiff(30);
                        RemoveItem(item);
                        AddItem(new Item { itemName = Item.ItemName.EmptyJar, number = 1 });
                        break;
                    case Item.ItemName.BloodJar:
                        PlayerClara.GetComponent<Clara>().ChangeHealthByDiff(50);
                        RemoveItem(item);
                        AddItem(new Item { itemName = Item.ItemName.EmptyJar, number = 1 });
                        break;
                    case Item.ItemName.SlimeMeat:
                        PlayerClara.GetComponent<Clara>().ChangeHungerByDiff(30);
                        RemoveItem(item);
                        break;
                }
                break;
        }
    }
}
