using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{
    [SerializeField]
    private bool _isGameOver;

    public void GameIsOver()
    {
        _isGameOver = true;
    }

    private void Update()
    {
        if (_isGameOver)
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                _isGameOver = false;
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
              
            }
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }
}
