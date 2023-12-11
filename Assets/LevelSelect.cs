using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LevelSelect : MonoBehaviour
{
    public TextMeshProUGUI greenText;
    public TextMeshProUGUI whiteText;

    private int currentIndex = 0;

    void Start()
    {
        SelectText(currentIndex);
    }

    void Update()
    {
        // Check for arrow key input
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            // Move to the previous index
            currentIndex = Mathf.Max(0, currentIndex - 1);
            SelectText(currentIndex);
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            // Move to the next index
            currentIndex = Mathf.Min(1, currentIndex + 1);
            SelectText(currentIndex);
        }
    }

    void SelectText(int index)
    {
        // Reset both texts to white
        greenText.color = Color.white;
        whiteText.color = Color.white;

        // Set the selected text to green
        if (index == 0)
        {
            greenText.color = Color.green;
        }
        else
        {
            whiteText.color = Color.green;
        }
    }
}