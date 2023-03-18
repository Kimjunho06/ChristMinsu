using System.Text;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using ChristMinsu.Packet;
using System.Security.Cryptography;

public class LoginPage : MonoBehaviour
{
    private UIDocument _uiDocument;

    private Button _confirmBtn;
    private Button _changeBtn;
    private Button _exitBtn;

    private TextField _inputID;
    private TextField _inputPW;

    private Toggle _showPW;
    private VisualElement _checkmark;

    private Label _windowTitle;
    private Label _errMsg;

    private bool _isLoginWindow = true;
    private bool hidePW = true;
    private PublicKey publicKeyInfo;

    private void OnEnable()
    {
        _uiDocument = GetComponent<UIDocument>();
        VisualElement root = _uiDocument.rootVisualElement;

        _confirmBtn = root.Q<Button>("btn_confirm");
        _changeBtn = root.Q<Button>("btn_change");
        _exitBtn = root.Q<Button>("btn_exit");

        _inputID = root.Q<TextField>("input_id");
        _inputPW = root.Q<TextField>("input_pw");

        _showPW = root.Q<Toggle>("show_pw");
        _checkmark = _showPW.Q("unity-checkmark");

        _windowTitle = root.Q<Label>("window_title");
        _errMsg = root.Q<Label>("error_msg");
        _errMsg.text = String.Empty;

        _confirmBtn.RegisterCallback<ClickEvent>(Confirm);

        _changeBtn.RegisterCallback<ClickEvent>(ChangeWindow);

        _exitBtn.RegisterCallback<ClickEvent>(e => {
            Application.Quit();
        });

        _showPW.RegisterValueChangedCallback<bool>(e => {
            _inputPW.isPasswordField = !_showPW.value;
        });
    }

    private void Confirm(ClickEvent e)
    {
        if (_inputID.value == string.Empty)
        {
            Debug.Log("ID is null");
            return;
        }
        if (_inputPW.value == string.Empty)
        {
            Debug.Log("PW is null");
            return;
        }

        if (_isLoginWindow)
        {
            Debug.Log($"Login id: {_inputID.value}\n\tpw: {_inputPW.value}");

            LoginReq req = new LoginReq { Name = _inputID.value, Pw = _inputPW.value };
            NetworkManager.Instance.RegisterSend(MSGID.LoginReq, req);
        }
        else
        {
            Debug.Log($"Register id: {_inputID.value}\n\tpw: {_inputPW.value}");

            // using(RSACryptoServiceProvider RSA = new RSACryptoServiceProvider())
            // {
            //     RSAParameters publicKey = new RSAParameters();
                
            //     publicKey.Modulus = Encoding.UTF8.GetBytes(publicKeyInfo.Modulus);
            //     publicKey.Exponent = Encoding.UTF8.GetBytes(publicKeyInfo.Exponent);
            //     RSA.ImportParameters(publicKey);
            //     byte[] encryptedData = RSA.Encrypt(Encoding.UTF8.GetBytes(_inputPW.value), false);
            // }
            RegisterReq req = new RegisterReq { Name = _inputID.value, Pw = _inputPW.value };
            NetworkManager.Instance.RegisterSend(MSGID.RegisterReq, req);

        }
    }

    private void ChangeWindow(ClickEvent e)
    {
        if(_isLoginWindow)
        {
            _windowTitle.text = "가입하기";
            _confirmBtn.text = "가입";
        }
        else
        {
            _windowTitle.text = "로그인 하기";
            _confirmBtn.text = "로그인";
        }
        _changeBtn.text = _windowTitle.text;

        _isLoginWindow = !_isLoginWindow;
    }

    public void SetPublicKey(PublicKey publicKey)
    {
        this.publicKeyInfo = publicKey;
    }

    public void ShowRes(LoginRes response, ResType type)
    {
        if(response.Success) {
            switch(type)
            {
                case ResType.Login:
                    _errMsg.text = "로그인 성공";
                    break;
                case ResType.Register:
                    _errMsg.text = "가입에 성공했습니다.";
                    break;
            }
        }
        else {
            _errMsg.text = "아이디 또는 비밀번호가 일치하지 않습니다.";
        }
    }
}

public enum ResType
{
    Login,
    Register
}
