using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using UnityEngine.UI; // Quan trọng để làm việc với Image UI

public class PickHeroManager : MonoBehaviour
{
    private TextMeshProUGUI timerText;
    private RectTransform rightBar;
    private RectTransform footer;
    private float timeLeft = 60f;
    private Image background;
    private Dictionary<string, Sprite> heroBackgrounds;

    private bool wasPickHero = false;
    private string lastHeroName = "";

    //hien thi anh tuong duoc chon
    private TextMeshProUGUI heroName;
    private Image heroImg;
    private Dictionary<string, Sprite> heroImgs;

    // hien thi image supplymentary
    private Image supplymentaryImg;
    private Image supplymentaryImg2;
    private Dictionary<string, Sprite> supplymentaryImgs;

    void Start()
    {
        timerText = GameObject.Find("Timer").GetComponent<TextMeshProUGUI>();
        rightBar = GameObject.Find("RightBarPickHero").GetComponent<RectTransform>();
        footer = GameObject.Find("FooterPickHero").GetComponent<RectTransform>();
        background = GameObject.Find("BackgroundPickHero").GetComponent<Image>();
        heroImg = GameObject.Find("Heroimg").GetComponent<Image>();
        heroName = GameObject.Find("HeroName").GetComponent<TextMeshProUGUI>();
        supplymentaryImg = GameObject.Find("SupplymentaryImg").GetComponent<Image>();
        supplymentaryImg2 = GameObject.Find("SupplymentaryImg2").GetComponent<Image>();

        if (UIManager.Instance.isPickHeroScreen)
        {
            InitBackground();
            InitHeroImage();
            InitSupplymentaryImg();
            DeactivatePanel();
            StartCoroutine(Timer());
        }
        lastHeroName = UIManager.Instance.nameHero;
    }

    void Update()
    {
        if (UIManager.Instance.isPickHero != wasPickHero)
        {
            if (UIManager.Instance.isPickHero)
            {
                ActivePanel();
            }
            else
            {
                DeactivatePanel();
            }
            wasPickHero = UIManager.Instance.isPickHero;
        }

        if (UIManager.Instance.nameHero != lastHeroName)
        {
            ChangeBackground(UIManager.Instance.nameHero);
            ChangeHeroImage(UIManager.Instance.nameHero);
            lastHeroName = UIManager.Instance.nameHero;
            heroName.text = lastHeroName;
        }
        if (UIManager.Instance.extraSkillName != "")
        {
            ChangeSupplymentaryImg(UIManager.Instance.extraSkillName);
        }
    }

    private void InitBackground()
    {
        heroBackgrounds = new Dictionary<string, Sprite>
        {
            { "TelAnas", Resources.Load<Sprite>("UI/heroBackground/BackgroundHerro/TelAnas") },
            { "Nakroth", Resources.Load<Sprite>("UI/heroBackground/BackgroundHerro/Nakroth") },
            { "Omen", Resources.Load<Sprite>("UI/heroBackground/BackgroundHerro/Omen") },
            { "Toro", Resources.Load<Sprite>("UI/heroBackground/BackgroundHerro/Toro") },
            { "Violet", Resources.Load<Sprite>("UI/heroBackground/BackgroundHerro/Violet") },
            { "Zill", Resources.Load<Sprite>("UI/heroBackground/BackgroundHerro/Zill") },
            { "Volkath", Resources.Load<Sprite>("UI/heroBackground/BackgroundHerro/Volkath") },
            { "Ryoma", Resources.Load<Sprite>("UI/heroBackground/BackgroundHerro/Ryoma") },
            { "Valhein", Resources.Load<Sprite>("UI/heroBackground/BackgroundHerro/Valhein") },
            { "Veres", Resources.Load<Sprite>("UI/heroBackground/BackgroundHerro/Veres") }
        };
    }

    private void ChangeBackground(string heroName)
    {
        if (heroBackgrounds.ContainsKey(heroName))
        {
            background.sprite = heroBackgrounds[heroName];
        }
        else
        {
            Debug.LogError("Không tìm thấy background cho tướng: " + heroName);
        }
    }

    private void InitHeroImage()
    {
        heroImgs = new Dictionary<string, Sprite>
        {
            { "TelAnas", Resources.Load<Sprite>("UI/heroBackground/TelAnas") },
            { "Nakroth", Resources.Load<Sprite>("UI/heroBackground/Nakroth") },
            { "Omen", Resources.Load<Sprite>("UI/heroBackground/Omen") },
            { "Toro", Resources.Load<Sprite>("UI/heroBackground/Toro") },
            { "Violet", Resources.Load<Sprite>("UI/heroBackground/Violet") },
            { "Zill", Resources.Load<Sprite>("UI/heroBackground/Zill") },
            { "Volkath", Resources.Load<Sprite>("UI/heroBackground/Volkath") },
            { "Ryoma", Resources.Load<Sprite>("UI/heroBackground/Ryoma") },
            { "Valhein", Resources.Load<Sprite>("UI/heroBackground/Valhein") },
            { "Veres", Resources.Load<Sprite>("UI/heroBackground/Veres") }
        };
    }

    private void ChangeHeroImage(string heroName)
    {
        if (heroImgs.ContainsKey(heroName))
        {
            heroImg.sprite = heroImgs[heroName];
        }
        else
        {
            Debug.LogError("Không tìm thấy background cho tướng: " + heroName);
        }
    }

    private void InitSupplymentaryImg()
    {
        supplymentaryImgs = new Dictionary<string, Sprite>
        {
            {"Execute",Resources.Load<Sprite>("UI/Supplymentary/Execute")},
            {"Flicker",Resources.Load<Sprite>("UI/Supplymentary/Flicker")},
            {"Purge",Resources.Load<Sprite>("UI/Supplymentary/Purge")},
            {"Roar",Resources.Load<Sprite>("UI/Supplymentary/Roar")},
            {"Stun",Resources.Load<Sprite>("UI/Supplymentary/Stun")}
        };
    }
    private void ChangeSupplymentaryImg(string supplymentaryName)
    {
        if (supplymentaryImgs.ContainsKey(supplymentaryName))
        {
            supplymentaryImg.sprite = supplymentaryImgs[supplymentaryName];
            supplymentaryImg2.sprite = supplymentaryImgs[supplymentaryName];
        }
        else
        {
            Debug.LogError("Không tìm thấy background cho tướng: " + heroName);
        }
    }


    private void ActivePanel()
    {
        rightBar.gameObject.SetActive(true);
        footer.gameObject.SetActive(true);
    }

    private void DeactivatePanel()
    {
        rightBar.gameObject.SetActive(false);
        footer.gameObject.SetActive(false);
    }

    private IEnumerator Timer()
    {
        while (timeLeft > 0)
        {
            timeLeft -= Time.deltaTime;
            timerText.text = Mathf.Ceil(timeLeft).ToString(); // Hiển thị số giây còn lại

            yield return null;
        }

        timerText.text = "";
        OnTimerEnd();
    }

    private void OnTimerEnd()
    {
        // Khi thời gian đếm ngược kết thúc, chuyển sang màn chơi 1v1
        if (!UIManager.Instance.isPickHero)
        {
            UIManager.Instance.GetNameHero("Valhein");
            SceneManager.LoadScene("1vs1");
            UIManager.Instance.InitHero();
            UIManager.Instance.ResetBool();
        }
        if (UIManager.Instance.isPickHero)
        {
            SceneManager.LoadScene("1vs1");
            UIManager.Instance.InitHero();
            UIManager.Instance.ResetBool();
        }
    }
}
