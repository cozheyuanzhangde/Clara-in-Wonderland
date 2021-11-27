using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InGameMenuCtrl : MonoBehaviour
{
    public void ClickResume()
    {
        Time.timeScale = 1;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        gameObject.SetActive(false);
    }

    public void ClickMainMenu()
    {
        SceneManager.LoadScene("Menu");
    }
}
