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
    public Image m_empezar;
    public GameObject m_flecha;
    public GameObject m_mal;
    public GameObject m_camaraPrincipal;
    public GameObject m_maestro;
    public GameObject m_jugador;

    public Animator m_animadorMaestro;
    public Animator m_animadorJugador;

    public AudioSource m_miFuenteDeSonido;

    public enum TipoPaso { None, Right, Up, Left, Down };
    static Quaternion[] PasoBase = new Quaternion[]{
        Quaternion.Euler(0,0,0f),
        Quaternion.Euler(0,0,0f),
        Quaternion.Euler(0,0,90f),
        Quaternion.Euler(0,0,180f),
        Quaternion.Euler(0,0,270f)
    };

    public AudioClip[] EfectosPaso;

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
        ActualizaEmpezar();
    }
    void ReiniciarJuego()
    {
        m_empezar.enabled = false ;
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
            m_miFuenteDeSonido.PlayOneShot(EfectosPaso[paso - 1]);
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
            m_miFuenteDeSonido.PlayOneShot(EfectosPaso[paso - 1]);
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

    float m_timerEmpezar;

    public void MuestraEmpezar()
    {
        TocarSonido(MainGame.Efectos.Empezar1, MainGame.Efectos.Empezar2);
        m_empezar.enabled = true;
        m_empezar.color = Color.white;
        m_timerEmpezar = 2;
    }

    public void ActualizaEmpezar()
    {
        if (m_empezar.enabled)
        {
            m_timerEmpezar -= Time.deltaTime * 2.0f;
            if (m_timerEmpezar < 0) { m_timerEmpezar = 0; m_empezar.enabled = false; }
            Color tmp = m_empezar.color;
            tmp.a = m_timerEmpezar;
            m_empezar.color = tmp;
        }
    }

    public void MalPaso()
    {
        TocarSonido(MainGame.Efectos.Mal1, MainGame.Efectos.Mal2);
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

    public enum Efectos
    {
        Empezar1, Empezar2, Bien1, Bien2, Mal1, Mal2, Ganar, Perder
    };

    public AudioClip[] EfectosSonido;
    public void TocarSonido(params Efectos[] efectos) {
        int indice = Random.Range(0, efectos.Length);
        m_miFuenteDeSonido.PlayOneShot(EfectosSonido[(int)efectos [indice]]);
        }

}

