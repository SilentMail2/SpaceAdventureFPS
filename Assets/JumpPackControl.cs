using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;
using UnityEngine.UI;

public class JumpPackControl : MonoBehaviour
{
    public bool floating;
    public float fuel;
    public bool fuelReady;
    public float minFuel;
    public float maxFuel;
    public float fuelTake;
    public float fuelGiveMultiplier;
    public RigidbodyFirstPersonController fPSController;
    public float speed;
    public float maxSpeed;
    public float minSpeed;
    public float time;
    public Rigidbody rb;
    public AnimationCurve flightCurve;
    public bool flight;
    public GameObject FuelGageObj;
    public Slider FuelGage;

    // Start is called before the first frame update
    void Awake()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!flight)
        { FuelGage.gameObject.SetActive (false); }
        if (flight)
        {
            FuelGage.gameObject.SetActive(true);
            FuelGage.maxValue = maxFuel;
            FuelGage.value = fuel;
            if (fuel <= 0)
            { fuelReady = false; }
            if (fuel >= 20)
            { fuelReady = true; }
            if (!fPSController.Grounded && Input.GetKey(KeyCode.Space) && fuelReady)
            {
                time += 0.5f * Time.deltaTime;
                fuel -= fuelTake*4;
                speed = Mathf.Lerp(minSpeed, maxSpeed, time);
                rb.drag = 0f;
                rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);
                rb.AddForce(new Vector3(0f, speed, 0f), ForceMode.Impulse);
                
               // flight = true;
                if (time >= 1)
                {
                    time = 1;
                }
            }
            if (!fPSController.Grounded && Input.GetKeyDown(KeyCode.LeftControl) && fuelReady && !floating)
            {
                rb.constraints = RigidbodyConstraints.FreezePositionY;
                floating = true;
            }

            if (floating)
            {
                Debug.Log("Check if floating");
                fuel -= fuelTake * 4;

                if (Input.GetKeyUp(KeyCode.LeftControl) || !fuelReady || fPSController.Grounded)
                {
                    floating = false;
                }

            }
            if (!floating)
            { rb.constraints = ~RigidbodyConstraints.FreezePosition; }
            if (fPSController.Grounded || !Input.GetKey(KeyCode.Space))
            {
                speed = 0;
                if (fuel < maxFuel)
                {
                    fuel += fuelTake /fuelGiveMultiplier;
                }
                if (fuel>=maxFuel)
                { fuel = maxFuel; }


                speed = Mathf.Lerp(minSpeed, maxSpeed, time);
                time -= 0.5f * Time.deltaTime;
                if (time <= 0)
                { time = 0; }
            }
            //givefuel bug test
            if (Input.GetKeyDown(KeyCode.I))
            { GiveFuel(100); }
        }
    }
    void GiveFuel(float newFuel)
    {
        fuel += newFuel;
    }
}
