using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
 * Clase encargada del disparo de las naves del boss, 
 * clase muy parecida a la de EnemyShooting pero esta no usa el componente renderer para instanciar la bala ya que los modelos son diferentes
 */
public class bossMinionsShooting : MonoBehaviour
{
    const int ROTATION_SPEED = 1000;

    float minTimeShoot;     // Tiempo minimo para el siguiente disparo
    float maxTimeShoot;     // Tiempo maximo para el siguiente disparo

    [SerializeField] GameObject bullet;     // Bala que se va a instanciar

    private void Awake()
    {
        minTimeShoot = 4f;
        maxTimeShoot = 5f;
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
            bulletInstance.transform.localScale = new Vector3(0.5f, 1f, 0.5f);
            bulletInstance.transform.SetParent(null);
            yield return new WaitForSeconds(Random.Range(minTimeShoot, maxTimeShoot));
        }
    }
}
