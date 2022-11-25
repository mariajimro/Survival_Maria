using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class itemObject : MonoBehaviour, IInteractuable
{
    public ItemSO item;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public string GetMensajeEnPantalla()
    {
        //DEVOLVER EL MENSAJE QUE TIENE QUE PINAR EL CANVAS CUANDO APUNTEMOS A UN OBJETO INTERACTUABLE.
        //RECOGER NOMBRE DEL OBJETO
        return "Recoger " + item.nombre;
    }

    public void AlInteractuar()
    {
        Destroy(gameObject);
    }
}
