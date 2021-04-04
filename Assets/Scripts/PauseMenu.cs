using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    bool pause = false;

    public void QuitToMainMenu()
    {
        SceneManager.LoadScene(0); ;
    }
}
