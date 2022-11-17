using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; //permite passar as cenas


public class NavScenes : MonoBehaviour
{
    public AudioSource audioSourceClick;
    public string nameScena;
    public string[] scene;

    // public string level = MenuController.levelIndex.ToString();
    public MenuController mc;
    public int level;


    void Start(){
        level = mc.levelIndex;
    }

    public void ButtonShowScena(){
        level = mc.levelIndex;
        audioSourceClick.Play();
        nameScena = scene[level];
        StartCoroutine("ShowScener");
    }

    private IEnumerator ShowScener(){
        yield return new WaitForSeconds(0.5f);
          SceneManager.LoadScene(nameScena);
    }
}
