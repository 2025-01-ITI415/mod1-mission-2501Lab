using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelMenu : MonoBehaviour
{
    public void Openlevel(int levelId){
        string levelName = "Level " + levelId;
        SceneManagement.LoadScene(levelName);
    }
}
