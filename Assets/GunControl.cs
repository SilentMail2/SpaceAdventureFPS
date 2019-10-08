using UnityEngine;

public class GunControl : MonoBehaviour
{
    public int selectedWeapon = 0;
    public Camera cameraPoint;
    public float range = 10f;
    public string weapon1 = null;
    public string weapon2 = null;
    public string weaponReadytoDrop = null;
    public Transform Weapondrop;
    // Start is called before the first frame update
    void Start()
    {
        SelectedWeapon();
        cameraPoint = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("p"))
        { DropWeapon(); }
        if (Input.GetKeyDown("e"))
        {
            PickupWeapon();
        }
        int previousSelectedWeapon = selectedWeapon;
        if (Input.GetAxis("Mouse ScrollWheel") > 0f)
        {
            if (selectedWeapon >= transform.childCount - 1)
                selectedWeapon = 0;
            else
                selectedWeapon++;
        }
        if (Input.GetAxis("Mouse ScrollWheel") < 0f)
        {
            if (selectedWeapon <= 0)
                selectedWeapon = transform.childCount - 1;
            else
                selectedWeapon--;
        }
        if (previousSelectedWeapon != selectedWeapon)
        {
            SelectedWeapon();
        }
    }
    void PickupWeapon()
    {
        Debug.Log("Pressed E");
        RaycastHit hit;
        
        Physics.Raycast(cameraPoint.transform.position, cameraPoint.transform.forward, out hit, range);
        if (transform.childCount <= 2)
        {
            if (Physics.Raycast(cameraPoint.transform.position, cameraPoint.transform.forward, out hit, range))
            {


                Debug.Log(hit.transform.gameObject.name);
                GunPickup target = hit.transform.GetComponent<GunPickup>();
                AmmoPickUp clipTarget = hit.transform.GetComponent<AmmoPickUp>();
                if (target != null)
                {
                    //weapon registry
                    if (transform.childCount == 0)
                    {
                        weapon1 = target.Gun.ToString();
                        weaponReadytoDrop = weapon1;
                        Debug.Log(weapon1 + ',' + weapon2);
                    }

                    if (transform.childCount == 1)
                    {
                        weapon2 = target.Gun.ToString();
                        weaponReadytoDrop = weapon2;
                        Debug.Log(weapon1 + ',' + weapon2);
                    }
                    if (transform.childCount == 2)
                    {
                        if (selectedWeapon == 0)
                        {
                            DropWeapon();
                            weapon1 = weapon2;
                            weapon2 = target.Gun.ToString();
                            weaponReadytoDrop = weapon2;
                            Debug.Log(weapon1 + ',' + weapon2);
                        }
                        if (selectedWeapon == 1)
                        {
                            DropWeapon();
                            weapon2 = target.Gun.ToString();
                            weaponReadytoDrop = weapon2;
                            Debug.Log(weapon1 + ',' + weapon2);
                        }

                    }
                    GameObject newGun = Instantiate(target.Gun, this.gameObject.transform);
                    newGun.GetComponent<Gun>().ammo = target.gameObject.GetComponent<GunPickup>().ammo;
                    newGun.GetComponent<Gun>().clip = target.gameObject.GetComponent<GunPickup>().clip;
                    newGun.GetComponent<Gun>().randomDirMax = target.gameObject.GetComponent<GunPickup>().randomRange;
                    newGun.GetComponent<Gun>().randomDirMin = -target.gameObject.GetComponent<GunPickup>().randomRange;
                    if (transform.childCount == 2)
                    {
                        if (selectedWeapon == 0)
                        {
                            Transform unusedWeapon;
                            unusedWeapon = this.transform.GetChild(0);
                            unusedWeapon.gameObject.SetActive(false);
                            selectedWeapon = 1;

                            Debug.Log("Pickup Step one");

                        }

                    }
                    target.DeleteSelf();
                }
                if (transform.childCount > 2)
                {
                    if (Physics.Raycast(cameraPoint.transform.position, cameraPoint.transform.forward, out hit, range))
                    {
                        Debug.Log(hit.transform.gameObject.name);

                        if (target != null)
                        {// GameObject newObject = Instantiate(target.Gun, this.gameObject.transform);
                            

                            Transform unusedWeapon;
                            unusedWeapon = this.transform.GetChild(selectedWeapon);
                            
                            Destroy(unusedWeapon.gameObject);
                            selectedWeapon = 1;
                            // SelectedWeapon();
                            //newObject.transform.SetAsFirstSibling();
                            
                            target.DeleteSelf();
                            Debug.Log("Pickup Step 2");

                        }

                    }
                }
                else if (clipTarget != null)
                {
                    if (this.transform.childCount > 0)
                    {
                        //has gun
                        GameObject GunHeld = this.transform.GetChild(selectedWeapon).gameObject;
                        if (GunHeld.GetComponent<Gun>().GunClipType == clipTarget.gameObject.GetComponent<AmmoPickUp>().GunclipType)
                        {
                            GunHeld.GetComponent<Gun>().PickupClip(clipTarget.gameObject.GetComponent<AmmoPickUp>());
                        }
                        else
                        {
                            Debug.Log("Incapatible Ammotype");
                        }
                    }
                }
            }

        }

    }
    void SelectedWeapon()
    {
        int i = 0;
        foreach(Transform weapon in transform)
        {
            if (i == selectedWeapon)
            {
                weapon.gameObject.SetActive(true);
            }
            else
            {
                weapon.gameObject.SetActive(false);
            }
            i++;
            if (selectedWeapon == 0){
                weaponReadytoDrop = weapon1;
            }
            else
            {
                weaponReadytoDrop = weapon2;
            }
        }
    }
    void DropWeapon()
    {
        //if (selectedWeapon == 0)
        //{

        GameObject droppedGun = (GameObject)Instantiate(Resources.Load("GunPickup(" + weaponReadytoDrop + ")"), Weapondrop.position, Weapondrop.rotation);
        GameObject Droppedgun = this.transform.GetChild(selectedWeapon).gameObject;
        droppedGun.GetComponent<GunPickup>().ammo = Droppedgun.GetComponent<Gun>().ammo;
        droppedGun.GetComponent<GunPickup>().clip = Droppedgun.GetComponent<Gun>().clip;
        droppedGun.GetComponent<GunPickup>().randomRange = Droppedgun.GetComponent<Gun>().randomDirMax;

        // GameObject instance = Instantiate((Resources.Load(("GunPickup("+weaponReadytoDrop + ")"), typeof(GameObject)))) as GameObject;
        //}
        /*   if (selectedWeapon == 1)
           {
               GameObject instance = Instantiate(Resources.Load(("GunPickup(" + weaponReadytoDrop + ")"), typeof(GameObject))) as GameObject;
           }*/
    }

}
