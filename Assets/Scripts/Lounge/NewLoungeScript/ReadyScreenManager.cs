using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class ReadyScreenManager : MonoBehaviour
{
    [Inject] UILoungeManager uILoungeManager;
    [Inject] LoungeManager loungeManager;
    private GameObject animationTransition;
    private GameObject readyPanel;
    private Button readyButton;

    private void Awake()
    {
        InitiaLize();
    }

    private void OnEnable()
    {
        StartCoroutine(OnScreenReady());
    }

    private void InitiaLize()
    {
        animationTransition = transform.Find("AnimationTransition").gameObject;
        readyPanel = transform.Find("ReadyPanel").gameObject;
        readyButton = transform.Find("ReadyPanel/ReadyButton")?.GetComponent<Button>();
        readyButton.onClick.AddListener(() => HandleReadyButtonClick());
    }

    private IEnumerator OnScreenReady()
    {
        yield return new WaitForSeconds(1.5f);
        RunAnimationTransition(false);
    }

    private void RunAnimationTransition(bool state)
    {
        animationTransition.SetActive(state);
    }

    private void HandleReadyButtonClick()
    {
        uILoungeManager.ShowScreen("PickHero");
    }
}
