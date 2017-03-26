using UnityEngine;
public class EstadoJugador : InstanciaEstadoBase<EstadoJugador>
{
    float m_reloj = 0;
    float m_duracion = 2.0f;
    public override void Start(MainGame game)
    {
        m_reloj = 0;
        game.IniciaPasos();
        game.MuestraEmpezar();
        game.m_camaraPrincipal.transform.LookAt(
            game.m_jugador.transform.position + Vector3.up);
    }
    public override void Update(MainGame game)
    {
        m_reloj += Time.deltaTime;
        if (m_reloj < m_duracion)
        {
            MainGame.TipoPaso paso = ControlTeclado();
            if (paso != MainGame.TipoPaso.None)
            {
                m_reloj = 0;
                if (game.CompruebaPaso(game.m_animadorJugador, paso))
                {
                    game.SumaPuntuacion(100);
                    if (game.HaTerminado())
                    {
                        game.AddNewStep();
                        EstadoBase.Cambiar(EstadoFinJugador.Instancia);
                    }
                    return;
                }
                else
                    game.MalPaso();
            }
            else
                return;
            // Puedo marcar mi moviento
        }
        else
        {
            game.MalPaso();
        }
    }

    MainGame.TipoPaso ControlTeclado()
    {
        if (Input.GetKeyDown(KeyCode.DownArrow)) return MainGame.TipoPaso.Down;
        if (Input.GetKeyDown(KeyCode.UpArrow)) return MainGame.TipoPaso.Up;
        if (Input.GetKeyDown(KeyCode.LeftArrow)) return MainGame.TipoPaso.Left;
        if (Input.GetKeyDown(KeyCode.RightArrow)) return MainGame.TipoPaso.Right;
        return ControlGesto();
    }

    Vector2 m_inicio;
    MainGame.TipoPaso ControlGesto()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.touches[0];
            if (touch.phase == TouchPhase.Began)
            {
                m_inicio = touch.position;
            }
            else
            if (touch.phase == TouchPhase.Ended)
            {
                Vector2 delta = touch.position - m_inicio;
                if (Mathf.Abs(delta.x) > Mathf.Abs(delta.y))
                {
                    if (delta.x < 0) return MainGame.TipoPaso.Left;
                    else return MainGame.TipoPaso.Right;
                }
                else
                {
                    if (delta.y < 0) return MainGame.TipoPaso.Up;
                    else return MainGame.TipoPaso.Down;
                }
            }
        }
        return MainGame.TipoPaso.None;
    }

}
