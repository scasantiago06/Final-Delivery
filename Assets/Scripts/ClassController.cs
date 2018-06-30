using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using NPC.Enemy;                                                                                    
using NPC.Ally;

/*************************************************************************************************************************Clase ClassController*************************************************************************************************************************/
/// <summary>
/// Clase principal que se encarga de iniciar el juego.
/// </summary>
public class ClassController : MonoBehaviour
{
    /// <summary>
    /// Se definen las variables necesarias.
    /// </summary>
    /// 
    int numberOfCubes;                                                                              
    int randomComponent;                                                                            
    int randomZombie;
    const int MAXCUBES = 25;  
    
    public static List<GameObject> zombieList = new List<GameObject>();                  
    public static List<GameObject> citizenList = new List<GameObject>();
    public static int numberZombies;                                                                
    public static int numberCitizens;                                                               
    public static Text zo;                                                                          
    public static Text ci;

    /*************************************************************************************************************************Funcion "Awake"***************************************************************************************************************************/
    /// <summary>
    /// En la función "Awake" se encuentran objetos en la escena para
    /// llenar las respectivas variables.
    /// </summary>
    void Awake()
    {
        zo = GameObject.FindGameObjectWithTag("ZombieText").GetComponent<Text>();
        ci = GameObject.FindGameObjectWithTag("CitizenText").GetComponent<Text>();
    }

    /*************************************************************************************************************************Función "Start"***************************************************************************************************************************/
    /// <summary>
    /// Primero instancio el constructor de la clase "_ClassController" para utilizar su
    /// variable "ReadOnly". Luego, dependiendo de las variables llamadas "randomComponent" y
    /// "numberOfCubes" se crearán cubos y se les asignara un script al azar, la creacion esta
    /// dentro de un bucle "for". Por último, con los bucles foreach se cuenta la cantidad de
    /// zombies y de ciudadanos para poderlos imprimir el la UI.
    /// </summary>
    void Start()
    {
        _ClassController cl = new _ClassController();                                               
        numberOfCubes = Random.Range(cl.minCubes, MAXCUBES);                                        
        randomComponent = 0;          
        
        for (int i = 0; i <= numberOfCubes; i++)                                                    
        {
            GameObject go = GameObject.CreatePrimitive(PrimitiveType.Cube);                         
            go.transform.position = new Vector3(Random.Range(-30, 30), 0, Random.Range(-30, 30));   

            if (randomComponent == 0)                                                               
            {
                go.AddComponent<Hero>();                                                            
                go.AddComponent<Rigidbody>();                                                       
                citizenList.Add(go);
            }
            else if (randomComponent == 1)                                                          
            {
                go.AddComponent<Zombie>();                                                          
                go.AddComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation;     
                zombieList.Add(go);
                randomZombie = Random.Range(0, 2);

                if(randomZombie == 1)
                {
                    go.transform.localScale = new Vector3(1, 2, 1);
                }
            }
            else if (randomComponent == 2)                                                          
            {
                go.AddComponent<Citizen>();                                                         
                go.AddComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation;     
                citizenList.Add(go);                                                                
            }

            randomComponent = Random.Range(1, 3);                                                   
        }
        
        foreach (GameObject z in zombieList)                                                        
        {
            if (z.GetComponent<Zombie>())
            { 
                zo.text = "Number of zombies: " + (numberZombies += 1);                               
            }
        }
        foreach (GameObject c in citizenList)
        {
            if (c.GetComponent<Citizen>())
            { 
                ci.text = "Number of citizens: " + (numberCitizens += 1);
            }
        }
    }
}

/*******************************************************************************************************************Clase "_ClassController"*************************************************************************************************************************/
/// <summary>
/// Creo una clase con su constructor para crear e inicializar una
/// variable "ReadOnly".
/// </summary>
public class _ClassController                                                                       
{
    public readonly int minCubes;                                                                   

    /************************************************************************************************************Constructor _ClassController"************************************************************************************************************************/
    public _ClassController()                                                                       
    {
        minCubes = Random.Range(5, 15);                                                             
    }
}