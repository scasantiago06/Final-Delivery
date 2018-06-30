using System.Collections;                                               
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using NPC.Ally;                                                         
using NPC.Enemy;                                                        
using NPC;

/****************************************************************************************************************************Clase Hero*****************************************************************************************************************************/
public class Hero : MonoBehaviour                                       
{
    /// <summary>
    /// Creo las variables necesarias, incluyendo variables del tipo de 
    /// las estructura, y una de ellas estática.
    /// </summary>
    public static HeroStruct heroStruct_H;                                                                                                              
    ZombieStruct zombieStruct_H;                                                                                                                        
    CitizenStruct citizenStruct_H;                                                                                                                      
    NpcStruct npcStruct_H;

    GameObject clone;
    GameObject ammunition;
    GameObject health;

    WaitForSeconds textEnabled = new WaitForSeconds(2);  
    
    bool citizenText = false;
    float distancesH_Z;
    int randomDialogue;

    /************************************************************************************************************************Funcion "Awake"***************************************************************************************************************************/
    /// <summary>
    /// Busco objetos en la escena para guardarlos en sus respectivas variables.
    /// </summary>
    void Awake()
    {
        heroStruct_H.health = GameObject.FindGameObjectWithTag("Health").GetComponent<Slider>();
        heroStruct_H.finalText = GameObject.FindGameObjectWithTag("Finish").GetComponent<Text>();
        heroStruct_H.dialogue = GameObject.FindGameObjectWithTag("Dialogue").GetComponent<Text>();
        heroStruct_H.cam = GameObject.FindGameObjectWithTag("MainCamera");
        heroStruct_H.heroObject = gameObject;
    }

    /************************************************************************************************************************Funcion "Start"***************************************************************************************************************************/
    /// <summary>
    /// En esta función se hacen varias cosas, primero instancio una clase con constructor, luego
    /// lleno una variable con la posición del héroe, le asigno un valor a la variable que maneja el
    /// "Slider" de UI, le doy un Tag y un nombre al héroe, se le da color también, guardo el componente 
    /// "Rigidbody" para congelar las rotaciones, entre otras cosas más.
    /// </summary>
    void Start()
    {
        S_Hero speedHero = new S_Hero();                                                                            
        heroStruct_H.positionHero = gameObject.transform.position;              
        heroStruct_H.health.value = 100;

        gameObject.name = "Hero";                                                                                   
        gameObject.tag = "Player";

        gameObject.GetComponent<Renderer>().material.color = Color.black;

        heroStruct_H.rb = GetComponent<Rigidbody>();                                          
        heroStruct_H.rb.constraints = RigidbodyConstraints.FreezeRotation; 
                                                 
        heroStruct_H.cam.transform.position = new Vector3(heroStruct_H.positionHero.x, heroStruct_H.positionHero.y + 1.5f, heroStruct_H.positionHero.z - 2);  
        heroStruct_H.cam.transform.SetParent(gameObject.transform);    
        
        gameObject.AddComponent<FPSMove>().speed = speedHero.speed_Hero;                                                                                    
        heroStruct_H.cam.AddComponent<FPSAim>();          
        
        GunInstance();
        AmmunitionAndHealth();
    }

    /***********************************************************************************************************************Funcion "Update"***********************************************************************************************************************/
    /// <summary>
    /// Constantemente se va verificando la distancia entre el héroe y cada uno de los zombies
    /// para así, si la distancia en un número en específico mostrar un mensaje en la UI.
    /// Por otro lado también se verifica cada que se presiona clic, para así instanciar un objeto que
    /// sirve como proyectil para destruir zombies.
    /// </summary>
    void Update()
    {
        foreach (GameObject zo in ClassController.zombieList)
        {
            if (zo != null)
            {
                distancesH_Z = Vector3.Distance(gameObject.transform.position, zo.transform.position);
            }

            if (citizenText == false)
            {
                if (distancesH_Z <= 5)
                {
                    StopCoroutine("RemoveDialogue");
                    zombieStruct_H = zo.GetComponent<Zombie>().ZombieMessage();
                    heroStruct_H.dialogue.enabled = true;
                    heroStruct_H.dialogue.text = "ZOMBIE: Waaaarrrr i want to eat " + zombieStruct_H.bodyPart;
                    StartCoroutine("RemoveDialogue");
                }
            }
        }

        if (Input.GetButtonDown("Fire1") && heroStruct_H.countProjectiles > 0)
        {
            clone = Instantiate(Resources.Load("Projectile", typeof(GameObject)) as GameObject, heroStruct_H.gun.transform.position, transform.localRotation);
            clone.GetComponent<Rigidbody>().velocity = heroStruct_H.gun.transform.TransformDirection(Vector3.forward * 70);
            heroStruct_H.countProjectiles--;
            Destroy(clone, 0.5f);
        }

        GameOver();
    }

