using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class returnButton : MonoBehaviour
{
    private void Update()
    {
    if (Input.GetKeyUp(KeyCode.Escape))
        {
            SceneManager.LoadScene("Menu screen");
        }
        
    }
}
