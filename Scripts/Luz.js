#pragma strict

var m_miLuz : Light;
var m_reloj : float;

var m_tiempo : float = 2.0;

function Start () {
    m_miLuz = gameObject.GetComponent(Light);
    m_reloj = 0;
}

function Update () {
    m_reloj += Time.deltaTime;
    if(m_reloj >= m_tiempo){
        m_miLuz.enabled = !m_miLuz.enabled;
        m_reloj=0;
    }
}
