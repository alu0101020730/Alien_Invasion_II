using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
 * Clase encargada de instanciar a las naves subditas del boss
 */
public class bossMinionSpawner : MonoBehaviour
{ 
    [SerializeField] GameObject minion1;    // Nave 1
    [SerializeField] GameObject minion2;    // Nave 2

    void Start()
    {
        StartCoroutine(SpawnMinions());
    }

    /*
     * Instancia a ambas naves 
     */
    private IEnumerator SpawnMinions()
    {
        
        GameObject minionShip1 = Instantiate(minion1, minion1.transform.position, Quaternion.identity) as GameObject;
        minionShip1.GetComponent<bossMinion>().setDelegate(gameObject);
        GameObject minionShip2 = Instantiate(minion2, minion2.transform.position, Quaternion.identity) as GameObject;
        minionShip2.GetComponent<bossMinion>().setDelegate(gameObject);
        yield return new WaitForSeconds(10f);
    }

}
