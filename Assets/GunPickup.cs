using UnityEngine;

public class GunPickup : MonoBehaviour
{
    public GameObject Gun;
    public int ammo = 10;
    public int clip = 10;
    public float randomRange = 0.5f;

    public void DeleteSelf()
    {
        Destroy(this.gameObject);
    }
}
