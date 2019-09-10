using UnityEngine;

public class Target : MonoBehaviour
{
    public float health = 10;

    public void TakeHealth(float dam)
    {
        health -= dam;
        if (health<=0f)
        {
            Death();
        }
    }
    public void Death()
    {
        Destroy(gameObject);
    }
}
