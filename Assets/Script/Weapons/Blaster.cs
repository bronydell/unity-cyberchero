using UnityEngine;

public class Blaster : BaseWeapon
{
    [SerializeField]
    private Transform projectileSource;
    // TODO: Move pool system

    public override void Use()
    {
        var createdProjectile = Instantiate(
            parameters.Projectile,
            projectileSource.position,
            projectileSource.rotation
        );
    }
}