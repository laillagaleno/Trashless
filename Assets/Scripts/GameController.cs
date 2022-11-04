using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; //permite passar as cenas
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public static GameController gc;

    //pontuação de acordo com o tempo
    public float scoreDist;
    public Text scoreText;
 
    private Player player;
    private UIController uiController;

    private float speedAux; 
    private float speedConst = 0.01f;
 


    void Start()
    {
        PlayerPrefs.SetInt("PL", 0);
        PlayerPrefs.SetInt("P", 0);
        PlayerPrefs.SetInt("M",0);
        PlayerPrefs.SetInt("O",0);
        PlayerPrefs.SetInt("V",0);

        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        uiController = FindObjectOfType<UIController>();
        
    }
    void Update()
    {
        if(!player.isDie){
            //velocidade da somatoria da pontuação
            scoreDist += Time.deltaTime * player.speed;
            //precisa receber umaQ string, arredonda e converte
            uiController.UpdateScore(scoreDist);
            // scoreText.text = Mathf.Round(scoreDist).ToString() + "m";
            speedAux = speedConst*scoreDist + 15;
            if(speedAux > 40){
                speedAux = 40;
            }
            player.speed = speedAux;
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
}
