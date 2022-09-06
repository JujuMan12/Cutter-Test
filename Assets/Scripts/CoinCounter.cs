using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinCounter : MonoBehaviour
{
    [HideInInspector] private float currentAmount;
    [HideInInspector] private float targetAmount;

    [Header("Counting")]
    [SerializeField] private float blockPrice = 15f;
    [SerializeField] private float countSpeed = 1f;

    [Header("UI")]
    [SerializeField] private Transform canvas;
    [SerializeField] private TMPro.TextMeshProUGUI coinText;
    [SerializeField] private GameObject coinPrefab;

    private void FixedUpdate()
    {
        coinText.text = Mathf.Round(currentAmount).ToString();
    }

    private void Update()
    {
        if (currentAmount != targetAmount)
        {
            currentAmount = Mathf.Lerp(currentAmount, targetAmount, Time.deltaTime * countSpeed);
        }
    }

    public void CreateCoin(Vector3 position)
    {
        GameObject coin = Instantiate(coinPrefab, position, Quaternion.identity, canvas);
        CoinMovement coinMovement = coin.GetComponent<CoinMovement>();

        coinMovement.targetPosition = coinText.rectTransform.position;
        coinMovement.coinCounter = this;
    }

    public void TakeCoin(GameObject coin)
    {
        targetAmount += blockPrice;
        Destroy(coin);
    }
}
