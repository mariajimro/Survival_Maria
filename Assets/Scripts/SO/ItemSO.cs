using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//1. Necesito decirle a Unity donde aparecerá dentro del menú de creación de assets

[CreateAssetMenu(fileName = "Objeto", menuName = "Nuevo objeto")]
public class ItemSO : ScriptableObject
{
    //Esto es una plantilla para crear assets de tipo item
    [Header("Info")]
    public string nombre;
    public string descripcion;
    public Tipoitem tipo;
    //Necesitamos el Sprite que  va a tener el objeto cuando esté en el inventario.
    public Sprite icono;
    //Prefab para cuando lo soltemos del inventario
    public GameObject dropPrefab;

    [Header("Stackeo")] 
    public bool puedeStackear;
    public int maxCantidadStack;
}

public enum Tipoitem
{
    Recurso,
    Equipable,
    Consumible
}