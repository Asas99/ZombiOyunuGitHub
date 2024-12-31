using UnityEngine;
using UnityEngine.UI;

public class CharacterCombat : MonoBehaviour
{
    //Hasar alma
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("zombie hands"))
        {
            //print("col detected");
            gameObject.GetComponent<PlayerStatus>().Health -= other.gameObject.GetComponent<HandAttack>().Damage;
        }
    }
}
