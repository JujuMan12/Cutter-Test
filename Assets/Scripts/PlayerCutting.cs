using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCutting : MonoBehaviour
{
    [HideInInspector] public bool isCutting;

    [Header("Cutting")]
    [SerializeField] private Animator animator;
    [SerializeField] public GameObject sickle;

    private void Update()
    {
        HandleCutting();
    }

    private void HandleCutting()
    {
        if (Input.GetButtonDown("Jump") && !isCutting) //TODO
        {
            sickle.SetActive(true);
            isCutting = true;
            animator.SetTrigger("cutting");
        }
    }

    public void StopCutting()
    {
        sickle.SetActive(false);
        isCutting = false;
    }
}
