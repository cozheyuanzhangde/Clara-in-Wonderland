using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryItems : MonoBehaviour
{
    public static InventoryItems all { get; private set; }

    private void Awake()
    {
        all = this;
    }

    public Sprite default_img;
    public Sprite empty_jar;
    public Sprite water_jar;
    public Sprite blood_jar;
    public Sprite metal_sword;
}
