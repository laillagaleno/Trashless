using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement; //permite passar as cenas


public class RecycleController : MonoBehaviour
{
     public Image[] trashButton;
     public GameObject[] trash;
     public string[] textTypeTrash;
     public Text textType;

    private int indexTrash;
    private int correctSelection;

    // Start is called before the first frame update
    void Start()
    {
        indexTrash = 0;
        correctSelection = 0;
        trash[indexTrash].SetActive(true);
        textType.text = textTypeTrash[indexTrash];

    }

    // Update is called once per frame
    void Update()
    {
        if(correctSelection == trash.Length){
            SceneManager.LoadScene("Poits");
        }
    }
    public void ButtonNext(){
        if(indexTrash < trash.Length -1){
            trash[indexTrash].SetActive(false);
            indexTrash+=1;
            trash[indexTrash].SetActive(true);
            textType.text = textTypeTrash[indexTrash];
        }
    }
    public void ButtonPrev(){
        if(indexTrash > 0){
            trash[indexTrash].SetActive(false);
            indexTrash-=1;
            trash[indexTrash].SetActive(true);
            textType.text = textTypeTrash[indexTrash];
        }
    }

     public void SelectTrash(int id){
        switch (id)
        {
            case 0:
                if(indexTrash == id){
                    trashButton[id].color = Color.gray;
                    correctSelection+=1;
                }
                break;
            case 1:
               if(indexTrash == id){
                    trashButton[id].color = Color.gray;
                    correctSelection+=1;

                }
                break;
            case 2:
              if(indexTrash == id){
                    trashButton[id].color = Color.gray;
                    correctSelection+=1;

                }
                break;
            case 3:
               if(indexTrash == id){
                    trashButton[id].color = Color.gray;
                    correctSelection+=1;
                }
                break;
            case 4:
                if(indexTrash == id){
                    trashButton[id].color = Color.gray;
                    correctSelection+=1;
                }
                break;

            default:
                Debug.Log("CLIQUEI NO B");
                break;

        }
      
    }

}
