using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameMangerController : MonoBehaviour
{
    public Text scoreText;

    public Text livesText;

    private int score;

    public int lives;
    // Start is called before the first frame update
    void Start()
    {
        score = 0;
        lives = 5;
    }
    public void GanarPuntos(int puntos){
        score += puntos;
        PrintLivesInScreen();
        PrintScoreInScreen();
    }
    public void PerderVida(){
        lives -= 1;
        PrintLivesInScreen();
    }
    public void PrintScoreInScreen(){
        scoreText.text = "Puntaje " + score;
    }
    public void PrintLivesInScreen(){
        livesText.text = "Balas: " + lives;
    }

}
