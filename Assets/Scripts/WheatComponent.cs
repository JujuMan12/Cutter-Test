using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WheatComponent : MonoBehaviour
{
    [HideInInspector] private bool shouldGrow;
    [HideInInspector] private float timeToGrow;

    [Header("Cutting")]
    [SerializeField] private BoxCollider boxCollider;
    [SerializeField] private GameObject[] meshes;
    [SerializeField] private float growingTime = 10f;

    private void FixedUpdate()
    {
        if (shouldGrow)
        {
            if (timeToGrow > 0f)
            {
                timeToGrow -= Time.deltaTime;
            }
            else
            {
                SetState(true);
            }
        }
    }

    public void Cut()
    {
        SetState(false);
        timeToGrow = growingTime;
    }

    private void SetState(bool isGrown)
    {
        shouldGrow = !isGrown;

        foreach (GameObject mesh in meshes)
        {
            mesh.SetActive(isGrown);
        }

        boxCollider.enabled = isGrown;
    }
}
