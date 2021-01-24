using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
/*
 * Clase encargada de manejar la vida de las naves basicas enemigas
 */
public class EnemyHealth : MonoBehaviour
{
    int maxHealth;                      // Vida maxima del enemigo
    public int currentHealth;           // Vida actual del enemigo
    
    public GameObject player;           // Jugador
    private Shop shop;                  // Tienda
    public GameObject clipDestroy;      // Sonido cuando se destruye

    [SerializeField] Slider healthBar;  // Barra de vida

    private void Awake()
    {
        maxHealth = 100;
        clipDestroy = GameObject.FindGameObjectWithTag("Player");
        shop = GameObject.FindGameObjectWithTag("UI").GetComponent<Shop>();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    /*
     * Establece la vida maxima y actual mas un extra
     */
    public void SetHealth(int extraHP)
    {
        currentHealth = maxHealth + extraHP;
        healthBar.maxValue = maxHealth + extraHP;
        healthBar.value = maxHealth + extraHP;
    }

    /*
     * Actualiza la barra de vida
     */
    public void UpdateHealthBar()
    {
        healthBar.value = currentHealth;
    }

    /*
     * Hace que el jugador empiece a disparar
     */
    public void MakePlayerStartShooting()
    {
        player.GetComponent<Weapon>().enemyOnFocus = gameObject;
        player.GetComponent<Weapon>().StartShootingFunc();
    }

    /*
     * Hace que el jugador deje de disparar
     */
    public void MakePlayerStopShooting()
    {
        player.GetComponent<Weapon>().StopShootingFunc();
        player.GetComponent<Weapon>().enemyOnFocus = null;
    }

    /*
     * Maneja el daño recibido
     */ 
    public void GetDMG(int dmg)
    {
        currentHealth -= dmg;

        UpdateHealthBar();
        if (currentHealth <= 0)
        {
            if (player.GetComponent<Weapon>().startShooting)
            {
                MakePlayerStopShooting();
            }
            shop.AddScore(100);
            shop.AddMoney(20);
            clipDestroy.GetComponent<AudioSource>().Play();
            Destroy(gameObject);
        }
              
    }


}
