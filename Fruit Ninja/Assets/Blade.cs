using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blade : MonoBehaviour {

    bool isCutting = false;
    public GameObject bladeTrailPrefab;
    public float minCuttingVelocity = .001f;

    Vector2 previousPosition;

    GameObject currentBlateTrail;
    Rigidbody2D rb;
    Camera cam;
    CircleCollider2D circleCollider;



    void Start()
    {
        cam = Camera.main;
        rb = GetComponent<Rigidbody2D>();
        circleCollider = GetComponent<CircleCollider2D>();
    }


    void Update () {
        if (Input.GetMouseButtonDown(0))
        {
            StartCutting();
        } else if (Input.GetMouseButtonUp(0))
        {
            StopCutting();
        }

        if (isCutting)
        {
            UpdateCut();
        }
    }

    void UpdateCut()
    {
        Vector2 newPosition = cam.ScreenToWorldPoint(Input.mousePosition);
        rb.position = newPosition;

        float velocity = (newPosition - previousPosition).magnitude/Time.deltaTime;

        if(velocity > minCuttingVelocity)
        {
            circleCollider.enabled = true;
        }
        else
        {
            circleCollider.enabled = false;
        }
        previousPosition = newPosition;
    }
     void StartCutting()
    {
        isCutting = true;
        rb.position = cam.ScreenToWorldPoint(Input.mousePosition);
        transform.position = rb.position;
        previousPosition = cam.ScreenToWorldPoint(Input.mousePosition);
        currentBlateTrail = Instantiate(bladeTrailPrefab, transform);
        circleCollider.enabled = false;
    }

     void StopCutting()
    {
        isCutting = false;
        currentBlateTrail.transform.SetParent(null);
        Destroy(currentBlateTrail,1);
        circleCollider.enabled = false;
    }
}
