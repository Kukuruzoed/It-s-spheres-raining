using UnityEngine;

public class ExternalNudger : MonoBehaviour
{
    public float radius = 2f;
    public float force = 1f;
    public bool isBlackHole = false; // false = отталкивание, true = притяжение

    private void FixedUpdate()
    {
        Collider[] cols = Physics.OverlapSphere(transform.position, radius);
        foreach (var c in cols)
        {
            Rigidbody rb = c.attachedRigidbody;
            if (rb != null && !rb.isKinematic)
            {
                Vector3 dir = rb.position - transform.position;
                float distance = dir.magnitude;

                if (distance > 0f && distance <= radius)
                {
                    // Нормализованное направление
                    Vector3 normalizedDir = dir / distance;

                    // Инверсия направления, если attract = true
                    if (isBlackHole)
                        normalizedDir = -normalizedDir;

                    // Линейный falloff (можно заменить на Pow или 1/distance)
                    float falloff = 1f - (distance / radius);

                    rb.AddForce(normalizedDir * force * falloff, ForceMode.Acceleration);
                }
            }
        }
    }

}
