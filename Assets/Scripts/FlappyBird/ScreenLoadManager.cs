using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScreenLoadManager : MonoBehaviour
{
    private Button playButton;
    private Slider loader;
    private void Awake()
    {
        InitLize();
        ChangeStatePlayButton(false);
    }
    private void Start()
    {
        StartCoroutine(FakeDataSlider());
    }

    private void InitLize()
    {
        playButton = transform.Find("PlayButton").GetComponent<Button>();
        loader = transform.Find("Loader").GetComponent<Slider>();
    }

    private IEnumerator FakeDataSlider()
    {
        while (loader.value < 1)
        {
            loader.value += Time.deltaTime / 2;
            yield return null;
        }

        ChangeStatePlayButton(true);
        ChangeStateSlider(false);
    }

    private void ChangeStatePlayButton(bool state)
    {
        playButton.gameObject.SetActive(state);
    }
    private void ChangeStateSlider(bool state)
    {
        loader.gameObject.SetActive(state);
    }

}
