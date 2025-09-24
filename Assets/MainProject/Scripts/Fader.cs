using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Renderer))]
[RequireComponent(typeof(Sphere))]
public class Fader : MonoBehaviour
{
    [Header("Nudge on land")]
    public float minNudgeForce = 0.5f;
    public float maxNudgeForce = 2.0f;


    [Header("Fade settings")]
    public float fadeOutDuration = 5.0f;

    [Header("Misc")]
    public float collisionFadeDelay = 0.05f;


    private Sphere sphere;
    private Renderer renderer;
    private Material matInstance;


    private bool hittedGound = false;
    private bool isFading = false;
    private float spawnY;
    private float groundY;

    void Start()
    {
        renderer = GetComponent<Renderer>();
        sphere = GetComponent<Sphere>();
        sphere.OnLanded.AddListener(OnLanded);

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
    }

    void Update()
    {
        if (!sphere.IsLanded)
        {
            float denom = spawnY - groundY;
            float alpha = denom != 0f ? Mathf.Clamp01((spawnY - transform.position.y) / denom) : 1f;
			alpha = alpha*alpha*alpha;
            SetAlpha(alpha);
        }
        else if (!isFading)
        {
            StartFadeOut();
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
    void OnLanded()
    {
        if (isFading) return;
        hittedGound = true;
        SetAlpha(1f);
    }


    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Sphere") && hittedGound)
        {
            if (!isFading)
            {
                StartFadeOut();
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
