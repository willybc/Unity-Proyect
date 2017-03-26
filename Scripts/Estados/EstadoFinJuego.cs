using UnityEngine;
using UnityEngine.SceneManagement;
public class EstadoFinJuego : InstanciaEstadoBase<EstadoFinJuego>
{
    float m_reloj = 0;
    float m_duracion = 5.0f;
    public override void Start(MainGame game)
    {
        m_reloj = 0;
        game.GameOver();
    }
    public override void Update(MainGame game)
    {
        m_reloj += Time.deltaTime;
        if (m_reloj > m_duracion)
        {
            SceneManager.LoadScene("MainMenu");
        }
    }
}
