using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UserLoginPlane : MonoBehaviour
{

    [SerializeField]
    private TMP_InputField userName;        //用户名

    [SerializeField]
    private Button login_BTN;           //登录按钮

    public static string userNameLabel;
    private void Awake()
    {
        login_BTN.onClick.AddListener(SaveUser);
        userName.onValueChanged.AddListener(CheckWhetherUserExists);
    }

    //用户输入
    public void CheckWhetherUserExists(string _name)
    {
        if (userName.text != "")
        {
            userNameLabel = _name;
            login_BTN.interactable = true;
        }
        else
            login_BTN.interactable = false;
    }

    //保存用户信息
    public void SaveUser()
    {
        if (!GameManage.Instance.personList.Contains(GameManage.Instance.FindPersonByName(userNameLabel)))
        {
            GameManage.Instance.AddPersonData(userNameLabel, false);
        }

        InterfaceManagement.Instance.OpenStarPlane();
    }


}
