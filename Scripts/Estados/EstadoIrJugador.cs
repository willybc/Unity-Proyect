using UnityEngine;

public class EstadoIrJugador: InstanciaEstadoBase<EstadoIrJugador> {

    float m_reloj = 0;
    float m_duracion = 0.5f;

    Vector3 m_currentTarget;

    public override void Start(MainGame game)
    {
        m_reloj = 0;
        float distance = (game.m_camaraPrincipal.transform.position -
            game.m_jugador.transform.position).magnitude;
        m_currentTarget = game.m_camaraPrincipal.transform.position +
            game.m_camaraPrincipal.transform.forward * distance;
    }

    public override void Update(MainGame game)
    {
        m_reloj += Time.deltaTime;
        if (m_reloj > m_duracion)
        {
            EstadoBase.Cambiar(EstadoJugador.Instancia);
        }else
        {
            m_currentTarget = Vector3.Lerp(m_currentTarget,
            game.m_jugador.transform.position, m_reloj / m_duracion);
            game.m_camaraPrincipal.transform.LookAt(m_currentTarget+Vector3.up);
        }
    }


}
