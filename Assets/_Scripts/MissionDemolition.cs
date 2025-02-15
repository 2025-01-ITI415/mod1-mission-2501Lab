using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum GameMode{
    idle,
    playing,
    levelEnd
}

public class MissionDemolition : MonoBehaviour
{
    // Start is called before the first frame update
    [Header("Inscribed")]
    public Text               uitLevel;  // The UIText_Level Text
    public Text               uitShots;  // The UIText_Shots Text
    public Vector3            castlePos; // The place to put castles
    public GameObject[]       castles;   // An array of the castles

    [Header("Dynamic")]
    public int                level;     // The current level
    public int                levelMax;  // The number of levels
    public int                shotsTaken;
    public GameObject         castle;    // The current castle
    public GameMode           mode = GameMode.idle;
    public string             showing = "Show Slingshot"; // FollowCam mode


    void Start()
    {
        S = this;
        level = 0;
        shotsTaken = 0;
        levelMax = castles.Length;
       StartLevel();
    }

    void StartLevel(){
        if (castle != null){
            Destroy(castle);
        }
        Projectile.DESTROY_PROJECTILES();

        castle = Instantiate<GameObject>(castles[level]);
        castle.transform.position = castlePos;

        global.goalMet = false;

        UpdateGUI();

        mode = GameMode.playing;
    }

    void UpdateGUI(){
        uitLevel.text = "Level: " +(level+1) " of " + levelMax;
        uitShots.text = "Shots Taken: " + shotsTaken;
    }

    // Update is called once per frame
    void Update()
    {
        UpdateGUI();

        if ((mode == GameMode.playing) && Goal.goalMet){
            mode = GameMode.levelEnd
            Invoke("NextLevel", 2f);
        }
    }
    void NextLevel(){
        level++;
        if (level == levelMax){
            level = 0;
            shotsTaken =0;
        }
        StartLevel();
    }

    static public void SHOT_FIRED(){
        S.shotsTaken++;
    }

    static public GameObject GET_CASTLE(){
        return S.castle;
    }
}
