using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AliadoEstado
{
    public AliadoIA aliadoIA;

    public void inicializarVariables(AliadoIA _aliadoIA)
    {
        aliadoIA = _aliadoIA;
    }
    
    public enum ESTADO
    {
        ESPERAR, SEGUIR, ATACAR
    };

    public enum EVENTO
    {
        ENTRAR, ACTUALIZAR, SALIR
    };

        public ESTADO nombre;
        protected EVENTO faseActual;
        protected AliadoEstado siguienteEstado;

    // Constructor
    public AliadoEstado()
    {

    }

    public virtual void Entrar() { faseActual = EVENTO.ACTUALIZAR; } // La primera fase que se ejecuta cuando cambiamos de estado. El siguiente estado deberia ser "actualizar".
    public virtual void Actualizar() { faseActual = EVENTO.ACTUALIZAR; } // Una vez estas en ACTUALIZAR, te quedas en ACTUALIZAR hasta que quieras cambiar de estado.
    public virtual void Salir() { faseActual = EVENTO.SALIR; } // La fase de SALIR es la ultima antes de cambiar de ESTADO, aqui deberiamos limpiar lo que haga falta.

    public AliadoEstado Procesar()
    {
       
        if (faseActual == EVENTO.ENTRAR) Entrar();
        if (faseActual == EVENTO.ACTUALIZAR) Actualizar();
        if (faseActual == EVENTO.SALIR)
        {
            Salir();
            return siguienteEstado; // IMPORTANTE: Aqui hacemos el cambio de estado.
        }
        return this; // Si no salimos por el return de arriba, seguimos en el mismo estado.

    }
}
