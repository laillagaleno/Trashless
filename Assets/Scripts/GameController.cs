using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; //acessar canva, textos, imagens etc

public class GameController : MonoBehaviour
{
    public GameObject gameOver;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

//setando true para a tela de game pver aparecer
    public void ShowGameOver(){
        gameOver.SetActive(true);
    }
}
