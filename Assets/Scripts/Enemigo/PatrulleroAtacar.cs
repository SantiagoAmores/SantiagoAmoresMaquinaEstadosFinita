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
      
                    return true; // Confirma que el enemigo ve al jugador
      

    }

    public bool PuedeAtacar()
    {
        float distancia = Vector3.Distance(enemigoIA.enemigo.transform.position, enemigoIA.jugador.transform.position);
        return distancia <= rangoAtaque;
    }
}