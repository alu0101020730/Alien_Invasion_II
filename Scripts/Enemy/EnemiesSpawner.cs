using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
 * Clase encargada del manejo de la aparición de enemigos, las rondas y la dificultad
 */
public class EnemiesSpawner : MonoBehaviour
{
    const int ROTATION_SPEED = 1000;        // Velocidad de rotación a la que girará la nave cuando se instancie para mirar hacia el jugador
    const int ROUNDSTOBOSS = 4;             // Cantidad de rondas para que aparezca el boss
    const int MAXENEMIES = 5;               // Cantidad máxima de enmigos

    int enemyQuantity;                      // Cantidad de enemigos que aparecerá en cada ronda
    int countToBossRound;                   // Contador de rondas restantes hasta que aparezca el boss
    int extraHPMinions;                     // Vida extra que se les sumará a los minions cada vez que se aumente la dificultad
    int extraHPBoss;                        // Vida extra que se les sumará al boss cada vez que se aumente la dificultad
    float radius;                           // Radio al que aparecerán todas las naves enemigas
    bool allEnemiesSpawned;                 // Booleano que comprueba si han aparecido todos los enemigos
    bool waitForNextRound;                  // Booleano que comprueba si hay que esperar hasta que termine el timer para empezar la siguiente ronda
    bool startTimer;                        // Booleano que comprueba si hay que empezar el timer
    bool timerFinished;                     // Booleano que comprueba si el timer terminó

    [SerializeField] GameObject ship;           // Nave basica enemiga
    [SerializeField] GameObject boss;           // Boss
    [SerializeField] GameObject player;         // Jugador
    [SerializeField] GameObject bossSign;       // Texto de boss

    public AudioSource boss_sound;              // Sonido que se reproduce cuando se va a spawnear el boss

    void Start()
    {
        boss_sound.volume = 0.3F;
        extraHPMinions = 0;
        extraHPBoss = 0;
        radius = 200;
        enemyQuantity = 1;
        allEnemiesSpawned = false;
        waitForNextRound = false;
        countToBossRound = ROUNDSTOBOSS;
        startTimer = false;
        timerFinished = false;
        StartCoroutine(SpawnEnemies());
    }

    /*
     * Comprueba si hay algun enemigo vivo
     */
    bool CheckIfAnyEnemyAlive()
    {
        if (transform.childCount == 0)
            return false;
        else 
            return true;
    }

    /*
     * Comprueba cuando hay que empezar el temporizador
     */
    private void Update()
    { 
        if (allEnemiesSpawned)
        {
            startTimer = !CheckIfAnyEnemyAlive();
        }
    }

    /*
     * Comprueba si el timer ya terminó
     */
    public void TimerFinished()
    {
        timerFinished = true;
    }

    /*
     * Cada ronda los minions reciben 10 mas de vida
     */
    void MakeItHarderMinions()
    {
        extraHPMinions += 10;
    }

    /*
     * Cada vez que el boss aparece consigue 25 mas de vida
     */
    void MakeItHarderBoss() 
    {
        extraHPBoss += 25;
    }

    /*
     * Cada ronda 2 y 0, aparece un minion mas, maximo 5
     */
    void AddEnemy()
    {
        if (countToBossRound == 2 || countToBossRound == 0)
            if (enemyQuantity < MAXENEMIES)
                enemyQuantity++;
    }

    /*
     * Corrutina encargada de manejar las rondas
     */
    private IEnumerator SpawnEnemies()
    {

        bool startTimerOnce = true;
        
        while (true)
        {
            if (countToBossRound != 0 && !waitForNextRound)
            {
                for (int i = 0; i < enemyQuantity; i++)
                {
                    SpawnMinion();
                    yield return new WaitForSeconds(1f);
                }
                countToBossRound--;
                AddEnemy();
                waitForNextRound = true;
                allEnemiesSpawned = true;
            }
            else if (countToBossRound == 0 && !waitForNextRound)
            {
                boss_sound.Play();
                for (int i = 0; i < 8; i++)
                {
                    bossSign.SetActive(!bossSign.activeSelf);
                    yield return new WaitForSeconds(0.5f);
                }
                
                SpawnBoss();
                countToBossRound = ROUNDSTOBOSS;
                waitForNextRound = true;
                allEnemiesSpawned = true;
                startTimerOnce = true;
                MakeItHarderBoss();

            }
            else if(allEnemiesSpawned && waitForNextRound && startTimer && startTimerOnce)
            {
                GetComponent<Timer>().enabled = true;
                GetComponent<Timer>().Start();
                startTimerOnce = false;
            }
            else if (timerFinished)
            {
                timerFinished = false;
                startTimer = false;
                startTimerOnce = true;
                allEnemiesSpawned = false;
                waitForNextRound = false;
                MakeItHarderMinions();
            }
            yield return null;
        }
    }

    /*
     * Instancia al boss
     */
    private void SpawnBoss()
    {
        GameObject bosship = Instantiate(boss, boss.transform.position, Quaternion.identity) as GameObject;
        bosship.GetComponent<BossHealth>().SetHealthStats(extraHPBoss);
        bosship.transform.parent = gameObject.transform;
    }

    /*
     * Instancia a una nave enemiga
     */
    private void SpawnMinion()
    {
        float angle = Random.Range(0f, 1f) * Mathf.PI * 2;
        float x = Mathf.Cos(angle) * radius;
        float z = Mathf.Sin(angle) * radius;
        GameObject spaceship = Instantiate(ship, new Vector3(x + transform.position.x, 10, z + transform.position.z), Quaternion.identity) as GameObject;
        spaceship.GetComponent<EnemyHealth>().SetHealth(extraHPMinions);
        Vector3 direction = (player.transform.position - spaceship.gameObject.transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(-direction);
        spaceship.transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * ROTATION_SPEED);
        spaceship.transform.rotation = Quaternion.Euler(-30, spaceship.transform.eulerAngles.y, spaceship.transform.eulerAngles.z);
        spaceship.transform.parent = gameObject.transform;
    }
}
