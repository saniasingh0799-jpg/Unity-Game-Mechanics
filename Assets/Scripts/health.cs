using UnityEngine;
public class health : MonoBehaviour, Idamagable
{
    float Max_Health = 100f;
    float Current_Health;
    bool isdead = false;
    bool isprojectile;
    void Idamagable.takedamge(float amount)
    {
        if (isdead) return;
        Current_Health -= amount;
        Current_Health = Mathf.Clamp(Current_Health, 0, Max_Health);
        Debug.Log("damage" + amount + "now health" + Current_Health);

        if (Current_Health <= 0)
        {
            Die();
        }
    }
    void Die()
    {
        isdead = true;
        Debug.Log("player died");
        gameObject.SetActive(false);
    }
    void Heal(float amount)
    {
        if (isdead) return;
        Current_Health += amount;
        Current_Health = Mathf.Clamp(Current_Health, 0, Max_Health);
    }
    void Awake()
    {
        Current_Health = Max_Health;
    }
}
