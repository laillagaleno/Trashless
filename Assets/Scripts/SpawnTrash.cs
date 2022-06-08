using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnTrash : MonoBehaviour
{

    public GameObject[] trash;
    public Vector2 numberOfTrash;

    public List<GameObject> newTrash;

    void Start()
    {
        int newNumberTrash = (int)Random.Range(numberOfTrash.x, numberOfTrash.y);
        for (int i = 0; i < newNumberTrash; i++)
        {
            newTrash.Add(Instantiate(trash[Random.Range(0,trash.Length)],transform));
            newTrash[i].SetActive(false);
        }
        PositionTrash();
    }

  public void PositionTrash()
	{
		float minZPos = 10f;
		for (int i = 0; i < newTrash.Count; i++)
		{
			float maxZPos = minZPos + 2f;
			float randomZPos = Random.Range(minZPos, maxZPos);

            float randomSinal = Random.Range(-1,2); //sorteia se vai ser positivo ou negativo

            if(randomSinal == -1){
                randomZPos= randomZPos * -1;
            }

			newTrash[i].transform.localPosition = new Vector3(transform.position.x, transform.position.y, randomZPos);
			newTrash[i].SetActive(true);
			newTrash[i].GetComponent<ChangeLane>().PositionLane();
			minZPos = randomZPos + 1;
		}
	}
}
