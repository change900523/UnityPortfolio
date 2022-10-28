using UnityEngine;
using UnityEngine.UI;

public class HPBar : MonoBehaviour
{
    [SerializeField]
    private Slider bar = null;

    private float maxHP = 0f;
    private float hp = 0f;

    private Transform cam;

    private void Start()
    {
        cam = Camera.main.transform;
    }

    private void Update()
    {
        transform.LookAt(transform.position + cam.forward);
    }

    public void Initialize(float inHP)
    {
        maxHP = inHP;
        hp = inHP;
        UpdateBar();
    }

    public void SetHP(float inHP)
    {
        hp = inHP;
        UpdateBar();
    }

    private void UpdateBar() => bar.value = hp / maxHP;
}
