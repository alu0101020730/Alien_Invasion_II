using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/*
 * Clase que controla el menu de Game Over.
 */
public class GameOverMenu : MonoBehaviour
{

    private SaveLoad load;
    public Text scoreText;           // Texto que muestra la puntuación actual del jugador.
    public Text top1ScoreText;       // Texto que muestra el top 1 de la tabla de puntuaciones.
    public Text top2ScoreText;       // Texto que muestra el top 2 de la tabla de puntuaciones.
    public Text top3ScoreText;       // Texto que muestra el top 3 de la tabla de puntuaciones.

    public AudioSource game_over_sound; // Música que se escucha en la escena de Game Over.

    /*
     * Inicializa algunas variables, carga la puntuación del jugador y el top 3 de la tabla de puntuaciones.
     */
    void Start()
    {
        game_over_sound.volume = 0.2F;
        game_over_sound.loop = true;
        game_over_sound.Play();


        load = GameObject.FindGameObjectWithTag("UI").GetComponent<SaveLoad>();

        scoreText = GameObject.FindGameObjectWithTag("actualScore").GetComponent<Text>();
        top1ScoreText = GameObject.FindGameObjectWithTag("top1").GetComponent<Text>();
        top2ScoreText = GameObject.FindGameObjectWithTag("top2").GetComponent<Text>();
        top3ScoreText = GameObject.FindGameObjectWithTag("top3").GetComponent<Text>();

        scoreText.text = "Score: " + load.loadActualScore().ToString();
        top1ScoreText.text = "1.     " + load.loadTop1Score().ToString();
        top2ScoreText.text = "2.     " + load.loadTop2Score().ToString();
        top3ScoreText.text = "3.     " + load.loadTop3Score().ToString();
    }

    /*
     * Cuando se acciona le botón de restart, cambia a la escena del juego principal.
     */
    public void restartGame(){
        SceneManager.LoadScene("EscenaMarcianos");
    }

    /*
     * Cuando se acciona le botón de backMenu, cambia a la escena menú principal.
     */
    public void backMainMenu(){
        SceneManager.LoadScene("UI");
    }

}
