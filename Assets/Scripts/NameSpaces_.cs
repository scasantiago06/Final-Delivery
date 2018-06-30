using NPC.Ally;
using NPC.Enemy;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/******************************************************************************************************************************NameSpace NPC***************************************************************************************************************************/
/// <summary>
/// Creo el "namespace" para albergar dentro las clases de zombie y citizen.
/// </summary>
namespace NPC                                                                                                   
{
    /***************************************************************************************************************************Clase "Npc"****************************************************************************************************************************/
    /// <summary>
    /// En esta clase se realiza lo que zombie y citizen tienen en común.
    /// </summary>
    public class Npc : MonoBehaviour                                                                            
    {
        /// <summary>
        /// Creo las variables de las estructuras más otra necesaria.
        /// </summary>
        public ZombieStruct zombieStruct_N;                                                                     
        public CitizenStruct citizenStruct_N;                                                                   
        public NpcStruct npcStruct_N;

        public WaitForSeconds timeBehaviourChange = new WaitForSeconds(3);

        /**********************************************************************************************************************Función "Awake"*************************************************************************************************************************/
        /// <summary>
        /// En esta función se condiciona la edad de cada uno para asignarle una 
        /// velocidad dependiendo de la edad.
        /// </summary>
        public void Awake()                                                                                     
        {
            npcStruct_N.age = Random.Range(15, 101);                                                            
            npcStruct_N.rotationVelocity = Random.Range(15, 100);                                               

            if (npcStruct_N.age <= 15)                                                                         
            {
                npcStruct_N.runSpeed = 15.0f;
            }
            else if (npcStruct_N.age <= 30 && npcStruct_N.age > 15)
            {
                npcStruct_N.runSpeed = 10.0f;
            }
            else if (npcStruct_N.age <= 50 && npcStruct_N.age > 30)
            {
                npcStruct_N.runSpeed = 7.0f;
            }
            else if (npcStruct_N.age <= 75 && npcStruct_N.age > 50)
            {
                npcStruct_N.runSpeed = 6.0f;
            }
            else if (npcStruct_N.age <= 100 && npcStruct_N.age > 75)
            {
                npcStruct_N.runSpeed = 5.0f;
            }

            StartCoroutine("ChangeBehaviour");                                                                  
        }

        /**********************************************************************************************************************Función "Movement"*********************************************************************************************************************************/
        /// <summary>
        /// Esta función se encarga de verificar el estado y según este hacer una acción 
        /// u otra.
        /// </summary>
        public void Movement()
        {
            switch (npcStruct_N.npcBehaviour)                                                                   
            {
                case NpcBehaviour.Idle:                                                                         
                    transform.position = transform.position;                                                    
                    goto case NpcBehaviour.Running;                                      

                case NpcBehaviour.Moving:                                                                       
                    transform.position += (transform.forward * npcStruct_N.runSpeed) * Time.deltaTime;          
                    goto case NpcBehaviour.Running;                                      

                case NpcBehaviour.Rotating:                                                                     
                    switch (npcStruct_N.randomRotation)                                                         
                    {
                        case 0:                                                                                 
                            transform.Rotate(0, npcStruct_N.rotationVelocity * Time.deltaTime, 0, 0);           
                            break;                                                                              
                        case 1:                                                                                 
                            transform.Rotate(0, -npcStruct_N.rotationVelocity * Time.deltaTime, 0, 0);          
                            break;                                                                              
                    }
                    goto case NpcBehaviour.Running;                                                             

                case NpcBehaviour.Running:
                    DistanceFunction();
                    break;
            }
        }

        /************************************************************************************************************Función Virtual"DistanceFunction"*****************************************************************************************************************/
        /// <summary>
        /// En esta función se verifican las distancias entre los zombis, ciudadanos y héroe para saber en que momento
        /// se entra en el estado "Running".
        /// </summary>
        public virtual void DistanceFunction()
        {
            foreach (GameObject cit in ClassController.citizenList)
            {
                if(cit != null)
                { 
                    npcStruct_N.distances = Vector3.Distance(gameObject.transform.position, cit.transform.position);
                }

                if (npcStruct_N.distances < 5 && npcStruct_N.distances > 1.5f)
                {
                    if (cit.GetComponent<Citizen>() || cit.GetComponent<Hero>())
                    {
                        gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, cit.transform.position, npcStruct_N.runSpeed * Time.deltaTime);
                        npcStruct_N.npcBehaviour = NpcBehaviour.Running;
                    }
                }
            }
        }

        /**************************************************************************************************************Corrutina "ChangeBehaviour"*********************************************************************************************************************/
        /// <summary>
        /// La corrutina se encarga de cambiar de, cada cierto tiempo,
        /// llamar la función "ChooseBehaviour".
        /// </summary>
        /// <returns></returns>
        IEnumerator ChangeBehaviour()
        {
            ChooseBehaviour();                                                                                  
            yield return timeBehaviourChange;                                                                   
            StartCoroutine("ChangeBehaviour");                                                                  
        }

