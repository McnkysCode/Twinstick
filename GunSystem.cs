using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

//[RequireComponent(typeof(PlayerInput))]
public class GunSystem : MonoBehaviour
{
    //gun stats
    [Header("gun stats")]
    public int damage;
    public float timeBetweenShooting, spread, range, reloadtime, timeBetweenShots;
    public int magazineSize, bulletsPerTap;
    public bool allowButtonHold;
    int bulletsleft, bulletsShot;

   
    bool shooting, readyToShoot, reloading;
    [SerializeField]bool cheatAmmo = false;
    bool singleFire;

    //refrence
    [Header("refrence")]
    public Transform rayCastEnd;
    public Transform attackPoint;
    public RaycastHit rayHit;
    public LayerMask whatIsEnemy;
    private PlayerControls playerControls;
    private TwinStickMovement twinStickMovement;
    public TextMeshProUGUI text;
    private float fireArm;


  


    private void Awake()
    {
        //twinStickMovement= GetComponentInParent<TwinStickMovement>();
        //playerControls = twinStickMovement.playerControls;
        //bulletsleft = magazineSize;
        //readyToShoot = true;
        ////fireArm.Enable();
        ////fireArm = playerControls.Controls.fireArm;
        //gameObject.SetActive(true);
    }
    private void Start()
    {
        twinStickMovement = GetComponentInParent<TwinStickMovement>();
        playerControls = twinStickMovement.playerControls;
        bulletsleft = magazineSize;
        readyToShoot = true;
        //fireArm.Enable();
        //fireArm = playerControls.Controls.fireArm;
        gameObject.SetActive(true);
    }
    private void Update()
    {
        MyInput();
        HandleInput();


        text.SetText(bulletsleft + " / " + magazineSize);

        Debug.DrawRay(rayCastEnd.position, rayCastEnd.transform.right, Color.magenta);
        Debug.Log(Input.inputString);


        if (Input.GetKey(KeyCode.B))
        {
            if(cheatAmmo== false)
            {
                cheatAmmo = true;
            }
            else if(cheatAmmo == true) 
            {
                cheatAmmo = false;
            }
        }

        if(cheatAmmo == true)
        {
            bulletsleft = magazineSize;
        }

    }
    void HandleInput()
    {
        fireArm = playerControls.Controls.fireArm.ReadValue<float>();
    } 
    private void MyInput()
    {
       

        if (allowButtonHold)
        {
            
            if (fireArm >= 0.5f)
            {
                shooting = true;
               Debug.Log("AUTOFIRE");
                if (readyToShoot && shooting && !reloading && bulletsleft > 0)
                {
                    bulletsShot = bulletsPerTap;
                    Shoot();
                }

            }

        }
        else
        {
            if (fireArm >= 0.5f && singleFire== true)
            {
                shooting = true;
                Debug.Log("singefire");
                if (readyToShoot && shooting && !reloading && bulletsleft > 0)
                {
                    bulletsShot = bulletsPerTap;
                    Shoot();
                }
                singleFire = false;
            }

        }

        if(fireArm < 0.5f)
        {
            singleFire = true;
          if(bulletsleft <= 0)
          {
            Reload();
          }
        }
        if (Input.GetKeyDown(KeyCode.R) && bulletsleft < magazineSize && !reloading)
        {
            Reload();
        }


    }
    private void Shoot()
    {
        readyToShoot = false;

      
        //calc direction with spread
        Vector3 direction = rayCastEnd.transform.forward; //+ new Vector3(x, y, 0);

        Debug.DrawRay(rayCastEnd.transform.position, rayCastEnd.transform.right, Color.green);
        
        
        if(Physics.Raycast(rayCastEnd.transform.position, rayCastEnd.transform.right, out rayHit, range, whatIsEnemy))
        {
            Debug.Log(rayHit.collider.name);

            if (rayHit.collider.CompareTag("Enemy"))
            {
                rayHit.collider.GetComponent<EnemyAi>().TakeDamage(damage);
            }
            else if (rayHit.collider.CompareTag("boss"))
            {
                Debug.Log("hit boss");
                rayHit.collider.GetComponent<BossAi>().TakeDamage(damage);
            }
        }

        bulletsleft--;
        bulletsShot--;
        Invoke("ResetShot", timeBetweenShooting);

        if(bulletsShot>0 && bulletsleft > 0)
        {
         Invoke("Shoot", timeBetweenShots);
        }
    }

    private void ResetShot()
    {
        readyToShoot = true;
    }

    public void Reload()
    {
        reloading = true;
        Invoke("ReloadFinished", reloadtime);
    }
    private void ReloadFinished()
    {
        bulletsleft = magazineSize;
        reloading = false;
    }

}