using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class ReadyScreenManager : MonoBehaviour
{
    [Inject] UILoungeManager uILoungeManager;
    [Inject] LoungeManager loungeManager;
    private GameObject animationTransition;
    private GameObject readyPanel;
    private TextMeshProUGUI timer;
    private Image yourIcon;
    private Button readyButton;

    private float countdownTime = 30f; // Thời gian đếm ngược 30 giây
    private bool isReady = false; // Trạng thái của người chơi (ready hay không)
    private Color originalIconColor; // Màu icon gốc

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
        timer = transform.Find("ReadyPanel/Bar/Timer")?.GetComponent<TextMeshProUGUI>();
        yourIcon = transform.Find("ReadyPanel/Bar/You/Icon")?.GetComponent<Image>();
        originalIconColor = yourIcon.color; // Lưu màu icon ban đầu
        readyButton.onClick.AddListener(() => StartCoroutine(HandleReadyButtonClick()));
    }

    private IEnumerator OnScreenReady()
    {
        yield return new WaitForSeconds(1.5f); // Thực hiện sau 1.5 giây

        // Bắt đầu đếm ngược và thay đổi màu icon
        StartCoroutine(StartCountdown());

        RunAnimationTransition(false);
    }

    private void RunAnimationTransition(bool state)
    {
        animationTransition.SetActive(state);
    }

    private IEnumerator StartCountdown()
    {
        float remainingTime = countdownTime;

        // Đếm ngược thời gian và cập nhật UI
        while (remainingTime > 0)
        {
            timer.text = Mathf.Ceil(remainingTime).ToString(); // Hiển thị thời gian còn lại
            remainingTime -= Time.deltaTime;

            // Nếu người chơi chưa ready, chuyển icon sang màu xám
            if (!isReady)
            {
                yourIcon.color = Color.gray;
            }

            yield return null;
        }

        // Sau khi đếm ngược xong và người chơi chưa nhấn Ready, giữ màu xám cho icon
        if (!isReady)
        {
            yourIcon.color = Color.gray;
        }
    }

    private IEnumerator HandleReadyButtonClick()
    {
        // Khi người chơi nhấn Ready, chuyển lại màu icon gốc
        isReady = true;
        yourIcon.color = originalIconColor;
        yield return new WaitForSeconds(1f);
        uILoungeManager.ShowScreen("PickHero");
    }
}
