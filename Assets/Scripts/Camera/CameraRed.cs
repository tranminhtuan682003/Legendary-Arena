using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRed : MonoBehaviour
{
    private HeroBase hero;
    private Vector3 offset = new Vector3(0, 13, 13);
    private bool isHeroAssigned = false;

    void Update()
    {
        if (isHeroAssigned)
        {
            FollowTarget();
        }
    }

    private void OnEnable()
    {
        HeroEventManager.OnHeroCreated += OnHeroCreated;
    }

    private void OnDisable()
    {
        HeroEventManager.OnHeroCreated -= OnHeroCreated;
    }

    private void OnHeroCreated(HeroBase hero)
    {
        if (hero.GetTeam() == Team.Red)
        {
            this.hero = hero;
            isHeroAssigned = true;
        }
        else
        {
            gameObject.SetActive(false);
        }
    }

    private void FollowTarget()
    {
        if (UIManager.Instance.isDragCamera) return;
        transform.position = hero.transform.position + offset;
    }
}
