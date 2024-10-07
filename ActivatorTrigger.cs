using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivatorTrigger : MonoBehaviour
{
    [SerializeField] private GameObject[] objectsToActivate;

    private void OnTriggerEnter(Collider other)
    {
        Activate();
    }
    public void Activate()
    {
        for (int i = 0; i < objectsToActivate.Length; i++)
        {
            objectsToActivate[i].SetActive(true);
        }
       
    }
}
