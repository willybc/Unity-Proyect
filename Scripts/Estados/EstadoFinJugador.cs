using UnityEngine;

public class EstadoFinJugador: InstanciaEstadoBase<EstadoFinJugador> {

    float m_reloj = 0;
    float m_duracion = 1.0f;

    public override void Start(MainGame game)
    {
        m_reloj = 0;
        
    }

    public override void Update(MainGame game)
    {
        m_reloj += Time.deltaTime;
        if (m_reloj > m_duracion)

        {
            EstadoBase.Cambiar(EstadoIrProfesor.Instancia);
        }
    }

    public override void End(MainGame game)
    {
        game.ReiniciarPaso();
    }

}
