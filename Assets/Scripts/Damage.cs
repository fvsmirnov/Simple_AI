using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damage : MonoBehaviour
{
    public string enemyTag = "Player";

    private void OnTriggerEnter(Collider enemy)
    {
        if (enemy.gameObject.CompareTag(enemyTag))
        {
            Destroy(enemy.gameObject);
        }
    }
}
