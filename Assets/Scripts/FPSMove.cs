using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/***************************************************************************************************************Clase FPSMove*******************************************************************************************************************************************/
/// <summary>
/// Esta clase hará que el heroe se pueda mover.
/// </summary>
public class FPSMove : MonoBehaviour                                            
{
    public float speed;

    /*********************************************************************************************************Función "Update"******************************************************************************************************************************************/
    /// <summary>
    /// El "Update" verificará que tecla se presiona y dependiendo de esta, moverá
    /// a un lado o al otro al objeto que tenga este script.
    /// La velocudad será aplicada por la variable "float" llamada "speed", a la cual
    /// se le da un valor en otra clase.
    /// </summary>
    void Update()                                                                   
    {
        if (Input.GetKey(KeyCode.W))                                                
        {
            transform.position += transform.forward * (speed * Time.deltaTime);     
        }
        if (Input.GetKey(KeyCode.S))                                                
        {
            transform.position -= transform.forward * (speed * Time.deltaTime);     
        }
        if (Input.GetKey(KeyCode.A))                                                
        {
            transform.position -= transform.right * (speed * Time.deltaTime);       
        }
        if (Input.GetKey(KeyCode.D))                                                
        {
            transform.position += transform.right * (speed * Time.deltaTime);       
        }
    }
}