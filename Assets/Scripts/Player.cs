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

    //colisão
    public float rayRadius;
    public LayerMask layer;

    //animação de jump
    public Animator jump;
    public Animator die;
    private bool isDie;

    void Start()
    {
         controller = GetComponent<CharacterController>();
    }
    void Update()
    {
        //movimentação base do personagem
        Vector3 direction = Vector3.forward * speed; //add 1 no eixo z
        //verificação se o personagem est tocando no chã
        if(controller.isGrounded){
            //da um pulo, a condição isDie desabilita a ação de pular se for true
            if(Input.GetKeyDown(KeyCode.Space) && !isDie){
                
                jump.SetTrigger("jump"); //reconhece a condição jump
                jumpVelocity = jumpHeight;
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
        for(float i = 0; i <2.5; i+= 0.1f){
            controller.Move(Vector3.left * Time.deltaTime * horizontalSpeed);
            yield return null;
        }
        isMovingLeft = false;
    }

    //mover pra direita
    IEnumerator RighMove(){
        for(float i = 0; i<2.5;i += 0.1f){
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

                //zera os valores 
                speed=0;
                horizontalSpeed=0;
                jumpHeight=0;
                jumpVelocity=0;
                Debug.Log("bateu!");
                
                isDie = true;
        }
    }
}
