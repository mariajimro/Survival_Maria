using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadSceneManager : MonoBehaviour
{
    //Singleton
    //1. nos instanciamos a nosotros mismos(Reservamos un hueco para mi en la RAM)
    private static LoadSceneManager instance;
    // Necesitaremos otras variables
    public Image image;
    public float fadeSpeed;
    
    
    //Nos tenemos que inicializar para crear la copia.

    private void Awake() //El awake se hace antes que el start
    {
        //1. Comprobamos nuestra existencia
        if (instance == null)
        {
            //3. Nos inicializamos , creamos nuestra copia
            //this es una abreviatura para decir LoadSceneManager(a esta propia clase)
            instance = this;
            DontDestroyOnLoad(gameObject); //game object en minúscula es el game object que está asociado el script.
            image.enabled = false; //reseteamos la imagen por si se resetea el alfa.
        }
        else
        {
            Destroy(gameObject);
        }
    }
    
    //Metodo al que llamaremos desde fuera para activar esta carga de escena.

    public static void LoadScene(int buildIndex)
    {
        //TODO: Llamar a la coruting de carga.
        instance.StartCoroutine(instance.LoadNextScene(buildIndex));
    }
    
    //Corutina de carga y face
    IEnumerator LoadNextScene(int buildIndex)
    {
        //1. Activamos el image
        image.enabled = true;
        
        //2. Hacemos el fade in de la pantalla de carga
        
        float a = 0.0f;
        //Bajamos el alpha hasta 0 en un tiempo determinado
        while (a < 1.0f)
        {
            a += (1.0f / fadeSpeed) * Time.deltaTime;
            //Establecer en la imagen el nuevo alpha
            image.color = new Color(0, 0, 0, a);
            yield return null; 
        }
        //3. cuando termine haremos la carga asincrona de la escena
        AsyncOperation ao = SceneManager.LoadSceneAsync(buildIndex);
        while (ao.isDone == false)
        {
            yield return null; //corutina vueleve a repetir el mismo bucle pero en el siguiente frame
        }
        
        //4. haremos el fade out  de la pantalla de carga
        a = 1.0f;
        
        //Bajamos el alpha hasta 0 en un tiempo determinado
        while (a > 0.0f)
        {
            a -= (1.0f / fadeSpeed) * Time.deltaTime;
            //Establecer en la imagen el nuevo alpha
            image.color = new Color(0, 0, 0, a);
            yield return null; 
        }
        
        //5. Desactivamos image
        image.enabled = false;


    }
}
