using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hazerd : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            DamageLog dmg;
            dmg.source = this.gameObject;
            dmg.damageAmount = 100f;
            collision.gameObject.GetComponent<Damageable<DamageLog>>().OnDamage(dmg);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            DamageLog dmg;
            dmg.source = this.gameObject;
            dmg.damageAmount = 100f;
            collision.gameObject.GetComponent<Damageable<DamageLog>>().OnDamage(dmg);
        }
    }
}
