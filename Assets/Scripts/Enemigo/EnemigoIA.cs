using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;  // Added since we're using a navmesh.

public class EnemigoIA: MonoBehaviour
{
    Estado FSM;
    public GameObject jugador;
    public Renderer render;

    void Start()
    {
        jugador = GameObject.Find("Jugador");
        render = this.GetComponent<Renderer>();
        
        FSM = new Vigilar(); // CREAMOS EL ESTADO INICIAL DEL NPC
        FSM.inicializarVariables(this);

    }

    void Update()
    {
        FSM = FSM.Procesar(); // INICIAMOS LA FSM
    }
}