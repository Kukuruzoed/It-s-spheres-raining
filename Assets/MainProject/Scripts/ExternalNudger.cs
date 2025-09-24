using UnityEngine;

public class ExternalNudger : MonoBehaviour
{
    public float radius = 2f;
    public float force = 1f;
    public bool isBlackHole = false;

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
                    Vector3 normalizedDir = dir / distance;

                    if (isBlackHole)
                        normalizedDir = -normalizedDir;

                    float falloff = 1f - (distance / radius);

                    rb.AddForce(normalizedDir * force * falloff, ForceMode.Acceleration);
                }
            }
        }
    }

}
