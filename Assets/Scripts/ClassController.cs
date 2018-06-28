using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using NPC.Enemy;                                                                                    //Para utilizar la clase "Zombie" utilizo la directiva donde se encuentra esta.
using NPC.Ally;                                                                                     //Para utilizar la clase "Citizen" utilizo la directiva donde se encuentra esta.

/*************************************************************************************************************************Clase ClassController**********************************************************************************************************************/
public class ClassController : MonoBehaviour
{
    int numberOfCubes;                                                                              //Creo una variable de tipo "int" que se encargará del número de cubos que se creen.                                                                
    int randomComponent;                                                                            //Creo una variable de tipo "int" que se encargará luego de, según el número, agregar un componente.
    const int MAXCUBES = 25;                                                                        //Creo una variable constante de tipo "int" que será el valor máximo de cubos que se podrán crear y no se puede editar una vez comience el juego.
    public static List<GameObject> zombieList = new List<GameObject>();                  
    public static List<GameObject> citizenList = new List<GameObject>();
    public static int numberZombies;                                                                              //Creo una variable de tipo "int" que será el contador de zombies.
    public static int numberCitizens;                                                                             //Creo una variable de tipo "int" que será el contador de ciudadanos.
    public static Text zo;                                                                                 //Creo una variable a la cual añadir el texto del zombie.
    public static Text ci;                                                                                 //Creo una variable a la cual añadir el texto del ciudadano.

    /************************************************************************************************************************Funcion "Start"************************************************************************************************************************/
    private void Awake()
    {
        zo = GameObject.FindGameObjectWithTag("ZombieText").GetComponent<Text>();
        ci = GameObject.FindGameObjectWithTag("CitizenText").GetComponent<Text>();
    }
    public void Start()
    {
        //projectile = GameObject.FindGameObjectWithTag("Projectile").GetComponent<Rigidbody>();
        _ClassController cl = new _ClassController();                                               //Llamo la clase que contiene el contructor necesario para cambiar la variable "ReadOnly".
        numberOfCubes = Random.Range(cl.minCubes, MAXCUBES);                                        //Inicializo "numberOfCubes" a un valor aleatorio entre 10 y 21, esto definirá la cantidad de cubos que se creen, lo mínimo será 10 y el máximo 20.
        randomComponent = 0;                                                                        //La variable "randomComponent" se inicializa a cero.
        for (int i = 0; i <= numberOfCubes; i++)                                                    //Se hace el bucle con la siguiente condición: "i" = 0, hasta que sea menor a "numberOfCubes" vaya aumentando en uno.
        {
            GameObject go = GameObject.CreatePrimitive(PrimitiveType.Cube);                         //Creo una variable de tipo "GameObject" llamada "go" para almacenar el cubo que se crea después del "=".
            go.transform.position = new Vector3(Random.Range(-30, 30), 0, Random.Range(-30, 30));   //Luego decimos que la posición de "go" será igual a una nueva posición con el "Vector3" y entre paréntesis le damos un valor a X, Y, Z. De los cuales y será cero, y los demás serán valores aleatorios entre -20 y 20.

            if (randomComponent == 0)                                                               //En un principio "randomComponent" es igual a 0, así que en la primera vuelta de bucle entrará a este condicional.
            {
                go.AddComponent<Hero>();                                                            //Al cubo "go" que se acabó de crear se le agrega el componente llamado "Hero" que es una clase.
                go.AddComponent<Rigidbody>();                                                       //Al cubo "go" que se acabó de crear se le agrega el componente llamado "RigidBody".
                citizenList.Add(go);
            }
            else if (randomComponent == 1)                                                          //Cuando el valor almacenado en "randomComponent" sea igual a 1, entrará en este condicional.
            {
                go.AddComponent<Zombie>();                                                          //Al cubo "go" que se acabó de crear se le agrega el componente llamado "Zombie" que es una clase dentro de la directiva "NPC.Enemy".
                go.AddComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation;     //Al cubo "go" que se acabó de crear se le agrega el componente llamado "RigidBody" y le doy verdadero a la rotación para que no se de la vuelta.
                zombieList.Add(go);                              
            }
            else if (randomComponent == 2)                                                          //Cuando el valor almacenado en "randomComponent" sea igual a 2, entrará en este condicional.
            {
                go.AddComponent<Citizen>();                                                         //Al cubo "go" que se acabó de crear se le agrega el componente llamado "Citizen" que es una clase dentro de la directiva "NPC.Ally".
                go.AddComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation;     //Al cubo "go" que se acabó de crear se le agrega el componente llamado "RigidBody" y le doy verdadero a la rotación para que no se de la vuelta.
                citizenList.Add(go);                                                                
            }
            randomComponent = Random.Range(1, 3);                                                   //Por último, después de verificar todos los condicionales "if" anteriores, se cambia el valor de "randomComponent" para la próxima vuelta del bucle.
        }

        //zo.text = "Number of Zombies" + zombieList.Capacity;
        //////
        foreach (GameObject z in zombieList)                                                        //Creo un "foreach" y entre paréntesis digo: para cada objeto "gos" (Nombre que le dí a dichos objetos) en la lista hacer:
        {
           zo.text = "Number of zombies: " + (numberZombies += 1);                                 //El texto de "zo" cambia por lo que esta después del igual.
        }

        foreach (GameObject c in citizenList)
        {
            ci.text = "Number of citizens: " + (numberCitizens += 1);
        }
    }
}

/*******************************************************************************************************************Clase "_ClassController"*************************************************************************************************************************/
public class _ClassController                                                                       //Creo una clase que no hereda de "Monobehaviour".
{
    public readonly int minCubes;                                                                   //Creo una variable "ReadOnly" de tipo "int" que será el valor mínimo de cubos que se podrán crear y no se puede editar a menos que sea desde un constructor.

    /************************************************************************************************************Constructor _ClassController"************************************************************************************************************************/
    public _ClassController()                                                                       //Creo un constructor de la clase "_ClassController".
    {
        minCubes = Random.Range(5, 15);                                                             //Modifico la variable minCubes y la igualo a 5, recordando que las variables "ReadOnly" solo se pueden modificar al momento de crearse o en un constructor.
    }
}