using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AliadoIA : MonoBehaviour
{
    public GameObject Jugador;
    public GameObject Enemigo;

    public NavMeshAgent Agente;
    public Animator animator;

    AliadoEstado FSM;
    
    // Start is called before the first frame update
    void Start()
    {
        Jugador = GameObject.Find("Jugador");
        Enemigo = GameObject.Find("Enemigo");

        Agente = GetComponent<NavMeshAgent>();

        animator = GetComponent<Animator>();

        FSM = new AliadoEsperando(this);

    }

    // Update is called once per frame
    void Update()
    {
        FSM = FSM.Procesar();
    }

    void OnTriggerEnter(Collider other)
    {
        //Al entrar en contacto se destruyen el aliado y el enemigo
        if (other.CompareTag("Enemigo"))
        {
            Destroy(other.gameObject);
            Destroy(gameObject);
        }
    }

}
