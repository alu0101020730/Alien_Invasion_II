using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*
 * Clase que controla la puntuación del jugador y la interfaz de la tienda.
 */
public class Shop : MonoBehaviour
{

    public TextMesh scoreText = null;

    public int score = 0;               // Puntuación actual del jugador.
    public int money = 50;              // Dinero actual del jugador.

    
    public Text TextScore;
    public Text TextMoney;
    public static Shop shop;
    public AudioSource money_sound;     // Sonido al obtener dinero.
    public AudioSource main_music;      // Música principal del juego.

    /*
     * Inicializa la música del juego.
     */
    private void Awake()
    {
        main_music.volume = 0.08F;
        main_music.loop = true;
        main_music.Play();

        shop = this;
    }

    /*
     * Actualiza el texto con la información del dinero y la puntuación del jugador.
     */
    void Update()
    {
        if(TextScore != null)
        {
            TextScore.text = "Score: " + score.ToString();
        }

        if (TextMoney != null)
        {
            TextMoney.text = money.ToString();
        }
    }
    
    /*
     * Añade la puntuación que le indica 'newValue'.
     */
    public void AddScore(int newValue)
    {
        score += newValue;
    }

    /*
     * Añade el dinero que le indica 'newValue'.
     */
    public void AddMoney(int newValue)
    {
        money_sound.Play();
        money += newValue;
    }

    /*
     * Le quita el dinero que le indica 'newValue'.
     */
    public void RemoveMoney(int newValue)
    {
        money -= newValue;
    }

}
