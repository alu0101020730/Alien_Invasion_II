using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Clase que permite mover al jugador a través del mapa.
 */
public class playermov : MonoBehaviour
{
    // Declaración de variables
    public Transform vrCamera;          // Camara del jugador
    public float toggleAngle = 30.0f;   // Ángulo necesario para que el jugador se mueva.
    public float MovementSpeed;         // Velocidad de movimiento.
    public bool MoveForward;
    private CharacterController cc;
    
    /*
     * Establecemos el valor de algunas variables.
     */
    void Start()
    {
        MovementSpeed = 30.0f;
        cc = GetComponent<CharacterController>();
    }

    /*
     * En caso de que se baje la cámara un determinado ángulo el jugador se mueve.
     */
    void Update()
    {
        if (vrCamera.eulerAngles.x >= toggleAngle && vrCamera.eulerAngles.x < 90.0f) {
            MoveForward = true;
        } else {
            MoveForward = false;
        }
        if (MoveForward) {
            Vector3 forward = vrCamera.TransformDirection(Vector3.forward);
            cc.SimpleMove(forward * MovementSpeed);
        }
    }

}
