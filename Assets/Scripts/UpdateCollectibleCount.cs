using UnityEngine;
using TMPro;

public class UpdateCollectibleCount : MonoBehaviour
{
    private TextMeshProUGUI collectibleText;
    private int lastCount = -1;

    void Start()
    {
        collectibleText = GetComponent<TextMeshProUGUI>();

        if (collectibleText == null)
        {
            Debug.LogError("TextMeshProUGUI component not found.");
        }
    }

    void Update()
    {
		int totalCollectibles = Object.FindObjectsByType<Collectible2D>(FindObjectsSortMode.None).Length;

		// Only update if count changed
		if (totalCollectibles != lastCount)
        {
            lastCount = totalCollectibles;
            collectibleText.text = $"Collectibles remaining: {totalCollectibles}";

            if (totalCollectibles == 0 && !GameManager.Instance.IsWinTriggered)
            {
                GameManager.Instance.PlayerWin();
            }
        }
    }
}
