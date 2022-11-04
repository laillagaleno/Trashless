using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; //permite passar as cenas


public class NavScenes : MonoBehaviour
{
    public AudioSource audioSourceClick;
    public string nameScena;

    public void ButtonShowScena(string scene){
        audioSourceClick.Play();
        nameScena = scene;
        StartCoroutine("ShowScener");
    }

    private IEnumerator ShowScener(){
        yield return new WaitForSeconds(0.5f);
          SceneManager.LoadScene(nameScena);
    }
}
