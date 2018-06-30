using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/****************************************************************************************************************************Clase "FPSAim"*****************************************************************************************************************************/
/// <summary>
/// Esta clase se encargará de el movimiento de la cámara.
/// </summary>
public class FPSAim : MonoBehaviour                                     
{
    /// <summary>
    /// Definimos las variables necesarias.
    /// </summary>
    float mouseX;                                                       
    float mouseY;                                                       
    GameObject body;                                                    
    float mx = -45;                                                     
    float yx = 45;

    /*************************************************************************************************************************Función "Start"***************************************************************************************************************************/
    /// <summary>
    /// La función "Start" guardará en la variable "body" el objeto que se
    /// encuentre con el Tag de "Player".
    /// </summary>
    void Start()                                                        
    {
        body = GameObject.FindGameObjectWithTag("Player");              
    }

    /*************************************************************************************************************************Función "Update"**************************************************************************************************************************/
    /// <summary>
    /// La función "Update" verificará el movimiento del mouse para por medio de este
    /// se mueva la cámara, a la vez que se asigna un límite al movimiento tanto en el
    /// eje "x" como en el "y", para que no pueda dar giros completos.
    /// </summary>
    void Update()                                                       
    {
        if(body != null)
        { 
            mouseX += Input.GetAxis("Mouse X");                         
            mouseY -= Input.GetAxis("Mouse Y");                         
            mouseY = Mathf.Clamp(mouseY, mx, yx);                       
            transform.eulerAngles = new Vector3(mouseY, mouseX, 0);     
            body.transform.eulerAngles = new Vector3(0, mouseX, 0);     
        }
    }
}