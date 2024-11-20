using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameHandler : MonoBehaviour
{
    public Transform pfHealthBar; 
    public Transform player; 

    private void Start()
    {
        HealthSystem healthSystem = new HealthSystem(100);

        
        Transform healthBarTransform = Instantiate(pfHealthBar, new Vector3(0, 10, 0), Quaternion.identity);
        healthBarTransform.SetParent(player); 

        
        healthBarTransform.localPosition = new Vector3(0, 1, 0); 

        
        HealthBar healthBar = healthBarTransform.GetComponent<HealthBar>();
        healthBar.Setup(healthSystem);

        
        Debug.Log("Health: " + healthSystem.GetHealth());
        healthSystem.Damage(10);
    }
}
