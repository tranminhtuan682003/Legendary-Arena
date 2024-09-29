using UnityEngine;

public class AttackArea : MonoBehaviour
{
    private LineRenderer lineRenderer;
    public int segments = 50;
    public float radius;
    public Material lineMaterial;

    // Khởi tạo LineRenderer trong phương thức Awake
    void Awake()
    {
        if (lineRenderer == null)
        {
            lineRenderer = gameObject.AddComponent<LineRenderer>();
            lineRenderer.positionCount = segments + 1;
            lineRenderer.useWorldSpace = false;
            lineRenderer.loop = true;
            lineRenderer.startWidth = 0.05f;
            lineRenderer.endWidth = 0.05f;
            lineRenderer.material = lineMaterial;
            lineRenderer.enabled = false;
        }
    }

    // Vẽ hình tròn bằng LineRenderer
    public void DrawCircle()
    {
        if (lineRenderer == null)
        {
            Debug.LogError("LineRenderer chưa được khởi tạo!");
            return;
        }

        float angle = 0f;

        for (int i = 0; i <= segments; i++)
        {
            float x = Mathf.Sin(Mathf.Deg2Rad * angle) * radius;
            float z = Mathf.Cos(Mathf.Deg2Rad * angle) * radius;
            lineRenderer.SetPosition(i, new Vector3(x, 0, z));
            angle += (360f / segments);
        }
    }

    // Cập nhật bán kính hình tròn
    public void SetRadius(float newRadius)
    {
        if (lineRenderer == null)
        {
            Awake(); // Đảm bảo LineRenderer đã được khởi tạo
        }
        radius = newRadius;
        DrawCircle();
    }

    // Kích hoạt LineRenderer
    public void EnableLine()
    {
        if (lineRenderer != null)
        {
            lineRenderer.enabled = true;
        }
    }

    // Vô hiệu hóa LineRenderer
    public void DisableLine()
    {
        if (lineRenderer != null)
        {
            lineRenderer.enabled = false;
        }
    }
}
