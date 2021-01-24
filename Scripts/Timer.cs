using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/*
 * Clase encargada de manejar el tiempo de espera entre rondas
 */
public class Timer : MonoBehaviour
{
    
    float timeLeft;     // Tiempo restante hasta que se acabe el temporizador
    float maxTime;      // Tiempo máximo en el que empezará el temporizador
    bool finished;      // Booleano que confirma que el temporizador se acabó
    Slider slider;      // Slider para mostrar visualmente dentro del juego el temporizador

    [SerializeField] GameObject nextWaveText;           // Gameobject del texto que avisa de que viene la siguiente ronda
    [SerializeField] GameObject nextWaveSliderTimer;    // Gameobject del slider usado para representar el temporizador

    /*
     * Establezco los valores y activo los gamebobject del texto y el slider
     */
    public void Start()
    {
        nextWaveText.SetActive(true);
        nextWaveSliderTimer.SetActive(true);
        slider = nextWaveSliderTimer.GetComponent<Slider>();
        maxTime = 7f;
        finished = false;
        timeLeft = maxTime;
        slider.value = timeLeft;
    }

    /*
     * Es el temporizador, reduce su valor en cada frame
     */
    void Update()
    {
       
        if (timeLeft > 0)
        {
            timeLeft -= Time.deltaTime;
            slider.value = timeLeft / maxTime;
        }
        else
        {
            TimerFinished();
        }
    }

    /*
     * Se llama una vez ha finalizado el temporizador y se encarga de apagar todo de nuevo y llamar a una funcion de la clase EnemiesSpawner
     */
    public void TimerFinished()
    {
        nextWaveText.SetActive(false);
        nextWaveSliderTimer.SetActive(false);
        GetComponent<EnemiesSpawner>().TimerFinished();
        this.enabled = false;
    }
}
