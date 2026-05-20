using UnityEngine;

public class GlowController : MonoBehaviour
{
    public Transform Firefly1;
    public Transform Firefly2;
    private float maxDistance = 18f;
    private SpriteRenderer spriteRenderer;
    private Vector3 initialScale;
    private float initialAlpha;

    public LineRenderer lineRenderer; 
    public Color safeColor = Color.yellow;
    public Color warningColor = Color.red;
        
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        initialScale = transform.localScale;
        initialAlpha = spriteRenderer.color.a;
    }

    void LateUpdate()
    {
        float distance = Vector2.Distance(Firefly1.position, Firefly2.position);
        float oran = distance / maxDistance;
        float intensity = Mathf.Clamp01(1 - oran);

        Color newColor = spriteRenderer.color;
        newColor.a = initialAlpha * intensity; 
        spriteRenderer.color = newColor;

        transform.localScale = initialScale * intensity; 

        Color currentColor = Color.Lerp(warningColor, safeColor, intensity);
        lineRenderer.startColor = currentColor;
        lineRenderer.endColor = currentColor;

        lineRenderer.material.mainTextureOffset = new Vector2(Time.time * intensity * 2f, 0);
        
        }
}