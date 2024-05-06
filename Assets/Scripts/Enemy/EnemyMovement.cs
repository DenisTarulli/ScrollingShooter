using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyMovement : MonoBehaviour
{
    [SerializeField, Range(0, 1f)] private float speedModifier;
    [SerializeField] protected string wavePattern;
    protected Transform[] routes = new Transform[4];

    protected int routeToGo;
    protected float tParam;
    protected Vector3 objectPosition;
    protected bool courutineAllowed;    

    protected void FindWavePattern()
    {
        Transform wave = GameObject.FindWithTag(wavePattern).transform;

        for (int i = 0; i < routes.Length; i++)
        {
            routes[i] = wave.GetChild(i);
        }
    }

    protected IEnumerator GoByTheRoute(int routeIndex)
    {
        courutineAllowed = false;

        Vector3 p0 = routes[routeIndex].GetChild(0).position;
        Vector3 p1 = routes[routeIndex].GetChild(1).position;
        Vector3 p2 = routes[routeIndex].GetChild(2).position;
        Vector3 p3 = routes[routeIndex].GetChild(3).position;

        while (tParam < 1)
        {
            tParam += Time.deltaTime * speedModifier;

            objectPosition = Mathf.Pow(1 - tParam, 3) * p0 +
                3 * Mathf.Pow(1 - tParam, 2) * tParam * p1 +
                3 * (1 - tParam) * Mathf.Pow(tParam, 2) * p2 +
                Mathf.Pow(tParam, 3) * p3;

            transform.LookAt(objectPosition);
            transform.position = objectPosition;
            yield return new WaitForEndOfFrame();
        }

        tParam = 0f;

        routeToGo += 1;

        if (routeToGo > routes.Length - 1)
            routeToGo = 0;

        courutineAllowed = true;
    }
}
