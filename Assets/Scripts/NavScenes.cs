using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; //permite passar as cenas


public class NavScenes : MonoBehaviour
{
    public void ButtonShowScena(string nameScena){
        SceneManager.LoadScene(nameScena);
    }
}
