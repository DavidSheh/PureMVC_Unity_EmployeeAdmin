using Demo.PureMVC.EmployeeAdmin.Model.VO;
using UnityEngine;
using UnityEngine.UI;

public class UserItem : MonoBehaviour {

    public UserVO userData;

    public Text txtUserName;//用户名
    public Text txtFirstName;//名
    public Text txtLastName;//姓
    public Text txtEmail;//邮件
    public Text txtDepartment;//部门

    //更新User信息
    public void UpdateData(UserVO data)
    {
        this.userData = data;

        txtUserName.text = data.UserName;
        txtFirstName.text = data.FirstName;
        txtLastName.text = data.LastName;
        txtEmail.text = data.Email;
        txtDepartment.text = data.Department.ToString();
    }
}
