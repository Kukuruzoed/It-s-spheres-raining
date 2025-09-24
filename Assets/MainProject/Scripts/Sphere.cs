using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Collider))]
public class Sphere : MonoBehaviour
{
    public UnityEvent OnLanded;
    public bool IsLanded { get; set; } = false;

    void OnCollisionEnter(Collision collision)
    {
        if (!IsLanded && collision.gameObject.CompareTag("Ground"))
        {
            OnLanded?.Invoke();
            IsLanded = true;
        }
    }
}
