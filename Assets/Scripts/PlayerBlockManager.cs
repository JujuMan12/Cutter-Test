using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBlockManager : MonoBehaviour
{
    [HideInInspector] public List<GameObject> collectedBlocks;
    [HideInInspector] private float deliveringCooldown;

    [Header("Collecting")]
    [SerializeField] private string blockTag = "Block";
    [SerializeField] public int maxBlocks = 40;
    [SerializeField] private Transform backpack;
    [SerializeField] private float posYLowest;
    [SerializeField] private float posYOffset;

    [Header("Delivering")]
    [SerializeField] private string deliveringZoneTag = "DeliveringZone";
    [SerializeField] private float deliveringTime = 1f;
    [SerializeField] private CoinCounter coinCounter;

    private void Start()
    {
        collectedBlocks = new List<GameObject>();
    }

    private void Update()
    {
        if (deliveringCooldown > 0f)
        {
            deliveringCooldown -= Time.deltaTime;
        }
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.CompareTag(blockTag) && collectedBlocks.Count < maxBlocks)
        {
            collectedBlocks.Add(collider.gameObject);

            int blockId = collectedBlocks.Count - 1;
            float posY = posYLowest + posYOffset * blockId;

            BlockComponent blockComponent = collider.GetComponent<BlockComponent>();
            blockComponent.Collect(backpack, posY);
        }
    }

    private void OnTriggerStay(Collider collider)
    {
        if (collider.CompareTag(deliveringZoneTag) && collectedBlocks.Count != 0 && deliveringCooldown <= 0f)
        {
            GameObject lastBlock = collectedBlocks[collectedBlocks.Count - 1];
            BlockComponent blockComponent = lastBlock.GetComponent<BlockComponent>();

            blockComponent.Deliver(collider.transform, deliveringTime);
            collectedBlocks.Remove(lastBlock);

            coinCounter.CreateCoin(Camera.main.WorldToScreenPoint(transform.position));

            deliveringCooldown = deliveringTime;
        }
    }
}
