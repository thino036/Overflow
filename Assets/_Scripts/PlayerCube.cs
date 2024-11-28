using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCube : MonoBehaviour
{
    [Header("Movement Variables")]
    public float HoMoveSpeed = 5f;
    public float VeMoveSpeed = 8f;

    private Rigidbody rb;
    private Vector3 mvmt;

    [Header("Ice Platform Variables")]
    public GameObject icePlatformPrefab;
    public GameObject indicatorPrefab;
    public float spawnDistance = 10f;

    [Header("Dynamic")]
    public GameObject indicator;
    public Vector3 playerPos;

    private Camera mainCam;

    [Header("Air Variables")]
    public float maxAir = 100f;
    public float airDecreaseRate = 10f;
    public float currentAir;
    private bool isUnderwater = false;
    
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        
        // Get main camera for converting mouse position to world position
        mainCam = Camera.main;

        playerPos = transform.position;

        indicator = Instantiate(indicatorPrefab) as GameObject;
        indicator.transform.position = playerPos;

        indicator.SetActive(true);

        this.GetComponent<SphereCollider>().enabled = false;

        currentAir = maxAir;
    }

    // Update is called once per frame
    void Update()
    {
        mvmt.x = Input.GetAxisRaw("Horizontal");
        mvmt.y = Input.GetAxisRaw("Vertical");

        // Display indicator
        Vector3 mousePos2D = Input.mousePosition;
        mousePos2D.z = -mainCam.transform.position.z;
        Vector3 mousePos3D = mainCam.ScreenToWorldPoint(mousePos2D);

        Vector3 mouseDelta = mousePos3D - playerPos;
        // Limit mouseDelta to radius of SphereCollider
        float maxMagnitude = this.GetComponent<SphereCollider>().radius;
        if (mouseDelta.magnitude > maxMagnitude)
        {
            mouseDelta.Normalize();
            mouseDelta *= maxMagnitude;
        }
        Vector3 placementPos = playerPos + mouseDelta;
        indicator.transform.position = placementPos;
        
        if (Input.GetMouseButtonDown(0))
        {
            Instantiate(icePlatformPrefab, placementPos, Quaternion.identity);
        }

        if (isUnderwater)
        {
            currentAir -= airDecreaseRate * Time.deltaTime;
            currentAir = Mathf.Max(currentAir, 0);
            
            if(currentAir <= 0)
            {
                Debug.Log("Player is out of air!");
                // Add code for when player is out of air.
                // Decrease health I guess?
            }
        }
    }

    void FixedUpdate()
    {
        Vector3 newPos = new Vector3(
            rb.position.x + mvmt.x * HoMoveSpeed * Time.fixedDeltaTime,
            rb.position.y + mvmt.y * VeMoveSpeed * Time.fixedDeltaTime);

        rb.MovePosition(newPos);
        
        // Update player position so that indicator logic follows position
        playerPos = rb.position;
        FollowCam.POI = this.gameObject;
    }

    private void OnTriggerEnter(Collider collision)
    {
        if(collision.CompareTag("Water"))
        {
            isUnderwater = true;
        }
    }

    private void OnTriggerExit(Collider collision)
    {
        if(collision.CompareTag("Water"))
        {
            isUnderwater = false;
            currentAir = maxAir;
            Debug.Log("Player can breathe again!");
        }
    }
}
