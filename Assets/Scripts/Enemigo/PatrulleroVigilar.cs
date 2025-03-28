using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

// Constructor para VIGILAR
public class PatrulleroVigilar : PatrulleroEstado
{

    // Patrulla
    private NavMeshAgent agente;
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
        Vector3 puntoA = enemigoIA.puntoA.transform.position;
        Vector3 puntoB = enemigoIA.puntoB.transform.position;

        // Establecer el primer destino de la patrulla
        objetivoActual = puntoB;
        agente.SetDestination(objetivoActual);

    }

    public override void Actualizar()
    {
        // Le decimos que se vaya moviendo y patrullando...

        // Patrulla
        if (Vector3.Distance(enemigoIA.transform.position, objetivoActual) < 1f)
        {
            if (objetivoActual == enemigoIA.puntoA.transform.position)
            {
                objetivoActual = enemigoIA.puntoB.transform.position;
            }
            else
            {
                objetivoActual = enemigoIA.puntoA.transform.position;
            }

            agente.SetDestination(objetivoActual);
        }
    }

    public bool PuedeVerJugador()
    {
        Vector3 posEnemigo = enemigoIA.gameObject.transform.position;
        Vector3 posJugador = enemigoIA.jugador.transform.position;

        float distancia = Vector3.Distance(posEnemigo, posJugador);

        if (distancia < 15)
        {
            RaycastHit hit;
            Vector3 direccion = (posJugador - posEnemigo).normalized;

            // Dibujar el rayo en la escena para depuración
            Debug.DrawRay(posEnemigo, direccion * distancia, Color.red);

            // Lanza el Raycast desde el enemigo hacia el jugador
            if (Physics.Raycast(posEnemigo, direccion, out hit, distancia))
            {
                Debug.Log("Raycast golpeó: " + hit.collider.gameObject.name);

                // Si el raycast golpea al jugador, significa que lo ve
                if (hit.collider.gameObject.name == "Jugador")
                {
                    return true; // Confirma que el enemigo ve al jugador
                }
            }
        }

        return false; // Si no lo ve, devuelve falso

    }

    public override void Salir()
    {
        // Le resetearíamos la animación de andar, detener las corrutinas, o lo que sea...
        base.Salir();
    }
}

