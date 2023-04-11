using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchWeapon : MonoBehaviour
{
    public HUD Hud;
    float cooldown = 1f;
    static float lastPressTime = 0.0f;
    private void OnTriggerStay(Collider other)
    {
        if(other.name == "Astronaut" && other.GetComponent<Player>().EquippedWeapon != GetComponent<Weapon>())
        {
            Hud.OpenMessagePanel();
            if(Input.GetKey(KeyCode.F))
            {
                float currentTime = Time.time;
                float diffSecs = currentTime - lastPressTime;

                if (diffSecs >= cooldown)
                {
                    lastPressTime = currentTime;
                    if(other.GetComponent<Player>().EquippedWeapon != null)
                    {
                        other.GetComponent<Player>().EquippedWeapon.transform.SetParent(null);
                    }
                    other.GetComponent<Player>().EquippedWeapon = GetComponent<Weapon>();
                    transform.SetParent(other.transform);
                    transform.localPosition = new Vector3(0.87f, -0.38f, 1.28f);
                    Hud.CloseMessagePanel();
                }
            }
        }
    }
    
    private void OnTriggerExit(Collider other)
    {
        Hud.CloseMessagePanel();
    }
}
