using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrulleroEstado
{

    static public EnemigoIA enemigoIA;

    // PARA ACCEDER A LOS GAMEOBJECTS
    public void inicializarVariables(EnemigoIA _enemigoIA)
    {
        enemigoIA = _enemigoIA;
    }

    // 'ESTADOS' que tiene el NPC
    public enum ESTADO
    {
        VIGILAR, ATACAR
    };

    // 'EVENTOS' - En que parte nos encontramos del estado
    public enum EVENTO
    {
        ENTRAR, ACTUALIZAR, SALIR
    };

    public ESTADO nombre; // Para guardar el nombre del estado
    protected EVENTO faseActual; // Para guardar la fase en la que nos encontramos
    protected PatrulleroEstado siguienteEstado; // El estado que se EJECUTARÁ A CONTINUACIÓN del estado actual

    // Constructor
    public PatrulleroEstado()
    {
    }

    // Las fases de cada estado
    public virtual void Entrar() { faseActual = EVENTO.ACTUALIZAR; } // La primera fase que se ejecuta cuando cambiamos de estado. El siguiente estado debería ser "actualizar".
    public virtual void Actualizar() { faseActual = EVENTO.ACTUALIZAR; } // Una vez estas en ACTUALIZAR, te quedas en ACTUALIZAR hasta que quieras cambiar de estado.
    public virtual void Salir() { faseActual = EVENTO.SALIR; } // La fase de SALIR es la última antes de cambiar de ESTADO, aquí deberiamos limpiar lo que haga falta.

    // Este es la función a la que llamaremos para que el NPC inicie la máquina de estados. Vincula los EVENTOS con las funciones que ejecuta cada uno
    public PatrulleroEstado Procesar()
    {
        if (faseActual == EVENTO.ENTRAR) Entrar();
        if (faseActual == EVENTO.ACTUALIZAR) Actualizar();
        if (faseActual == EVENTO.SALIR)
        {
            Salir();
            return siguienteEstado; // IMPORTANTE: Aquí hacemos el cambio de estado.
        }
        return this; // Si no salimos por el return de arriba, seguimos en el mismo estado.
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
                    // Si ve al jugador, vuelve a vigilar
                    siguienteEstado = new PatrulleroAtacar();
                    faseActual = EVENTO.SALIR;

                    return true; // Confirma que el enemigo ve al jugador
                }
            }
        }

        return false; // Si no lo ve, devuelve falso

    }

}

