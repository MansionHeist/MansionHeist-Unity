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
        // ���� ��ȣ�� �ش��ϴ� �ڸ����� �߰��� ä�켼��.
    };

        // ���ڿ��� �־��� 4�ڸ� ���� �� �ڸ� ���ڷ� �и��Ͽ� ó��
        foreach (char digit in number)
        {
            // ���� ��ȣ�� ��ȯ�Ͽ� ����� �߰�
            if (elementMap.ContainsKey(digit))
            {
                result += elementMap[digit];
            }
            else
            {
                // �߸��� �ڸ����� ���� ��� ���� ó�� �Ǵ� ����ó���� �ϰ� �ʹٸ� ���⿡ �߰��ϼ���.
                // ���� ���, throw new Exception("Invalid digit: " + digit); ���� �ڵ带 �߰��� �� �ֽ��ϴ�.
                return "Error: Invalid digit";
            }
        }

        return result;
    }

    public void OnClickSubmitButton()
    {
        // TODO: ��й�ȣ�� ��ġ�ϴ��� GameManager�� �˻���
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
