using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Collider))]
public class Nudger : MonoBehaviour
{
    [Header("Nudge on land")]
    public float minNudgeForce = 0.5f;
    public float maxNudgeForce = 2.0f;


    private Rigidbody rb;
    private Sphere sphere;

    private bool landed = false;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        sphere = GetComponent<Sphere>();

        sphere.OnLanded.AddListener(OnLanded);
    }

    private void OnLanded()
    {
        Vector2 dir2d = Random.insideUnitCircle;
        if (dir2d.sqrMagnitude < 0.001f) dir2d = new Vector2(1f, 0f);
        dir2d.Normalize();
        Vector3 nudge = new Vector3(dir2d.x, 0f, dir2d.y);
        float force = Random.Range(minNudgeForce, maxNudgeForce);
        rb.AddForce(nudge * force, ForceMode.Impulse);
    }
}