        /***************************************************************************************************************Función "ChooseBehaviour"*************************************************************************************************************************/
        /// <summary>
        /// Esta función se encarga de escoger un estado al azar.
        /// </summary>
        void ChooseBehaviour()
        {
            npcStruct_N.randomRotation = Random.Range(0, 2);                                                    
            npcStruct_N.npcBehaviour = (NpcBehaviour)Random.Range(0, 3);                                        
        }

        /*****************************************************************************************************************Función "NpcMessage"*************************************************************************************************************************/
        /// <summary>
        /// Creo una función para retornar la estructura.
        /// </summary>
        /// <returns></returns>
        public NpcStruct NpcMessage()
        {
            return npcStruct_N;
        }
    }

    /***********************************************************************************************************************NameSpace Enemy****************************************************************************************************************************/
    /// <summary>
    /// Creo un "namespace" para albergar la clase zombie.
    /// </summary>
    namespace Enemy                                                                                             
    {
        /*****************************************************************************************************************************Clase "Zombie"*******************************************************************************************************************************/
        /// <summary>
        /// Esta clase se encarga del comportamiento del zombie y se sella
        /// para que no se pueda heredar de esta.
        /// </summary>
        public sealed class Zombie : Npc                                                                        
        {
            float distanceZ_H;
            GameObject hero;
            bool activateDamage = true;
            Vector3 BigZombie;

            /****************************************************************************************************************Función "Start"***************************************************************************************************************************/
            /// <summary>
            /// Inicializo las variables necesarias
            /// </summary>
            void Start()
            {
                BigZombie = new Vector3(1, 2, 1);
                gameObject.name = "Zombie";                                                                     
                gameObject.tag = "Zombie";                                                                      
                zombieStruct_N.randomColor = Random.Range(0, 3);                                                
                zombieStruct_N.bodyPart = (BodyPart)Random.Range(0, 5);                                         
                ChangeColor();                                                                                  
                hero = GameObject.FindGameObjectWithTag("Player");
            }

            /****************************************************************************************************************Función "Update"**************************************************************************************************************************/
            /// <summary>
            /// Llamo dos funciones que se necesitan estar verificando
            /// constantemente.
            /// </summary>
            void Update()
            {
                Movement();
                CheckDistance();
            }

            /*************************************************************************************************************Funcion "CheckDistance"**********************************************************************************************************************/
            /// <summary>
            /// Con esta función se verifica la distancia del zombie con el héroe para saber
            /// si es suficiente y empezar a hacer daño.
            /// </summary>
            void CheckDistance()
            {
                if(hero != null)
                { 
                    distanceZ_H = Vector3.Distance(gameObject.transform.position, hero.transform.position);
                    if(activateDamage == true && distanceZ_H <= 1.5f)
                    {
                        activateDamage = false;
                        StartCoroutine("Damage");
                    }
                }
            }

            /*****************************************************************************************************************Corrutina "Damage"***********************************************************************************************************************/
            /// <summary>
            /// En la corrutina se hace el daño al héroe dependiendo del tipo del zombie.
            /// </summary>
            /// <returns></returns>
            IEnumerator Damage()
            {
                npcStruct_N.npcBehaviour = NpcBehaviour.Running;
                if(transform.localScale == BigZombie)
                {
                    Hero.heroStruct_H.health.value -= 30;
                }
                else
                {
                    Hero.heroStruct_H.health.value -= 10;
                }
                yield return timeBehaviourChange;
                activateDamage = true;
            }

            /****************************************************************************************************************Funcion "ChangeColor"*********************************************************************************************************************/
            /// <summary>
            /// Esta función se encarga de cambiar el color del zombie de forma aleatoria.
            /// </summary>
            public void ChangeColor()
            {
                switch (zombieStruct_N.randomColor)                                                             
                {
                    case 0:                                                                                     
                        gameObject.GetComponent<Renderer>().material.color = Color.cyan;                        
                        break;                                                                                  
                    case 1:                                                                                     
                        gameObject.GetComponent<Renderer>().material.color = Color.green;                       
                        break;                                                                                  
                    case 2:                                                                                     
                        gameObject.GetComponent<Renderer>().material.color = Color.magenta;                     
                        break;                                                                                  
                }
            }

            /**************************************************************************************************************Función "OnCollisionEnter"******************************************************************************************************************/
            /// <summary>
            /// Verifico si collisiona con algún ciudadano para convertirlo ó, si 
            /// colisiona con un proyectil para en ese caso, este zombie ser destruido.
            /// </summary>
            /// <param name="collision"></param>
            void OnCollisionEnter(Collision collision)
            {
                if (collision.gameObject.GetComponent<Citizen>())
                {
                    Citizen c = collision.gameObject.GetComponent<Citizen>();
                    Zombie z = c;
                    print(z.name);
                    ClassController.zombieList.Add(collision.gameObject);
                    ClassController.citizenList.Remove(collision.gameObject);
                    ClassController.numberZombies += 1;
                    ClassController.numberCitizens -= 1;
                    ClassController.zo.text = "Number of Zombies: " + ClassController.numberZombies;
                    ClassController.ci.text = "Number of Citizens: " + ClassController.numberCitizens;

                    if(ClassController.numberCitizens == 0)
                    {
                        Hero.heroStruct_H.cam.transform.SetParent(null);
                        ClassController.citizenList.Remove(gameObject);
                        Destroy(Hero.heroStruct_H.cam.GetComponent<FPSAim>());
                        Hero.heroStruct_H.cam.transform.position = new Vector3(0, 90, 0);
                        Quaternion rot = Quaternion.Euler(90, 0, 0);
                        Hero.heroStruct_H.cam.transform.rotation = rot;
                        Hero.heroStruct_H.dialogue.text = " ";
                        Hero.heroStruct_H.finalText.text = "You Lost";
                        Destroy(Hero.heroStruct_H.heroObject);
                    }
                }

                if (collision.gameObject.CompareTag("Projectile"))
                {
                    ClassController.zombieList.Remove(gameObject);
                    ClassController.numberZombies -= 1;
                    ClassController.zo.text = "Number of Zombies: " + ClassController.numberZombies;
                    Destroy(gameObject);

                    if(ClassController.numberZombies == 0)
                    {
                        Hero.heroStruct_H.cam.transform.SetParent(null);
                        ClassController.citizenList.Remove(gameObject);
                        Destroy(Hero.heroStruct_H.cam.GetComponent<FPSAim>());
                        Hero.heroStruct_H.cam.transform.position = new Vector3(0, 90, 0);
                        Quaternion rot = Quaternion.Euler(90, 0, 0);
                        Hero.heroStruct_H.cam.transform.rotation = rot;
                        Hero.heroStruct_H.dialogue.text = " ";
                        Hero.heroStruct_H.finalText.text = "You Won";
                    }
                }
            }

            /****************************************************************************************************************Función "ZombieMessage"*******************************************************************************************************************/
            /// <summary>
            /// Una función para retornar la estructura.
            /// </summary>
            /// <returns></returns>
            public ZombieStruct ZombieMessage()
            {
                return zombieStruct_N;                                                                          
            }
        }
    }

    /****************************************************************************************************************************NameSpace Ally************************************************************************************************************************/
    /// <summary>
    /// Creo un namespace para albergar la clase Citizen
    /// </summary>
    namespace Ally                                                                                              
    {
        /************************************************************************************************************************Clase "Citizen"***********************************************************************************************************************/
        /// <summary>
        /// Esta clase se encarga del comportamiento del ciudadano y se sella
        /// para que no se pueda heredar de esta.
        /// </summary>
        public sealed class Citizen : Npc                                                                       
        {
            float distanceC_Z;

            /********************************************************************************************************************Funcion "Start"***********************************************************************************************************************/
            /// <summary>
            /// Se le da nombre escogido de la enumeración y se le asigna un Tag.
            /// </summary>
            void Start()
            {                                                                                   
                citizenStruct_N.names = (Names)Random.Range(0, 20);                                             
                gameObject.name = citizenStruct_N.names.ToString();                                             
                gameObject.tag = "Citizen";                                                                     
            }

            /*******************************************************************************************************************Función "Update"***********************************************************************************************************************/
            /// <summary>
            /// Se llama una función constantemente
            /// </summary>
            void Update()
            {
                Movement();                                                                                     
            }

            /**************************************************************************************************************Funcion "CitizenMessage"********************************************************************************************************************/
            /// <summary>
            /// Una función para retornar la estructura.
            /// </summary>
            /// <returns></returns>
            public CitizenStruct CitizenMessage()                                                               
            {
                return citizenStruct_N;                                                                         
            }

            /*********************************************************************************************************Función Override"DistanceFunction"***************************************************************************************************************/
            /// <summary>
            /// Sobreescribimos la función de la clase "NPC" ya que el ciudadano
            /// hace un par de cosas distintas.
            /// </summary>
            public override void DistanceFunction()
            {
                foreach (GameObject zom in ClassController.zombieList)
                {
                    npcStruct_N.distances = Vector3.Distance(gameObject.transform.position, zom.transform.position);

                    if (npcStruct_N.distances < 5)
                    {
                        if (gameObject.GetComponent<Citizen>() && zom.GetComponent<Zombie>())
                        {
                            gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, zom.transform.position, -npcStruct_N.runSpeed * Time.deltaTime);
                            npcStruct_N.npcBehaviour = NpcBehaviour.Running;
                        }
                    }
                }
            }

            /*****************************************************************************************************************Implicit Operator************************************************************************************************************************/
            /// <summary>
            /// Esta función se encarga hacer el cast de estructuras
            /// </summary>
            /// <param name="c"></param>
            public static implicit operator Zombie(Citizen c)
            {
                Zombie z = c.gameObject.AddComponent<Zombie>();
                z.npcStruct_N.age = c.npcStruct_N.age;
                Destroy(c);
                return z;
            }
        }
    }
}