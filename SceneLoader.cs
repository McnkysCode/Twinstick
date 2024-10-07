using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    [SerializeField] private int scenenumber;


 

    public void sceneloader()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + scenenumber);
    }


    private void OnCollisionEnter(Collision collision)
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + scenenumber);
        Debug.Log("collisone");
    }

    public void mainMenu()
    {
        SceneManager.LoadScene("Menu screen");
    }
    public void Credits()
    {
        SceneManager.LoadScene("Credits");
    }
    public void startGame()
    {
        SceneManager.LoadScene("testtte");
    }

}
