using UnityEngine;
public class Enemy:MonoBehaviour,IDamagable
{
    [SerializeField]
    private int health=2;
    private Animator anim;

    private void Start()
    {
        anim = GetComponentInChildren<Animator>();
    }


    public void ApplyDamage()
    {
        // apply damage
        print($"Applying damage on {GetType()}");
        health--;
        anim?.SetTrigger("Hit");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player")) return;
        ApplyDamage();

    }
    private void Update()
    {
        if (health<=0)
        {
            //death animation once
        }
    }
}
