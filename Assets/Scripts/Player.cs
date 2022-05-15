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
            //da um pulo
            if(Input.GetKeyDown(KeyCode.Space)){
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
    }

    //move o player pra esquerda
    IEnumerator LeftMove(){
        for(float i = 0; i <2.5; i+= 0.1f){
            controller.Move(Vector3.left * Time.deltaTime * horizontalSpeed);
            yield return null;
        }
        isMovingLeft = false;
    }

    //mover pra 
    IEnumerator RighMove(){
        for(float i = 0; i<2.5;i += 0.1f){
            controller.Move(Vector3.right * Time.deltaTime * horizontalSpeed);
            yield return null;
        }
        isMovingRight = false;
    }
}
