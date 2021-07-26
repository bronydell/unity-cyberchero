using System.Collections;
using System.Runtime.InteropServices;
using UnityEngine;

public abstract class BaseWeapon : MonoBehaviour
{
    [SerializeField]
    protected WeaponParameters parameters;

    private float cooldown = 0.0f;
    private Coroutine usageCoroutine;

    private void Awake()
    {
        StartCoroutine(Cooldown());
    }

    public void StartUse()
    {
        if (usageCoroutine != null)
        {
            StopCoroutine(usageCoroutine);
            usageCoroutine = null;
        }
        usageCoroutine = StartCoroutine(UsageCoroutine());
    }

    public void StopUse()
    {
        if (usageCoroutine != null)
        {
            StopCoroutine(usageCoroutine);
            usageCoroutine = null;
        }
    }

    public abstract void Use();

    private IEnumerator UsageCoroutine()
    {
        yield return new WaitForSeconds(0.1f);
        while (true)
        {
            while (cooldown > 0.0f)
            {
                yield return null;
            }
            Use();
            cooldown = parameters.FireCooldown;
            yield return null;
        }
    }

    private IEnumerator Cooldown()
    {
        while (true)
        {
            while (cooldown > 0.0f)
            {
                yield return null;
                cooldown -= Time.deltaTime;
            }
            yield return null;
        }
    }
}