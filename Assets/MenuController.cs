using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    public GameObject levelSelectorMenu;

    public GameObject StartSelectorMenu;
    private int selectedLevel = 1; // Default selected level

    // Start is called before the first frame update
    void Start()
    {
        levelSelectorMenu.SetActive(false); // Initially hide the level selector menu
    }

    // Update is called once per frame
    void Update()
    {
        // Check for the "Enter" key press to show/hide level selector menu
        if (Input.GetKeyDown(KeyCode.Return))
        {
            if (levelSelectorMenu.activeSelf)
            {
                // If level selector menu is active, start the selected level
                StartGame();
            }
            else
            {
                // If level selector menu is not active, show it
                levelSelectorMenu.SetActive(true);
                StartSelectorMenu.SetActive(false);
            }
        }

        // Check for left and right arrow key presses to change the selected level
        if (levelSelectorMenu.activeSelf)
        {
            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                ChangeSelectedLevel(-1); // Move to the previous level
            }
            else if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                ChangeSelectedLevel(1); // Move to the next level
            }
        }
    }

    // Method to start the game with the selected level
    void StartGame()
    {
        // Load the game scene with the selected level. Adjust the scene names accordingly.
        SceneManager.LoadScene("Level" + selectedLevel.ToString());
    }

    // Method to change the selected level with bounds checking
    void ChangeSelectedLevel(int direction)
    {
        selectedLevel += direction;

        // Adjust the level bounds based on your game's structure
        if (selectedLevel < 1)
        {
            selectedLevel = 1;
        }
        else if (selectedLevel > 2) // Adjust this based on the number of levels you have
        {
            selectedLevel = 2; // Change this value accordingly
        }

        // Update the level selector UI or perform any other necessary actions
        Debug.Log("Selected Level: " + selectedLevel);
    }
}
