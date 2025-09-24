using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class UI : MonoBehaviour
{
    [SerializeField]
    private Spawner spawner;
    [SerializeField]
    private Slider spawnRateSlider;
    [SerializeField]
    private TextMeshProUGUI spawnRateLabel;
    void Start()
    {
        if (spawner == null) return;
        spawnRateSlider.minValue = 0.1f;
        spawnRateSlider.maxValue = 10f;
        spawnRateSlider.value = spawner.spawnRate;
        spawnRateSlider.onValueChanged.AddListener(OnSliderChanged);
        UpdateLabel();
    }
    void OnSliderChanged(float value)
    {
        if (spawner != null) spawner.SetSpawnRate(value);
        UpdateLabel();
    }
    void UpdateLabel()
    {
        if (spawnRateLabel != null && spawner != null) spawnRateLabel.text =
        $"Spawn rate: {spawner.spawnRate:0.0}/s";
    }
}
