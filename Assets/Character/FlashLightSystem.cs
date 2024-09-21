using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;
using UnityEngine.UI;

public class FlashLightSystem : MonoBehaviour
{
    [SerializeField] float lightDecay = 0.1f;
    [SerializeField] float angleDecay = 1f;
    [SerializeField] float minimunAngle = 30f;

    Light myLight;
    [SerializeField] Slider batterySlider;

    private void Start()
    {
        myLight = GetComponent<Light>();
        UpdateBatteryUI();
    }

    private void Update()
    {
        DecreaseLightAngle();
        DecreaseLightIntensity();
        UpdateBatteryUI();
    }
    //¹èÅÍ¸®½Àµæ
    public void RestoreLightAngle(float restoreAngle)
    {
        myLight.spotAngle = restoreAngle;
    }public void AddLightIntensity(float intensityAmount)
    {
        float currentintensity = myLight.intensity + intensityAmount;
        if (currentintensity > 3)
        {
            myLight.intensity = 3f;
        }
        else
        {
            myLight.intensity += intensityAmount;
        }
    }
    private void DecreaseLightIntensity()
    {
        myLight.intensity -=lightDecay * Time.deltaTime;
    }

    private void DecreaseLightAngle()
    {
        if(myLight.spotAngle <= minimunAngle)
        {
            return;
        }
        else
        {
            myLight.spotAngle -= angleDecay * Time.deltaTime;
        }
    }

    private void UpdateBatteryUI()
    {
        batterySlider.value = Mathf.Clamp(myLight.intensity / 3f, 0, 1);
    }
}
