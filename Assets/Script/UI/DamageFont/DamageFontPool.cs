using System.Collections.Generic;
using UnityEngine;

public class DamageFontPool : MonoSingleton<DamageFontPool>
{
    [SerializeField]
    private GameObject fontObj = null;

    private List<DamageFont> fontPool = new List<DamageFont>();

    public DamageFont GetDamageFont()
    {
        DamageFont result = null;

        foreach (DamageFont font in fontPool)
        {
            if (font.IsActive == false)
            {
                result = font;
            }
        }

        if (result == null)
        {
            GameObject obj = Instantiate(fontObj, transform);
            result = obj.GetComponent<DamageFont>();
            result.Initialize(transform);
            fontPool.Add(result);
        }

        return result;
    }
}
