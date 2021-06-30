using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    bool _isGameOver;

    private void Update()
    {
        if (Input.GetKey(KeyCode.R) && _isGameOver == true)
        {
            SceneManager.LoadScene(0);
        }
    }

    public void GameOVer()
    {
        _isGameOver = true;
    }
}
