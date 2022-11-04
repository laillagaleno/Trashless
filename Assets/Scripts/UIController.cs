using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public Image[] lifeHearts;

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

     void Start(){
       Debug.Log("stard");
    }

    public void UpdateScore(float score){
        scoreText.text = Mathf.Round(score).ToString() + "m";
    }
    
    public void UpdateLives(int lives){
        for (int i = 0; i < lifeHearts.Length; i++)
        {
            if (lives > i)
            {
                lifeHearts[i].color = Color.white;
            }else{
                lifeHearts[i].color = Color.black;
            }
        }
    }

    public void AddTash(string trashLayer){

        if(trashLayer == "paper"){
            scoreTrashP++;
            PlayerPrefs.SetInt("P",scoreTrashP);
            textTrashP.text = scoreTrashP.ToString();
        }else if(trashLayer == "metal"){
           scoreTrashM++;
            PlayerPrefs.SetInt("M",scoreTrashM);
            textTrashM.text = scoreTrashM.ToString();
        } else if(trashLayer == "plastic"){
            scoreTrashPL++;
            PlayerPrefs.SetInt("PL",scoreTrashPL);
            textTrashPL.text = scoreTrashPL.ToString();
        }else if(trashLayer == "glass"){
            scoreTrashV++;
            PlayerPrefs.SetInt("V",scoreTrashV);
            textTrashV.text = scoreTrashV.ToString();
        }else if(trashLayer ==  "organic"){
            scoreTrashO++;
            PlayerPrefs.SetInt("O",scoreTrashO);
            textTrashO.text = scoreTrashO.ToString();
        }
    }

}
