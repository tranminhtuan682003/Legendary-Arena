using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosiveEffect : MonoBehaviour
{
    private LineRenderer lineRenderer;

    void Start()
    {
        // Thêm LineRenderer vào GameObject
        lineRenderer = gameObject.AddComponent<LineRenderer>();

        // Cài đặt số lượng điểm cho mũi tên
        lineRenderer.positionCount = 8;

        // Tạo một gradient màu từ đỏ sang vàng
        Gradient gradient = new Gradient();
        gradient.SetKeys(
            new GradientColorKey[] { new GradientColorKey(Color.red, 0.0f), new GradientColorKey(Color.yellow, 1.0f) },
            new GradientAlphaKey[] { new GradientAlphaKey(1.0f, 0.0f), new GradientAlphaKey(1.0f, 1.0f) }
        );
        lineRenderer.colorGradient = gradient;

        // Thay đổi độ rộng của mũi tên: bắt đầu mỏng, sau đó dày dần ở phần đuôi
        AnimationCurve widthCurve = new AnimationCurve();
        widthCurve.AddKey(0.0f, 0.05f); // Đầu mũi tên
        widthCurve.AddKey(0.9f, 0.1f);  // Thân mũi tên dày hơn
        widthCurve.AddKey(1.0f, 0.05f); // Cuối mũi tên
        lineRenderer.widthCurve = widthCurve;

        // Cài đặt Material cho mũi tên (thay thế bằng Material của bạn)
        lineRenderer.material = new Material(Shader.Find("Sprites/Default"));

        // Sử dụng hệ tọa độ cục bộ để mũi tên xoay theo object
        lineRenderer.useWorldSpace = false;
    }

    void Update()
    {
        // Cập nhật vị trí cho LineRenderer sử dụng hệ tọa độ cục bộ
        lineRenderer.SetPosition(0, new Vector3(-0.25f, 0, 0));
        lineRenderer.SetPosition(1, new Vector3(0.25f, 0, 0));
        lineRenderer.SetPosition(2, new Vector3(0.25f, 0, 1.5f));
        lineRenderer.SetPosition(3, new Vector3(0.35f, 0, 1.5f));
        lineRenderer.SetPosition(4, new Vector3(0, 0, 2f)); // Mũi tên nhọn
        lineRenderer.SetPosition(5, new Vector3(-0.35f, 0, 1.5f));
        lineRenderer.SetPosition(6, new Vector3(-0.25f, 0, 1.5f));
        lineRenderer.SetPosition(7, new Vector3(-0.25f, 0, 0));

        // Thêm logic cho mũi tên di chuyển hoặc xoay nếu cần
    }
}
