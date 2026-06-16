using System.Collections.Generic;
using UnityEngine;

public class RopeManager : MonoBehaviour
{
    public Rigidbody2D firefly1;
    public Rigidbody2D firefly2;
    public GameObject segmentPrefab;
    
    public int segmentCount = 15; 
    public LineRenderer ropeVisualizer;
    public bool isRopeActive = true; 

    private List<Rigidbody2D> segments = new List<Rigidbody2D>();
    private DistanceJoint2D endJoint;
    private float totalRopeLength;
    private float segmentLength;

    void Start()
    {
        // İpe %10 esneme payı (slack) verelim ki dümdüz gergin bir metal çubuk gibi kırılmasın
        totalRopeLength = Vector2.Distance(firefly1.position, firefly2.position) * 1.1f; 
        
        segmentLength = totalRopeLength / (segmentCount + 1);
        
        CreateRopeOnce();
    }

    void Update()
    {
        // X tuşuna basıldığında aç/kapat tetikleyicisi
        if (Input.GetKeyDown(KeyCode.X))
        {
            if (isRopeActive) HideRope();
            else ShowRope();
        }
    }

    void CreateRopeOnce()
    {
        Rigidbody2D previousBody = firefly1;

        for (int i = 0; i < segmentCount; i++)
        {
            GameObject seg = Instantiate(segmentPrefab, Vector2.zero, Quaternion.identity);
            Rigidbody2D segRb = seg.GetComponent<Rigidbody2D>();
            segments.Add(segRb);
            
            Joint2D existingJoint = seg.GetComponent<Joint2D>();
            if (existingJoint != null) Destroy(existingJoint);

            DistanceJoint2D joint = seg.AddComponent<DistanceJoint2D>();
            joint.connectedBody = previousBody;
            joint.autoConfigureConnectedAnchor = false;
            joint.anchor = Vector2.zero;
            joint.connectedAnchor = Vector2.zero;
            
            // Unity'nin mesafeyi kendi kendine değiştirmesini YASAKLA
            joint.autoConfigureDistance = false; 
            joint.distance = segmentLength;      
            
            joint.maxDistanceOnly = true; // Sopa olmasın, ip gibi esnesin

            previousBody = segRb; 
        }

        // Bitiş eklemi
        endJoint = firefly2.gameObject.AddComponent<DistanceJoint2D>();
        endJoint.connectedBody = previousBody;
        endJoint.autoConfigureConnectedAnchor = false;
        endJoint.anchor = Vector2.zero;
        endJoint.connectedAnchor = Vector2.zero;
        
        endJoint.autoConfigureDistance = false; // YASAKLA
        endJoint.distance = segmentLength;      
        endJoint.maxDistanceOnly = true; 

        ropeVisualizer.positionCount = segments.Count + 2;
        RepositionSegments();
    }

    void HideRope()
    {
        isRopeActive = false;
        ropeVisualizer.enabled = false;
        endJoint.enabled = false; // Ateşböceklerinin birbirini çekmesini engellemek için son bağı kopar

        // Objeleri ASLA SİLME, sadece uyut (FPS drop yaşatmaz, jilet gibi kapanır)
        foreach (Rigidbody2D rb in segments)
        {
            rb.gameObject.SetActive(false);
        }
    }

    void ShowRope()
    {
        isRopeActive = true;
        
        // Uyanmadan önce böceklerin o anki konumlarına göre ipi düzgünce diz
        RepositionSegments();

        // Aynı ipi uykudan uyandır
        foreach (Rigidbody2D rb in segments)
        {
            rb.gameObject.SetActive(true);
            rb.linearVelocity = Vector2.zero; // Uyandığında sapıtmaması için eski fiziksel hızını sıfırla
        }

        endJoint.enabled = true; // Bağı geri tak
        ropeVisualizer.enabled = true;
    }

    void RepositionSegments()
    {
        Vector2 startPos = firefly1.position;
        Vector2 endPos = firefly2.position;
        
        // KİLİT NOKTA: Böcekler ipin boyundan daha uzaktaysa, parçaları zorla esnetme!
        float currentDistance = Vector2.Distance(startPos, endPos);
        if (currentDistance > totalRopeLength)
        {
            Vector2 direction = (endPos - startPos).normalized;
            endPos = startPos + (direction * totalRopeLength);
        }
        
        // Böcekler yakınken ipin büzüşmeyip aşağı doğru doğal sarkmasını (U kavisi) hesapla
        float distanceRatio = currentDistance / totalRopeLength;
        float maxSag = (1f - Mathf.Clamp01(distanceRatio)) * (totalRopeLength * 0.5f);
        
        for (int i = 0; i < segments.Count; i++)
        {
            float t = (i + 1) / (float)(segments.Count + 1);
            Vector2 spawnPos = Vector2.Lerp(startPos, endPos, t);
            
            // Sarkmayı uygula ki dümdüz ve kısa görünmesin
            float sag = Mathf.Sin(t * Mathf.PI) * maxSag;
            spawnPos.y -= sag;
            
            segments[i].transform.position = spawnPos;
        }
    }

    void LateUpdate()
    {
        // Sadece ip aktifse görseli çiz
        if (isRopeActive && ropeVisualizer != null && segments.Count > 0)
        {
            ropeVisualizer.SetPosition(0, firefly1.position);
            for (int i = 0; i < segments.Count; i++)
            {
                if (segments[i] != null) 
                    ropeVisualizer.SetPosition(i + 1, segments[i].position);
            }
            ropeVisualizer.SetPosition(segments.Count + 1, firefly2.position);
        }
    }
}