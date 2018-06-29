using System.Collections;                                               //UpWork / HackerRank
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using NPC.Ally;                                                                                                                                         //Para utilizar la clase "Citizen" utilizo la directiva donde se encuentra esta.                         
using NPC.Enemy;                                                                                                                                        //Para utilizar la clase "Zombie" utilizo la directiva donde se encuentra esta.
using NPC;                                                                                                                                              //Para utilizar la clase "Npc" utilizo la directiva donde se encuentra esta.
using UnityEditor;

/****************************************************************************************************************************Clase Hero*****************************************************************************************************************************/
public class Hero : MonoBehaviour                                                                                                                       //La clase del héroe.
{
    public static HeroStruct heroStruct_H;                                                                                                                            //Creo una variable de tipo "HeroStruct" que es la estructura del héroe.
    ZombieStruct zombieStruct_H;                                                                                                                        //Creo una variable de tipo "ZombieStruct" que es la estructura del zombie.
    CitizenStruct citizenStruct_H;                                                                                                                      //Creo una variable de tipo "CitizenStruct" que es la estructura del ciudadano.
    NpcStruct npcStruct_H;                                                                                                                              //Creo una variable de tipo "NpcStruct" que es la estructura del npc.

    WaitForSeconds textEnabled = new WaitForSeconds(2);                                                                                                 //Creo una variable de tipo "waitForSeconds" para utilizarla en la corrutina.
    bool citizenText = false;
    float distancesH_Z;
    GameObject clone;
    GameObject ammunition;
    GameObject health;
    //public GameObject SantoDios;
    /******************************************************************************************************************Funcion "OnCollisionEnter"******************************************************************************************************************/
    void OnCollisionEnter(Collision collision)                                                                                                          //Utilizo la función "OnCollisionEnter" para detectar cuando hay una colisión.
    {
        if (collision.gameObject.GetComponent<Citizen>())                                                                                               //Verifico con el condicional si el objeto que tiene este script esta chocando con algún otro que tenga el componente de "Citizen".
        {
            citizenText = true;
            citizenStruct_H = collision.gameObject.GetComponent<Citizen>().CitizenMessage();                                                            //En resumen, "citizenStruct_H" es igual a la función "CitizenMessage()" ubicada en la clase "Citizen" y, esta función retorna la estructura local de dicha clase, por lo tanto, "CitizenStruct_H" será igual a "CitizenStruct_C".
            npcStruct_H = collision.gameObject.GetComponent<Npc>().NpcMessage();
            StopCoroutine("RemoveDialogue");                                                                                                            //Detengo la corrutina, esto es para que siempre que colisione se asegure de, en caso de que este activada, la detenga para que no se acumule.
            heroStruct_H.dialogue.enabled = true;                                                                                                       //Activo el texto para que se pueda hacer la siguiente línea.
            heroStruct_H.dialogue.text = "CITIZEN: Hello, i am " + citizenStruct_H.names + " and i am " + npcStruct_H.age + " years old";               //Ahora el texto de la variable "dialogue" de la estructura cambiará a lo que aparece entre comillas más "CitizenStruct_H.bodyPart", es decir, "CitizenStruct_H" se acabó de sobrescribir por "CitizenStruct_Z" y luego accedimos a la variable "randomName" de la estructura, y lo mismo con "citizenStruct.age"
            StartCoroutine("RemoveDialogue");                                                                                                           //Llamo la corrutina que desactivará el texto.
        }
    }

