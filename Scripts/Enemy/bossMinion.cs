using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
 * Clase encargada de manejar a las naves subditas del boss
 */
public class bossMinion : MonoBehaviour
{
    const int ROTATION_SPEED = 1000;        // Velocidad de rotación a la que girará la nave una vez sea instanciada para mirar hacia el jugador

    public GameObject player;               // Jugador
    BossHealth bossHealth;                  // Clase de la vida del boss, tiene el metodo delegado

    void Start()
    {
        player = GameObject.FindWithTag("Player");
    }

    /*
     * Establece el metodo delegado
     */
    public void setDelegate(GameObject boss)
    {
        bossHealth = boss.GetComponent<BossHealth>();
        bossHealth.death += Delete;
    }

    void Update()
    {
        LookTarget();   
    }

    /*
     * Hace que mire hacia el jugador
     */
    private void LookTarget()
    {
        
        var direction = (player.transform.position - transform.position).normalized;
        var lookRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * ROTATION_SPEED);
        transform.rotation = Quaternion.Euler(20, transform.eulerAngles.y, transform.eulerAngles.z);
    }

    /*
     * Destruye la nave
     */
    public void Delete()
    {
        Destroy(gameObject);
    }
}
