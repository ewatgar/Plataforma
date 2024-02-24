using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BouncingAnimation : MonoBehaviour
{
    private Vector2 initHeight;
    [SerializeField] float animAmplitude = 0.5f;
    [SerializeField] float animSpeed = 1f;

    private void Start()
    {
        initHeight = transform.position;
    }

    void Update()
    {
        StartCoroutine(InitAnimation());
    }

    IEnumerator InitAnimation()
    {
        while (true)
        {
            Vector2 minHeight = initHeight - new Vector2(0, animAmplitude / 2);
            Vector2 maxHeight = initHeight + new Vector2(0, animAmplitude / 2);

            for (float t = 0; t < 1; t += Time.deltaTime * animSpeed)
            {
                transform.position = Vector2.Lerp(minHeight, maxHeight, t);
                yield return null;
            }
            for (float t = 0; t < 1; t += Time.deltaTime * animSpeed)
            {
                transform.position = Vector2.Lerp(maxHeight, minHeight, t);
                yield return null;
            }
        }
    }
}