    /******************************************************************************************************************Funcion "OnCollisionEnter"******************************************************************************************************************/
    /// <summary>
    /// Se verifica cuando el héroe colisiona contra un ciudadano para mostrar
    /// en la UI un mensaje que depende de una variable aleatoria.
    /// </summary>
    /// <param name="collision"></param>
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<Citizen>())
        {
            citizenText = true;
            citizenStruct_H = collision.gameObject.GetComponent<Citizen>().CitizenMessage();
            npcStruct_H = collision.gameObject.GetComponent<Npc>().NpcMessage();

            StopCoroutine("RemoveDialogue");

            heroStruct_H.dialogue.enabled = true;
            randomDialogue = Random.Range(0, 3);

            if (randomDialogue == 0)
            {
                heroStruct_H.dialogue.text = "CITIZEN: Hello, i am " + citizenStruct_H.names + " and i am " + npcStruct_H.age + " years old";
                StartCoroutine("RemoveDialogue");
            }
            else if (randomDialogue == 1)
            {
                heroStruct_H.dialogue.text = "You can find ammunition around the map";
                StartCoroutine("RemoveDialogue");
            }
            else if (randomDialogue == 2)
            {
                heroStruct_H.dialogue.text = "You can heal yourself with a green capsule, look for it!";
                StartCoroutine("RemoveDialogue");
            }
        }
    }

    /*******************************************************************************************************************Función "OnTriggerEnter"***********************************************************************************************************************/
    /// <summary>
    /// Verifico si los coliders de la municion y de la vida chocan con el heroe,
    /// para desactivarlos y empezar la corrutina.
    /// </summary>
    /// <param name="other"></param>
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Ammunition"))
        {
            heroStruct_H.countProjectiles = 10;
            ammunition.SetActive(false);
            StartCoroutine("ActiveObjects");
        }
        if (other.gameObject.CompareTag("Health"))
        {
            heroStruct_H.health.value += 15;
            if (heroStruct_H.health.value > 100)
            {
                heroStruct_H.health.value = 100;
            }
            health.SetActive(false);
            StartCoroutine("ActiveObjects");
        }
    }

    /******************************************************************************************************************Corrutina "RemoveDialogue"**********************************************************************************************************************/
    /// <summary>
    /// Esta corrutina se encarga de desactivar el texto en un determinado tiempo.
    /// </summary>
    /// <returns></returns>
    IEnumerator RemoveDialogue()                                                                                                                        
    {
        yield return textEnabled;                                                                                                                       
        citizenText = false;
        heroStruct_H.dialogue.enabled = false;                                                                                                          
    }

    /*******************************************************************************************************************Corrutina "ActiveObjects"**********************************************************************************************************************/
    /// <summary>
    /// Esta corrutina se encarga de volver a activar los objetos del escenario y 
    /// reubicarlos aleatoriamente, todo esto si los objetos estan desactivados.
    /// </summary>
    /// <returns></returns>
    IEnumerator ActiveObjects()
    {
        yield return new WaitForSeconds(15f);
        if (!ammunition.activeInHierarchy)
        {
            ammunition.SetActive(true);
            ammunition.transform.position = new Vector3(Random.Range(0, 30), 0.5f, Random.Range(0, 30));
        }
        if (!health.activeInHierarchy)
        {
            health.SetActive(true);
            ammunition.transform.position = new Vector3(Random.Range(0, 30), 0.5f, Random.Range(0, 30));
        }
    }

    /*********************************************************************************************************************Función "GunInstance"************************************************************************************************************************/
    /// <summary>
    /// Esta función hace lo necesario para el arma, la instancia, la ubica, la escala
    /// etc.
    /// </summary>
    void GunInstance()
    {
        heroStruct_H.gun = GameObject.CreatePrimitive(PrimitiveType.Cube);
        heroStruct_H.gun.GetComponent<Collider>().enabled = false;
        heroStruct_H.gun.name = "Gun";
        heroStruct_H.gun.transform.position = new Vector3(transform.position.x + 0.7f, transform.position.y, transform.position.z + 0.5f);
        heroStruct_H.gun.GetComponent<Renderer>().material.color = Color.blue;
        heroStruct_H.gun.transform.localScale = new Vector3(0.3f, 0.3f, 1f);
        heroStruct_H.gun.transform.SetParent(gameObject.transform);
        heroStruct_H.countProjectiles = 15;
    }

    /*****************************************************************************************************************Función "AmmunitionAndHealth"********************************************************************************************************************/
    /// <summary>
    /// En esta función instancio los objetos del mapa y los reubico.
    /// </summary>
    void AmmunitionAndHealth()
    {
        ammunition = Instantiate(Resources.Load("Ammunition", typeof(GameObject)) as GameObject);
        ammunition.transform.position = new Vector3(Random.Range(0, 30), 0.5f, Random.Range(0, 30));
        health = Instantiate(Resources.Load("Health", typeof(GameObject)) as GameObject);
        health.transform.position = new Vector3(Random.Range(0, 30), 0.5f, Random.Range(0, 30));
    }

    /**********************************************************************************************************************Función "GameOver"**************************************************************************************************************************/
    /// <summary>
    /// Esta función es para cuando el héroe pierde por slider de vida.
    /// La camara se desemparenta, el heroe se remueve, se reubica la cámara
    /// entre otras cosas.
    /// </summary>
    void GameOver()
    {
        if(heroStruct_H.health.value == 0)
        {
            heroStruct_H.cam.transform.SetParent(null);                                                 
            ClassController.citizenList.Remove(gameObject);
            Destroy(heroStruct_H.cam.GetComponent<FPSAim>());
            heroStruct_H.cam.transform.position = new Vector3(0, 90, 0);
            Quaternion rot = Quaternion.Euler(90, 0, 0);
            heroStruct_H.cam.transform.rotation = rot;
            heroStruct_H.dialogue.text = " ";
            heroStruct_H.finalText.text = "You Lost";
            Destroy(gameObject);
        }
    }
}

/****************************************************************************************************************************Clase "S_Hero"****************************************************************************************************************************/
/// <summary>
/// Creo una clase con su constructor para crear e inicializar una
/// variable "ReadOnly".
/// </summary>
public class S_Hero                                                                                                                                     
{
    public readonly float speed_Hero;                                                                                                                   

    /*********************************************************************************************************************Contructor "S_Hero"**************************************************************************************************************************/
    public S_Hero()                                                                                                                                     
    {
        speed_Hero = Random.Range(5.0f, 10.0f);                                                                                                         
    }
}