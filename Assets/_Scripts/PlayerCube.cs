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
    }

    void FixedUpdate()
    {
        Vector3 newPos = new Vector3(
            rb.position.x + mvmt.x * HoMoveSpeed * Time.fixedDeltaTime,
            rb.position.y + mvmt.y * VeMoveSpeed * Time.fixedDeltaTime);

        rb.MovePosition(newPos);
        
        // Update player position so that indicator logic follows position
        playerPos = rb.position;
    }
}
