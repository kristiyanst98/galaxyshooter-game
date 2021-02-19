using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ToMainMenu : MonoBehaviour
{
    
    void Start()
    {
        StartCoroutine(MainMenu());
    }

    
    void Update()
    {
        if (Input.anyKeyDown)
        {
            SceneManager.LoadScene(1);
        }
    }

    IEnumerator MainMenu()
    {
        yield return new WaitForSeconds(5.5f);
        SceneManager.LoadScene(1);
    }
}
