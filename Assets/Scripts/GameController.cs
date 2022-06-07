using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; //acessar canva, textos, imagens etc

public class GameController : MonoBehaviour
{
    public GameObject gameOver;

    //pontuação de acordo com o tempo
    public float score;
    public int scoreTrash;

    public Text scoreText;
    public Text trashText;
    private Player player;



    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();

    }

    void Update()
    {
        if(!player.isDie){
            //velocidade da somatoria da pontuação
            score += Time.deltaTime * player.speed;
            //precisa receber uma string, arredonda e converte
            scoreText.text = Mathf.Round(score).ToString() + "m";
        }
        
    }

//setando true para a tela de game pver aparecer
    public void ShowGameOver(){
        gameOver.SetActive(true);
    }

    public void AddTrash(){
        scoreTrash++;
        trashText.text = scoreTrash.ToString();
    }
}
