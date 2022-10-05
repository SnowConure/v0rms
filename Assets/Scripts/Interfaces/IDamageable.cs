using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamageable
{

    void TakeDamage(int damage, Transform dealer, Vector3 knockback);
    void TakeDamage(int damage, Transform dealer, float knockback);
    void TakeDamage(int damage);
}
