using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
 * Clase encargada del disparo de las naves básicas enemigas
 */
public class EnemyShooting : MonoBehaviour
{
    Renderer rend;          // Componente Renderer de la propia nave
    float minTimeShoot;     // Tiempo minimo para el siguiente disparo
    float maxTimeShoot;     // Tiempo máximo para el siguiente disparo

    [SerializeField] GameObject bullet;    // Bala que se va a instanciar

    private void Start()
    {
        minTimeShoot = 4f;
        maxTimeShoot = 5f;
        rend = GetComponent<Renderer>();
    }

    /*
     * Llama a la corrutina para empezar a disparar
     */
    public void Shoot()
    {
        StartCoroutine(ShootCoroutine());   
    }

    /*
     * Corrutina encargada del disparo, su cadencia y de instanciar la bala
     */
    private IEnumerator ShootCoroutine()
    {
        yield return new WaitForSeconds(Random.Range(minTimeShoot, maxTimeShoot));
        while(true)
        {
            var bulletInstance = Instantiate(bullet, transform, false);
            bulletInstance.transform.position = rend.bounds.center;
            bulletInstance.transform.SetParent(null);
            yield return new WaitForSeconds(Random.Range(minTimeShoot,maxTimeShoot));
        }
    }
}
