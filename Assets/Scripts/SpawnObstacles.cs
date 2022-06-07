using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnObstacles : MonoBehaviour
{
    // Start is called before the first frame update
    //referencia aos obstaculos
    public GameObject[] obstacles;
    public Vector2 numberOfObstacles;

    public List<GameObject> newObstaclesPos;

    void Start()
    {
        int newNumberObstacles = (int)Random.Range(numberOfObstacles.x, numberOfObstacles.y);
        for (int i = 0; i < newNumberObstacles; i++)
        {
            newObstaclesPos.Add(Instantiate(obstacles[Random.Range(0,obstacles.Length)],transform));
            newObstaclesPos[i].SetActive(false);
        }  
        PositionObstacles(); 
    }


//desabilita os obstaculos que são menores que 0 somente na plataforma 0(inicial)
    public void SetDisable(){
        for (int i = 0; i < newObstaclesPos.Count; i++)
        {
            if(newObstaclesPos[i].transform.position.z < -40){
                 newObstaclesPos[i].SetActive(false);
            }
        }
    }

//posicionamento dos obstaculos
    public void PositionObstacles(){
        
        float divPos = (64 / newObstaclesPos.Count); //metade do tamanho da plataforma dividida pela quantidade de obstaculos

        for (int i = 0; i < newObstaclesPos.Count; i++)
        {
            //o posicionamento sempre vai ser de (n,n+1), n++
            float posZMin = divPos + divPos * i; 
            float posZMax =  divPos + divPos * i + 1;

            float randomZPos = Random.Range(posZMin, posZMax);
            float randomSinal = Random.Range(-1,2); //sorteia se vai ser positivo ou negativo

            if(randomSinal == -1){
                randomZPos= randomZPos * -1;
            }
        
            newObstaclesPos[i].transform.localPosition = new Vector3(0, 0, randomZPos);
            newObstaclesPos[i].SetActive(true);

            //verifica se o obstaculo tem que mudar de posição no eixo x
            if (newObstaclesPos[i].GetComponent<ChangeLane>() != null)
                newObstaclesPos[i].GetComponent<ChangeLane>().PositionLane();
        }
    }
}
