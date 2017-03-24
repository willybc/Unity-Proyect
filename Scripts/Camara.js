#pragma strict

var m_objetivo : GameObject;
function Start () {
    
}

function Update () {
    transform.LookAt(m_objetivo.transform);
}
