using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraKnightController : MonoBehaviour
{
    private Transform knightTransform; // Tham chiếu đến Knight
    public float followSpeed = 0.125f; // Tốc độ camera di chuyển
    public Vector3 offset; // Offset để giữ khoảng cách với Knight
    private Vector3 velocity = Vector3.zero; // Biến lưu vận tốc cho SmoothDamp

    private void OnEnable()
    {
        KnightEventManager.OnKnightEnable += HandleKnightOnEnable;
    }

    private void Start()
    {
        // Khởi tạo giá trị offset nếu cần
        offset = new Vector3(0, 2, -10); // Ví dụ: khoảng cách mặc định từ camera đến Knight
    }

    private void Update()
    {
        if (knightTransform != null)
        {
            FollowKnightSmoothly();
        }
    }

    private void FollowKnightSmoothly()
    {
        // Tính toán vị trí đích (target position)
        Vector3 targetPosition = knightTransform.position + offset;

        // Dùng SmoothDamp để di chuyển mượt mà
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, followSpeed);
    }

    private void HandleKnightOnEnable(Transform knight)
    {
        knightTransform = knight; // Gán tham chiếu khi Knight xuất hiện
    }

    private void OnDisable()
    {
        KnightEventManager.OnKnightEnable -= HandleKnightOnEnable;
    }
}
