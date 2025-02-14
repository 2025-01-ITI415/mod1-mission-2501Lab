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
   
    
    void Start(){
    S = this; 
    
    level = 0;
    shotsTaken = 0;

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
