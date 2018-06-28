using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/****************************************************************************************************************************Clase "FPSAim"*************************************************************************************************************************************/
public class FPSAim : MonoBehaviour                                     //La clase para controlar la cámara.
{
    float mouseX;                                                       //Creo la variable para guardar las coordenadas del mouse en "x".
    float mouseY;                                                       //Creo la variable para guardar las coordenadas del mouse en "y".
    GameObject body;                                                    //Un "GameObject" que necesito para gurdar el cuerpo.
    float mx = -45;                                                     //Esta variable será utilizada para determinar el máximo al que puede llegar "x".
    float yx = 45;                                                      //Esta variable será utilizada para determinar el máximo al que puede llegar "y".

    /*************************************************************************************************************************Función "Start"***********************************************************************************************************************************/
    void Start()                                                        //La función "Start" para que se ejecute al empezar.
    {
        body = GameObject.FindGameObjectWithTag("Player");              //En la variable "body" guardo el objeto que se encuentra con "FindObjectOfType".
    }

    /*************************************************************************************************************************Función "Update"***********************************************************************************************************************************/
    void Update()                                                       //En la "Update" debo verificar constantemente la posición del mouse.
    {
        mouseX += Input.GetAxis("Mouse X");                             //En la variable "mouseX" guardo la posición en "x".
        mouseY -= Input.GetAxis("Mouse Y");                             //En la variable "mouseY" guardo la posicion en "y".
        mouseY = Mathf.Clamp(mouseY, mx, yx);                           //Con "Mathf.Clamp" limito con las variables máximas creadas.
        transform.eulerAngles = new Vector3(mouseY, mouseX, 0);         //Con esta línea muevo la cámara.
        body.transform.eulerAngles = new Vector3(0, mouseX, 0);         //"body" rota con respecto a la cámara solo en "y".
    }
}