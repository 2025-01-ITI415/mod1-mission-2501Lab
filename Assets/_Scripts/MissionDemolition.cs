using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum GameMode
{
    idle,
    playing,
    levelEnd
}

public class MissionDemolition : MonoBehaviour
{
    static private MissionDemolition S;

    [Header("Inscribed")]
    public Text uitLevel;
    public Text uitShots;
    public Vector3 castlePos;
    public GameObject[] castles;

    [Header("Dynamic")]
    public int level;
    public int levelMax;
    public int shotsTaken;
    public GameObject castle;
    public GameMode mode = GameMode.idle;
    public string showing = "Show Slingshot";

    void Start()
    {
        S = this;
        level = 0;
        levelMax = castles.Length; // Fixed: Use Length instead of length
        StartLevel(); // Fixed: Corrected method name
    }

    void StartLevel() // Fixed: Corrected method name
    {
        Debug.Log("Starting level " + (level + 1));
        if (castle != null)
        {
            Debug.Log("Destroying previous castle...");
            Destroy(castle);
        }
        Projectile.DESTROY_PROJECTILES();
        Debug.Log("Instantiating new castle...");
        castle = Instantiate<GameObject>(castles[level]);
        castle.transform.position = castlePos;
        Goal.goalMet = false;
        UpdateGUI();
        mode = GameMode.playing;
    }

    void UpdateGUI()
    {
        if (uitLevel != null)
        {
            uitLevel.text = "Level: " + (level + 1) + " of " + levelMax;
        }
        else
        {
            Debug.LogError("uitLevel is not assigned!");
        }

        if (uitShots != null)
        {
            uitShots.text = "Shots Taken: " + shotsTaken;
        }
        else
        {
            Debug.LogError("uitShots is not assigned!");
        }
    }

    void Update()
    {
        UpdateGUI();
        if ((mode == GameMode.playing) && Goal.goalMet)
        {
            Debug.Log("Goal met! Transitioning to next level...");
            mode = GameMode.levelEnd;
            Invoke("NextLevel", 2f); // Fixed: Use Invoke instead of invoke
        }
    }

    void NextLevel()
    {
        Debug.Log("Advancing to next level...");
        level++;
        if (level == levelMax)
        {
            Debug.Log("Resetting to level 0...");
            level = 0;
            shotsTaken = 0;
        }
        StartLevel(); // Fixed: Corrected method name
    }

    static public void SHOT_FIRED()
    {
        S.shotsTaken++;
    }

    static public GameObject GET_CASTLE()
    {
        return S.castle;
    }
}