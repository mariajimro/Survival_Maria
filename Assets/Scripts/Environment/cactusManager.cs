using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cactusManager : MonoBehaviour
{
    
    //Daño
    public float damage;
    //cada cuantos seg va a pegar
    public float damageRate;
    //lista de gente a la que tiene que pegar
    private List<GameObject> thingsToDamage = new List<GameObject>();
        
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(DealDamage()); //PARA INICIAR LAS CORUTINAS

    }

    //si alguien entra en colision lo meto en la lista
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            thingsToDamage.Add(collision.gameObject);
        }
        else if (collision.gameObject.CompareTag("Enemy"))
        {
            thingsToDamage.Add(collision.gameObject);
        }
    }

    private void OnCollisionExit(Collision other)
    {
        if (thingsToDamage.Contains(other.gameObject))
        {
            thingsToDamage.Remove(other.gameObject);
        }
    }
    //Rutina con bucle infinito que se va a ejecutar cada x segundos
    //es como un metodo pero no empieza por public suno por ienumerator
    IEnumerator DealDamage()
    {
        //Dentro de un bucle infinito vamos a hacer daño a todos los de la lista, cada 0.5 segundos
        while (true) //bucle infinito pero como es cada 0.5 segundos no sobrecargamos el juego.
        {
            //toodo es para especificar tareas que todavía tengo que programar
            
            
            for (int i = 0; i < thingsToDamage.Count; i++)
            {
                //OJO podemos tener al player o enemigos, t hay que comprobar cual es. 
                switch (thingsToDamage[i].tag)
                {
                    case "Player":
                        thingsToDamage[i].GetComponent<PlayerNeedsManager>().TakeDamage(damage);
                        break;
                    //TODO: PONER CASE DE PLAYER
                    
                }
                
            }
            //decirle a la corutina que pare 0.5seg
            yield return new WaitForSeconds(damageRate);

        }
        
            
        
    }
}
