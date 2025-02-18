using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public enum GameMode {                                                            // b
idle, 
playing,
levelEnd
 }

public class MissionDemolition : MonoBehaviour {
    static private MissionDemolition S; //a private singleton //c

    [Header("Inscribed")]
    public Text                  utiLevel;  // the ui_level text
    public Text                   utiShots; // the ui_shots text 
    public Vector3                castlePos; // place to put castle 
    public GameObject []          castles; //an array of the  castles 

    [Header("Dynamic")]
    public int                  level; //the current level 
    public int                  levelMax; // the numbers of levels 
    public int                  shotsTaken; 
    public GameObject            castle; // the current castle 
    public GameMode             mode = GameMode.idle;
    public string               showing = "Show Slingshot"; //follow cam mode 
    // Start is called before the first frame update
    void Start(){
        S = this; //define the singleton

        level = 0;
        shotsTaken = 0;
        levelMax = castles.Length;
        StartLevel();
    }
    void StartLevel() {
    // Get rid of the old castle if one exist 
    if (castle != null){
        Destroy( castle );
     }

     //destroy old projectiles if they exist; the method is not written yet 
     Projectile.DESTROY_PROJECTILES(); //d

     //instantiate the new castle 
     castle = Instantiate<GameObject>( castles [level ] );
     castle.transform.position = castlePos; 

     //rest the goal 
     Goal.goalMet = false; 
     
     UpdateGUI();

     mode = GameMode.playing; 

     FollowCam.SWITCH_VIEW( FollowCam.eView.both );
    }
    void UpdateGUI() {
        // show the data in GUITexts 
    utiLevel.text = "Level: "+(level+1)+" of "+levelMax;
    utiShots.text = "Shots Taken: " +shotsTaken;
    }

    void Update() {
        UpdateGUI();
        
        //check the level end 
    if ( ( mode == GameMode.playing) && Goal.goalMet ) {
            //change mode to stop checking for level end 
            mode = GameMode.levelEnd;
            FollowCam.SWITCH_VIEW( FollowCam.eView.both );
            //start the next level in 2 seconds 
            Invoke("NextLevel", 2f);
        }
        
    }
    void NextLevel() {
        level++;
        if (level == levelMax) {
            level = 0;
            shotsTaken = 0;
        }
        StartLevel();
    }
    //static method that allows code anywhere to increase shots taken //f    
     static public void SHOT_FIRED() {
        S.shotsTaken++;
    }
    //static method that allos code anywhere to refence  castle //g
    static public GameObject GET_CASTLE(){
        return S.castle;
    }
}
