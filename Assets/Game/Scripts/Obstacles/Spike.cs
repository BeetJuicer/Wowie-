using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spike : MonoBehaviour
{
    private Animator anim;
    private bool outSpike;

    [SerializeField]private GameObject damager;

    [SerializeField] private float damageRadius = .5f;
    [SerializeField]private float damageAmount = 5f;

    [SerializeField] private float knockbackStrength;
    [SerializeField] private Vector2 knockbackAngle;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        anim.SetBool("out", outSpike);
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (outSpike == false)
        {
            outSpike = true;
        }
    }

    public void ActiveDamage()
    {
        InvokeRepeating("Damage", 0f, 1f);
    }

    public void CancelDamage()
    {
        CancelInvoke();
    }

    public void SpikeDone()
    {
        outSpike = false;
    }

    public void Damage()
    {
        Collider2D[] detectedObjects = Physics2D.OverlapCircleAll(damager.transform.position, damageRadius);

        foreach (Collider2D collider in detectedObjects)
        {
            IDamageable damageable = collider.GetComponent<IDamageable>();

            if (damageable != null)
            {
                damageable.Damage(damageAmount);
            }

            IKnockbackable knockbackable = collider.GetComponent<IKnockbackable>();

            if (knockbackable != null)
            {
                knockbackable.Knockback(knockbackAngle, knockbackStrength, 1);
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(damager.transform.position, damageRadius);
    }
}
