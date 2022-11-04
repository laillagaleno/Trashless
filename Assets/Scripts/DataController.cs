// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;
// using System; 
// using System.Runtime.Serialization.Formatters.Binary; //dados em binario
// using System.IO; 
// using Random = UnityEngine.Random;

// [Serializable]
// public class PlayerData{
//     public int coins;
//     public int scoreMax; 
//     public int numberV;
//     public int numberV;
//     public int numberP;
//     public int numberPL;
//     public int numberM;
//     public int numberO;
// }



// public class DataController : MonoBehaviour
// {
//     // Start is called before the first frame update

//     private string filePath; //caminho do arquivo 

//     //chamada antes do start
//     private void private void Awake() {
//         filePath = Application.persistentDataPath + "playerinfor.data";
//     }
//     public void SaveScore(){
//         BinaryFormatter bf = new BinaryFormatter();
//         FileStream file = File.Create(filePath); //cria o caminho

//         PlayerData data = new PlayerData();

//         data.coins = coins;
//         data.scoreMax = scoreMax;
//         data.numberV = numberV;
//         data.numberM = numberM;
//         data.numberPL = numberPL;
//         data.numberO = numberO;
//         data.numberP = numberP;

//         bf.Serialize(file,data);
//         file.Close();
//     }

//     void Start()
//     {
        
//     }

//     // Update is called once per frame
//     void Update()
//     {
        
//     }
// }
