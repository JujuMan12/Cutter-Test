using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinCounter : MonoBehaviour
{
    [HideInInspector] private float currentAmount;
    [HideInInspector] private float targetAmount;
    [HideInInspector] private GameObject coin;

    [Header("Counting")]
    [SerializeField] private float blockPrice = 15f;
    [SerializeField] private float countSpeed = 1f;

    [Header("UI")]
    [SerializeField] private Transform canvas;
    [SerializeField] private TMPro.TextMeshProUGUI coinText;
    [SerializeField] private GameObject coinPrefab;
    [SerializeField] private float moveSpeed = 500f;

    private void FixedUpdate()
    {
        coinText.text = Mathf.Round(currentAmount).ToString();
    }

    private void Update()
    {
        if (coin != null)
        {
            MoveCoin();
        }

        if (currentAmount != targetAmount)
        {
            currentAmount = Mathf.Lerp(currentAmount, targetAmount, Time.deltaTime * countSpeed);
        }
    }

    public void CreateCoin(Vector3 position)
    {
        coin = Instantiate(coinPrefab, position, Quaternion.identity, canvas);
    }

    private void MoveCoin()
    {
        if (coin.transform.position != coinText.rectTransform.position)
        {
            coin.transform.position = Vector3.MoveTowards(coin.transform.position, coinText.rectTransform.position, Time.deltaTime * moveSpeed);
        }
        else
        {
            targetAmount += blockPrice;
            Destroy(coin);
            coin = null;
        }
    }
}
