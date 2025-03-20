using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeIn : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        StartCoroutine(Fadein());
    }

    private IEnumerator Fadein()
    {
        while (spriteRenderer.color.a > 0f)
        {
            Color c = spriteRenderer.color;
            c.a -= Time.deltaTime;
            spriteRenderer.color = c;
            yield return null;
        }

        yield return new WaitForSeconds(2);
        Destroy(gameObject);
    }
}
