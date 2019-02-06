using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerScript : MonoBehaviour
{
    // Variaveis globais
    private Animator playerAnimator; // variavel privada
    private Rigidbody2D playerRb;

        public Transform groundCheck; // OBJETO RESPONSAVEL POR DETECTAR SE O PERSONAGEM ESTÁ SOBRE UMA SUPERFÍCIE 
    public float speed;  // VELOCIDADE DE MOVIMENTO DO PERSONAGEM
    public float jumpForce; // FORÇA APLICADA PARA GERAR PULO DO PERSONAGEM


    public bool Grounded; // INDICA SE O PERSONAGEM ESTÁ PISANDO EM ALGUMA SUPERFÍCIE
    public bool attacking; // INDICAR SE O PERSONAGEM ESTÁ EXECUTANDO UM ATAQUE
    public bool lookLeft; // INDICA SE O PERSONAGEM TA VIRADO PARA A ESQUERDA
    public int idAnimation; // INDICA O ID DA ANIMAÇÃO

    private float h, v;

    public Collider2D standing, crouching; // COLISOR EM PÉ E ABAIXADO
      


// USE ISSO PARA INICIAR
    void Start(){
        playerRb = GetComponent<Rigidbody2D>(); // ASSOCIA O COMPONENTE A VARIAVEL 
        playerAnimator = GetComponent<Animator>(); // ASSOCIA O COMPONENTE A VARIAVEL
                        
    }

    void FixedUpdate() // TAXA DE ATUALIZAÇÃO FIXA 0.02
    {
        Grounded = Physics2D.OverlapCircle(groundCheck.position, 0.02f);
        playerRb.velocity = new Vector2(h * speed, playerRb.velocity.y);

    }

// ATUALIZAÇÃO EM TEMPO DE EXECUÇÃO
    void Update() {
        h = Input.GetAxisRaw("Horizontal");
        v = Input.GetAxisRaw("Vertical");

        if(h > 0 && lookLeft == true && attacking == false){
            flip();
        }else if (h < 0 && lookLeft == false){
            flip();
        }

        if(v < 0){
            idAnimation = 2;
            if(Grounded == true){
            h = 0;
            }
           
        }else if(h != 0){
            idAnimation = 1;
        }else{
            idAnimation = 0;
        }

        if(Input.GetButtonDown("Fire1") && v >= 0 && attacking == false){
            playerAnimator.SetTrigger("atack");
        }

        if(Input.GetButtonDown("Jump") && Grounded == true){
            playerRb.AddForce(new Vector2(0, jumpForce));
            crouching.enabled = false;
            standing.enabled = true;
        }

        if(attacking == true && Grounded == true){
            h = 0;
        }

        if(v < 0 && Grounded == true){
            crouching.enabled = true;
            standing.enabled = false;
        }else if (v >= 0 && Grounded == true){
            crouching.enabled = false;
            standing.enabled = true;
        }else if(v != 0 && Grounded == false){
            crouching.enabled = false;
            standing.enabled = true;
        }
        
        
        
        
        
        playerAnimator.SetBool("grounded", Grounded);
        playerAnimator.SetInteger("idAnimation", idAnimation);
        playerAnimator.SetFloat("speedY", playerRb.velocity.y);
    }
    
    void flip(){
        lookLeft = !lookLeft; // INVERTE O VALOR DA VARIAVEL BOOLEANA
        float x = transform.localScale.x;
        x *= -1; // INVERTE O SINAL DO SCALE X
        transform.localScale = new Vector3(x, transform.localScale.y, transform.localScale.z);
    }

    void atack (int atk){
        switch(atk){
            case 0:
                attacking = false;
                break;
            
            case 1:
                attacking = true;
                break;

        }

    }


    }

