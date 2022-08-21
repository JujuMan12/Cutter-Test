using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBlockCollecting : MonoBehaviour
{
    [HideInInspector] private List<GameObject> collectedBlocks;

    [Header("Collecting")]
    [SerializeField] private string blockTag;
    [SerializeField] private Transform backpack;
    [SerializeField] private float posYLowest;
    [SerializeField] private float posYOffset;

    [Header("Delivering")]
    [SerializeField] private string deliveringZoneTag;

    private void Start()
    {
        collectedBlocks = new List<GameObject>();
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.CompareTag(blockTag))
        {
            collectedBlocks.Add(collider.gameObject);

            int blockId = collectedBlocks.Count - 1;
            float posY = posYLowest + posYOffset * blockId;

            collider.GetComponent<BlockIdle>().enabled = false;

            collider.transform.SetParent(backpack);
            collider.transform.localPosition = new Vector3(0f, posY, 0f);
            collider.transform.localRotation = Quaternion.Euler(Vector3.zero);
            //collider.GetComponent<BlockIdle>().SetLocalTargetPosition(new Vector3(0f, posY, 0f)); //TODO
        }

        if (collider.CompareTag(deliveringZoneTag) && collectedBlocks.Count != 0)
        {
            GameObject lastBlock = collectedBlocks[collectedBlocks.Count - 1];
            BlockIdle blockComponent = lastBlock.GetComponent<BlockIdle>();

            blockComponent.enabled = true;
            //blockComponent.SetTargetPosition
        }
    }
}
