using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; //acessar canva, textos, imagens etc
using UnityEngine.SceneManagement; //permite passar as cenas

public class GameController : MonoBehaviour
{
    public static GameController gc;
    public GameObject gameOver;
    public GameObject painelPoint;

    //pontuação de acordo com o tempo
    public float scoreDist;
    public int scoreTrashP;
    public int scoreTrashM;
    public int scoreTrashV;
    public int scoreTrashPL;
    public int scoreTrashO;

    public Text scoreText;
    public Text textTrashP;
    public Text textTrashM;
    public Text textTrashV;
    public Text textTrashPL;
    public Text textTrashO;

    private Player player;
    private float speedAux; 
    private float speedConst = 0.01f;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }

    void Update()
    {
        if(!player.isDie){
            //velocidade da somatoria da pontuação
            scoreDist += Time.deltaTime * player.speed;
            //precisa receber uma string, arredonda e converte
            scoreText.text = Mathf.Round(scoreDist).ToString() + "m";
            
            speedAux = speedConst*scoreDist + 15;
            if(speedAux > 40){
                speedAux = 40;
            }

            player.speed = speedAux;

            
            // if(score > speedAdd){
            //     player.speed = player.speed + 5f; 
            //     speedAdd = speedAdd + 500;
            // }
        }
    }


    public void StartMenu(){
        SceneManager.LoadScene("Menu");
    }

//Chamando a tela de game pver
    public void ShowGameOver(){
        // gameOver.SetActive(true);
        // painelPoint.SetActive(false);
        // Application.LoadLevel("GameOver");
        SceneManager.LoadScene("GameOver");
    }

//passar para a proxima cena
    public void ButtonShowScena(string nameScena){
        SceneManager.LoadScene(nameScena);
    }

    public void AddTrashP(){
        scoreTrashP++;
        textTrashP.text = scoreTrashP.ToString();
    }

     public void AddTrashM(){
        scoreTrashM++;
        textTrashM.text = scoreTrashM.ToString();
    }

     public void AddTrashPL(){
        scoreTrashPL++;
        textTrashPL.text = scoreTrashPL.ToString();
    }

     public void AddTrashV(){
        scoreTrashV++;
        textTrashV.text = scoreTrashV.ToString();
    }

     public void AddTrashO(){
        scoreTrashO++;
        textTrashO.text = scoreTrashO.ToString();
    }

}
