using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

// Constructor para VIGILAR
public class PatrulleroVigilar : PatrulleroEstado
{

    // Patrulla
    private NavMeshAgent agente;
    private Vector3 puntoA;
    private Vector3 puntoB;
    private Vector3 objetivoActual;

    public PatrulleroVigilar() : base()
    {
        Debug.Log("VIGILAR");
        nombre = ESTADO.VIGILAR; // Guardamos el nombre del estado en el que nos encontramos.
    }

    public override void Entrar()
    {
        // Le pondríamos la animación de andar, calcular los puntos por los que patrulla, etc...

        base.Entrar();


        // Obtener el render para que su material se haga de color verde
        enemigoIA.render.material.color = Color.green;

        // Obtener el agente de navegación
        agente = enemigoIA.GetComponent<NavMeshAgent>();

        // Puntos entre los que se realiza la patrulla
        puntoA = enemigoIA.transform.position;
        puntoB = puntoA + new Vector3(25, 0, 0); // Distancia a recorrer en el eje x

        objetivoActual = puntoB;
        agente.SetDestination(objetivoActual);

    }

    public override void Actualizar()
    {
        // Le decimos que se vaya moviendo y patrullando...

        // Patrulla
        if (Vector3.Distance(enemigoIA.transform.position, objetivoActual) < 1f)
        {
            if (objetivoActual == puntoA)
            {
                objetivoActual = puntoB;
            }
            else
            {
                objetivoActual = puntoA;
            }
            agente.SetDestination(objetivoActual);
        }

        // Salir del estado si ve al jugador
        if (PuedeVerJugador())
        {
            siguienteEstado = new PatrulleroAtacar();
            faseActual = EVENTO.SALIR; // Cambiamos de FASE ya que pasamos de VIGILAR a ATACAR.
        }
    }

    public override void Salir()
    {
        // Le resetearíamos la animación de andar, detener las corrutinas, o lo que sea...
        base.Salir();
    }
}

