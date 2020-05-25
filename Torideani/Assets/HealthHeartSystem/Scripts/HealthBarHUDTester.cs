/*
 *  Author: ariel oliveira [o.arielg@gmail.com]
 */

using UnityEngine;

public class HealthBarHUDTester : MonoBehaviour
{
    public GameObject hp;
    private void Update()
    {
        try
        {
            PlayerStats.Instance.health = hp.gameObject.GetComponent<Bandit_Class>().health;
        }
        catch
        {
            PlayerStats.Instance.health = hp.gameObject.GetComponent<Chasseur_Class>().health;
        }
    }

    public void AddHealth()
    {
        PlayerStats.Instance.AddHealth();
    }

    public void Heal(float health)
    {
        PlayerStats.Instance.Heal(health);
    }

    public void Hurt(float dmg)
    {
        PlayerStats.Instance.TakeDamage(dmg);
    }
}