    void Update()
    {
        foreach (GameObject zo in ClassController.zombieList)
        {
            distancesH_Z = Vector3.Distance(gameObject.transform.position, zo.transform.position);

            if (citizenText == false)
            { 
                if (distancesH_Z <= 5)
                {
                    StopCoroutine("RemoveDialogue");
                    zombieStruct_H = zo.GetComponent<Zombie>().ZombieMessage();
                    heroStruct_H.dialogue.enabled = true;
                    heroStruct_H.dialogue.text = "ZOMBIE: Waaaarrrr i want to eat " + zombieStruct_H.bodyPart;                                                  //Ahora el texto de la variable "dialogue" de la estructura cambiará a lo que aparece entre comillas más "zombieStruct_H.bodyPart", es decir, "zombieStruct_H" se acabó de sobrescribir por "zombieStruct_Z" y luego accedimos a la variable "bodyPart" de la estructura.
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

    private void Awake()
    {
        heroStruct_H.health = GameObject.FindGameObjectWithTag("Health").GetComponent<Slider>();
    }

    /************************************************************************************************************************Funcion "Start"************************************************************************************************************************/
    void Start()
    {
        S_Hero speedHero = new S_Hero();                                                                                                                    //Instancio la clase "S_Hero".
        heroStruct_H.positionHero = gameObject.transform.position;                                                                                          //Accedo a la estructura "heroStruct" y luego a la variable "positionHero" y digo que será igual a la posicion del "gameObject" que contiene este script.
        heroStruct_H.dialogue = GameObject.FindGameObjectWithTag("Dialogue").GetComponent<Text>();                                                          //A la variable de tipo texto "dialogue" que está dentro de la estructura le asigno el componente "text" del "GameObject" que tenga el tag "Dialogue".
        heroStruct_H.health.value = 100;

        gameObject.name = "Hero";                                                                                                                           //Al objeto que contiene este script le doy el nombre de "Hero" para que aparezca en la jerarquía con ese nombre.
        gameObject.tag = "Player";                                                                                                                          //Al objeto que contiene este script le doy el tag de "Hero".

        heroStruct_H.rb = GetComponent<Rigidbody>();                                                                                                        //A la variable "rb" de la estructura le doy el componente "RigidBoy".
        gameObject.GetComponent<Renderer>().material.color = Color.black;                                                                                   //Le doy un color al personaje, un simple cambio visual.
        heroStruct_H.rb.constraints = RigidbodyConstraints.FreezeRotation;                                                                                  //A la variable "rb" de la estructura le digo que acceda a los "Constraints" y ponga verdadero todo el "FreezeRotation" en el inspector.
        heroStruct_H.cam = GameObject.FindGameObjectWithTag("MainCamera");                                                                                  //A la variable "cam" de la estructura almaceno el "gameObject" que tenga el tag de "MainCamera", es decir que se guardará la cámara principal de la escena.
        heroStruct_H.cam.transform.position = new Vector3(heroStruct_H.positionHero.x, heroStruct_H.positionHero.y + 1.5f, heroStruct_H.positionHero.z - 2);   //Ahora a la variable "cam" le doy una posición en la escena, la cual va a ser igual que a la del personaje o "Hero" pero se le suma 1 a "y" para que este un poco más alta y simule la cabeza.
        heroStruct_H.cam.transform.SetParent(gameObject.transform);                                                                                         //Ahora a la variable "cam" la emparento al objeto que tenga este script, es decir al "Hero", para que a donde se mueva el objeto, también se mueva la cámara.
        gameObject.AddComponent<FPSMove>().speed = speedHero.speed_Hero;                                                                                    //Al personaje le agrego el componente "FPSMove" y le digo que la variable "speed" de dicha clase será igual a "speed_Hero" que esta en la clase "S_Hero".
        heroStruct_H.cam.AddComponent<FPSAim>();                                                                                                            //A la cámara le añado el componente "FPSAim".
        GunInstance();
        AmmunitionAndHealth();
    }

    /******************************************************************************************************************Corrutina "RemoveDialogue"**********************************************************************************************************************/
    IEnumerator RemoveDialogue()                                                                                                                        //Creo la corrutina que controla el texto del diálogo para desactivar.
    {
        yield return textEnabled;                                                                                                                       //Hago que la corrutina espere dos segundos.
        citizenText = false;
        heroStruct_H.dialogue.enabled = false;                                                                                                          //Y luego que desactive el texto.
    }

    IEnumerator ActiveObjects()
    {
        yield return new WaitForSeconds(10f);
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

    void AmmunitionAndHealth()
    {
        ammunition = Instantiate(Resources.Load("Ammunition", typeof(GameObject)) as GameObject);
        ammunition.transform.position = new Vector3(Random.Range(0, 30), 0.5f, Random.Range(0, 30));
        health = Instantiate(Resources.Load("Health", typeof(GameObject)) as GameObject);
        health.transform.position = new Vector3(Random.Range(0, 30), 0.5f, Random.Range(0, 30));
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Ammunition"))
        {
            heroStruct_H.countProjectiles = 15;
            ammunition.SetActive(false);
            StartCoroutine("ActiveObjects");
        }
        if (other.gameObject.CompareTag("Health"))
        {
            heroStruct_H.health.value += 15;
            if(heroStruct_H.health.value > 100)
            {
                heroStruct_H.health.value = 100;
            }
            health.SetActive(false);
            StartCoroutine("ActiveObjects");
        }
    }

    void GameOver()
    {
        if(heroStruct_H.health.value == 0)
        {
            heroStruct_H.cam.transform.SetParent(null);                                                 /*RRRRRRRROOOOOOOOOTTTTTTAAAAACCCCIIIIIOOOONNNNN*/
            ClassController.citizenList.Remove(gameObject);
            heroStruct_H.cam.transform.position = new Vector3(0, 50, 0);
            heroStruct_H.cam.transform.rotation = new Quaternion(0, 0, 0, 0);
            heroStruct_H.dialogue.text = " ";
            Destroy(gameObject);
        }
    }
}

/****************************************************************************************************************************Clase "S_Hero"****************************************************************************************************************************/
public class S_Hero                                                                                                                                     //Creo una clase que no hereda de "Monobehaviour".                                                                                                                             
{
    public readonly float speed_Hero;                                                                                                                   //Creo una variable de tipo "float" y "readOnly".

    /*********************************************************************************************************************Contructor "S_Hero"**************************************************************************************************************************/
    public S_Hero()                                                                                                                                     //Creo un constructor para poder cambiar la variable "speed_Hero".
    {
        speed_Hero = Random.Range(5.0f, 10.0f);                                                                                                         //Dentro del constructor cambio la variable "speed_Hero".
    }
}