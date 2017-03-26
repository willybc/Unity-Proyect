
public class EstadoBase
{
    public virtual void Start(MainGame game)
    {

    }
    public virtual void Update(MainGame game)
    {

    }
    public virtual void End(MainGame game)
    {

    }

    static MainGame m_game;
    static EstadoBase m_actual;
    public static void SetGame(MainGame mainGame)
    {
        m_game = mainGame;
    }
    public static void Cambiar(EstadoBase nuevoEstado)
    {
        if (m_actual != null) m_actual.End(m_game);
        m_actual = nuevoEstado;
        if (m_actual != null) m_actual.Start(m_game);
    }

    public static void Actualizar()
    {
        if (m_actual != null) m_actual.Update(m_game);
    }
}

public class InstanciaEstadoBase<T> : EstadoBase where T : EstadoBase, new()
{
    static T _instancia;
    public static T Instancia
    {
        get
        {
            if (_instancia == null)
                _instancia = new T();
            return _instancia;
        }
    }
}
