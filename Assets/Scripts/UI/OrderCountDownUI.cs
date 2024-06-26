using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OrderCountdownUI : MonoBehaviour
{
    [SerializeField] private Image countdownImage;

    public void Show()
    {
        gameObject.SetActive(true);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }

    public void UpdateCountdown(float timeRemaining, float totalTime)
    {
        Show();
        float progress = timeRemaining / totalTime;
        countdownImage.fillAmount = progress;

        if (progress <= 0)
        {
            Destroy(gameObject, 0.5f);
        }
    }
}
