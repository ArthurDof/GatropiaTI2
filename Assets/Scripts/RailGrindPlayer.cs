using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEditor.Tilemaps;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Splines;
using UnityEngine.UI;

public class RailGrindPlayer : MonoBehaviour
{
    [Header("Variables")]
    public bool onRail;
    [SerializeField] float grindSpeed;
    [SerializeField] float heightOffset;
    float timeForFullSpline;
    float elapsedTime;
    [SerializeField] float lerpSpeed = 10f;
    ScriptPlayer controller;
    GameManager gameManager;
    public float quicktimeevent;
    public Slider QuickTimeL;
    public Slider QuickTimeR;
    int nemclicou;

    [Header("Scripts")]
    [SerializeField] RailScript currentRailScript;
    Rigidbody playerRigidbody;
    public GameObject quicktime;
    private void Start()
    {
        nemclicou = 1;
        QuickTimeL.maxValue = 1;
        QuickTimeL.minValue = 0;
        QuickTimeR.maxValue = 1;
        QuickTimeR.minValue = 0;
        quicktimeevent = 1f;
        grindSpeed = 15;
        controller = GameObject.FindGameObjectWithTag("Player").GetComponent<ScriptPlayer>();
        gameManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();
        playerRigidbody = GetComponent<Rigidbody>();
    }
    private void FixedUpdate()
    {
        if (onRail)
        {
            quicktimeevent -= Time.deltaTime;
            QuickTimeL.value = quicktimeevent;
            QuickTimeR.value = quicktimeevent;
            MovePlayerAlongRail();
        }
    }
    private void Update()
    {
        if (onRail)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                clicouquicktime();
            }
            if (quicktimeevent <= -2f)
            {
                nemclicou = 1;
                quicktimeevent = 1f;
                quicktime.gameObject.SetActive(true);
            }
            if (quicktimeevent <= 0f)
            {
                quicktime.gameObject.SetActive(false);
                if (nemclicou == 1)
                {
                    clicouquicktime();
                }
            }
        }
    }
    void MovePlayerAlongRail()
    {
        if (currentRailScript != null && onRail)
        {
            float progress = elapsedTime / timeForFullSpline;
            if (progress < 0 || progress > 1)
            {
                ThrowOffRail();
                return;
            }
            float nextTimeNormalised;
            if (currentRailScript.normalDir)
                nextTimeNormalised = (elapsedTime + Time.deltaTime) / timeForFullSpline;
            else
                nextTimeNormalised = (elapsedTime - Time.deltaTime) / timeForFullSpline;
            float3 pos, tangent, up;
            float3 nextPosfloat, nextTan, nextUp;
            SplineUtility.Evaluate(currentRailScript.railSpline.Spline, progress, out pos, out tangent, out up);
            SplineUtility.Evaluate(currentRailScript.railSpline.Spline, nextTimeNormalised, out nextPosfloat, out nextTan, out nextUp);
            Vector3 worldPos = currentRailScript.LocalToWorldConversion(pos);
            Vector3 nextPos = currentRailScript.LocalToWorldConversion(nextPosfloat);
            transform.position = worldPos + (transform.up * heightOffset);
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(nextPos - worldPos), lerpSpeed * Time.deltaTime);
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.FromToRotation(transform.up, up) * transform.rotation, lerpSpeed * Time.deltaTime);

            if (currentRailScript.normalDir)
                elapsedTime += Time.deltaTime;
            else
                elapsedTime -= Time.deltaTime;
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Rail")
        {
            onRail = true;
            controller.EntrouSaiurail(1);
            quicktime.gameObject.SetActive(true);
            currentRailScript = collision.gameObject.GetComponent<RailScript>();
            CalculateAndSetRailPosition();
        }
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Rail")
        {
            onRail = true;
            controller.EntrouSaiurail(1);
            quicktime.gameObject.SetActive(true);
            currentRailScript = other.gameObject.GetComponent<RailScript>();
            CalculateAndSetRailPosition();
        }
    }
    void CalculateAndSetRailPosition()
    {
        timeForFullSpline = currentRailScript.totalSplineLength / grindSpeed;

        Vector3 splinePoint;

        float normalisedTime = currentRailScript.CalculateTargetRailPoint(transform.position, out splinePoint);
        elapsedTime = timeForFullSpline * normalisedTime;

        float3 pos, forward, up;
        SplineUtility.Evaluate(currentRailScript.railSpline.Spline, normalisedTime, out pos, out forward, out up);
        currentRailScript.CalculateDirection(forward, transform.forward);
        transform.position = splinePoint + (transform.up * heightOffset);
    }
    void ThrowOffRail()
    {
        onRail = false;
        controller.EntrouSaiurail(0);
        quicktime.gameObject.SetActive(false);
        quicktimeevent = 1f;
        nemclicou = 1;
        currentRailScript = null;
        transform.position += transform.forward * 1;
    }
    public void clicouquicktime()
    {
        nemclicou = 0;
        if (quicktimeevent <= 0.5f && quicktimeevent > 0f )
        {
            gameManager.AdicionarPontos(200);
        }
        else
        {
            gameManager.AdicionarPontos(-500);
            gameManager.ColetavelTempo(-10);
        }
        quicktime.gameObject.SetActive(false);
    }
}
