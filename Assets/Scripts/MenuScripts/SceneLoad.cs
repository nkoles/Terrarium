using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoad : MonoBehaviour
{

    public void ChangeScene(String name) 
    {
        SceneManager.LoadScene(name);
    }


    public void CloseGame() 
    {
        Application.Quit();
    }


}
