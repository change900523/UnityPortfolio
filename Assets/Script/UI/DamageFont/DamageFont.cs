using UnityEngine;
using UnityEngine.UI;

public class DamageFont : MonoBehaviour
{
    [SerializeField]
    private Text damageText = null;
    [SerializeField]
    private float activeTime = 1f;

    public bool IsActive { get; private set; }

    private Transform pool = null;
    private Transform owner = null;
    private float deltaTime = 0f;

    private void Update()
    {
        deltaTime += Time.deltaTime;
        if (deltaTime > activeTime)
        {
            IsActive = false;
            gameObject.SetActive(false);
            transform.parent = pool;
        }
        else
        {
            damageText.transform.Translate(new Vector3(0f, 5f, 0f) * Time.deltaTime);
        }
    }

    public void Initialize(Transform inOwner)
    {
        pool = inOwner;
    }

    public void ShowDamage(float inDamage, Transform inTarget)
    {
        owner = inTarget;
        transform.parent = null;
        int damage = Mathf.FloorToInt(inDamage);
        damageText.text = damage.ToString();
        deltaTime = 0f;
        IsActive = true;
        damageText.transform.position = Camera.main.WorldToScreenPoint(owner.position);
        gameObject.SetActive(true);
    }
}
