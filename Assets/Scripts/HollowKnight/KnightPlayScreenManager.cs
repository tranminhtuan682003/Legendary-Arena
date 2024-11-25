using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class KnightPlayScreenManager : MonoBehaviour
{
    private readonly string[] skillNames = { "Attack", "Skill1", "Skill2", "Skill3", "Heal", "Sup", "Recall" };
    private readonly string[] moveNames = { "Up", "Down", "Right", "Left" };

    private TextMeshProUGUI timer;
    private float elapsedTime = 0f; // Thời gian đã trôi qua
    private bool isGameRunning = false; // Trạng thái của trận đấu
    private TextMeshProUGUI notice;

    private ButtonControlManager buttonKnightManager;
    private UIKnightManager uIKnightManager;
    [Inject] SoundKnightManager soundKnightManager;

    [Inject]
    public void Construct(ButtonControlManager buttonKnightManager, UIKnightManager uIKnightManager)
    {
        this.buttonKnightManager = buttonKnightManager;
        this.uIKnightManager = uIKnightManager;
    }

    private void Awake()
    {
        SetupSkillButtons();
        SetupMoveButtons();
        InitLize();
    }

    private void OnEnable()
    {
        StartTimer();
    }
    private void OnDisable()
    {
        StopTimer();
    }

    private void Start()
    {
        StartCoroutine(NoticeStart());
    }

    private void Update()
    {
        if (isGameRunning)
        {
            UpdateTimer();
        }
    }

    private void InitLize()
    {
        timer = transform.Find("LeaderBoard/Score/Time/Timer").GetComponent<TextMeshProUGUI>();
        notice = transform.Find("Notice").GetComponent<TextMeshProUGUI>();
    }

    // Bắt đầu đếm thời gian
    private void StartTimer()
    {
        elapsedTime = 0f;
        isGameRunning = true;
    }

    // Dừng đếm thời gian
    private void StopTimer()
    {
        isGameRunning = false;
    }

    // Cập nhật thời gian và hiển thị lên giao diện
    private void UpdateTimer()
    {
        elapsedTime += Time.deltaTime; // Cộng thời gian đã trôi qua
        TimeSpan time = TimeSpan.FromSeconds(elapsedTime);

        // Hiển thị theo định dạng mm:ss
        if (timer != null)
        {
            timer.text = string.Format("{0:D2}:{1:D2}", time.Minutes, time.Seconds);
        }
    }

    // Thiết lập các button skill
    private void SetupSkillButtons()
    {
        foreach (string buttonName in skillNames)
        {
            var button = FindButton(buttonName);
            if (button == null)
            {
                continue;
            }

            // Gán script tương ứng dựa trên tên button
            Type actionType = GetSkillActionType(buttonName);
            if (actionType != null)
            {
                var action = button.gameObject.AddComponent(actionType) as SkillKnightBase;
            }
        }
    }

    // Thiết lập các nút di chuyển
    private void SetupMoveButtons()
    {
        foreach (string buttonName in moveNames)
        {
            var button = FindButton(buttonName);
            if (button == null)
            {
                continue;
            }

            // Gán script tương ứng dựa trên tên button
            Type moveType = GetMoveActionType(buttonName);
            if (moveType != null)
            {
                var moveAction = button.gameObject.AddComponent(moveType) as MoveKnightBase;
            }
        }
    }

    // Tìm button theo tên (bao gồm cả button Inactive)
    private Button FindButton(string name)
    {
        var buttons = GetComponentsInChildren<Button>(true);
        foreach (var button in buttons)
        {
            if (button.name == name)
            {
                return button;
            }
        }
        return null;
    }

    // Lấy script hành động tương ứng với tên skill button
    private Type GetSkillActionType(string buttonName)
    {
        return buttonName switch
        {
            "Attack" => typeof(KnightAttack),
            "Skill1" => typeof(KnightSkill1),
            "Skill2" => typeof(KnightSkill2),
            "Skill3" => typeof(KnightSkill3),
            "Heal" => typeof(KnightHeal),
            "Sup" => typeof(KnightSup),
            "Recall" => typeof(KnightRecall),
            _ => null
        };
    }

    // Lấy script hành động tương ứng với tên move button
    private Type GetMoveActionType(string buttonName)
    {
        return buttonName switch
        {
            "Up" => typeof(KnightMoveUp),
            "Down" => typeof(KnightMoveDown),
            "Right" => typeof(KnightMoveRight),
            "Left" => typeof(KnightMoveLeft),
            _ => null
        };
    }

    private void SetNotice(string notice)
    {
        this.notice.text = notice;
    }

    private IEnumerator NoticeStart()
    {
        yield return new WaitForSeconds(5f);
        soundKnightManager.PlayMusicGetReady();
        SetNotice("Get ready! Minions will be deployed in ten seconds");
        yield return new WaitForSeconds(3f);
        SetNotice("");
    }
}
