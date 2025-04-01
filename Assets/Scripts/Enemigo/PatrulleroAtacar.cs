using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class PatrulleroAtacar : PatrulleroEstado
{

    // Distancia maxima de ataque
    private float rangoAtaque = 10f;

    public PatrulleroAtacar() : base()
    {
        Debug.Log("ATACAR");
        nombre = ESTADO.ATACAR; // Guardamos el nombre del estado en el que nos encontramos.
        PuedeAtacar();
    }

    public override void Entrar()
    {
        // Le pondríamos la animación de disparar, o lo que sea...
        base.Entrar();

        // Obtener el render para que su material se haga de color rojo indicando que esta en modo ataque
        enemigoIA.render.material.color = Color.red; 

    }

    public override void Actualizar()
    {
        // Si el enemigo ya no puede atacar, cambia de estado a "Vigilar"
        if (!PuedeAtacar())
        {
            siguienteEstado = new PatrulleroVigilar(); // Si el NPC no puede atacar al jugador, lo ponemos a vigilar.
            faseActual = EVENTO.SALIR; // Cambiamos de FASE ya que pasamos de ATACAR a VIGILAR.

        }
    }

    public override void Salir()
    {
        // Detener el disparo al salir
        enemigoIA.pararDisparar(); 

        // Marcamos que el enemigo ya no está disparando
        enemigoIA.estaDisparando = false;
        base.Salir();
    }

    public bool PuedeAtacar()
    {
        // Declaramos Raycast
        RaycastHit hit;

        // Calculamos la distancia entre el enemigo y el jugador
        float distancia = Vector3.Distance(enemigoIA.enemigo.transform.position, enemigoIA.jugador.transform.position);

        // Si el jugador está dentro del rango de ataque
        if (distancia < rangoAtaque)
        {
            // Calcula la dirección en la que el enemigo debe disparar
            Vector3 direccion = (enemigoIA.jugador.transform.position - enemigoIA.transform.position);

            if (Physics.Raycast(enemigoIA.transform.position, direccion, out hit, distancia))
            {
                Debug.Log("Raycast golpeó: " + hit.collider.gameObject.name);

                // Si el raycast golpea al jugador, significa que lo ve
                if (hit.collider.gameObject.name == "Jugador")
                {
                    // Si el enemigo no está disparando, inicia el disparo
                    if (!enemigoIA.estaDisparando)
                    {
                        enemigoIA.agente.speed = 0f;
                        enemigoIA.empezarDisparar();
                        return true;
                    }

                }

                // Si no colisiona con el jugador
                else
                {
                    enemigoIA.pararDisparar();
                    enemigoIA.estaDisparando = true;
                    return false;
                }
            }

        }

        // Retorna true si la distancia es menor o igual al rango de ataque
        return distancia <= rangoAtaque;

    }
}