using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockComponent : MonoBehaviour
{
    [HideInInspector] private bool shouldRotate = true;
    [HideInInspector] private bool shouldReposition = true;
    [HideInInspector] private Vector3 targetPosition;

    [Header("Components")]
    [SerializeField] private BoxCollider boxCollider;

    [Header("Spawn")]
    [SerializeField] private float initialOffset = 2f;
    [SerializeField] private float posXMax = 3f;
    [SerializeField] private float posXMin = -3f;
    [SerializeField] private float posZMax = 4f;
    [SerializeField] private float posZMin = -2.5f;

    [Header("Movement")]
    [SerializeField] private float rotationSpeed = 50f;
    [SerializeField] private float repositionSpeed = 2.5f;


    private void Start()
    {
        SetInitialPosition();
    }

    private void FixedUpdate()
    {
        if (shouldReposition && transform.position != targetPosition)
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
        targetPosition = transform.position + randomOffset;

        targetPosition.x = Mathf.Clamp(targetPosition.x, posXMin, posXMax);
        targetPosition.z = Mathf.Clamp(targetPosition.z, posZMin, posZMax);
    }

    private void MoveToTargetPosition()
    {
        transform.position = Vector3.Lerp(transform.position, targetPosition, repositionSpeed * Time.deltaTime);
    }

    private void IdleRotate()
    {
        transform.Rotate(0f, rotationSpeed * Time.deltaTime, 0f);
    }

    private void SetTargetPosition(Vector3 newPosition)
    {
        targetPosition = newPosition;
    }

    public void Collect(Transform backpack, float posY)
    {
        transform.SetParent(backpack);
        transform.localPosition = new Vector3(0f, posY, 0f);
        transform.localRotation = Quaternion.Euler(Vector3.zero);
        //SetLocalTargetPosition(new Vector3(0f, posY, 0f)); //TODO

        shouldReposition = false;
        shouldRotate = false;
        boxCollider.enabled = false;
    }

    public void Deliver(Transform deliveringZone, float deliveringTime)
    {
        transform.SetParent(deliveringZone);
        transform.localRotation = Quaternion.Euler(Vector3.zero);

        shouldReposition = true;
        SetTargetPosition(deliveringZone.position);

        Destroy(gameObject, deliveringTime);
    }
}
