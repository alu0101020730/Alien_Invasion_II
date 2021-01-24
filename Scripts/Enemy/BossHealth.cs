using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
/*
 * Clase encargada de la vida del boss
 */
public delegate void delegatedMethodBoss();
public class BossHealth : MonoBehaviour
{
    public event delegatedMethodBoss death;       // Evento delegado

    int maxHealth = 400;        // Vida Maxima
    int currentHealth;          // Vida actual
    
    GameObject player;          // Jugador
    Shop shop;                  // Tienda
    Slider healthBar;           // Barra de vida

    [SerializeField] Slider healthBarPrefab;        // Prefab de la barra de vida para instanciar


    private void Awake()
    {
        shop = GameObject.FindGameObjectWithTag("UI").GetComponent<Shop>();
        maxHealth = 400;
        player = GameObject.FindGameObjectWithTag("Player");
        GameObject canvas = GameObject.Find("Player_UI");
        healthBar = (Slider)Instantiate(healthBarPrefab); 
        healthBar.transform.SetParent(canvas.transform, false);
        SetHealthStats();
    }

    /*
     * Establece la vida la primera vez
     */
    public void SetHealthStats()
    {
        currentHealth = maxHealth;
        healthBar.maxValue = maxHealth;
        healthBar.value = maxHealth;
    }

    /*
     * Establece la vida maxima y actual mas un extra
     */
    public void SetHealthStats(int extraHP)
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
     * Hace que el jugador pare de disparar
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
            shop.AddScore(1000);
            shop.AddMoney(200);
            death();
            Destroy(healthBar.transform.gameObject);
            Destroy(gameObject);
        }

    }


}
