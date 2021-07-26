using UnityEngine;
[RequireComponent(typeof(BaseAreaEffect))]
[RequireComponent(typeof(Rigidbody))]
public class Projectile : MonoBehaviour
{
    [SerializeField]
    private float speed;
    [SerializeField]
    private bool canAffectOnlyPlayer;

    private Rigidbody rigidBody;
    private BaseAreaEffect areaEffect;

    private void Awake()
    {
        rigidBody = GetComponent<Rigidbody>();
        areaEffect = GetComponent<BaseAreaEffect>();
        areaEffect.OnObjectAreaCollide += enteredObject =>
        {
            if (enteredObject.CompareTag("Projectile"))
            {
                return;
            }
            Debug.Log($"Collided with {enteredObject.name}");
            Die();
        };
        areaEffect.OnObjectAreaEnter += stats => !canAffectOnlyPlayer || stats.gameObject.CompareTag("Player");
        areaEffect.OnObjectAreaActivate += stats => Die();
    }

    public void FixedUpdate()
    {
        rigidBody.MovePosition(transform.position + transform.forward * speed * Time.fixedDeltaTime);
    }

    private void Die()
    {
        Destroy(gameObject);
    }
}