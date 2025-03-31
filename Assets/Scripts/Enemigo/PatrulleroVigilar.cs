using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

// Constructor para VIGILAR
public class PatrulleroVigilar : PatrulleroEstado
{

    // Patrulla
    private NavMeshAgent agente;

    // Puntos entre los que se realiza la patrulla
    private Vector3 puntoA;
    private Vector3 puntoB;

    private Vector3 posicionEnemigo;
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

        // Establecer el primer destino de la patrulla
        puntoA = enemigoIA.puntoA.transform.position;
        puntoB = enemigoIA.puntoB.transform.position;
        objetivoActual = puntoB;
        agente.SetDestination(objetivoActual);

    }

    public override void Actualizar()
    {
        // Le decimos que se vaya moviendo y patrullando...

        posicionEnemigo = enemigoIA.enemigo.transform.position;

        // Patrulla
        if (Vector3.Distance(posicionEnemigo, objetivoActual) < 5f)
        {

            Debug.Log("HE LLEGADO");

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

        Vector3 posEnemigo = enemigoIA.gameObject.transform.position;
        Vector3 posJugador = enemigoIA.jugador.transform.position;
       
        RaycastHit hit;
        Vector3 direccion = (posJugador - posEnemigo).normalized;

        float distancia = Vector3.Distance(posEnemigo, posJugador);
        Debug.DrawRay(posEnemigo, direccion * distancia, Color.red);

        if (distancia < 15)
        {
            if (Physics.Raycast(posEnemigo, direccion, out hit, distancia))
            {
                Debug.Log("Raycast golpeó: " + hit.collider.gameObject.name);

                // Si el raycast golpea al jugador, significa que lo ve
                if (hit.collider.gameObject.name == "Jugador")
                {
                    siguienteEstado = new PatrulleroAtacar();
                    faseActual = EVENTO.SALIR;

                }
            }

        }

    }

    public bool PuedeVerJugador()
    {
        return true;

    }

    public override void Salir()
    {
        // Le resetearíamos la animación de andar, detener las corrutinas, o lo que sea...
        base.Salir();
    }
}

