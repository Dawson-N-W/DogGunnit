using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

public class BuffTimer : MonoBehaviour
{
    [SerializeField]
    public Text damageText;

    [SerializeField]
    public Text speedText;

    public float damageDuration = 10.0f;
    public float speedDuration = 5.0f;

    public float damageStartTime;
    public float speedStartTime;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    IEnumerator StartDamageTimer()
    {
        damageStartTime = Time.time;

        while (Time.time < damageStartTime + damageDuration)
        {
            float timeLeft = (damageStartTime + damageDuration) - Time.time;
            damageText.text = "Damage buff: " + Mathf.RoundToInt(timeLeft) + "s";
            yield return null;
        }

        damageText.text = "Damage buff: Ended";
        // Code to revert damage buff goes here
    }

    IEnumerator StartSpeedTimer()
    {
        speedStartTime = Time.time;

        while (Time.time < speedStartTime + speedDuration)
        {
            float timeLeft = (speedStartTime + speedDuration) - Time.time;
            speedText.text = "Speed buff: " + Mathf.RoundToInt(timeLeft) + "s";
            yield return null;
        }

        speedText.text = "Speed buff: Ended";
        // Code to revert speed buff goes here
    }
}
