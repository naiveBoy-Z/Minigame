using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public float speed, surprisedTime, stamina, add, sub;
    public CarController carController;
    public GameObject money, staminaPanel, guideImage, defeatPanel;
    public SpriteRenderer fadeout;
    public Image staminaBar;
    public RectTransform velocityBar, velocityPointer;

    private float targetSpeed, velocityBarWidth;
    private bool isStart, isTired;
    private Animator playerAnimator, moneyAnimator;

    private void Start()
    {
        isStart = false; isTired = false;
        speed = carController.speed;
        stamina = 100f;

        targetSpeed = money.GetComponent<MoneyController>().speed - 0.2f;
        velocityBarWidth = velocityBar.sizeDelta.x;

        playerAnimator = GetComponent<Animator>();
        moneyAnimator = money.GetComponent<Animator>();

        StartCoroutine(Appear());
    }

    private void Update()
    {
        if (isStart)
        {
            if (Input.GetKeyDown(KeyCode.Space) && !isTired)
            {
                speed += add;
            }
            speed -= sub * Time.deltaTime;
            if (speed < 0) speed = 0;

            playerAnimator.speed = Mathf.Clamp(speed / targetSpeed, 0.8f, 1.2f);

            float staminaUsed = speed - targetSpeed;
            if (staminaUsed > 0)
            {
                staminaUsed *= staminaUsed;
                stamina -= staminaUsed * Time.deltaTime;
                if (stamina < 10f)
                {
                    isTired = true;
                    playerAnimator.SetBool("isTired", isTired);
                    speed = 0;
                }
            }
            stamina += 5 * Time.deltaTime;
            stamina = Mathf.Clamp(stamina, 0f, 100f);
            staminaBar.fillAmount = stamina / 100f;

            if (isTired && stamina > 60)
            {
                isTired = false;
                playerAnimator.SetBool("isTired", isTired);
            }

            Vector3 v = velocityPointer.anchoredPosition;
            v.x = speed / 24 * velocityBarWidth;
            velocityPointer.anchoredPosition = v;

            // Win condition:
            if (money.transform.localPosition.x < 6f)
            {
                staminaPanel.SetActive(false);
                guideImage.SetActive(false);
                speed = 0;
                isStart = false;
                playerAnimator.SetBool("isWin", true);
            }
        }
    }

    private IEnumerator Appear()
    {
        yield return new WaitForSeconds(3f);

        float translatingSpeed = speed - 5;
        while (speed > 0)
        {
            transform.Translate(translatingSpeed * Time.deltaTime * Vector3.left);
            yield return null;
        }
    }

    public void Surprise()
    {
        speed = 0;
        money.SetActive(true);
        
        playerAnimator.SetBool("isWalking", false);
        moneyAnimator.SetBool("isFlied", true);
    }

    public void StartGame()
    {
        staminaPanel.SetActive(true);
        guideImage.SetActive(true);
        isStart = true;
        speed = 10;
    }

    public void DestroyChildObject()
    {
        Destroy(money);
    }

    public void Victory()
    {
        PlayerPrefs.SetInt("Played", 1);
        StartCoroutine(Fadeout());
    }

    private IEnumerator Fadeout()
    {
        while (fadeout.color.a < 1f)
        {
            Color c = fadeout.color;
            c.a += Time.deltaTime;
            fadeout.color = c;
            yield return null;
        }

        yield return new WaitForSeconds(1);
        UnityEngine.SceneManagement.SceneManager.LoadScene(2);
    }

    public void Defeat()
    {
        isTired = false;
        playerAnimator.SetBool("isTired", true);
        speed = 0;
        defeatPanel.SetActive(true);
    }

    public void Replay()
    {
        SceneManager.LoadScene(1);
    }

    public void BackToMenu()
    {
        SceneManager.LoadScene(0);
    }
}
