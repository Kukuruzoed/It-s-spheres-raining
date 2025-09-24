using UnityEngine;

public class Glower : MonoBehaviour
{
    private Renderer rend;
    private Material mat;
    private Sphere sphere;

    [Header("Glow Settings")]
    public Color glowColor = Color.blue;
    public float glowPeriod = 1f; 
    
    private float phaseOffset;

    void Start()
    {
        rend = GetComponent<Renderer>();
        sphere = GetComponent<Sphere>();

        mat = rend.material; 
        phaseOffset = Random.Range(0f, glowPeriod);

        sphere.OnLanded.AddListener(OnLanded);
    }

    private void OnLanded()
    {
        mat.EnableKeyword("_EMISSION");
    }

    void Update()
    {
        float t = (Mathf.Sin((Time.time + phaseOffset) * Mathf.PI * 2f / glowPeriod) + 1f) * 0.5f;

        Color current = Color.Lerp(Color.black, glowColor, t) * mat.color.a;

        mat.SetColor("_EmissionColor", current);
    }
}
