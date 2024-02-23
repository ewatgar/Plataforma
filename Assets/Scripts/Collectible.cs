using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectible : MonoBehaviour
{
    PlayerHealth playerHealth;
    private Vector2 initHeight;
    [SerializeField] float animAmplitude = 0.5f;
    [SerializeField] float animSpeed = 1f;
    [SerializeField] int hp = 1;

    private void Start()
    {
        initHeight = transform.position;
    }

    private void Update()
    {
        StartCoroutine(BouncingAnimation());
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        Collect(other);
    }

    void OnTriggerStay2D(Collider2D other)
    {
        Collect(other);
    }

    private void Collect(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            playerHealth = other.GetComponent<PlayerHealth>();
            playerHealth.Heal(hp);
            Destroy(gameObject);
        }
    }

    IEnumerator BouncingAnimation()
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
