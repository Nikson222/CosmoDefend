using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarierDestroyer : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        EnemySpacecraft enemySpacecraft = collision.gameObject.GetComponent<EnemySpacecraft>();
        if (enemySpacecraft != null)
            enemySpacecraft.GetDamage(enemySpacecraft.Health * 2);
        else
            collision.gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        collision.gameObject.SetActive(false);
    }
}
