using UnityEngine;

public abstract class _Ability : MonoBehaviour
{
    public float cooldown = 1f;
    protected bool isOnCooldown = false;
    public virtual bool CanUse()
    {
        return !isOnCooldown;
    }
    public void StartCooldown()
    {
        isOnCooldown = true;
        Invoke(nameof(ResetCooldown), cooldown);
    }
    void ResetCooldown()
    {
        isOnCooldown = false;
    }
    public abstract void Activate(); 
}
