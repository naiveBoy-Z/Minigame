using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoneyController : MonoBehaviour
{
    public PlayerController playerController;
    public float speed;

    private bool isFloating;

    private void Start()
    {
        isFloating = false;
    }

    private void Update()
    {
        if (isFloating)
        {
            float s = speed - playerController.speed;
            transform.localPosition += s * Time.deltaTime * Vector3.right;
            if (transform.localPosition.x > 120f) playerController.Defeat();
        }
    }

    public void MoneyFly()
    {
        isFloating = true;
    }
}
