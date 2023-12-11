using UnityEngine;

using UnityEngine.SceneManagement;  
public class LifeController : MonoBehaviour
{
    public int maxLives = 3;
    public int currentLives;

    public GameObject objectToDisableOnLife1;
    public GameObject objectToDisableOnLife2;
    public GameObject objectToDisableOnLife3;

    public PlayerController thisPlayer;
    void Start()
    {
        currentLives = maxLives;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Hazard"))
        {
            LoseLife();
        }
    }

    void LoseLife()
    {
        currentLives--;
        thisPlayer.hurt = true;
        if (currentLives == 2)
        {
            objectToDisableOnLife1.SetActive(false);
        }
        else if (currentLives == 1)
        {
            objectToDisableOnLife2.SetActive(false);
        }
        else if (currentLives == 0)
        {
            objectToDisableOnLife3.SetActive(false);
            SceneManager.LoadScene("Game Over");
        }
    }
}