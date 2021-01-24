using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
 * Clase encargada del movimiento de las naves basicas enemigas
 */
public class EnemyMovement : MonoBehaviour
{
    const int ROTATION_SPEED = 1000;    // Velocidad de rotación a la que girará la nave una vez sea instanciada para mirar hacia el jugador
    const int RADIUS = 30;              // Distancia del centro del mapa a la que la nave empezará a girar en circulos

    float swingTime;                    // Tiempo que dura cada balanceo vertical de la nave
    float degreesPerSecond;             // Grados a los que girará la nave para moverse en circulos
    int yAltitudeToStartMoving;         // Altitud a la que la nave empezará a moverse hacia el centro
    Vector3 auxVec;                     // Vector auxiliar usado para calcular el giro circular de la nave
    Vector3 risePosition;               // Posicion hacia la que la nave se dirige cuando se instancia
    bool rised;                         // Booleano que confirma si la nave ya ha llegado a la altura o todavía no
    bool startSpinning;                 // Booleano que confirma si la nave debe empezar a girar en circulos
    GameObject player;                  // Objeto jugador usado para que la nave mire hacia él
        
    [SerializeField] GameObject center;     // Posición central a la que se dirije la nave una vez se haya elevado a la altura mínima

    void Awake()
    {
        yAltitudeToStartMoving = 28;
        player = GameObject.FindWithTag("Player");
        rised = false;
        startSpinning = false;
        swingTime = 1.0f;
        degreesPerSecond = -20.0f;
        risePosition = new Vector3(transform.position.x, 30, transform.position.z);
        IdleAnimation();
    }

    /*
     * Balanceo vertical
     */
    IEnumerator IdleAnimation()
    {
        while (true)
        {
            yield return StartCoroutine(MoveObject(transform, Vector3.up, swingTime));
            yield return StartCoroutine(MoveObject(transform, Vector3.down, swingTime));
        }
    }

    /*
     * Mueve la nave en la dirección establecida por dir a razon de la variable time
     */
    IEnumerator MoveObject(Transform thisTransform, Vector3 dir, float time)
    {
        var i = 0.0f;
        var rate = 1.0f / time;
        while (i < 1.0f)
        {
            i += Time.deltaTime * rate;
            thisTransform.Translate(dir * 3.0f * Time.deltaTime);
            yield return null;
        }
    }

    void Update()
    {
        if (!rised)
            Rise();
        else if (!startSpinning)
            GoToCenter();
        else
            MoveInCircles();
    }

    /*
     * Eleva la nave hasta la posicion risePosition y una vez hecho, rised se transforma a verdadero y se empieza a disparar
     */
    private void Rise()
    {
        transform.position = Vector3.MoveTowards(transform.position, risePosition, Time.deltaTime * 10f);
        if (transform.position.y >= yAltitudeToStartMoving)
        {
            rised = true;
            GetComponent<EnemyShooting>().Shoot();
        }
    }

    /*
     * Mueve la nave hasta el radio del centro del mapa, una vez llegado startSpinning se vuelve verdadero y el vector auxiliar para hacer el giro se calcula
     */
    private void GoToCenter()
    {
        if(Vector3.Distance(transform.position, center.transform.position) <= RADIUS)
        {
            startSpinning = true;
            auxVec = transform.position - center.transform.position;
        }
        else
        {
            LookTarget();
            transform.position = Vector3.MoveTowards(transform.position, center.transform.position, Time.deltaTime * 15f);
        }

    }

    /*
     * Movimiento circular de la nave
     */
    private void MoveInCircles()
    {
        LookTarget();
        auxVec = Quaternion.AngleAxis(degreesPerSecond * Time.deltaTime, Vector3.down) * auxVec;
        transform.position = center.transform.position + auxVec;
    }

    /*
     * Hace que la nave siempre mire hacia el jugador
     */
    private void LookTarget()
    {
        var direction = (player.transform.position - transform.position).normalized;
        var lookRotation = Quaternion.LookRotation(-direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * ROTATION_SPEED);
        transform.rotation = Quaternion.Euler(-30, transform.eulerAngles.y, transform.eulerAngles.z);
    }
}
