using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BulletController : MonoBehaviour
{
    public float velocity = 4;
    float velocity1;

    Rigidbody2D rb;
    // Start is called before the first frame update
    private GameManagerController gameManager;

    public void SetRightDirection(){
        velocity1 = velocity;
    }
    
    public void SetLeftDirection(){
        velocity1 = -velocity;
    }

    void Start()
    {
        gameManager = FindObjectOfType<GameManagerController>();
        rb = GetComponent<Rigidbody2D>();
        Destroy(this.gameObject, 5);
        gameManager.GanarPuntos(0);
    }

    // Update is called once per frame
    void Update()
    {
        rb.velocity = new Vector2(velocity1, 0);
    }

    void OnCollisionEnter2D(Collision2D other){
        if(other.gameObject.tag == "Enemy"){
            Destroy(this.gameObject);
            gameManager.GanarPuntos(1);
            gameManager.PerderVida();
            gameManager.SaveGame();
        }

    }
}