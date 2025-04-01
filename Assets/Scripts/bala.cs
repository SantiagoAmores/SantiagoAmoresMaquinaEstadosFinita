using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bala : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Pared"))
        {
            // Destruye la bala al impactar la pared para que no se acumulen en escena
            Destroy(gameObject); 
        }
    }
}
