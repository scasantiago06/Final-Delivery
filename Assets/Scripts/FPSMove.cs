using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/***************************************************************************************************************Clase FPSMove*************************************************************************************************************************************/
public class FPSMove : MonoBehaviour                                                //Creo la clase para hacer mover el personaje.
{
    public float speed;                                                             //Creo una variable de tipo "float" que será la velocidad.

    /*********************************************************************************************************Función "Update"************************************************************************************************************************************/
    void Update()                                                                   //El "Update" se utiliza para comprobar la presión de botones.
    {
        if (Input.GetKey(KeyCode.W))                                                //Si se presiona la tecla "W" pasa lo siguiente.
        {
            transform.position += transform.forward * (speed * Time.deltaTime);     //Se le agrega velocidad al "transform" hacia el frente.
        }
        if (Input.GetKey(KeyCode.S))                                                //Si se presiona la tecla "S" pasa lo siguiente.
        {
            transform.position -= transform.forward * (speed * Time.deltaTime);     //Se le agrega velocidad al "transform" hacia el frente pero en negativo, es decir, hacia atrás.
        }
        if (Input.GetKey(KeyCode.A))                                                //Si se presiona la tecla "A" pasa lo siguiente.
        {
            transform.position -= transform.right * (speed * Time.deltaTime);       //Se le agrega velocidad al "transform" hacia la derecha en negativo, es decir, hacia la izquierda.
        }
        if (Input.GetKey(KeyCode.D))                                                //Si se presiona la tecla "D" pasa lo siguiente.
        {
            transform.position += transform.right * (speed * Time.deltaTime);       //Se le agrega velocidad al "transform" hacia la derecha.
        }
    }
}