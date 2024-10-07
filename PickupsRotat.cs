using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupsRotat : MonoBehaviour
{
    [SerializeField] private float floatSpeed = 0.5f;    // Snelheid van het op en neer vliegen
    [SerializeField] private float rotateSpeed = 60f;   // Snelheid van het draaien om de Y-as
    [SerializeField] private float floatHeight = 1f;    // Hoogte van het op en neer vliegen

    private Vector3 initialPosition;    // Oorspronkelijke positie van het object

    void Start()
    {
        initialPosition = transform.position;    // Sla de oorspronkelijke positie op
    }

    void Update()
    {
        // Beweeg het object op en neer met behulp van de sinusfunctie
        float newY = initialPosition.y + Mathf.Sin(Time.time * floatSpeed) * floatHeight;
        transform.position = new Vector3(transform.position.x, newY, transform.position.z);

        // Draai het object om de Y-as
        float newRotationY = transform.rotation.eulerAngles.y + rotateSpeed * Time.deltaTime;
        transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, newRotationY, transform.rotation.eulerAngles.z);
    }
}
