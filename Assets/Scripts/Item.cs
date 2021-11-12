using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item
{
    public enum ItemName
    {
        EmptyJar,
        WaterJar,
    }

    public ItemName itemName;
    public int number;

    public Sprite GetSprite()
    {
        switch (itemName)
        {
            default: return InventoryItems.all.empty_jar; //TODO: change this to a default sprite
            case ItemName.EmptyJar: return InventoryItems.all.empty_jar;
            case ItemName.WaterJar: return InventoryItems.all.water_jar;
        }
    }

    public bool CanStack()
    {
        switch (itemName)
        {
            default: return false;
            case ItemName.EmptyJar: return true;
            case ItemName.WaterJar: return true;
        }
    }
}
