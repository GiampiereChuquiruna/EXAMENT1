using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NinjaController : MonoBehaviour
{
    public float velocity = 0, jumpForce = 8, vCorrer = 2, defaultVelocity= 4;
    int d=1;
    public GameObject bullet;
    Rigidbody2D rb;
    SpriteRenderer sr;
    Animator animator;
    Collider2D cl;
    
    private GameManagerController gameManager;
    const int IDLE = 0;
    const int RUN= 1;
    const int ATTACK = 2;
    const int GLIDE = 3;
    const int SLIDE = 4;
    const int JUMP = 5; 
    const int DEAD = 6; 
    const int CLIMB = 7;

    private float timer;
    int p = 1;
    // Start is called before the first frame update
    void Start()
    {
        gameManager = FindObjectOfType<GameManagerController>();
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        cl = GetComponent<Collider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Movimiento();
    }
    private void Movimiento(){
        if(Input.GetKeyDown(KeyCode.RightArrow)){
            WalkRight();
        }
        if(Input.GetKeyUp(KeyCode.RightArrow)){
            Stop();
        }
        if(Input.GetKeyDown(KeyCode.LeftArrow)){
            WalkLeft();
        }
        if(Input.GetKeyUp(KeyCode.LeftArrow)){
            Stop();
        }
        if(Input.GetKeyDown(KeyCode.Space)){
            JUMPN();
        }
        Walk();
    }
    private void Walk(){
        rb.velocity = new Vector2(velocity, rb.velocity.y);
        if(velocity < 0)
            sr.flipX = true;
        if(velocity > 0)
            sr.flipX = false;
    }
    public void WalkRight(){
        velocity = defaultVelocity; 
        d = 1;
        ChangeAnimation(RUN);  
    }
    public void Stop(){
        velocity = 0;
        ChangeAnimation(IDLE); 
    }
    public void JUMPN(){
        rb.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
        ChangeAnimation(JUMP); 
    }
    public void WalkLeft(){
        velocity = -defaultVelocity;
        d = -1;
        ChangeAnimation(RUN);   
    }
    public void Atacar(){
        if(p == 1){
            var bulletPosition = transform.position + new Vector3(d,0,0);
            var gb = Instantiate(bullet, bulletPosition, Quaternion.identity) as GameObject;
            var controller = gb.GetComponent<BulletController>();
            //controller.SetLeftDirection();
            if(d>0)controller.SetRightDirection();
            else if(d<0)controller.SetLeftDirection();
            //controller.SetScoreText(scoreText); 
            CambioArma();
        }  
        else {
            ChangeAnimation(ATTACK);
        }       
    }
    public void CambioArma(){
        p = p*-1;
    }
    private void ChangeAnimation (int v){
        animator.SetInteger("Estado", v);
    }
}
