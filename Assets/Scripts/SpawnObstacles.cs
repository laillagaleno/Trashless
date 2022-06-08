using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnObstacles : MonoBehaviour
{
    // Start is called before the first frame update
    //referencia aos obstaculos
    public GameObject[] obstacles;
    public Vector2 numberOfObstacles;

    public List<GameObject> newObstacles;

    void Start()
    {
        int newNumberObstacles = (int)Random.Range(numberOfObstacles.x, numberOfObstacles.y);
        for (int i = 0; i < newNumberObstacles; i++)
        {
            newObstacles.Add(Instantiate(obstacles[Random.Range(0,obstacles.Length)],transform));
            newObstacles[i].SetActive(false);
        }  
        PositionObstacles(); 
    }


//desabilita os obstaculos que são menores que 0 somente na plataforma 0(inicial)
    public void SetDisable(){
        for (int i = 0; i < newObstacles.Count; i++)
        {
            if(newObstacles[i].transform.position.z < -40){
                 newObstacles[i].SetActive(false);
            }
        }
    }

//posicionamento dos obstaculos
    public void PositionObstacles(){
        
        float divPos = (64 / newObstacles.Count); //metade do tamanho da plataforma dividida pela quantidade de obstaculos

        for (int i = 0; i < newObstacles.Count; i++)
        {
            //o posicionamento sempre vai ser de (n,n+1), n++
            float posZMin = divPos + divPos * i; 
            float posZMax =  divPos + divPos * i + 1;

            float randomZPos = Random.Range(posZMin, posZMax);
            float randomSinal = Random.Range(-1,2); //sorteia se vai ser positivo ou negativo

            if(randomSinal == -1){
                randomZPos= randomZPos * -1;
            }
        
            newObstacles[i].transform.localPosition = new Vector3(0, 0, randomZPos);
            newObstacles[i].SetActive(true);

            //verifica se o obstaculo tem que mudar de posição no eixo x
            if (newObstacles[i].GetComponent<ChangeLane>() != null)
                newObstacles[i].GetComponent<ChangeLane>().PositionLane();
        }
    }
}
