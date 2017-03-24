#pragma strict

function Start () {
    
    
}

function Update () {
    
    var Y : float = Time.deltaTime * 90;
  	transform.rotation *= Quaternion.Euler(0, Y ,0) ;
}
