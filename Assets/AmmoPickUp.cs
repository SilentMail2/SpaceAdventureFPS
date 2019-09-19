using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoPickUp : MonoBehaviour
{
    enum ammoType {Auto, HighCal, LowCal}
    [SerializeField] private ammoType clipType = ammoType.Auto;
    public string GunclipType;
    public int clipAmmount = 1;
    private void Awake()
    {
        GunclipType = clipType.ToString();
    }
    // Start is called before the first frame update

}
