using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatternDrawGizmos : MonoBehaviour
{
    [SerializeField] private Transform[] controlPoints;

    private Vector3 gizmosPosition;

    private void OnDrawGizmos()
    {
        for (float t = 0; t <= 1; t += 0.05f)
        {
            // Bezier curve formula
            gizmosPosition = Mathf.Pow(1 - t, 3) * controlPoints[0].position + 
                3 * Mathf.Pow(1 - t, 2) * t * controlPoints[1].position + 
                3 * (1 - t) * Mathf.Pow(t, 2) * controlPoints[2].position + 
                Mathf.Pow(t, 3) * controlPoints[3].position;

            Gizmos.DrawSphere(gizmosPosition, 1f);
        }

        Gizmos.DrawLine(new Vector3(controlPoints[0].position.x, 0f, controlPoints[0].position.z),
            new Vector3(controlPoints[1].position.x, 0f, controlPoints[1].position.z));

        Gizmos.DrawLine(new Vector3(controlPoints[2].position.x, 0f, controlPoints[2].position.z),
            new Vector3(controlPoints[3].position.x, 0f, controlPoints[3].position.z));
    }
}
