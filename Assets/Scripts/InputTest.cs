using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InputTest : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public TMP_InputField inputField;

    public void ActivateInput()
    {
        inputField.ActivateInputField();

        // 모바일에서만 소프트 키보드 호출
#if UNITY_ANDROID || UNITY_IOS
        TouchScreenKeyboard.Open("", TouchScreenKeyboardType.Default);
#endif
    }
}
