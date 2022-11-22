using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    CharacterController controller; //armazena o componente Capsular Controller no Player
    public float speed; //velocidade 
    private float minSpeed = 10f;
    
    private float jumpVelocity; 
    public float jumpHeight; //força do pulo
    private float jumpStart;
    public float slideHeight;
    private float slideStart;

    public float gravity; //gravidade

    public float horizontalSpeed; //velocidade horizontal
    private bool isMovingLeft;
    private bool isMovingRight;

    //colisão
    public float rayRadius;
    public LayerMask obstacleLayer;
    public LayerMask trashLayer;
    public int maxLife = 2; //quantidade de vidas
    public int currentLife; //vidas atuais
    public float invicibleTime;
    public bool invincible = false;


    //animação de jump
    public Animator jump;
    public Animator slide;
    public Animator die;

    public bool isJump = false;
    public bool isSlide = false;
    public bool isDie;

    //controler do game over
    private GameController gc;

    public Transform player;
    public GameObject modelPlay;
    private UIController uiController;
 

    //touch para mobile
    private bool isSwipping = false;
    private Vector2 startingTouch;
    
    void Start(){
         player = GameObject.FindGameObjectWithTag("Player").transform;

         controller = GetComponent<CharacterController>();
         gc = FindObjectOfType<GameController>(); //so posso fazer isso pq so tenho um gamecontroller na cena 

        currentLife = maxLife; 
        uiController = FindObjectOfType<UIController>();
    }

    void Update(){
        //movimentação base do personagem
        Vector3 direction = Vector3.forward * speed; //add 1 no eixo z
        //verificação se o personagem esta tocando no chão
        if(controller.isGrounded){
            //da um pulo, a condição isDie desabilita a ação de pular se for true
            if(Input.GetKeyDown(KeyCode.Space)){
                Jump();
            } 

            if(Input.GetKeyDown(KeyCode.DownArrow)){
                Slide();
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

        if(isJump){
            float tempJump = (transform.position.z - jumpStart) / jumpHeight;
            if(tempJump >=0.5f){
                isJump = false; 
            }

        }

        if(isSlide){
            float tempSlide = (transform.position.z - slideStart) / slideHeight;
            if(tempSlide >= 1f){
                controller.center = new Vector3(0,1,0);
                controller.height = 2.5f;
                controller.radius= 1f;
                 
                isSlide=false;
            }
        }

        direction.y=jumpVelocity;
        controller.Move(direction * Time.deltaTime);
        OnCollision();//deve ser chamado o tempo todo

        if(Input.touchCount == 1){
            if(isSwipping){
                Vector2 diff = Input.GetTouch(0).position - startingTouch;
                diff = new Vector2(diff.x / Screen.width, diff.y / Screen.width);
                if(diff.magnitude > 0.01f){
                    if(Mathf.Abs(diff.y) > Mathf.Abs(diff.x)){
                        if(diff.y < 0){
                            Slide();
                        }
                        else{
                            Jump();
                        }
                    }
                    else{
                        if(diff.x < 0 && transform.position.x>-1f && !isMovingLeft){
                            //esquerda
                            isMovingLeft = true;
                            //chamada da co-rotina 
                            StartCoroutine(LeftMove());
                        }else if( transform.position.x<1f && !isMovingRight ){
                            //direita
                            //chamada da co-rotina 
                            isMovingRight = true;
                            StartCoroutine(RighMove());
                        }
                    }
                    isSwipping = false;
                }
            }

            if(Input.GetTouch(0).phase == TouchPhase.Began){
                startingTouch = Input.GetTouch(0).position;
                isSwipping = true;
            }else if(Input.GetTouch(0).phase == TouchPhase.Ended){
                isSwipping = false;
            }
        }

      

    }

    void Jump(){
        if(!isJump && !isDie && !isSlide){
                jumpStart = transform.position.z;
                jump.SetTrigger("jump"); //reconhece a condição jump
                jumpVelocity = jumpHeight;
                jump.SetFloat("animVelocity", speed/jumpHeight);
                isJump = true;
        }
    }

    void Slide(){
        if(!isJump && !isSlide && !isDie){
            slideStart = transform.position.z;
            slide.SetTrigger("slide"); //reconhece a condição slide
            jump.SetFloat("animVelocity", speed/slideHeight);
            controller.center = new Vector3(0,0,0);
            controller.height = 0;
            controller.radius= 0.3f;
            isSlide = true;
        }
    }

    //move o player pra esquerda
    IEnumerator LeftMove(){
        if(!isDie){
            for(float i = 0; i < 2.4; i+= 0.1f){
                controller.Move(Vector3.left * Time.deltaTime * horizontalSpeed);
                yield return null;
            }
            isMovingLeft = false;
        }
        
    }

    //mover pra direita
    IEnumerator RighMove(){
        if(!isDie){
            for(float i = 0; i< 2.4;i += 0.1f){
                controller.Move(Vector3.right * Time.deltaTime * horizontalSpeed);
                yield return null;
            }
            isMovingRight = false;
        }
        
    }

    private void OnTriggerEnter(Collider other)
        {
            if(invincible)
                return;

            if(other.CompareTag("Obstacle")){
                uiController.UpdateLives(currentLife);

                // executa a condição die do Animator 
                die.SetTrigger("die");
                player.transform.position = new Vector3(transform.position.x, 0, transform.position.z); //se colidir garante q iraá cair no chão 
                isDie=true;
                speed = 0;
                currentLife--;

                if(currentLife <= 0 ){
                    Invoke("GameOver", 1f);
                }else{
                    StartCoroutine(Blinking(invicibleTime));
                }
                    
            }
            
    }

    //identificar a colisão com algo
    void OnCollision(){
        //colisão ao bater nos lixos
        //linha desenhada pra frente e pra cima
        RaycastHit trashHit;
        if(Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out trashHit, rayRadius, trashLayer)){
            //destroi ao bater no lixo
            
            string trashTag = trashHit.transform.gameObject.tag; //aramzena o nome da tag do lixo em contato
           
            //verifica qual tipo de lixo foi recolhido e adiciona o ponto
            uiController.AddTash(trashTag);
            trashHit.transform.gameObject.SetActive(false); //Desabilita 
            // Destroy(trashHit.transform.gameObject); //destroi o lixo
        } 
    }


//corrotina de invensivel
IEnumerator Blinking(float time){
    invincible = true;
    float timer = 0;
    float currentBlink = 1f;
    float lastBlink = 0;
    float blinkPeriod=0.1f;
    bool enabled = false;
    yield return new WaitForSeconds(1f);
    speed = minSpeed;
    isDie=false;

    while (timer<time && invincible){
        modelPlay.SetActive(enabled);
        yield return null;

        timer += Time.deltaTime;
        lastBlink += Time.deltaTime;

        if(blinkPeriod < lastBlink){
            lastBlink = 0;
            currentBlink = 1f - currentBlink;
            enabled = !enabled;
        }
    }
    modelPlay.SetActive(true);
    invincible = false;
}

//metodo pra chamar, pq para dar o RELLEY o nome precisa ser uma string
    void GameOver(){
        gc.ShowGameOver();//chamando outra função
    }
}