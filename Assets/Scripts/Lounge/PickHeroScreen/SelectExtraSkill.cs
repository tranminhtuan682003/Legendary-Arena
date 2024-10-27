using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectExtraSkill : MonoBehaviour
{
    private Button button;
    private RectTransform supplymentaryTable;
    void Start()
    {
        button = GetComponent<Button>();
        supplymentaryTable = GameObject.Find("SupplymentaryTable").GetComponent<RectTransform>();
        button.onClick.AddListener(() => { HandleClick(); });
    }
    private void HandleClick()
    {
        UIManager.Instance.extraSkillName = button.name;
        supplymentaryTable.gameObject.SetActive(false);
    }
}
