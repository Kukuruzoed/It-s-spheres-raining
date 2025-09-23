using System.Collections;
using UnityEngine;

public class Fader : MonoBehaviour
{
    [Header("Nudge on land")]
    public float minNudgeForce = 0.5f;
    public float maxNudgeForce = 2.0f;


    [Header("Fade settings")]
    public float fadeOutDuration = 5.0f;


    [Header("Start fading distance")]
    public float maxDistanceFromLand = 3.0f;


    [Header("Misc")]
    public float collisionFadeDelay = 0.05f;


    private Renderer renderer;
    private Material matInstance;


    private bool landed = false;
    private bool isFading = false;
    private Vector3 landPoint;
    private float spawnY;
    private float groundY;

    void Start()
    {
        renderer = GetComponent<Renderer>();

        matInstance = renderer.material;

        Color c = matInstance.color;
        c.a = 0f;
        matInstance.color = c;


        spawnY = transform.position.y;

        GameObject ground = GameObject.FindWithTag("Ground");
        if (ground != null)
        {
            groundY = ground.transform.position.y;
        }
        else
        {
            RaycastHit hit;
            Vector3 origin = transform.position + Vector3.up * 0.1f;
            if (Physics.Raycast(origin, Vector3.down, out hit, Mathf.Infinity))
            {
                groundY = hit.point.y;
            }
            else
            {
                groundY = spawnY - 10f;
            }
        }
    }

    void Update()
    {
        if (!landed)
        {
            float denom = spawnY - groundY;
            float alpha = denom != 0f ? Mathf.Clamp01((spawnY - transform.position.y) / denom) : 1f;
            SetAlpha(alpha);
        }
        else if (!isFading)
        {
            float dist = Vector3.Distance(transform.position, landPoint);
            if (dist > maxDistanceFromLand)
            {
                StartFadeOut();
            }
        }
    }

    void SetAlpha(float a)
    {
        if (matInstance != null)
        {
            Color col = matInstance.color;
            col.a = a;
            matInstance.color = col;
        }
    }
    void OnCollisionEnter(Collision collision)
    {
        if (isFading) return;


        if (!landed && collision.gameObject.CompareTag("Ground"))
        {
            landed = true;
            landPoint = collision.contacts[0].point;

            SetAlpha(1f);
        }

        if (collision.gameObject.CompareTag("Sphere") && landed)
        {
            Fader other = collision.gameObject.GetComponent<Fader>();
            if (other != null && !other.isFading)
            {
                other.StartFadeOut();
            }

            StartCoroutine(DelayedFadeOut(collisionFadeDelay));
        }
    }
    IEnumerator DelayedFadeOut(float delay)
    {
        yield return new WaitForSeconds(delay);
        StartFadeOut();
    }


    public void StartFadeOut()
    {
        if (isFading) return;
        StartCoroutine(FadeOutCoroutine());
    }


    IEnumerator FadeOutCoroutine()
    {
        isFading = true;

        float startAlpha = matInstance.color.a;
        float t = 0f;
        while (t < fadeOutDuration)
        {
            t += Time.deltaTime;
            float a = Mathf.Lerp(startAlpha, 0f, t / fadeOutDuration);
            SetAlpha(a);
            yield return null;
        }

        Destroy(gameObject);
    }

}
