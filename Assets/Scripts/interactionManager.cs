using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class interactionManager : MonoBehaviour
{
    
    //VARIABLES
    public float checkRate = 0.05f;
    private float lastCheckTime;
    public float distancia;
    public LayerMask layerMask;
    

}

//LA INTERFAZ SE PONE FUERA DE LA CLASE PRINCIPAL
public interface IInteractuable
{
    //NOMBRAMOS 2 MÉTODOS.
    string GetMensajeEnPantalla();
    void AlInteractuar();
}
