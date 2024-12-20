using UnityEngine;
using UnityEngine.UI;

public interface ICombat 
{
    public void Attack();
    public void Die();
}

public interface ICombatADS
{
    public void ADS();
}
