using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{
    // public static MenuController mc;
    
    public Text missionText;
    public Text sceneText;
    public Text levelText;
    public Text coinText;

    public string[] sceneLevel;
    public string[] numLevel;
    public string[] missionLevel;
    public int[] coinLevel;

    public int levelIndex = 0;
    // Start is called before the first frame update
    void Start()
    {
        SelectionLevel(0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //botoes
    public void SelectionLevel(int index){
        levelIndex += index;
        if (levelIndex >= sceneLevel.Length){
            levelIndex = 0;
      }else if(levelIndex < 0){
        levelIndex = sceneLevel.Length -1;
      }
    
    string coin = "";

      for (int i = 0; i < sceneLevel.Length; i++)
      {
        if(i == levelIndex){
           sceneText.text = sceneLevel[i];
           levelText.text = numLevel[i];
           missionText.text = missionLevel[i];

           if(coinLevel[i]==0){
            coin = "LIBERADA";
           }else{
            coin = coinLevel[i].ToString();
           }
        }
        coinText.text = coin;
      }
    }
}
