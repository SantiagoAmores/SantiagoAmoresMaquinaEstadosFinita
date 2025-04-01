using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;  // Added since we're using a navmesh.

public class EnemigoIA: MonoBehaviour
{

    PatrulleroEstado FSM;

    // Patrulla
    public NavMeshAgent agente;
    public GameObject puntoA;
    public GameObject puntoB;

    // EnemigoIA
    public GameObject enemigo;
    public Renderer render;
    public GameObject bala;
    public float fuerzaDisparar = 10f;
    public bool estaDisparando = false;

    // Jugador
    public GameObject jugador;
    

    void Start()
    {
        // Referenciamos al jugador
        jugador = GameObject.Find("Jugador");

        // Referenciamos al enemigo
        enemigo = GameObject.Find("Enemigo");
        render = this.GetComponent<Renderer>();
        
        // Iniciamos FSM
        FSM = new PatrulleroVigilar(); // CREAMOS EL ESTADO INICIAL DEL NPC
        FSM.inicializarVariables(this);

        // Referenciamos puntos para la patrulla
        puntoA = GameObject.Find("PuntoA");
        puntoB = GameObject.Find("PuntoB");

    }

    void Update()
    {
        FSM = FSM.Procesar(); // INICIAMOS LA FSM
    }

    public void empezarDisparar()
    {

        // Solo inicia la corrutina si no está disparando
        if (!estaDisparando)
        {
            StartCoroutine("disparando");
        }
       
    }

    public void pararDisparar()
    {
        // Dejamos de disparar y marcamos el bool como false
        StopCoroutine("disparando");
        estaDisparando = false;

    }

    public IEnumerator disparando()
    {

        //Cuando bool estaDisparando es true puede disparar
        estaDisparando = true;
        
        while (true)
        {
            // Dirección del disparo
            Vector3 dondeDisparar = jugador.transform.position;

            // Instanciar bala y aplicamos fuerza
            GameObject balaInstanciada = Instantiate(bala, transform.position, Quaternion.identity);
            balaInstanciada.GetComponent<Rigidbody>().AddForce((dondeDisparar - transform.position).normalized * fuerzaDisparar, ForceMode.Impulse);

            // Esperar antes del siguiente disparo
            yield return new WaitForSeconds(2);
        }
    }

}