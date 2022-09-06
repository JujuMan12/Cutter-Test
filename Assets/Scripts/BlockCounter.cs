using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockCounter : MonoBehaviour
{
    [HideInInspector] private Color stockedDeltaColor;

    [Header("Player")]
    [SerializeField] private PlayerBlockManager playerBlockManager;

    [Header("Counter")]
    [SerializeField] private TMPro.TextMeshProUGUI blockAmountText;
    [SerializeField] private Color stockedMinColor = Color.green;
    [SerializeField] private Color stockedMaxColor = Color.red;

    private void Start()
    {
        stockedDeltaColor = stockedMaxColor - stockedMinColor;
    }

    private void FixedUpdate()
    {
        UpdateCounterText();
    }

    private void UpdateCounterText()
    {
        int blockAmount = playerBlockManager.collectedBlocks.Count;
        int maxBlocks = playerBlockManager.maxBlocks;

        blockAmountText.text = $"{blockAmount}/{maxBlocks}";
        blockAmountText.color = stockedDeltaColor / maxBlocks * blockAmount + stockedMinColor;
    }
}
