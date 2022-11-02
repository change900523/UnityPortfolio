using UnityEngine;
using UnityEngine.UI;

public class DamageFont : MonoBehaviour
{
    [SerializeField]
    private Text damageText = null;
    [SerializeField]
    private float activeTime = 1f;
    [SerializeField]
    private float fontSpeed = 200f;

    public bool IsActive { get; private set; }

    private Transform pool = null;
    private float deltaTime = 0f;

    private void Update()
    {
        deltaTime += Time.deltaTime;
        if (deltaTime > activeTime)
        {
            IsActive = false;
            gameObject.SetActive(false);
            transform.SetParent(pool);
        }
        else
        {
            damageText.transform.Translate(new Vector3(0f, fontSpeed, 0f) * Time.deltaTime);
        }
    }

    public void Initialize(Transform inOwner)
    {
        pool = inOwner;
    }

    public void ShowDamage(float inDamage, Vector3 targetPosition, EFontType fontType)
    {
        transform.SetParent(null);
        int damage = Mathf.FloorToInt(inDamage);
        damageText.text = damage.ToString();
        deltaTime = 0f;
        IsActive = true;

        float randomX = Random.Range(-0.5f, 0.5f);
        float randomY = Random.Range(-0.5f, 0.5f);
        targetPosition = new Vector3(targetPosition.x + randomX, targetPosition.y + randomY, targetPosition.z);
        damageText.transform.position = Camera.main.WorldToScreenPoint(targetPosition);

        damageText.color = fontType == EFontType.Player ? Color.blue : Color.red;

        gameObject.SetActive(true);
    }
}
