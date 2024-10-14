using UnityEngine;
using UnityEngine.UI;

public class PickHeroButton : MonoBehaviour
{
    private Button button;
    private string nameHero;

    private void Start()
    {
        button = GetComponent<Button>();
        nameHero = button.name;

        button.onClick.AddListener(() => { HandleClick(); });
    }

    private void HandleClick()
    {
        foreach (Transform sibling in transform.parent)
        {
            Button siblingButton = sibling.GetComponent<Button>();
            if (siblingButton != null)
            {
                siblingButton.image.color = Color.white;
            }
        }
        button.image.color = Color.grey;
        UIManager.Instance.ShowHeroBackground(nameHero);
        UIManager.Instance.GetNameHero(nameHero);
    }
}
