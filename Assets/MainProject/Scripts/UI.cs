using UnityEngine;
using UnityEngine.UI;
public class UI : MonoBehaviour
{
    [SerializeField]
    private Spawner spawner;
    [SerializeField]
    private Slider slider;
    [SerializeField]
    private Text label;
    void Start()
    {
        if (spawner == null) return;
        slider.minValue = 0.1f;
        slider.maxValue = 10f;
        slider.value = spawner.spawnRate;
        slider.onValueChanged.AddListener(OnSliderChanged);
        UpdateLabel();
    }
    void OnSliderChanged(float value)
    {
        if (spawner != null) spawner.SetSpawnRate(value);
        UpdateLabel();
    }
    void UpdateLabel()
    {
        if (label != null && spawner != null) label.text =
        $"Spawn rate: {spawner.spawnRate:0.0}/s";
    }
}
