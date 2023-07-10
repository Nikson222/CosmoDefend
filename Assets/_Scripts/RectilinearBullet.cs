using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RectilinearBullet : Projectile
{
    public override void Fly()
    {
        _rigidbody.velocity = _shootingSide  * _speed * _senderForce * Time.fixedDeltaTime;
    }

    private void FixedUpdate()
    {
        Fly();
    }
}
