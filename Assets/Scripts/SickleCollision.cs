using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EzySlice;

public class SickleCollision : MonoBehaviour
{
    [HideInInspector] private Transform uncollectedBlocks;

    [Header("Cutting")]
    [SerializeField] private string targetTag;
    [SerializeField] private GameObject wheatBlock;
    [SerializeField] private float wheatBlockPosY = 0.25f;

    private void Start()
    {
        uncollectedBlocks = GameObject.FindGameObjectWithTag("UncollectedBlocks").transform;
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.CompareTag(targetTag))
        {
            collider.GetComponent<WheatComponent>().Cut();
            //SliceWheat(collider.gameObject, ); //TODO

            Instantiate(wheatBlock, collider.transform.position + new Vector3(0f, wheatBlockPosY, 0f), Quaternion.identity, uncollectedBlocks);
        }
    }

    //private SlicedHull SliceWheat(GameObject objectToSlice, Vector3 planeWorldPosition, Vector3 planeWorldDirection) //TODO
    //{
    //    return objectToSlice.Slice(planeWorldPosition, planeWorldDirection);
    //}
}
