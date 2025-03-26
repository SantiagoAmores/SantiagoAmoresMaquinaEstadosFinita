using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class PatrulleroAtacar : PatrulleroEstado
{
    public PatrulleroAtacar() : base()
    {
        Debug.Log("ATACAR");
        nombre = ESTADO.ATACAR; // Guardamos el nombre del estado en el que nos encontramos.
    }

    public override void Entrar()
    {
        // Le pondr�amos la animaci�n de disparar, o lo que sea...
        base.Entrar();

        enemigoIA.render.material.color = Color.red;
    }

    public override void Actualizar()
    {

        if (!PuedeVerJugador())
        {
            siguienteEstado = new PatrulleroVigilar(); // Si el NPC no puede atacar al jugador, lo ponemos a vigilar (por ejemplo).
            faseActual = EVENTO.SALIR; // Cambiamos de FASE ya que pasamos de ATACAR a VIGILAR.
        }
    }

    public override void Salir()
    {
        // Le resetear�amos la animaci�n de disparar, detener las corrutinas, o lo que sea...
        base.Salir();
    }

    public bool PuedeAtacar()
    {
        // ...
        return false; // El NPC NO EST� lo suficientemente cerca para atacar al jugador.
    }
}