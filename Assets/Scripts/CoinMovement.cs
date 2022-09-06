using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinMovement : MonoBehaviour
{
    [HideInInspector] public Vector3 targetPosition;
    [HideInInspector] public CoinCounter coinCounter;

    [Header("UI")]
    [SerializeField] private float moveSpeed = 500f;

    private void FixedUpdate()
    {
        if (transform.position != targetPosition)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, Time.deltaTime * moveSpeed);
        }
        else
        {
            coinCounter.TakeCoin(gameObject);
        }
    }
}
