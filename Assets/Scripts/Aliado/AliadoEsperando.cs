using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AliadoEsperando : AliadoEstado
{
    public Color morado = Color.magenta;

    public AliadoEsperando(AliadoIA aliado) : base()
    {
        
        Debug.Log("VIGILAR");
        nombre = ESTADO.ESPERAR;
        inicializarVariables(aliado);

    }

    public override void Entrar()
    {
       
        aliadoIA.GetComponent<Renderer>().material.color = morado; //Material
        aliadoIA.animator.Play("Idle"); //Animacion

        base.Entrar();

    }

    public override void Actualizar()
    {
        //Si puede ver al jugador deja de esperar
        if (PuedeVerJugador())
        {
            siguienteEstado = new AliadoSiguiendo(aliadoIA);
            faseActual = EVENTO.SALIR;
        }
    }

    public override void Salir()
    {
   
        base.Salir();

    }

    public bool PuedeVerJugador()
    {

        //Distancia a la que detectar al jugador para seguirle
        if (Vector3.Distance(aliadoIA.Jugador.transform.position, aliadoIA.transform.position) <= 5f)
        {
            return true;
        }
        return false;

    }

}
