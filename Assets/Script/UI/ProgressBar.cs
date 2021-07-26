using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBar : MonoBehaviour
{
    [SerializeField]
    private Image fillImage;
    [SerializeField]
    private TextMeshProUGUI progressBarState;

    [SerializeField]
    [Range(0, 1)]
    private float progress;

    public void SetState(float min, float max)
    {
        progressBarState.text = $"{min} / {max}";
    }

    public void SetProgress(float progress)
    {
        this.progress = progress;
        fillImage.fillAmount = progress;
    }

    public float GetProgress()
    {
        return progress;
    }
}