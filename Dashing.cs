using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.UI;

public class Dashing : MonoBehaviour
{
    [SerializeField] private float dashDuration = 0.5f;
    [SerializeField] private KeyCode dashKey = KeyCode.LeftShift;
    [SerializeField] private float dashDistance = 10f;
    [SerializeField] private CharacterController character;
    [SerializeField] private float dashCooldown = 2f;
    private bool isDashCooldown = false;
    private bool isDashing = false;

    [SerializeField] private Text cooldownText;

    private void Start()
    {
        character = GetComponent<CharacterController>();
    }

    void Update()
    {
        if (!isDashing && !isDashCooldown)
        {
            if (Input.GetKeyDown(dashKey))
            {
                StartCoroutine(Dash());
            }
        }
    }

    IEnumerator Dash()
    {
        isDashing = true;

        character.enabled = false;

        Vector3 initialPosition = transform.position;

        Vector3 dashEndPosition = initialPosition + (transform.forward * dashDistance);

        float elapsedTime = 0f;
        while (elapsedTime < dashDuration)
        {
            transform.position = Vector3.Lerp(initialPosition, dashEndPosition, elapsedTime / dashDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.position = dashEndPosition;
        character.enabled = true;

        
        isDashCooldown = true;
        float remainingTime = dashCooldown;
        while (remainingTime > 0f)
        {
            cooldownText.text = remainingTime.ToString("F1");
            remainingTime -= Time.deltaTime;
            yield return null;
        }
        cooldownText.text = "";
        isDashCooldown = false;

        isDashing = false;
    }
}