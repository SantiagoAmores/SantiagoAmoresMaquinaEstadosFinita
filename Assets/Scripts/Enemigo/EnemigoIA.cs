using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;  // Added since we're using a navmesh.

public class EnemigoIA: MonoBehaviour
{
    PatrulleroEstado FSM;
    public GameObject jugador;
    public Renderer render;

    public Transform puntoA;
    public Transform puntoB;
    public float velocidad = 10f;

    private Vector3 objetivo;
    //private bool enMovimiento = true;

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
}