using UnityEngine;

public class Target : MonoBehaviour
{
    public float health = 10;
    public bool hasShield = false;
    public float shield = 10;

    public void TakeHealth(float dam)
    {
        if (hasShield)
        {
            health -= dam;
            if (health <= 0f)
            {
                Death();
            }
        }
    }
    public void Death()
    {
        Destroy(gameObject);
    }
}
