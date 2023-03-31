using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JoustDetection : MonoBehaviour
{
    public RoundHandler roundHandler;

    private void Awake()
    {
        roundHandler = GameObject.Find("GameManager").GetComponent<RoundHandler>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Collision detected");
        if (other.gameObject.GetComponent<BoxCollider2D>() != null)
        {
            roundHandler.CalculateJousteResult();
        }
    }
}
