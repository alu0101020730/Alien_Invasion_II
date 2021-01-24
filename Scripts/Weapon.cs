using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/*
* Clase Weapon encargada del funcionamiento de las 2 armas y las bombas
*/
public class Weapon : MonoBehaviour
{
    // Declaración de las variables utilizadas
    public bool shootingRateBool;
    public int actual_ammo;                  // Munición actual.
    public int actual_weapon;                // En caso de que sea 0 es la pistola, en caso de que sea 1 el rifle.
    public int cs_counter;
    public int gun_ammo_rn = 6;              // Munición actual de la pistola.
    public int machine_ammo_rn = 20;         // Munición actual del rifle.
    public int total_machinegun_ammo = 120;  // Munición máxima del rifle.
    public int total_gun_ammo = int.MaxValue;// Munición máxima de la pistola.
    public bool can_anything = true;         // En caso de que sea negativo no se puede cambiar de arma o disparar.
    public int gun_dmg;                      // Daño de la pistola.
    public int rifle_dmg = 20;               // Daño del rifle.
    public float fireRate;
    [SerializeField] public bool startShooting;
    public GameObject enemyOnFocus;


    public Button Pistola2;
    public Button rifle2;
    public Button Bomba2;

    public Button GunAttackBuff;
    public Button RifleAttackBuff;

    public GameObject spawnerEnemigos;
    public List<GameObject> enemigos;

    private Shop shop;

    //Declaración de objetos los cuales se asignan desde el inspector
    public GameObject gun;
    public GameObject machine_gun;
    public AudioSource gun_sound;
    public AudioSource bomb_sound;
    public AudioSource change_weapon;
    public AudioSource reload_sound;

    public AudioSource powerup_sound;
    

    /*
     * Establecemos el valor de algunas variables.
     */
    void Start()
    {
        gun_sound.volume = 0.1F;
        bomb_sound.volume = 0.1F;
        change_weapon.volume = 0.1F;
        reload_sound.volume = 0.1F;

        shop = GameObject.FindGameObjectWithTag("UI").GetComponent<Shop>();

        Pistola2.onClick.AddListener(cambiar_pistola);
        rifle2.onClick.AddListener(cambiar_rifle);
        Bomba2.onClick.AddListener(tirar_bomba);

        GunAttackBuff.onClick.AddListener(gunAttackUp);
        RifleAttackBuff.onClick.AddListener(rifleAttackUp);

        shootingRateBool = true;
        startShooting = false;
        gun_dmg = 30;
        fireRate = 1f;
        actual_weapon = 1;
        actual_ammo = 6;
        machine_gun.SetActive(false);

    }

    /*
     * Función que busca todos los enemigos y los añade a un array.
     */
    void GetAllActiveEnemies()
    {
        enemigos.Clear();
        foreach (Transform child in spawnerEnemigos.transform)
            enemigos.Add(child.gameObject);
    }

    /*
     * En caso de que no quede munición en la arma actual la recarga.
     */
    private void Update()
    {
        GetAllActiveEnemies();
        if (actual_ammo == 0) {
            reload_sound.Play();
            if (actual_weapon == 1) {
                can_anything = false;
                StartCoroutine(reload_gun());
                return;
            } else {
                can_anything = false;
                StartCoroutine(reload_machinegun());
                return;
            }
        }

        if (startShooting && actual_ammo > 0 && shootingRateBool)
        {
            StartCoroutine(Shoot());
        }
    }

    /*
     * Cambia el valor del booleano que comienza a disparar.
     */
    public void StartShootingFunc()
    {
        startShooting = true;
    }

    /*
     * Cambia el valor del booleano que hace que pare de disparar.
     */    
    public void StopShootingFunc()
    {
        startShooting = false;
    }

    /*
     * Hace daño al enemigo que se está disparando.
     */ 
    public IEnumerator Shoot()
    {
        shootingRateBool = false;
        if (can_anything)
        {
            gun_sound.Play();
            if (enemyOnFocus.name != "Boss(Clone)")
                enemyOnFocus.GetComponent<EnemyHealth>().GetDMG(get_dmg());
            else
                enemyOnFocus.GetComponent<BossHealth>().GetDMG(get_dmg());
            //enemyOnFocus.GetComponent<EnemyHealth>().GetDMG(get_dmg());
            actual_ammo--;
        }
        yield return new WaitForSeconds(fireRate);
        shootingRateBool = true;
    }

    /*
     * Aumento de daño de pistola.
     */ 
    public void gunAttackUp()
    {
        if (shop.money >= 100){
            shop.RemoveMoney(100);
            gun_dmg += 10;
            powerup_sound.Play();
        }   
    }

    /*
     * Aumento daño de rifle.
     */ 
    public void rifleAttackUp()
    {
        if (shop.money >= 150)
        {
            shop.RemoveMoney(150);
            rifle_dmg += 15;
            powerup_sound.Play();
        }
    }

    /*
     * Devuelve el daño de la arma actual.
     */ 
    public int get_dmg() {
        if (actual_weapon == 1) {
            return gun_dmg;
        } else {
            return rifle_dmg;
        }
    }

    /*
     * Cambia el arma a la pistola.
     */ 
    void cambiar_pistola() {
            if (can_anything && actual_weapon != 1) {
                actual_weapon = 1;
                machine_ammo_rn = actual_ammo;
                actual_ammo = gun_ammo_rn;
                fireRate = 1;
                gun.SetActive(true);
                machine_gun.SetActive(false);
                change_weapon.Play();

            }
    }

    /*
     * Cambia el arma al rifle.
     */ 
    void cambiar_rifle() {
            if (can_anything && actual_weapon != 2) {
                actual_weapon = 2;
                gun_ammo_rn = actual_ammo;
                actual_ammo = machine_ammo_rn;
                fireRate = 0.3f;
                gun.SetActive(false);
                machine_gun.SetActive(true);
                change_weapon.Play();
            }
    }

    /*
     * Si tiene dinero suficiente tira la bomba a todos los objetivos.
     */ 
    public void tirar_bomba() {
        if (shop.money >= 80){
            if (can_anything){
                shop.RemoveMoney(80);
                foreach (GameObject enemigo in enemigos)
                {
                    if (enemigo.name != "Boss(Clone)")
                        enemigo.GetComponent<EnemyHealth>().GetDMG(100);
                    else
                        enemigo.GetComponent<BossHealth>().GetDMG(100);
                }
                bomb_sound.Play();
            }
        }
    }

    /*
     * Recarga la pistola y esperas 1 segundo.
     */ 
    IEnumerator reload_gun() {
        yield return new WaitForSeconds (1);
        actual_ammo = 6;
        can_anything = true;
    }

    /*
     * Recarga el rifle y esperas 1 segundo.
     */ 
    IEnumerator reload_machinegun() {
        yield return new WaitForSeconds (2);
        int aux = 20 - actual_ammo;
        total_machinegun_ammo -= aux;
        if (total_machinegun_ammo < 0) {
            actual_ammo = 20 + total_machinegun_ammo;
            total_machinegun_ammo = 0;
        } else {
            actual_ammo = 20;
        }
        can_anything = true;
    }

}
