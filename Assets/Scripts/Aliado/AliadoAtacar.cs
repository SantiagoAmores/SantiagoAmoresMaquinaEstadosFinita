using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using UnityEngine;

public class AliadoAtacar : AliadoEstado
{
    public Color blanco = Color.white;

    public AliadoAtacar(AliadoIA aliado) : base()
    {
        
        Debug.Log("ATACAR");
        nombre = ESTADO.ATACAR;
        inicializarVariables(aliado);

    }

    public override void Entrar()
    {

        aliadoIA.Agente.speed = 10f; //Velocidad
        aliadoIA.GetComponent<Renderer>().material.color = blanco; //Material
        aliadoIA.transform.localScale *= 2; //Escala
        aliadoIA.Agente.SetDestination(aliadoIA.Enemigo.transform.position); //Direccion
        aliadoIA.animator.Play("Runing"); //Animacion

        base.Entrar();

    }

    public override void Actualizar()
    {
        
        siguienteEstado = new AliadoEsperando(aliadoIA); // Si el NPC no puede atacar al jugador, lo ponemos a vigilar (por ejemplo).
        faseActual = EVENTO.SALIR;

    }

    public override void Salir()
    {

        base.Salir();

    }

    public bool PuedeAtacarEnemigo()
    {
        
        //Distancia a la que detectar al enemigo para dirigirse hacia el
        if (Vector3.Distance(aliadoIA.Enemigo.transform.position, aliadoIA.transform.position) <= 6f)
        {
            return true;
        }

        return false;

    }
}
