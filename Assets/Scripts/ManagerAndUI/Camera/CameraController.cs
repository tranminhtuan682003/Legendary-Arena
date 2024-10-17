using UnityEngine;

public class CameraController : MonoBehaviour
{
    private HeroBase hero;
    public Vector3 offset = new Vector3(0, 6f, -6f);
    public Vector3 rotationAngles = new Vector3(35, 0, 0);
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
        this.hero = hero;
        isHeroAssigned = true;
    }

    private void FollowTarget()
    {
        if (hero == null) return;
        if (UIManager.Instance.isDragCamera) return;
        transform.position = hero.transform.position + offset;
        transform.rotation = Quaternion.Euler(rotationAngles);
    }
}
