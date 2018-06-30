using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// En este documento se crean todas las estructuras necesarias con sus respectivas variables
/// </summary>

/*********************************************************************************************Estructura del héroe****************************************************************************************/
public struct HeroStruct                                                                                
{
    public Vector3 positionHero;                                                                        
    public GameObject heroObject;                                                                       
    public GameObject cam;                                                                              
    public GameObject gun;                                                                              
    public GameObject shoot;
    public Rigidbody rb;                                                                               
    public Text dialogue;                                                                              
    public Text finalText;
    public int countProjectiles;
    public Slider health;
}

/**********************************************************************************************Estructura del Npc*****************************************************************************************/
public struct NpcStruct
{
    public NpcBehaviour npcBehaviour;                                                                   
    public int age;                                                                                     
    public int randomRotation;                                                                          
    public int rotationVelocity;                                                                        
    public float runSpeed;                                                        
    public float distances;
}

/***********************************************************************************************Estructura del Zombie**************************************************************************************/
public struct ZombieStruct                                                                              
{
    public int randomColor;                                                                             
    public BodyPart bodyPart;                                                                           
}

/***********************************************************************************************Estructura del ciudadano**************************************************************************************/
public struct CitizenStruct                                                                             
{
    public string randomName;                                                                           
    public Names names;                                                                                 
}

/************************************************************************************************Enumeración de estados**************************************************************************************/
public enum NpcBehaviour                                                                                
{
    Idle, Moving, Rotating, Running                                              
}

/********************************************************************************************Eumeración de partes del cuerpo*********************************************************************************/
public enum BodyPart                                                                                    
{
    Brain, Eyes, Legs, Fingers, neck                                                                    
}

/*************************************************************************************************Eumeración de nombres***************************************************************************************/
public enum Names                                                                                       
{
    Stubbs, Rob, Rodolfo, Arnulfo, Jesús, Cristian, Santiago, Alonso, Dios, Samuel,                     
    Ricardo, José, Armando, Luna, María, Mónica, Manuela, Cristobal, Furgo, Andrés                      
}