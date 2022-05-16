using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPlatform : MonoBehaviour
{
    //list para armazenar as plataformas do prefebs
    public List<GameObject> platforms = new List<GameObject>();
    //lista dos objetos instanciados
    public List<Transform> currentPlatforms = new List<Transform>();
    public int offset;
    private Transform player;
    private Transform currentPlatformPoint; //armazena o ponto da plataforma q esta passando
    private int platformIndex; 

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform; //procura o obj na cena q tem a tag player
        for(int i = 0; i < platforms.Count; i++){

            Transform p = Instantiate(platforms[i],new Vector3(0,0,i*128),transform.rotation).transform; //instancia as plataformas
            currentPlatforms.Add(p); //add na lista
            offset+=128;
        }
        currentPlatformPoint = currentPlatforms[platformIndex].GetComponent<Platform>().point;
    } 

    // Update is called once per frame
    
    void Update()
    {
       float distance = player.position.z - currentPlatformPoint.position.z; //distancia entre o player e o pont

       
    //use debug.log para printar a distancia do player ao passar do point
       if(distance>=-0.3){
           Recycle(currentPlatforms[platformIndex].gameObject);
           platformIndex++;

           //recomeça a lista em 0 se todas as plataformas foram recicladas
           if(platformIndex>currentPlatforms.Count -1){
               platformIndex = 0;
           }

           currentPlatformPoint = currentPlatforms[platformIndex].GetComponent<Platform>().point;
       }
    }
    public void Recycle(GameObject platform){
        platform.transform.position=new Vector3(0,0,offset);
        offset+=128;
    }
}
