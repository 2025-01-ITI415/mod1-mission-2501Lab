using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI; 


public enum GameMode
{
    idle,
    playing,
    levelEnd
}
public class MissionDemolition : MonoBehaviour {

    static private MissionDemolition S;

    [Header("Inscribed")]

    public Text uitLevel;

    public Text uitShots;

    public Vector3 castlePos;

    public GameObject[] castles;


    [Header("Dynamic")]
    public int level;

    public GameObject castle;

    public GameMode mode = GameMode.idle;

    public string showing = "Show SLingshot";



    }
   
    
    void Start() { 
        S = this; 
    
        level = 0;
        shotsTaken = 0;
        levelMax = castles.Length;
        StartLevel();
    }

    void StartLevel() {
        if (castle != null) {
             Destroy(castle);
    }

        Projectile.DESTROY_PROJECTILES();

        castle = Instatiate<GameObject>(castles[level]);
        castle.trasnsform.position = castlePos;

        Goal.goalMet = false;
        UpdateGUI():
        mode = GameMode.playing;
        
    }
    void UpdateGUI(){
        uitLevels.text = "Level: " + (level + 1) + " of " + levelMax;
        uitShots.text = "Shots Taken: " + shotsTaken;
    }
    void Update(){
        UpdateGUI():
        if ( (mode == GameMode.playing) && Goal.goalMet){
            mode = GameMode.levelEnd;
            Invoke("NextLevel", 2f);
        }
    }


    void NextLevel(){
        
        level++;
        if (level == levleMax){
            level == 0;
        shotsTaken = 0; 

        }
        StartLevel(); 
    }
    static public void SHOT_FIRED(){
        return S.castle
    }
}
