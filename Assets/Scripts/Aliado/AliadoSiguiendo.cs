using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class AliadoSiguiendo : AliadoEstado
{
    public Color azul = Color.cyan;
    
    public AliadoSiguiendo(AliadoIA aliado) : base()
    {
        Debug.Log("SEGUIR");
        nombre = ESTADO.SEGUIR;
        inicializarVariables(aliado);
    }
    public override void Entrar()
    {
        aliadoIA.GetComponent<Renderer>().material.color = azul;
        aliadoIA.transform.localScale *= 2;
        aliadoIA.animator.Play("Runing");

        base.Entrar();
    }

    public override void Actualizar()
    {
        if (PuedeVerEnemigo()){
            siguienteEstado = new AliadoAtacar(aliadoIA);
            faseActual = EVENTO.SALIR;
        }

        aliadoIA.Agente.SetDestination(aliadoIA.Jugador.transform.position);
    }

    public override void Salir()
    {
        base.Salir();
    }

    public bool PuedeVerEnemigo()
    {
        if (Vector3.Distance(aliadoIA.Enemigo.transform.position, aliadoIA.transform.position) <= 10f)
        {
            return true;
        }

        return false;

    }
}
