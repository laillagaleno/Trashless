using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPlatform : MonoBehaviour
{
    //list para armazenar as plataformas
    public List<GameObject> platforms = new List<GameObject>();
    public int offset;

    void Start()
    {
        for(int i = 0; i < platforms.Count; i++){
            Instantiate(platforms[i],new Vector3(0,0,i*117),transform.rotation);
            offset+=117;
        }
    }

    // Update is called once per frame
    public GameObject myPlatform;
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.A)){
            Recycle(myPlatform);
        }
    }
    public void Recycle(GameObject platform){
        platform.transform.position=new Vector3(0,0,offset);
        offset+=117;
    }
}
