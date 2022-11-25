using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController1 : MonoBehaviour
{
    [Header("Camera")] public Transform cameraContainer;
    
    //minima rotacion al girar v
    public float minXLook;
    public float maxXLook;
    public float lookSensi; //para limitar sensibilidad de raton <
    private float camCurXRot;    //paara guardar el vector 2 del raton<
    private Vector2 mouseDelta; //debuelve la diferncia de movimiento, si nos movemos de 1 a 5 serían 4

    [Header("Movimiento")]
    public float moveSpeed;
    private Vector2 curMovInput;
    private Rigidbody rig; //para pasarle el velocity
    public float jumpForce;
    public LayerMask groundLayerMask;
    
    //Inicializamos el rigidbody
    private void Awake()
    {
        rig = GetComponent<Rigidbody>();
    }


    // Start is called before the first frame update
    void Start()
    {
        //Escondemos el ratón
        Cursor.lockState = CursorLockMode.Locked;

    }

   
    /*
     * Metodo que será llamado por el InputSystem.
     * En el que capturaremos el Vector2 del ratón, y moveremos la cámara.
     */
    public void OnLookInput(InputAction.CallbackContext context)
    {
        mouseDelta = context.ReadValue<Vector2>();
       
    }
    /*
     * Metodo que será llamado por el InputSystem.
     * En el que capturaremos el Vector2 del movimiento.
     */
    public void OnMoveInput(InputAction.CallbackContext context)
    {
        //Comprobamos si se están pulsando las teclas AAAAA//vvvvvvv
        if (context.phase == InputActionPhase.Performed) //performed, es manteniendo el boton sigue detectando, started solo detecta el primer frame
        {
            curMovInput = context.ReadValue<Vector2>();
        }
        else if (context.phase == InputActionPhase.Canceled)
        {
            curMovInput = Vector2.zero;

        }

    }
    /*
     * Método que sera llamado por el imput sistem
     * en el que comprobaremos si se ha presionado
     * el botón de saltar
     */
    public void  OnJumpInput(InputAction.CallbackContext context)
    {
        /*
         * 1.Se acaba de presionar el botón de saltar?
         * vvvvvvv  context.started es lo mismo que AAAAAA context.phase == InputActionPhase.Performed)
         */
        if (context.started)
        {
            /*
             * 2.Comprobar si estamos tocando tierra
             */
            if (Grounded())
            {
                /*
                 * 3.Saltar
                 */
                rig.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            }
        }
    }

    private bool Grounded()
    {
        //Creamos un array de Ray llamada rays el numero 4 es el número de rayos
        Ray[] rays = new Ray[4]
        {
            new Ray(transform.position + (transform.forward * 0.2f), Vector3.down),
            new Ray(transform.position + (-transform.forward * 0.2f), Vector3.down),
            new Ray(transform.position + (transform.right * 0.2f), Vector3.down),
            new Ray(transform.position + (-transform.right * 0.2f), Vector3.down),

        };
        
        //Disparamos los 4 rayos , y comprobamos si alguno toca TIERRA 0.1 sera la distancia de rayo
        for (int i = 0; i < rays.Length; i++)
        {
            if (Physics.Raycast(rays[i],0.1f, groundLayerMask))
            {
                return true;
            }
            
        }

        return false;
    }

    private void LateUpdate()
    {
        CameraLook();
    }
    
    //fixed update para fisicas , se ejecuta 60 veces x seg,activar rigidbody

    private void FixedUpdate()
    {
        Move();
    }

    private void Move() //void no evuelve nada
    {
        /*
         * Calcular la dirección de movimiento, diciéndole la dirección en la que se tiene que mover
         * Pero relativa a donde estemos mirando
         */
        Vector3 dir = transform.forward * curMovInput.y
                      + transform.right * curMovInput.x;
        
        /*
         * Darle velocidad
         */

        dir *= moveSpeed;
        /*
         * Liberamos la Y del dir
         */
        dir.y = rig.velocity.y;
        dir.y = rig.velocity.y;
        
        /*
         * Para mover el rigidbody
         */

        rig.velocity = dir;
    }


    private void CameraLook()
    {
        //Rotación vertical de la cámara
        camCurXRot += mouseDelta.y * lookSensi;
        //Limitar la rotación para que no pase del ínimo ni máximo.
        camCurXRot = Mathf.Clamp(camCurXRot, minXLook, maxXLook); //mathfclamp limita los max y min de la camara.
        //Rotar en vertical
        cameraContainer.localEulerAngles = new Vector3(-camCurXRot, 0, 0);
        //Rotar en horizontal
        transform.eulerAngles += new Vector3(0, mouseDelta.x * lookSensi, 0);
    }
    
    

}