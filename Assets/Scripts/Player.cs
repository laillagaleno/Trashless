using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Player : MonoBehaviour
{
    CharacterController controller; //armazena o componente Capsular Controller no Player
    public float speed; //velocidade 

    public float jumpHeight; //força do pulo
    public float gravity; //gravidade
    private float jumpVelocity; 


    public float horizontalSpeed; //velocidade horizontal
    private bool isMovingLeft;
    private bool isMovingRight;
    private bool isJump;
    private bool isSlide;


    //colisão
    public float rayRadius;
    public LayerMask layer;
    public LayerMask trashLayer;

    //animação de jump
    public Animator jump;
    public Animator die;
    public bool isDie;

    //controler do game over
    private GameController gc;

    public Transform player;


    void Start(){
         player = GameObject.FindGameObjectWithTag("Player").transform;

         controller = GetComponent<CharacterController>();
         gc = FindObjectOfType<GameController>(); //so posso fazer isso pq so tenho um gamecontroller na cena 
    }

    void Update(){
        //movimentação base do personagem
        Vector3 direction = Vector3.forward * speed; //add 1 no eixo z
        //verificação se o personagem esta tocando no chão
        if(controller.isGrounded){

            //da um pulo, a condição isDie desabilita a ação de pular se for true
            if(Input.GetKeyDown(KeyCode.Space) && !isDie){
                jump.SetTrigger("jump"); //reconhece a condição jump
                jumpVelocity = jumpHeight;
                jump.SetFloat("jumpVelocity", speed/jumpHeight);
            } 

            if(Input.GetKeyDown(KeyCode.DownArrow)&& !isDie){
                // controller.

            }

            //ir para a direita,se ambas forem vdd, ate certa posição
            if(Input.GetKeyDown(KeyCode.RightArrow)&& transform.position.x<1f && !isMovingRight){
                //chamada da co-rotina 
                isMovingRight = true;
                StartCoroutine(RighMove());
            }

            //ir para a esquerda, se ambas forem vdd, ate certa posição
            if(Input.GetKeyDown(KeyCode.LeftArrow)&& transform.position.x>-1f && !isMovingLeft){
                isMovingLeft = true;
                //chamada da co-rotina 
                StartCoroutine(LeftMove());
            }
        }
        else{
                jumpVelocity -= gravity; //se ele estiver no ar, vai cair
            }
        direction.y=jumpVelocity;
        controller.Move(direction * Time.deltaTime);
        OnCollision();//deve ser chamado o tempo todo
    }

    //move o player pra esquerda
    IEnumerator LeftMove(){
        for(float i = 0; i < 2.5; i+= 0.1f){
            controller.Move(Vector3.left * Time.deltaTime * horizontalSpeed);
            yield return null;
        }
        isMovingLeft = false;
    }

    //mover pra direita
    IEnumerator RighMove(){
        for(float i = 0; i< 2.5;i += 0.1f){
            controller.Move(Vector3.right * Time.deltaTime * horizontalSpeed);
            yield return null;
        }
        isMovingRight = false;
    }


    //identificar a colisão com algo
    void OnCollision(){
        //cria o raio/linha imaginaria que sai do Player/ determina o tamanho
        RaycastHit hit; 
        if(Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward),out hit,rayRadius, layer)&& !isDie){
               //executa a condição die do Animator 
                die.SetTrigger("die");
                player.transform.position = new Vector3(transform.position.x, 0, transform.position.z); //se colidir garante q iraá cair no chão 
                //zera os valores 
                speed=0;
                horizontalSpeed=0;
                jumpHeight=0;
                jumpVelocity=0;
                Debug.Log("bateu!");
                Invoke("GameOver", 1f);
                isDie = true;
        }
        //colisão ao bater nos lixos
        //linha desenhada pra frente e pra cima
        RaycastHit trashHit;
        if(Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out trashHit, rayRadius, trashLayer)){
            //destroi ao bater no lixo
            
            string trashTag = trashHit.transform.gameObject.tag; //aramzena o nome da tag do lixo em contato
           
            //verifica qual tipo de lixo foi recolhido e adiciona o ponto
            if(trashTag == "paper"){
                gc.AddTrashP();
            }
            if(trashTag == "glass"){
                gc.AddTrashV();
            }
            if(trashTag == "plastic"){
                gc.AddTrashPL();
            }
            if(trashTag == "organic"){
                gc.AddTrashO();
            }
            if(trashTag == "metal"){
                gc.AddTrashM();
            }

            trashHit.transform.gameObject.SetActive(false); //Desabilita 
            // Destroy(trashHit.transform.gameObject); //destroi o lixo
        } 
    }

//metodo pra chamar, pq para dar o RELLEY o nome precisa ser uma string
    void GameOver(){
        gc.ShowGameOver();//chamando outra função
    }
}
