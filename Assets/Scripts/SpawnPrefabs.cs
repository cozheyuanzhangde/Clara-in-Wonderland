using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPrefabs : MonoBehaviour
{
    private GameObject PlayerClara;
    public GameObject empty_jar_prefab;
    public GameObject water_jar_prefab;
    public GameObject blood_jar_prefab;
    public GameObject metal_sword_prefab;
    public GameObject slime_meat_prefab;
    // Start is called before the first frame update
    void Start()
    {
        PlayerClara = GameObject.Find("Clara");

        empty_jar_prefab.GetComponent<MeshCollider>().convex = true;
        empty_jar_prefab.GetComponent<MeshCollider>().isTrigger = true;
        water_jar_prefab.GetComponent<MeshCollider>().convex = true;
        water_jar_prefab.GetComponent<MeshCollider>().isTrigger = true;
        blood_jar_prefab.GetComponent<MeshCollider>().convex = true;
        blood_jar_prefab.GetComponent<MeshCollider>().isTrigger = true;
        slime_meat_prefab.GetComponent<MeshCollider>().convex = true;
        slime_meat_prefab.GetComponent<MeshCollider>().isTrigger = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SpawnThis(Item item)
    {
        switch (item.itemName)
        {
            default: break;
            case Item.ItemName.EmptyJar:
                Instantiate(empty_jar_prefab, PlayerClara.transform.position, Quaternion.Euler(new Vector3(-90,0,0)));
                break;
            case Item.ItemName.WaterJar:
                Instantiate(water_jar_prefab, PlayerClara.transform.position, Quaternion.Euler(new Vector3(-90, 0, 0)));
                break;
            case Item.ItemName.BloodJar:
                Instantiate(blood_jar_prefab, PlayerClara.transform.position, Quaternion.Euler(new Vector3(-90, 0, 0)));
                break;
            case Item.ItemName.SlimeMeat:
                Instantiate(slime_meat_prefab, PlayerClara.transform.position, Quaternion.Euler(new Vector3(0, 0, 0)));
                break;
            case Item.ItemName.MetalSword:
                GameObject MetalSwordSpawned = Instantiate(metal_sword_prefab, PlayerClara.transform.position + new Vector3(0, 0.02f, 0), Quaternion.Euler(new Vector3(-90, 0, 0)));
                MetalSwordSpawned.transform.localScale = new Vector3(0.15f, 0.15f, 0.15f);
                break;
        }
    }
}
