using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : MonoBehaviour
{
    private LineRenderer lineRenderer;

    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.positionCount = 9;
        lineRenderer.widthMultiplier = 0.2f; // Điều chỉnh độ dày của mũi tên

        // Vẽ mũi tên
        Vector3[] positions = new Vector3[]
        {
            new Vector3(transform.position.x, transform.position.y, transform.position.z), // Điểm gốc
            new Vector3(transform.position.x + 1, transform.position.y, transform.position.z), // Đỉnh mũi tên
            new Vector3(transform.position.x + 1, transform.position.y, transform.position.z + 4), // Đỉnh mũi tên
            new Vector3(transform.position.x + 1.5f, transform.position.y, transform.position.z + 4), // Cạnh phải của mũi tên
            new Vector3(transform.position.x, transform.position.y, transform.position.z + 6), // Đỉnh mũi tên, phần đầu
            new Vector3(transform.position.x - 1.5f, transform.position.y, transform.position.z + 4), // Cạnh trái của mũi tên
            new Vector3(transform.position.x - 1, transform.position.y, transform.position.z + 4), // Đỉnh mũi tên bên trái
            new Vector3(transform.position.x - 1, transform.position.y, transform.position.z), // Cạnh trái của mũi tên
            new Vector3(transform.position.x, transform.position.y, transform.position.z) // Kết thúc tại điểm gốc
        };

        lineRenderer.SetPositions(positions);
    }
}
