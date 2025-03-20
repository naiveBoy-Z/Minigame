using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour
{
    public float speed;
    public Transform player;
    public HousesManager housesManager;

    private bool isReachPlayer;
    private PlayerController playerController;

    private void Start()
    {
        isReachPlayer = false;
        playerController = player.GetComponent<PlayerController>();
    }

    private void Update()
    {
        if (!isReachPlayer && player.position.x <= transform.position.x)
        {
            isReachPlayer = true;
            BlowMoney();
        }

        if (isReachPlayer) { 
            transform.Translate(speed * Time.deltaTime * Vector3.right);
            if (transform.position.x > 120) Destroy(gameObject);
        }
    }

    private void BlowMoney()
    {
        StartCoroutine(BlowMoneyCoroutine());
    }

    private IEnumerator BlowMoneyCoroutine()
    {
        playerController.Surprise();

        yield return new WaitForSeconds(0.5f);

        housesManager.PauseTheMovement();
    }
}
