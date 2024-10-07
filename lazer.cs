using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lazer : MonoBehaviour
{
    private LineRenderer lr;
    [SerializeField]int rangeGun;

    private void Start()
    {
        lr = GetComponent<LineRenderer>();
    }

    private void Update()
    {
        RaycastHit hit;

        if(Physics.Raycast(transform.position, transform.forward, out hit, (float)rangeGun))
        {
            if (hit.collider)
            {
                lr.SetPosition(1, new Vector3(hit.distance, 0,0));
            }
        }
        else
        {
            lr.SetPosition(1, new Vector3(rangeGun, 0,0));
        }
    }
}
