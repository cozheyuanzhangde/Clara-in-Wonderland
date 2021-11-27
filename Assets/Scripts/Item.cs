using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item
{
    public enum ItemName
    {
        EmptyJar,
        WaterJar,
        BloodJar,
        MetalSword,
        SlimeMeat,
    }

    public ItemName itemName;
    public int number;

    public Sprite GetSprite()
    {
        switch (itemName)
        {
            default: return InventoryItems.all.default_img; //TODO: change this to a default sprite
            case ItemName.EmptyJar: return InventoryItems.all.empty_jar;
            case ItemName.WaterJar: return InventoryItems.all.water_jar;
            case ItemName.BloodJar: return InventoryItems.all.blood_jar;
            case ItemName.MetalSword: return InventoryItems.all.metal_sword;
            case ItemName.SlimeMeat: return InventoryItems.all.slime_meat;
        }
    }

    public bool CanStack()
    {
        switch (itemName)
        {
            default: return false;
            case ItemName.EmptyJar: return true;
            case ItemName.WaterJar: return true;
            case ItemName.BloodJar: return true;
            case ItemName.MetalSword: return false;
            case ItemName.SlimeMeat: return true;
        }
    }
}
