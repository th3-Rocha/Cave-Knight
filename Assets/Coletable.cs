using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : MonoBehaviour
{
    public float amplitude = 0.5f; 
    public float frequency = 1f;

    private Vector3 startPos;

    public bool IsFluctuate = true;

    void Start()
    {
      
        float randomDelay = Random.Range(0f, 1f);
        Invoke("InitializeStartPosition", randomDelay);
    }

    void InitializeStartPosition()
    {
        if(IsFluctuate){
            startPos = transform.position;
            InvokeRepeating("Fluctuate", 0f, 0.1f); 
        }
    }

    void Fluctuate()
    {
        float yOffset = amplitude * Mathf.Sin(frequency * Time.time);

        transform.position = startPos + new Vector3(0f, yOffset, 0f);
    }
}
