using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*********************************************************************************************Estructura del héroe****************************************************************************************/
public struct HeroStruct                                                                                //Creo la estructura para almacenar las variables del héroe.
{
    public Vector3 positionHero;                                                                        //Creo una variable de tipo "Vector3" para guardar la posición del personaje.
    public GameObject cam;                                                                              //Creo una variable de tipo "GameObejct" para próximamente guardar la cámara principal de la escena.
    public GameObject gun;
    public GameObject shoot;
    public Rigidbody rb;                                                                                //Creo una variable de tipo "RigidBody".
    public Text dialogue;                                                                               //Creo una variable de tipo "Text".
    public int countProjectiles;
    public Slider health;
}

/**********************************************************************************************Estructura del Npc*****************************************************************************************/
public struct NpcStruct
{
    public NpcBehaviour npcBehaviour;                                                                   //Creo una variable del tipo de la enumeración "NpcBehaviour" para poder interáctuar los comportamientos que irá teniendo.
    public int age;                                                                                     //Dentro de la estructura creo una variable de tipo "age" para manejar la edad.
    public int randomRotation;                                                                          //Creo una variable de tipo "int" para determinar la dirección a la que va a rotar.
    public int rotationVelocity;                                                                        //Creo una variable de tipo "int" que será la velocidad a la que rotarán los objetos con este script.
    public float runSpeed;                                                        
    public float distances;
}

/***********************************************************************************************Estructura del Zombie**************************************************************************************/
public struct ZombieStruct                                                                              //Creo la estructura para almacenar las variables del Zombie.
{
    public int randomColor;                                                                             //Creo una variable de tipo "int" para determinar según el número, que color será asignado.
    public BodyPart bodyPart;                                                                           //Creo una variable del tipo de la enumeración "BodyPart" para poder interactuar con las partes que se irán eligiendo.
}

/***********************************************************************************************Estructura del ciudadano**************************************************************************************/
public struct CitizenStruct                                                                             //Creo la estructura para almacenar las variables del ciudadano.
{
    public string randomName;                                                                           //Dentro de la estructura creo una variable de tipo "string" para manejar los nombres aleatorios dentro de la matríz.
    public Names names;                                                                                 //Dentro de la estructura creo una variable del tipo de la enumeración "Names".
}

/************************************************************************************************Enumeración de estados**************************************************************************************/
public enum NpcBehaviour                                                                                //Creo la enumeración "ZombieBehaviour" y almaceno los comportamientos que tendrá.
{
    Idle, Moving, Rotating, Running                                              
}

/********************************************************************************************Eumeración de partes del cuerpo*********************************************************************************/
public enum BodyPart                                                                                    //Creo la enumeración "BodyPart" y almaceno las partes posibles para luego escoger.
{
    Brain, Eyes, Legs, Fingers, neck                                                                    //Creo los nombres de las partes que necesito.
}

/*************************************************************************************************Eumeración de nombres***************************************************************************************/
public enum Names                                                                                       //Creo una enumeración donde guardo en las próximas dos líneas 20 nombres para después acceder a ellos.
{
    Stubbs, Rob, Rodolfo, Arnulfo, Jesús, Cristian, Santiago, Alonso, Dios, Samuel,                     //En esta línea y la siguiente esta la lista de nombres que se utilizarón
    Ricardo, José, Armando, Luna, María, Mónica, Manuela, Cristobal, Furgo, Andrés                      //para darle nombres a los ciudadanos.
}