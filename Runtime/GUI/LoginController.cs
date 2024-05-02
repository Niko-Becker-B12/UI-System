using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


public class LoginController : UiWindow
{

    //public TMP_InputField usernameInputfield;
    public TMP_InputField passwordInputfield;

    public TextMeshProUGUI loginErrorMessageText;


    private void Start()
    {
        
    }

    public void ValidateLoginData()
    {

        if (GameManager.Instance.currentClientIndex == -1)
        {

            for (int i = 0; i < GameManager.Instance.clientSkinDataSets.Count; i++)
            {

                if (//usernameInputfield.text == clientSkinDataSets[i].ClientName &&
                    passwordInputfield.text == GameManager.Instance.clientSkinDataSets[i].ClientPassword)
                {

                    GameManager.Instance.ValidatedLogin(i);

                    return;

                }
                else
                    continue;

            }

            DisplayLoginError("Error! Username or password wrong!\nPlease check the help section for more info!");

        }
        else
        {

            GameManager.Instance.ValidatedLogin(-1);

            return;

        }

    }

    public void DisplayLoginError(string errorMessage = "")
    {

        loginErrorMessageText.text = errorMessage;

    }

}