using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IrisTransition : MonoBehaviour
{
    public float transitionSpeed;

    private void Start()
    {
        StartCoroutine(StartTransition());
    }

    private IEnumerator StartTransition()
    {
        yield return new WaitForSeconds(2);
        while (transform.localScale.x < 210)
        {
            float scaleX = transform.localScale.x + transitionSpeed * Time.deltaTime;
            transform.localScale = new Vector3(scaleX, scaleX, 1f);
            yield return null;
        }
        Destroy(gameObject);
    }
}
