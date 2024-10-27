using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExtraSkillTable : MonoBehaviour
{
    private RectTransform supplymentaryTable;
    private Button button;
    void Start()
    {
        button = GetComponent<Button>();
        supplymentaryTable = GameObject.Find("SupplymentaryTable").GetComponent<RectTransform>();
        supplymentaryTable.gameObject.SetActive(false); // Ban đầu tắt bảng
        button.onClick.AddListener(() => { HandleClick(); });
    }

    private void HandleClick()
    {
        supplymentaryTable.gameObject.SetActive(true); // Đặt trạng thái của bảng
    }
}
