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
            if(Input.GetKeyDown(KeyCode.Space)){
                jumpVelocity = jumpHeight;
            } 
        }
        else{
                jumpVelocity -= gravity; //se ele estiver no ar, vai cair
            }
        direction.y=jumpVelocity;
        controller.Move(direction * Time.deltaTime);
    }
}
