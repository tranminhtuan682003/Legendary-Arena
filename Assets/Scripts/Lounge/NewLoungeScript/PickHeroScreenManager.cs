using UnityEngine;
using UnityEngine.UI;
using Zenject;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class PickHeroScreenManager : MonoBehaviour
{
    [Inject] private UILoungeManager uILoungeManager;
    [Inject] private LoungeManager loungeManager;

    private Dictionary<TypeSupplymentary, Button> supplymentaryMap;
    private Dictionary<TypeHero, Button> heroButtonMap;
    private Button playGameButton;
    private Image heroImage;
    private Button supButton;
    private Image supImage;
    private GameObject tableSup;
    private bool tableSupIsOn;

    private void Awake()
    {
        InitiaLize();
    }

    private void OnEnable()
    {
        ChangeStateSupTable(false);
    }

    private void InitiaLize()
    {
        supplymentaryMap = new Dictionary<TypeSupplymentary, Button>();
        heroButtonMap = new Dictionary<TypeHero, Button>();
        FindHeroButtons();
        FindPlayGameButton();
        FindSupButtons();
    }
    #region FindButton
    private void FindHeroButtons()
    {
        heroButtonMap.Add(TypeHero.Nakroth, FindButtonByName("Nakroth"));
        heroButtonMap.Add(TypeHero.Telanas, FindButtonByName("Telanas"));
        heroButtonMap.Add(TypeHero.Valhein, FindButtonByName("Valhein"));

        heroButtonMap.Add(TypeHero.Violet, FindButtonByName("Violet"));
        heroButtonMap.Add(TypeHero.Omen, FindButtonByName("Omen"));
        heroButtonMap.Add(TypeHero.Veres, FindButtonByName("Veres"));

        heroButtonMap.Add(TypeHero.Ryoma, FindButtonByName("Ryoma"));
        heroButtonMap.Add(TypeHero.Zill, FindButtonByName("Zill"));
        heroButtonMap.Add(TypeHero.Volkath, FindButtonByName("Volkath"));

        heroButtonMap.Add(TypeHero.Toro, FindButtonByName("Toro"));

        heroImage = transform.Find("RightBarPickHero/Team/Members/Member1/Detail/Icon/IconHero/Intrinsic/Heroimg")?.GetComponent<Image>();

        foreach (var hero in heroButtonMap)
        {
            hero.Value.onClick.AddListener(() => HandleHeroButtonClick(hero.Key));
        }
    }

    private void FindPlayGameButton()
    {
        playGameButton = transform.Find("FooterPickHero/PlayGame/PlayButton")?.GetComponent<Button>();
        playGameButton.onClick.AddListener(() => HandlePlayButtonClick());
    }

    private Button FindButtonByName(string buttonName)
    {
        Button button = transform.Find($"LeftBar/Heros/Area/Body/ListHero/Scroll View/Viewport/Content/{buttonName}")?.GetComponent<Button>();
        return button;
    }
    #endregion

    #region FindSupplymentary
    private void FindSupButtons()
    {
        supplymentaryMap.Add(TypeSupplymentary.BocPha, FindSupByName("BocPha", "BocPha"));
        supplymentaryMap.Add(TypeSupplymentary.ThanhTay, FindSupByName("ThanhTay", "ThanhTay"));
        supplymentaryMap.Add(TypeSupplymentary.SuyNhuoc, FindSupByName("SuyNhuoc", "SuyNhuoc"));
        supplymentaryMap.Add(TypeSupplymentary.GamThet, FindSupByName("GamThet", "GamThet"));
        supplymentaryMap.Add(TypeSupplymentary.TocBien, FindSupByName("TocBien", "TocBien"));

        tableSup = transform.Find("FooterPickHero/SupplymentaryTable").gameObject;
        supImage = transform.Find("RightBarPickHero/Team/Members/Member1/Detail/Icon/Supplymentary/icon/SupplymentaryImg2")?.GetComponent<Image>();
        supButton = transform.Find("FooterPickHero/Item/Supplymentary/Sup/SupplymentaryImg")?.GetComponent<Button>();

        supButton.onClick.AddListener(() => HandleSupButtonClick());

        foreach (var sup in supplymentaryMap)
        {
            sup.Value.onClick.AddListener(() => HandleSupplymentaryButtonClick(sup.Key));
        }

    }
    private Button FindSupByName(string parrent, string buttonName)
    {
        Button button = transform.Find($"FooterPickHero/SupplymentaryTable/Supplymentary/{parrent}/Sup/{buttonName}")?.GetComponent<Button>();
        return button;
    }
    #endregion

    #region HandleButton

    private void HandleSupplymentaryButtonClick(TypeSupplymentary typeSupplymentary)
    {
        string selectedHero = typeSupplymentary.ToString();
        loungeManager.typeSupplymentary = typeSupplymentary;
        LoadImageSupplymentary(selectedHero);
        ChangeStateSupTable(false);
    }

    private void HandleSupButtonClick()
    {
        if (tableSupIsOn)
        {
            ChangeStateSupTable(false);
        }
        else
        {
            ChangeStateSupTable(true);
        }
    }

    private void HandleHeroButtonClick(TypeHero typeHero)
    {
        string selectedHero = typeHero.ToString();
        loungeManager.typeHero = typeHero;
        LoadImageHero(selectedHero);
    }

    private void ChangeStateSupTable(bool state)
    {
        tableSup.SetActive(state);
        tableSupIsOn = state;
    }


    private void LoadImageSupplymentary(string sup)
    {
        supButton.image.sprite = Resources.Load<Sprite>($"UI/Supplymentary/{sup}");
        supImage.sprite = Resources.Load<Sprite>($"UI/Supplymentary/{sup}");
    }
    private void LoadImageHero(string heroName)
    {
        heroImage.sprite = Resources.Load<Sprite>($"UI/heroBackground/{heroName}");
    }

    private void HandlePlayButtonClick()
    {
        SceneManager.LoadScene("OneVsOne");
    }
    #endregion
}
