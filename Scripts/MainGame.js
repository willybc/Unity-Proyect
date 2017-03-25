#pragma strict

var m_Chica : GameObject;

var m_miChica : GameObject;

function Start () {
    m_miChica = GameObject.Instantiate(m_Chica);
}

function Update () {
    if( Input.GetMouseButtonDown(0))
        GameObject.Destroy(m_miChica);
}
