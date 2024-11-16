using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    private static List<Target> targetList;

    
    public static Target GetClosest(Vector3 position, float maxRange)
    {
        Target closest = null;

        foreach (Target target in targetList)
        {
            float distanceToTarget = Vector3.Distance(position, target.GetPosition());
            if (distanceToTarget <= maxRange)
            {
                if (closest == null || distanceToTarget < Vector3.Distance(position, closest.GetPosition()))
                {
                    closest = target;
                }
            }
        }

        return closest;
    }

    private Animation animation;

    private void Awake()
    {
       
        if (targetList == null) targetList = new List<Target>();
        targetList.Add(this);

        
        animation = transform.Find("Sprite").GetComponent<Animation>();
        if (animation == null)
        {
            Debug.LogError("No Animation component found on 'Sprite' child object!");
        }
    }

    private void OnDestroy()
    {
        
        targetList.Remove(this);
    }

    public Vector3 GetPosition()
    {
        return transform.position;
    }

    public void Damage()
    {
  
        animation.Play();

        Vector3 randomOffset = new Vector3(0, 7.35f) + GetRandomDir() * UnityEngine.Random.Range(0f, 5f);

        Debug.Log("Damage called with random offset: " + randomOffset);
    }

    public static Vector3 GetRandomDir()
    {
        float angle = UnityEngine.Random.Range(0f, 360f); 
        return new Vector3(Mathf.Cos(angle * Mathf.Deg2Rad), Mathf.Sin(angle * Mathf.Deg2Rad), 0).normalized;
    }
}
