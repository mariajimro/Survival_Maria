using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DamageIndicatorController : MonoBehaviour
{
    //Variables que necesitamos
    //acceder al componente image para poder activarlo cuando muera
    public Image image;
    //variable para indicar la velocidad de fadedown
    public float fadeSpeed;
    //para controlar el estado de la corutina guardo su ejecucion en una variable
    private Coroutine fadeAway;
    // Start is called before the first frame update
    

  //creo un método que llamaremos desde PlayerNeeds, cada vez que se ejecute el método TakeDamage
  //para iniciar una corutina que mostrará el DamageIndicator

  public void Fade()
  {
      //Compruebo si hay una corutina ejecutándose
      if (fadeAway != null)
      {
          StopCoroutine(fadeAway);
      }

      //Empezamos una nueva corutina
      image.enabled = true;
      image.color = Color.white;
      //TODO: ejecutaremos la corutina 
      fadeAway = StartCoroutine(FadeAway());
  }

  IEnumerator FadeAway()
      {
          //Establecemos una variable para guardar el alfa inicial
          float a = 1.0f;
          //Bajamos el alpha hasta 0 en un tiempo determinado
          while (a > 0.0f)
          {
              a -= (1.0f / fadeSpeed) * Time.deltaTime;
              //Establecer en la imagen el nuevo alpha
              image.color = new Color(1, 1, 1, a);
              yield return null; //
          }

          image.enabled = false;
      }
  
}
