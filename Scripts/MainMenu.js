#pragma strict
import UnityEngine.SceneManagement;

function Start () {
	
}

function Update () {
    if( Input.anyKeyDown){
        SceneManager.LoadScene("MainGame");
    }
}
