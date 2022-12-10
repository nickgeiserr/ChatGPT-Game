using UnityEngine;
using UnityEngine.Serialization;

public class Bullet : MonoBehaviour
{
    // The lifetime of the bullet in seconds
    [FormerlySerializedAs("Lifetime")] public float lifetime = 2.0f;
    // The amount of damage the bullet does
    [FormerlySerializedAs("Damage")] public float damage = 5.0f;
}