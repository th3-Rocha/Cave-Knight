using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class Collector : MonoBehaviour
{
    public int points = 0; // Variable to store the points
    public TextMeshProUGUI pointsText; 

    public GameObject Effector;
    
    void Start()
    {
        
    }

    void Update()
    {
        UpdatePointsText();
    }

    // OnTriggerEnter is called when the Collider other enters the trigger
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Gem"))
        {
            GameObject effector = Instantiate(Effector, other.transform.position, Quaternion.identity);
            effector.GetComponent<Effector>().selectedAnimation = "GemExplosion"; //EffectorNothing
            Destroy(other.gameObject);

            points++;
            Debug.Log("Points: " + points);
        }
    }
      void UpdatePointsText()
    {
        if (pointsText != null)
        {
            pointsText.text = "Gems: " + points.ToString();
        }
    }
}