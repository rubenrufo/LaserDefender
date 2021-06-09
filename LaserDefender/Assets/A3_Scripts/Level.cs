using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;    // Necessary for loading different scenes

public class Level : MonoBehaviour
{

    [SerializeField] float waitAfterPlayerDies = 3f;



    public void LoadStartMenu()
    {
        SceneManager.LoadScene(0);   
    }

    public void LoadLevel1()
    {
        SceneManager.LoadScene("Level1");   // Not the best way to do it   
        FindObjectOfType<GameSession>().ResetGame();
    }

    public void LoadGameOver()
    {
        StartCoroutine(LoadGameOverWithDelay());
    }

    public void LoadOptionsMenu()
    {
        SceneManager.LoadScene("OptionsMenu");   // Not the best way to do it   
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    IEnumerator LoadGameOverWithDelay()
    {
        yield return new WaitForSeconds(waitAfterPlayerDies);
        SceneManager.LoadScene("GameOver");   // Not the best way to do it  
    }

}
