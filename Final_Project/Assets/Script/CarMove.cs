using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public float speed = 10.0f;      // Velocidad de avance
    public float turnSpeed = 100.0f; // Velocidad de giro
    private Rigidbody rb;            // Referencia al Rigidbody

    void Start()
    {
        rb = GetComponent<Rigidbody>(); // Obtén el Rigidbody
        if (rb == null)
        {
            Debug.LogError("No se encontró un Rigidbody en " + gameObject.name);
        }
        
        // Congelar las rotaciones en X y Z para evitar que el carro se voltee
        rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
    }

    void FixedUpdate()
    {
        // Movimiento hacia adelante y atrás
        float moveVertical = Input.GetAxis("Vertical"); // Teclas W/S o flechas arriba/abajo
        Vector3 moveDirection = transform.forward * moveVertical * speed;
        rb.MovePosition(rb.position + moveDirection * Time.fixedDeltaTime);

        // Rotación (giro)
        float moveHorizontal = Input.GetAxis("Horizontal"); // Teclas A/D o flechas izquierda/derecha
        Quaternion turnRotation = Quaternion.Euler(0f, moveHorizontal * turnSpeed * Time.fixedDeltaTime, 0f);
        rb.MoveRotation(rb.rotation * turnRotation);

        // Asegurarse de que no rote en X o Z por físicas adicionales
        rb.rotation = Quaternion.Euler(0f, rb.rotation.eulerAngles.y, 0f);
    }
}
