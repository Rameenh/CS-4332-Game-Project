using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DamageNumber : MonoBehaviour
{
    public Color NormalColor;
    public Color CritColor;
    public Color DeathColor;
    public Color CritDeathColor;
    public float CritScaleMult = 2;
    public float NormalSpeed = 3;
    public float CritSpeed = 1.5f;
    public float Lifetime = 3;
    public GameObject TextObject;

    private Vector3 velocity;
    private float timeAlive = 0;

    // Update is called once per frame
    void Update()
    {
        timeAlive += Time.deltaTime;
        if (timeAlive > Lifetime)
        {
            Destroy(gameObject);
        }
        transform.Translate(velocity * Time.deltaTime, Space.World);
        transform.LookAt(Camera.main.transform, Camera.main.transform.up);
    }

    public void Display(Combatant hit, float damage, bool crit, bool died)
    {
        timeAlive = 0;
        TextMeshProUGUI text = TextObject.GetComponent<TextMeshProUGUI>();
        string damageString = damage.ToString("F1") + (died ? "!" : "") + (crit ? "!" : "");
        text.text = damageString;
        if (!crit && !died)
        {
            text.faceColor = NormalColor;
        }
        else if (!died)
        {
            text.faceColor = CritColor;
        }
        else if (!crit)
        {
            text.faceColor = DeathColor;
        }
        else
        {
            text.faceColor = CritDeathColor;
        }
        transform.position = hit.transform.position;
        if (crit)
            transform.localScale *= CritScaleMult;
        velocity = Camera.main.transform.up * (crit ? CritSpeed : NormalSpeed);
    }
}
