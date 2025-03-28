using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;  // Added since we're using a navmesh.

public class EnemigoIA: MonoBehaviour
{
    PatrulleroEstado FSM;
    public GameObject jugador;
    public Renderer render;
    public GameObject bala;
    public float fuerzaDisparar = 10f;

    void Start()
    {
        jugador = GameObject.Find("Jugador");
        render = this.GetComponent<Renderer>();
        
        FSM = new PatrulleroVigilar(); // CREAMOS EL ESTADO INICIAL DEL NPC
        FSM.inicializarVariables(this);

    }

    void Update()
    {
        FSM = FSM.Procesar(); // INICIAMOS LA FSM
    }

    public void empezarDisparar()
    {
        StartCoroutine("disparando");
    }

    public void pararDisparar()
    {
        StopCoroutine("disparando");
    }

    public IEnumerator disparando()
    {
        while (true)
        {
            GameObject balaInstanciada = Instantiate(bala, transform.position, Quaternion.identity);

            balaInstanciada.GetComponent<Rigidbody>().AddForce(transform.forward * fuerzaDisparar, ForceMode.Impulse);

            yield return new WaitForSeconds(2);
        }

        yield return null;
    }
}