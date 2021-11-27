using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuCtrl : MonoBehaviour
{
    public void ClickStart()
    {
        SceneManager.LoadScene("World1");
    }
}
