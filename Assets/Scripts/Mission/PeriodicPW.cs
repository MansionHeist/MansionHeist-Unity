using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PeriodicPW : MissionUI
{
    [SerializeField]
    private TMP_Text _textMeshPro;

    [SerializeField]
    private TMP_InputField pwInputField;

    private string pw = "2349";

    private void Awake()
    {
        _textMeshPro.text = implementPW(pw);
    }

    private string implementPW(string number)
    {
        string result = "";
        Dictionary<char, string> elementMap = new Dictionary<char, string>()
    {
        { '1', "H" },
        { '2', "He" },
        { '3', "Li" },
        { '4', "Be" },
        { '5', "B" },
        { '6', "C" },
        { '7', "N" },
        { '8', "O" },
        { '9', "F" }
        // 원소 기호와 해당하는 자리수를 추가로 채우세요.
    };

        // 문자열로 주어진 4자리 수를 각 자리 문자로 분리하여 처리
        foreach (char digit in number)
        {
            // 원소 기호로 변환하여 결과에 추가
            if (elementMap.ContainsKey(digit))
            {
                result += elementMap[digit];
            }
            else
            {
                // 잘못된 자리수가 있을 경우 에러 처리 또는 예외처리를 하고 싶다면 여기에 추가하세요.
                // 예를 들어, throw new Exception("Invalid digit: " + digit); 등의 코드를 추가할 수 있습니다.
                return "Error: Invalid digit";
            }
        }

        return result;
    }

    public void OnClickSubmitButton()
    {
        // TODO: 비밀번호가 일치하는지 GameManager과 검사함
        if (pwInputField.text.Equals(pw))
        {
            Close();
        }
        else
        {
            pwInputField.GetComponent<Animator>().SetTrigger("on");
        }
    }
}
