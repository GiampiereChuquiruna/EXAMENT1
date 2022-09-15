using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class GameMangerController : MonoBehaviour
{
    public Text scoreText;

    public Text livesText;

    public Text m1Text;
    public Text m2Text;
    public Text m3Text;

    private int score;

    private int m1;
    private int m2;
    private int m3;

    public int lives;
    // Start is called before the first frame update
    void Start()
    {
        score = 0;
        lives = 5;
        m1 = 0;
        m2 = 0;
        m3 = 0;
        PrintLivesInScreen();
        PrintScoreInScreen();
        PrintM1InScreen();
        PrintM2InScreen();
        PrintM3InScreen();
        LoadGame();
    }

    public void GanarPuntos(int puntos){
        score += puntos;
        PrintScoreInScreen();

    }
    public void GanarBronce(){
        m1 += 10;
        PrintM1InScreen();

    }
    public void GanarPlata(){
        m2 += 20;
        PrintM2InScreen();

    }
    public void GanarOro(){
        m3 += 30;
        PrintM3InScreen();

    }
    public void PerderVida(){
        lives -= 1;
        PrintLivesInScreen();
    }

    public void PrintScoreInScreen(){
        scoreText.text = "Puntaje " + score;
    }
    public void PrintM1InScreen(){
        m1Text.text = "Bronce " + m1;
    }
    public void PrintM2InScreen(){
        m2Text.text = "Plata " + m2;
    }
    public void PrintM3InScreen(){
        m3Text.text = "Oro " + m3;
    }

    public void PrintLivesInScreen(){
        livesText.text = "Balas: " + lives;
    }

    public void SaveGame(){
        var filePath = Application.persistentDataPath + "/save.dat";
        
        FileStream file;
         
        if (File.Exists(filePath))
            file = File.OpenWrite(filePath);
        else
            file = File.Create(filePath);

        GameData data = new GameData();
        data.Score = score;
        data.Moneda1 = m1;
        data.Moneda2 = m2;
        data.Moneda3 = m3;
        BinaryFormatter bf = new BinaryFormatter();
        bf.Serialize(file, data);
        file.Close();  
    }

    public void LoadGame(){
        var filePath = Application.persistentDataPath + "/save.dat";
        
        FileStream file;
         
        if (File.Exists(filePath))
            file = File.OpenRead(filePath);
        else{
            Debug.Log("Archivo no encontrado");
            return;
        }
 
        BinaryFormatter bf = new BinaryFormatter();
        GameData data = (GameData) bf.Deserialize(file);
        file.Close(); 
        score = data.Score; 
        m1 = data.Moneda1;
        m2 = data.Moneda2;
        m3 = data.Moneda3;
        PrintScoreInScreen();
        PrintM1InScreen();
        PrintM2InScreen();
        PrintM3InScreen();
    }
}
