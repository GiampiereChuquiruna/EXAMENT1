using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class GameManagerController : MonoBehaviour
{
    public Text scoreText;

    public Text armaText;

    private int score;
    public const int Scena1 = 0;
    public const int Scena2 = 1;
    
    private int m1;
    private int m2;
    // Start is called before the first frame update
    void Start()
    {
        score = 0;
        m1 = 0;
        PrintLivesInScreen();
        PrintScoreInScreen();
        LoadGame();
    }

    public void GanarPuntos(int puntos){
        score += puntos;
        PrintScoreInScreen();

    }
    public void GanarBronce(){
        m1 += 10;

    }
    public void GanarPlata(){
        m2 += 20;
    }
    public void PerderVida(){
        m1 -= 1;
        PrintLivesInScreen();
    }
    public void PrintScoreInScreen(){
        scoreText.text = "Puntaje " + score;
    }
    public void PrintLivesInScreen(){
        armaText.text = "Bronce " + m1;
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
        Debug.Log("Si cargo");
        //PrintScoreInScreen();
        //PrintM1InScreen();
        //PrintM2InScreen();
        //PrintM3InScreen();
    }
}
