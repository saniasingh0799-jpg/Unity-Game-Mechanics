using UnityEngine;

public class Damage_Dealer : MonoBehaviour
{
    public float damage = 10f;
    private void OnTriggerEnter(Collider other)
    {
        Idamagable target = other.GetComponent<Idamagable>();
        Debug.Log("Triggered with: " + other.name);

        if (target != null)
        {
            target.takedamge(damage);
        }
    }


}