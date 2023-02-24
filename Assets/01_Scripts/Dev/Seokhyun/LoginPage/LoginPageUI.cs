using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class LoginPageUI : MonoBehaviour
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

    private bool _isLoginWindow = true;
    private bool hidePW = true;

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
        }
        else
        {
            Debug.Log($"Register id: {_inputID.value}\n\tpw: {_inputPW.value}");
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
}
