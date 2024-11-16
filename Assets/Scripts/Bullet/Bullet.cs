using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Vector3 shootDir;
    public void Setup(Vector3 shootDir)
    {
        this.shootDir = shootDir;
        transform.eulerAngles = new Vector3(0, 0, GetAngleFromVectorFloat(shootDir));
        Destroy(gameObject, 5f);
    }

    private void Update()
    {
        float moveSpeed = 100f;
        transform.position += shootDir * moveSpeed * Time.deltaTime;

        
        float hitDetectionSize = 3f;
        Target target = Target.GetClosest(transform.position, hitDetectionSize);
        if (target != null)
        {
            target.Damage();
            Destroy(gameObject);
        }
        
    }

    public static float GetAngleFromVectorFloat(Vector3 dir)
    {
        dir = dir.normalized;
        float n = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        if (n < 0) n += 360;

        return n;
    }

    /*
    private void OnTriggerEnter2D(Collider2D collider)
    {
        Target target = collider.GetComponent<Target>();
        if (target != null)
        {
            target.Damage();
            Destroy(gameObject);
        }
    */
    }


