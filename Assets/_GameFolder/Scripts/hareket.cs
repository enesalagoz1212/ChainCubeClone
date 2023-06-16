using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hareket : MonoBehaviour
{
    public float moveSpeed = 5f;     // Hareket hýzý
    public float forwardForce = 10f; // Ýleri yönde uygulanan kuvvet

    private bool isMoving = false;   // Hareket durumu
    private float initialX;          // Baþlangýç X pozisyonu
    private Rigidbody rb;            // Rigidbody bileþeni

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        initialX = transform.position.x;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            isMoving = true;
        }

        if (Input.GetMouseButtonUp(0))
        {
            isMoving = false;
            rb.AddForce(transform.forward * forwardForce, ForceMode.Impulse);
        }

        if (isMoving)
        {
            float mouseX = Input.GetAxis("Mouse X");
            float targetX = initialX + mouseX;
            Vector3 targetPosition = new Vector3(targetX, transform.position.y, transform.position.z);
            transform.position = Vector3.Lerp(transform.position, targetPosition, moveSpeed * Time.deltaTime);
        }
    }
}
