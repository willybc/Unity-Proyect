using UnityEngine;

public class EstadoProfesor: InstanciaEstadoBase<EstadoProfesor>
{

    float m_reloj = 0;
    float m_duracion = 0.75f;

    Vector3 m_currentTarget;

    public override void Start(MainGame game)
    {
        m_reloj = 0;
        game.m_camaraPrincipal.transform.LookAt(
            game.m_maestro.transform.position + Vector3.up);
        game.IniciaPasos();
    }

    public override void Update(MainGame game)
    {
        m_reloj += Time.deltaTime;
        if (m_reloj > m_duracion)
        {
            m_reloj = 0;
            if (!game.MuestraSiguientePaso(game.m_animadorMaestro))
            {
           //     EstadoBase.Cambiar(EstadoIrJugador.Instancia);
            }
            
        }
        else
        {

        }
    }


}
