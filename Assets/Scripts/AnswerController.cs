using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement; //permite passar as cenas

public class AnswerController : MonoBehaviour
{
    public Text question;
    public Text answerA;
    public Text answerB;
    public Text answerC;
    public Text answerD;

    public Text textTime;

    private bool isAnswer;

    public Image[] button;

    public string[] questions;
    public string[] optionsA; //armazena as alternativas a
    public string[] optionsB;
    public string[] optionsC;
    public string[] optionsD;
    public string[] answers; //armazena as questoes verdadeiras

    private int idQuestion;

    public float timeMax;
    private float time;

    void Start(){
        isAnswer = false;
        time = timeMax;
        UpdateTimer();

        idQuestion = Random.Range(0,questions.Length);
        Debug.Log(idQuestion);

        question.text = questions[idQuestion];
        answerA.text = optionsA[idQuestion];
        answerB.text = optionsB[idQuestion];
        answerC.text = optionsC[idQuestion];
        answerD.text = optionsD[idQuestion];

    }

    public void answerCorect(string option){

        switch (option)
        {
            case "A":
                if(optionsA[idQuestion] == answers[idQuestion]){
                   isAnswer = true;
                }

                break;
            case "B":

                if(optionsB[idQuestion] == answers[idQuestion]){
                   isAnswer = true;
                }
                break;
            case "C":
                if(optionsC[idQuestion] == answers[idQuestion]){
                   isAnswer = true;
                }
                break;

            case "D":
                if(optionsC[idQuestion] == answers[idQuestion]){
                    isAnswer = true;
                }
                break;
            case "E":
                if(optionsC[idQuestion] == answers[idQuestion]){
                    isAnswer = true;
                }
                break;
            default:
                Debug.Log("CLIQUEI NO B");
                break;

        }
      
    }

    void Update(){
        time -= Time.deltaTime;
        UpdateTimer();
        if(time < 1 || !!isAnswer){
            Invoke("GameOver", 1f);
        }
    }

   void GameOver(){
        SceneManager.LoadScene("Recycle");
    }

    private void UpdateTimer(){
        textTime.text = Mathf.Round(time).ToString();
    }

}
