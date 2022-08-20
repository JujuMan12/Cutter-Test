using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBlockCollecting : MonoBehaviour
{
    [HideInInspector] private List<GameObject> collectedBlocks;
    //[HideInInspector] private float backpackHeight;

    [Header("Collecting")]
    [SerializeField] private string blockTag;
    [SerializeField] private Transform backpack;
    //[SerializeField] private float posYHighest;
    [SerializeField] private float posYLowest;
    [SerializeField] private float posYOffset;

    private void Start()
    {
        collectedBlocks = new List<GameObject>();
        //backpackHeight = posYHighest - posYLowest;
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.CompareTag(blockTag))
        {
            collectedBlocks.Add(collider.gameObject);

            int blockId = collectedBlocks.Count - 1;
            //float posY = posYLowest + backpackHeight / 40 * blockId; //TODO: 40
            float posY = posYLowest + posYOffset * blockId;

            collider.GetComponent<BlockIdle>().enabled = false;

            collider.transform.SetParent(backpack);
            collider.transform.localPosition = new Vector3(0f, posY, 0f);
            collider.transform.localRotation = Quaternion.Euler(Vector3.zero);
            //collider.GetComponent<BlockIdle>().SetLocalTargetPosition(new Vector3(0f, posY, 0f)); //TODO
        }
    }
}
