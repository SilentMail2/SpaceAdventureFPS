using UnityEngine;

public class GunPickup : MonoBehaviour
{
    public GameObject Gun;
    
    public void DeleteSelf()
    {
        Destroy(this.gameObject);
    }
}
