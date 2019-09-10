using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public float hp = 100;
    public float mhp = 100;
    public float sp = 100;
    public float spIncrease = 10;
    public float shieldReplenishTime;
    public float shieldReplenishReset;
    public bool shieldReplenishing;
    public Slider Health;
    public Slider Shield;

    private void Update()
    {
         if (Input.GetKeyDown(KeyCode.K))
         {
             TakeHealth(10);
         }
        if (Input.GetKeyDown(KeyCode.H))
        { GiveHealth(10); }
            Health.value = (hp/100);
        Shield.value = sp/100;
        ShieldReplenish();
    }
    void GiveHealth(float health)
    {
        hp += health;
        if (hp >= mhp)
        { hp = mhp; }
    }

    void TakeHealth(float dam)
    {
        shieldReplenishing = false;
        if (sp > 0)
        { sp -= dam; }
        if (sp <= 0)
        {
            //hp += sp;
            sp = 0;
            hp -= dam;
        }
        if (hp <= 0)
        {
            Debug.Log("You Died");
        }
    }
    void ShieldReplenish()
    {
        if (sp >= 100)
        {
            shieldReplenishing = false;
            sp = 100;
        }
        if (sp < 100)
        {
            if (!shieldReplenishing)
            {
                shieldReplenishTime -= 1 * Time.deltaTime;
            }
            if (shieldReplenishTime <= 0)
            {
                shieldReplenishing = true;
                shieldReplenishTime = shieldReplenishReset;
            }
        }
        if (shieldReplenishing)
        {
            sp += spIncrease*Time.deltaTime;
        }
    }
}
