using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vision : VisionBase
{
    public float visionDistance = 2f;
    public string enemy_tag = "Player";
    private Quaternion startAngle = Quaternion.AngleAxis(-60, Vector3.up);
    private Quaternion steppingAngle = Quaternion.AngleAxis(5, Vector3.up);
    private RaycastHit hit;

    public override GameObject Detect()
    {
        Quaternion angle = transform.rotation * startAngle;
        Vector3 direction = angle * Vector3.forward;
        Vector3 origin = transform.position;

        for (int i = 0; i < 24; i++)
        {
            if (Physics.Raycast(origin, direction, out hit, visionDistance))
            {
                if (hit.collider != null && hit.collider.CompareTag(enemy_tag))
                {
                    Debug.DrawRay(origin, direction * visionDistance, Color.red);
                    return hit.collider.gameObject;
                }
                else
                {
                    Debug.DrawRay(origin, direction * visionDistance, Color.yellow);
                }
            }
            else
            {
                Debug.DrawRay(origin, direction * visionDistance, Color.white);
            }
            direction = steppingAngle * direction;
        }
        return null;
    }
}
