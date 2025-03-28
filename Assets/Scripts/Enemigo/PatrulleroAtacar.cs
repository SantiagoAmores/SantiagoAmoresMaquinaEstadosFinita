using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class PatrulleroAtacar : PatrulleroEstado
{

    private float rangoAtaque = 10f;
    public bool puedeDisparar;

    public PatrulleroAtacar() : base()
    {
        Debug.Log("ATACAR");
        nombre = ESTADO.ATACAR; // Guardamos el nombre del estado en el que nos encontramos.
    }

    public override void Entrar()
    {
        // Le pondríamos la animación de disparar, o lo que sea...
        base.Entrar();

        enemigoIA.render.material.color = Color.red; //  Obtener el render para que su material se haga de color rojo

        enemigoIA.empezarDisparar(); // Iniciar el disparo
    }

    public override void Actualizar()
    {

        if (!PuedeVerJugador())
        {
            siguienteEstado = new PatrulleroVigilar(); // Si el NPC no puede atacar al jugador, lo ponemos a vigilar (por ejemplo).
            faseActual = EVENTO.SALIR; // Cambiamos de FASE ya que pasamos de ATACAR a VIGILAR.

            enemigoIA.pararDisparar(); // Detener el disparo
        }
    }

    public override void Salir()
    {
        // Le resetearíamos la animación de disparar, detener las corrutinas, o lo que sea...

        enemigoIA.pararDisparar(); // Detener el disparo al salir
        base.Salir();
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

    public bool PuedeAtacar()
    {
        float distancia = Vector3.Distance(enemigoIA.transform.position, enemigoIA.jugador.transform.position);
        return distancia <= rangoAtaque;
    }
}