using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEngine;
/*
 * Clase encargada de guardar y cargar los datos de la puntuación del jugador
 */
public class SaveLoad : MonoBehaviour
{

    private int actualScore = 0;    // Score obtenida en la partida actual

    private int top1Score = 0;      // Score del top 1 de la tabla de puntuaciones
    private int top2Score = 0;      // Score del top 2 de la tabla de puntuaciones
    private int top3Score = 0;      // Score del top 3 de la tabla de puntuaciones


    private void Start()
    {
        actualScore = PlayerPrefs.GetInt("actualScore");
        top1Score = PlayerPrefs.GetInt("top1");
        top2Score = PlayerPrefs.GetInt("top2");
        top3Score = PlayerPrefs.GetInt("top3");
    }

    /*
     * Guarda la puntuación actual del jugador y en caso de superar la puntuación de alguien,
     * se encarga de organizar el top 3 en el orden correspondiente.
     */
    public void saveScore(int sc)
    {
        int auxScore1 = 0;
        int auxScore2 = 0;

        PlayerPrefs.SetInt("actualScore", sc);
        if (sc > top1Score) {
            auxScore1 = top1Score;
            auxScore2 = top2Score;
            PlayerPrefs.SetInt("top1", sc);
            PlayerPrefs.SetInt("top2", auxScore1);
            PlayerPrefs.SetInt("top3", auxScore2);
        } else if ((sc > top2Score)) {
            auxScore1 = top2Score;
            PlayerPrefs.SetInt("top2", sc);
            PlayerPrefs.SetInt("top3", auxScore1);
        } else if ((sc > top3Score)){
            PlayerPrefs.SetInt("top3", sc);
        }
    }

    /*
     * Carga la puntuación actual del jugador.
     */
    public int loadActualScore() {
        return PlayerPrefs.GetInt("actualScore");
    }

    /*
     * Carga la puntuación del top 1.
     */
    public int loadTop1Score() {
        return PlayerPrefs.GetInt("top1");
    }

    /*
     * Carga la puntuación del top 2.
     */
    public int loadTop2Score() {
        return PlayerPrefs.GetInt("top2");
    }

    /*
     * Carga la puntuación del top 3.
     */
    public int loadTop3Score() {
        return PlayerPrefs.GetInt("top3");
    }
}
