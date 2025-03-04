using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UserLoginPlane : MonoBehaviour
{

    [SerializeField]
    private TMP_InputField userName;        //�û���

    [SerializeField]
    private Button login_BTN;           //��¼��ť

    public static string userNameLabel;
    private void Awake()
    {
        login_BTN.onClick.AddListener(SaveUser);
        userName.onValueChanged.AddListener(CheckWhetherUserExists);
    }

    //�û�����
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

    //�����û���Ϣ
    public void SaveUser()
    {
        if (!GameManage.Instance.personList.Contains(GameManage.Instance.FindPersonByName(userNameLabel)))
        {
            GameManage.Instance.AddPersonData(userNameLabel, false);
        }

        InterfaceManagement.Instance.OpenStarPlane();
    }


}
