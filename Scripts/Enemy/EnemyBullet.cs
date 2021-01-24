using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
 * Clase encargada de representar las balas de la naves enmigas
 */
public class EnemyBullet : MonoBehaviour
{
    const int ROTATION_SPEED = 1000;    // Velocidad de rotación a la que girará la bala una vez sea instanciada para mirar hacia el jugador

    Vector3 playerPos;      // Posicion del jugador
    Vector3 direction;      // Direccion hacia la que se moverá la bala
    float speed;            // Velocidad a la que se moverá la bala

    GameObject player;      // Jugador

    /*
     * Se establecen los valores y la rotación
     */
    void Start()
    {
        speed = 30f;
        player = GameObject.FindWithTag("Player");
        playerPos = player.transform.position;
        direction = (playerPos - transform.position).normalized;
        var lookRotation = Quaternion.LookRotation(-direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * ROTATION_SPEED);
        transform.rotation = Quaternion.Euler(80, transform.eulerAngles.y, transform.eulerAngles.z);
    }

    private void Update()
    {
        Move();
    }

    /*
     * Mueve la bala hacia la posición del jugador en el momento que fue disparada
     */
    private void Move()
    {
        transform.Translate(direction * speed * Time.deltaTime, Space.World);
    }

    /*
     * Si se choca con cualquiero cosa que no sea el jugador o el enemigo, se destruirá
     */
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag != "Player" && other.gameObject.tag != "Enemy")
        {
            Destroy(gameObject);
        }
    }

}
