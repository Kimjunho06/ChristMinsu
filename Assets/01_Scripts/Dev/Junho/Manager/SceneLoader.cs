using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    private async void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            Loading.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }

    public void OnLoadScene(Action<AsyncOperation> callback) 
    {
        var next = SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex + 1);
        
        next.completed += callback;

    }
}
