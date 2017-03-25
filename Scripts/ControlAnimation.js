#pragma strict

var m_sonido1 : AudioClip;
var m_camara : AudioListener;


var m_miSonido : AudioClip;
var m_miOrigen : AudioSource;

var m_animador : Animator;

var m_inicioToque : Vector2;

function Start () {
    m_animador = gameObject.GetComponent(Animator);
}

function Update () {
    if (Input.GetKeyDown(KeyCode.Alpha1)){
        m_animador.SetInteger("Direccion",1);
        m_miOrigen.PlayOneShot(m_miSonido);
    }
    if (Input.GetKeyDown(KeyCode.Alpha2)){
        m_animador.SetInteger("Direccion",2);
    }
    if (Input.GetKeyDown(KeyCode.Alpha3)){
        m_animador.SetInteger("Direccion",3);
    }
    if (Input.GetKeyDown(KeyCode.Alpha4)){
        m_animador.SetInteger("Direccion",4);
    }

}