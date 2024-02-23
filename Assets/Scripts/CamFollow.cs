using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamFollow : MonoBehaviour
{
    PlayerHealth playerHealth;
    [SerializeField]
    GameObject target;
    Vector3 velocidad = Vector3.zero;
    public float smoothTime = 0.1f;
    public float minX = 0;
    public float minY = 0;
    public float maxX = 88;
    public float maxY = 5f;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void LateUpdate()
    {
        Vector3 posASeguir = new Vector3(
            target.transform.position.x,
            target.transform.position.y,
            transform.position.z
        );
        Vector3 p = Vector3.SmoothDamp(
            transform.position,
            posASeguir,
            ref velocidad,
            smoothTime
        );

        if (p.x < minX) p = new Vector3(minX, p.y, p.z);
        if (p.x > maxX) p = new Vector3(maxX, p.y, p.z);
        if (p.y < minY) p = new Vector3(p.x, minY, p.z);
        if (p.y > maxY) p = new Vector3(p.x, maxY, p.z);

        transform.position = p;
    }
}
