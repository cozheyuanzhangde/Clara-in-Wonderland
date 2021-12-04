using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingBoardAnim : MonoBehaviour
{
    // Start is called before the first frame update
    private GameObject Clara;
    void Start()
    {
        Clara = GameObject.Find("Clara");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.name.Contains("Foot_trigger"))
        {
            Clara.transform.parent = transform;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.name.Contains("Foot_trigger"))
        {
            Clara.transform.parent = null;
        }
    }
}
