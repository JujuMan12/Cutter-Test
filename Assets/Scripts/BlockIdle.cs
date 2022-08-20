using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockIdle : MonoBehaviour
{
    [HideInInspector] private bool shouldRotate = true;
    [HideInInspector] private Vector3 targetLocalPosition;

    [Header("Idle")]
    [SerializeField] private float rotationSpeed = 50f;
    [SerializeField] private float initialOffset = 2f;

    [Header("Reposition")]
    [SerializeField] private float repositionSpeed = 2.5f;

    private void Start()
    {
        SetInitialPosition();
    }

    private void FixedUpdate()
    {
        if (transform.position != targetLocalPosition)
        {
            MoveToTargetPosition();
        }

        if (shouldRotate)
        {
            IdleRotate();
        }
    }

    private void SetInitialPosition()
    {
        Vector3 randomOffset = new Vector3(Random.Range(-initialOffset, initialOffset), 0f, Random.Range(-initialOffset, initialOffset));
        targetLocalPosition = transform.localPosition + randomOffset;
    }

    private void MoveToTargetPosition()
    {
        transform.position = Vector3.Lerp(transform.position, targetLocalPosition, repositionSpeed * Time.deltaTime);
    }

    private void IdleRotate()
    {
        transform.Rotate(0f, rotationSpeed * Time.deltaTime, 0f);
    }

    public void SetLocalTargetPosition(Vector3 newPosition)
    {
        targetLocalPosition = newPosition;
        shouldRotate = false;
        transform.rotation = Quaternion.Euler(Vector3.zero);
    }
}
