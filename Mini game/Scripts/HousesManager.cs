using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HousesManager : MonoBehaviour
{
    public GameObject player;
    public List<Transform> houses = new();
    public float speed;

    private bool pause;
    private PlayerController playerController;

    private void Awake()
    {
        foreach (Transform house in houses)
        {
            float scaleY = 5f + Random.Range(-1f, 3f);
            house.localScale = new Vector3(5f, scaleY, 1f);
        }
    }

    private void Start()
    {
        pause = false;
        playerController = player.GetComponent<PlayerController>();
    }

    private void Update()
    {
        if (!pause)
        {
            foreach (Transform house in houses)
            {
                speed = playerController.speed;
                house.Translate(speed * Time.deltaTime * Vector3.left);
                if (house.position.x < -65) house.localPosition = new Vector3(68, 0, 0);
            }
        }
    }

    public void PauseTheMovement()
    {
        StartCoroutine(Pause());
    }

    private IEnumerator Pause()
    {
        pause = true;

        yield return new WaitForSeconds(playerController.surprisedTime);

        pause = false;
    }
}
