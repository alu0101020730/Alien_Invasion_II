using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

/*
 * Clase que controla la vida del jugador.
 */
public class PlayerHp : MonoBehaviour
{
    // Declaración de las variables
    public float hp;                        // Vida actual del jugador.
    public float hpMax;                     // Vida máxima que puede obtener el jugador.

    private Shop shop;
    private SaveLoad save;

    public Text TextLife;                   

    public Button MaxLifeBuff;              // Botón para subir la vida máxima del jugador.
    public Button RecoveryLife;             // Botón para recuperar la vida del jugador.

    public AudioSource restore_health_sound; // Sonido de recuperación de vida.
   
    /*
     * Establecemos el valor de algunas variables.
     */
    void Start() {
        shop = GameObject.FindGameObjectWithTag("UI").GetComponent<Shop>();
        save = GameObject.FindGameObjectWithTag("UI").GetComponent<SaveLoad>();
        hpMax = 100f;
        hp = hpMax;
        MaxLifeBuff.onClick.AddListener(maxLifeUp);
        RecoveryLife.onClick.AddListener(recoveryLife);
    }

    /*
     * Va mostrando en la UI la vida actual y máxima del jugador.
     */
    void Update()
    {
        if (TextLife != null)
        {
            TextLife.text = "Life: " + hp.ToString() + " / " + hpMax.ToString();
        }
    }

    /*
     * Aumenta la vida máxima del jugador si tiene dinero suficiente.
     */
    public void maxLifeUp() {
        if (shop.money >= 200)
        {
            shop.RemoveMoney(200);
            hpMax += 20;
            hp += 20;
            restore_health_sound.Play();
        }
    }

    /*
     * Recupera la vida del jugador si tiene dinero suficiente.
     */
    public void recoveryLife() {
        if (shop.money >= 80)
        {
            shop.RemoveMoney(80);
            hp = hpMax;
            restore_health_sound.Play();
        }
    }
    
    /*
     * Utilización de collider para si el jugador colisiona con una bala bajarle hp,
     * en caso de perder toda la vida, cambiamos a la escena de Game Over y guardamos la puntuación.
     */
    private void OnTriggerExit(Collider other) {
        if(other.gameObject.tag == "Bullet") {
            hp -= 10;
            if (hp <= 0) {
                save.saveScore(shop.score);
                SceneManager.LoadScene("GameOver");
            }
        }
    }
}
