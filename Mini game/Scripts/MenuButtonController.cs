using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System.Collections.Generic;

public class MenuButtonController : MonoBehaviour
{
    public GameObject pnlPlayingRemind, pnlTypingPwd, wrongSign, myWish;
    public List<TextMeshProUGUI> digits = new();

    private Coroutine DisplayPlayingRemindCoroutine, TypePwdCoroutine;
    private bool checkPwd;

    private void Start()
    {
        checkPwd = false;
    }

    public void PlayGame()
    {
        SceneManager.LoadScene(1);
    }

    public void OpenCipher()
    {
        if (PlayerPrefs.GetInt("Played", 0) == 0)
        {
            DisplayPlayingRemindCoroutine ??= StartCoroutine(DisplayPlayingRemind());
        }
        else
        {
            pnlTypingPwd.SetActive(true);
            TypePwdCoroutine = StartCoroutine(TypePassword());
        }
    }

    private IEnumerator DisplayPlayingRemind()
    {
        pnlPlayingRemind.SetActive(true);
        yield return new WaitForSeconds(1);
        pnlPlayingRemind.SetActive(false);

        DisplayPlayingRemindCoroutine = null;
    }

    private IEnumerator TypePassword()
    {
        bool needClickBackspace = false;
        for (int i = 0; i <= digits.Count; i++)
        {
            while (true)
            {
                if (Input.GetKeyDown(KeyCode.Backspace))
                {
                    checkPwd = false;
                    if (needClickBackspace)
                    {
                        needClickBackspace = false;
                        wrongSign.SetActive(false);
                    }
                    if (i == 0) i--;
                    else i -= 2;
                    digits[i + 1].text = "";
                    yield return null;
                    break;
                }

                if (!string.IsNullOrEmpty(Input.inputString) && !needClickBackspace && i < digits.Count)
                {
                    digits[i].text = "" + Input.inputString[0];
                    yield return null;
                    break;
                }

                yield return null;
            }

            if (i == digits.Count - 1)
            {
                string s = "";
                foreach (TextMeshProUGUI tmpUI in digits) s += tmpUI.text;
                if (s != "111111")
                {
                    wrongSign.SetActive(true);
                    needClickBackspace = true;
                }
                else
                {
                    checkPwd = true;
                }
            }
        }
    }

    public void ClosePwdPanel()
    {
        StopCoroutine(TypePwdCoroutine);
        wrongSign.SetActive(false);
        pnlTypingPwd.SetActive(false);
    }

    public void CheckPwd()
    {
        myWish.SetActive(checkPwd);
    }

    public void Reload()
    {
        SceneManager.LoadScene(0);
    }
}
