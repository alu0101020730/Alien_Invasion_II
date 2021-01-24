using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/*
 * Clase que controla el menu principal del juego.
 */
public class Menu : MonoBehaviour
{

    public AudioSource intro_sound; // Música que se escucha en la escena del menú principal.

    /*
     * Establecemos el valor de algunas variables.
     */
    private void Start()
    {
        intro_sound.volume = 0.2F;
        intro_sound.loop = true;
        intro_sound.Play();
    }

    /*
     * Cuando el jugador selecciona el botón Play, cambiamos a la escena del juego.
     */
    public void Play()
    {
        SceneManager.LoadScene("EscenaMarcianos");
    }


}
