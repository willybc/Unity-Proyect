using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class MainGame : MonoBehaviour
{
    // Use this for initialization
    public GameObject[] m_Vida;
    int m_nVidas;
    public Text m_puntuacion;
    int m_misPuntos;
    public GameObject m_ingame;
    public GameObject m_gameover;
    public GameObject m_vamos;
    public GameObject m_flecha;
    public GameObject m_mal;
    public GameObject m_camaraPrincipal;
    public GameObject m_maestro;
    public GameObject m_jugador;

    public Animator m_animadorMaestro;
    public Animator m_animadorJugador;


    public enum TipoPaso { None, Right, Up, Left, Down };
    static Quaternion[] PasoBase = new Quaternion[]{
        Quaternion.Euler(0,0,0f),
        Quaternion.Euler(0,0,0f),
        Quaternion.Euler(0,0,90f),
        Quaternion.Euler(0,0,180f),
        Quaternion.Euler(0,0,270f)
    };
    List<TipoPaso> m_Pasos = new List<TipoPaso>();
    List<TipoPaso>.Enumerator m_pasoActual;
    void Start()
    {
        ReiniciarJuego();

        AddNewStep();
        AddNewStep();
        AddNewStep();

        EstadoBase.SetGame(this);
        EstadoBase.Cambiar(EstadoIrProfesor.Instancia);
    }

    // Update is called once per frame
    void Update()
    {
        EstadoBase.Actualizar();
    }
    void ReiniciarJuego()
    {
        m_vamos.SetActive(false);
        m_nVidas = 3;
        m_misPuntos = 0;
        ActualizaVidas();
        SumaPuntuacion(0);
        m_Pasos.Clear();
        ReiniciarPaso();
    }
    public void ReiniciarPaso()
    {
        m_mal.SetActive(false);
        m_flecha.SetActive(false);
    }

    public void AddNewStep()
    {
        m_Pasos.Add((TipoPaso)Random.Range(1, 5));
    }

    void ActualizaVidas()
    {
        int i = 0;
        for (i = 0; i < m_nVidas; ++i) m_Vida[i].SetActive(true);
        for (; i < 3; ++i) m_Vida[i].SetActive(false);
    }

    public void IniciaPasos()
    {
        m_pasoActual = m_Pasos.GetEnumerator();
    }

    public bool MuestraSiguientePaso(Animator animator)
    {
        if (m_pasoActual.MoveNext())
        {
            m_flecha.SetActive(true);
            int paso = (int)m_pasoActual.Current;
            animator.SetInteger("Direccion", paso);
            // Sonido.
            m_flecha.transform.rotation = PasoBase[paso];
            return true;
        }
        animator.SetInteger("Direccion", 0);
        return false;
    }

    public bool CompruebaPaso(Animator animator, TipoPaso _paso)
    {
        if (m_pasoActual.MoveNext())
        {
            m_flecha.SetActive(true);
            int paso = (int)_paso;
            animator.SetInteger("Direccion", paso);
            // Sonido.
            m_flecha.transform.rotation = PasoBase[paso];
            if (m_pasoActual.Current == _paso)
            {
                animator.Update(0);
                animator.SetInteger("Direccion", 0);
                return true;
            }
        }
        animator.SetInteger("Direccion", 0);
        return false;
    }
    public bool HaTerminado()
    {
        var copia = m_pasoActual;
        return !copia.MoveNext();
    }

    public void SumaPuntuacion(int puntos)
    {
        m_misPuntos += puntos;
        m_puntuacion.text = m_misPuntos.ToString("D6");
    }

    public void MuestraEmpezar()
    {
        m_vamos.SetActive(true);
    }

    public void MalPaso()
    {
        m_mal.SetActive(true);
        m_animadorJugador.SetInteger("Direccion", 5);
        m_animadorJugador.Update(0);
        m_animadorJugador.SetInteger("Direccion", 0);

        m_nVidas--;
        ActualizaVidas();
        if (m_nVidas < 1)
        {
            EstadoBase.Cambiar(EstadoFinJuego.Instancia);
        }
        else
            EstadoBase.Cambiar(EstadoFinJugador.Instancia);
    }

    public void GameOver()
    {
        m_ingame.SetActive(false);
        m_gameover.SetActive(true);
    }
}
