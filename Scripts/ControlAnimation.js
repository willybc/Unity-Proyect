#pragma strict


var m_animador : Animator;
function Start () {
    m_animador = gameObject.GetComponent(Animator);
}

function Update () {
    if (Input.GetKeyDown(KeyCode.Alpha1)){
        m_animador.SetInteger("Direccion",1);
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
