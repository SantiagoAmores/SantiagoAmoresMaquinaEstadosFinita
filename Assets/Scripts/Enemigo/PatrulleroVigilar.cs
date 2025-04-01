using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

// Constructor para VIGILAR
public class PatrulleroVigilar : PatrulleroEstado
{
    // Puntos entre los que se realiza la patrulla
    private Vector3 puntoA;
    private Vector3 puntoB;

    // Posiciones
    private Vector3 posicionEnemigo;
    private Vector3 objetivoActual;

    public PatrulleroVigilar() : base()
    {
        Debug.Log("VIGILAR");
        nombre = ESTADO.VIGILAR; // Guardamos el nombre del estado en el que nos encontramos.
    }

    public override void Entrar()
    {
        // Le pondr�amos la animaci�n de andar, calcular los puntos por los que patrulla, etc...

        base.Entrar();


        // Obtener el render para que su material se haga de color verde
        enemigoIA.render.material.color = Color.green;

        // Obtener el agente de navegaci�n y declaramos su velocidad
        enemigoIA.agente = enemigoIA.GetComponent<NavMeshAgent>();
        enemigoIA.agente.speed = 3.5f;

        // Establecemos los puntos de patrulla el primer destino
        puntoA = enemigoIA.puntoA.transform.position;
        puntoB = enemigoIA.puntoB.transform.position;
        objetivoActual = puntoB;
        enemigoIA.agente.SetDestination(objetivoActual);

    }

    public override void Actualizar()
    {
        // Le decimos que se vaya moviendo y patrullando...

        // Actualizamos la posici�n del enemigo
        posicionEnemigo = enemigoIA.enemigo.transform.position;

        // Si el enemigo ha llegado cerca de su destino actual, cambia de objetivo
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

            // Asignamos el nuevo destino de patrulla
            enemigoIA.agente.SetDestination(objetivoActual);
        }

        // Obtenemos la posici�n del enemigo y del jugador
        Vector3 posEnemigo = enemigoIA.gameObject.transform.position;
        Vector3 posJugador = enemigoIA.jugador.transform.position;
       
        // Declaramos Raycast
        RaycastHit hit;

        // Calcula la direcci�n del enemigo hacia el jugador
        Vector3 direccion = (posJugador - posEnemigo).normalized;

        // Calcula la distancia entre el enemigo y el jugador
        float distancia = Vector3.Distance(posEnemigo, posJugador);

        // Dibujamos RayCast
        Debug.DrawRay(posEnemigo, direccion * distancia, Color.red);

        // Si el jugador est� dentro del rango de detecci�n
        if (distancia < 15)
        {
            if (Physics.Raycast(posEnemigo, direccion, out hit, distancia))
            {
                Debug.Log("Raycast golpe�: " + hit.collider.gameObject.name);

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
        // Le resetear�amos la animaci�n de andar, detener las corrutinas, o lo que sea...
        base.Salir();
    }
}

