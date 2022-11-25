using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerNeedsManager : MonoBehaviour
{
    /*
     * Variables necesarias:
     * 1.Necesitamos una variable para guardar el valor actual:
     * -> Una variable distinta para cada barra
     * 2. Necesitamos especificar el valor de inicio:
     * -> Una variable para cada barra
     * 3. Valor máximo de cada barra
     * -> Una variable para cada barra
     */
    
    //Creamos los objetos de tipo Need
    public Need health;
    public Need hunger;
    public Need water;
    public Need sleep;
    
    //Variables especificas
    //Cantidad de vida que voy a perder vuando tenga sed y/o hambre
    public float hungerHealthDecay;
    public float waterHealthDecay;
    
    //Variable Gradient para cambiar el color de la barra

    public Gradient colors;

    public DamageIndicatorController damageIndicator;
    
    void Start()
    {
        //Establecemos los valores iniciales de cada Need
        health.curValue = health.startValue; //Valor actual = Valor inicial
        hunger.curValue = hunger.startValue;
        water.curValue = water.startValue;
        sleep.curValue = sleep.startValue;
    }

    // Update is called once per frame
    void Update()
    {
        // Hambre , sed y sueño se desgasten por segundo,decavRate es la cantidad que queremos que baje por segundo.
        hunger.Subtract(hunger.decayRate * Time.deltaTime);
        water.Subtract(water.decayRate * Time.deltaTime);
        sleep.Add(sleep.regenRate * Time.deltaTime);
        //Hambre y sed a cero la vida empezará a bajar.
        if (hunger.curValue == 0.0f)
            health.Subtract(hungerHealthDecay * Time.deltaTime);
        if (water.curValue == 0.0f)
            health.Subtract(waterHealthDecay * Time.deltaTime);
        //Compruebo si estoy muerto
        if (health.curValue == 0.0f)
        {
            Die();
        }
            
        
        
        
        
        //Actualizar el tamaño de las barras
        health.uiBar.fillAmount = health.GetPercentage();
        hunger.uiBar.fillAmount = hunger.GetPercentage();
        hunger.uiBar.color = colors.Evaluate(hunger.GetPercentage());
        sleep.uiBar.fillAmount = sleep.GetPercentage();
        water.uiBar.fillAmount = water.GetPercentage();
    }
  // aciones
    public void Heal(float amount)

    {
        health.Add(amount);
    }

    public void Eat(float amount)
    {
        hunger.Add(amount);
    }

    public void Drink(float amount)
    {
        water.Add(amount);
    }

    public void Sleep(float amount)
    {
        sleep.Subtract(amount);
    }

    public void Die()
    {
        Debug.Log("Estoy muerto");
    }

    public void TakeDamage(float amount)
    {
        //Llamo al indicador de daño
        damageIndicator.Fade();
        health.Subtract(amount);
    }


}//Final de PlayerNeedsManager
//Creamos la clase Nuera de  la clase principal
//Programación orientda a objetos -> class
//Objeto Need
[System.Serializable] //Esto le dice a unity entre otras cosas que use sus variables en el editor siempre que no se ereden del monovieavour

public class Need
{
    //VARIABLES COMUNES
    //Valor actual de la barra
    [HideInInspector] //Solo oculta la linea de abajo en el inpector de unity

    public float curValue;
    //valor max de la barra
    public float maxValue;
    //Valor inicial de la barra
    public float startValue;
    //Cantidad  de regeneración por segundo
    public float regenRate;
    //Cantidad de desgaste
    public float decayRate;
    //Objeto imagen de la barra
    //Pra poder que suba y baje visualmente
    public Image uiBar;
    
    //Métodos comunes o acciones comunes (amount es cantidad)
    //Sumar

    public void Add(float amount)
    {
        curValue = Mathf.Min(curValue + amount, maxValue);
    }
    //restar
    public void Subtract(float amount)
    {
        curValue = Mathf.Max(curValue - amount, 0.0f);
    }
    //Teniendo en cuenta que nuestra barra funciona con porcentajes, tenemos que pasarel valor actual a porcentaje

    public float GetPercentage()
    {
        return curValue / maxValue;
    }
}
