using UnityEngine;

public class StarMovement : MonoBehaviour
{
    [Header("Alpha Twinkle")]
    public float minAlpha = 0.2f;
    public float maxAlpha = 1f;

    [Header("Animation")]
    public float animationSpeed = 2f;
    public float sizeAmount = 0.1f; // 10% pulse
    public float tiltAmount = 5f;   // ±5 degrees

    private SpriteRenderer sr;
    private Vector3 baseScale;
    private float offset;

    private void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        baseScale = transform.localScale;

        // Randomize each star
        offset = Random.Range(0f, Mathf.PI * 2f);
        animationSpeed = Random.Range(1f, 3f);
        sizeAmount = Random.Range(0.05f, 0.15f);
        tiltAmount = Random.Range(2f, 8f);
    }

    private void Update()
    {
        float t = (Mathf.Sin(Time.time * animationSpeed + offset) + 1f) * 0.5f;

        // Alpha twinkle
        Color c = sr.color;
        c.a = Mathf.Lerp(minAlpha, maxAlpha, t);
        sr.color = c;

        // Size pulse
        float scale = Mathf.Lerp(1f - sizeAmount, 1f + sizeAmount, t);
        transform.localScale = baseScale * scale;

        // Gentle tilt
        float rotation = Mathf.Lerp(-tiltAmount, tiltAmount, t);
        transform.rotation = Quaternion.Euler(0f, 0f, rotation);
    }
}