using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Gamekit3D
{
    public abstract class Projectile : MonoBehaviour, IPool<Projectile>
    {
        public int poolID { get; set; }
        public ObjectPooler<Projectile> pool { get; set; }

        public abstract void Hit(Vector3 target, Weapon shooter);
    }
}
