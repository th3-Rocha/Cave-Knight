using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelWin : MonoBehaviour
{
    public string sceneToLoad = "YourSceneName"; // Assign the scene name in the Inspector

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // Change the scene when the player enters the trigger zone
            SceneManager.LoadScene(sceneToLoad);
        }
    }
}