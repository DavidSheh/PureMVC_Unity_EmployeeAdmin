/* 
    作者：Sheh伟伟
    博客：http://www.jianshu.com/users/fd3eec0ab0f2/latest_articles
    Github：https://github.com/DavidSheh
*/

using System;

using Demo.PureMVC.EmployeeAdmin.Model.VO;
using Demo.PureMVC.EmployeeAdmin.Model.Enum;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections.Generic;

namespace Demo.PureMVC.EmployeeAdmin.View.Components
{
    public enum UserFormMode
    {
        ADD,
        EDIT,
    }

    /// <summary>
    /// Interaction logic for UserForm.xaml
    /// </summary>
    public partial class UserForm : MonoBehaviour
    {
        public Text txtUserName;
        public InputField inputFirstName;
        public InputField inputLastName;
        public InputField inputEmail;
        public InputField inputUserName;
        public InputField inputPassword;
        public InputField inputConfirmPassword;
        public Dropdown dropdownDepartment;
        public Button btnUpdateUser;
        public Button btnCancel;

        public Action AddUser;
        public Action UpdateUser;
        public Action CancelUser;

        //用户信息获取
        public UserVO User
        {
            get { return m_user; }
        }
        private UserVO m_user;

        //用户信息表单
        public UserFormMode Mode
        {
            get { return m_mode; }
        }
        private UserFormMode m_mode = UserFormMode.ADD;

        //开始
        void Start()
        {
            //设置UI
            btnUpdateUser.onClick.AddListener(BtnUpdateUserClick);
            btnCancel.onClick.AddListener(BtnCancelClick);

            inputUserName.onValueChanged.AddListener(OnInputChanged);
            inputPassword.onValueChanged.AddListener(OnInputChanged);
            inputConfirmPassword.onValueChanged.AddListener(OnInputChanged);

            List<Dropdown.OptionData> options = new List<Dropdown.OptionData>();
            var deptList = DeptEnum.ComboList;
            for (int i = 0; i < deptList.Count; i++)
            {
                Dropdown.OptionData data = new Dropdown.OptionData(deptList[i].Value);
                options.Add(data);
            }
            dropdownDepartment.AddOptions(options);

            InteractiveInput(false);
            UpdateButtons();
        }

        /// <summary>
        /// 清空用户信息
        /// </summary>
        public void ClearForm()
        {
            m_user = null;
            txtUserName.text = "";
            inputFirstName.text = inputLastName.text = inputEmail.text = inputUserName.text = "";
            inputPassword.text = inputConfirmPassword.text = "";
            dropdownDepartment.value = 0;

            InteractiveInput(false);

            UpdateButtons();
        }

        /// <summary>
        /// 显示当前用户信息
        /// </summary>
        /// <param name="user"></param>
        /// <param name="mode"></param>
        public void ShowUser(UserVO user, UserFormMode mode)
        {
            m_mode = mode;
            if (user == null)
            {
                ClearForm();
            }
            else
            {
                InteractiveInput(true);
                m_user = user;
                txtUserName.text = user.UserName;
                inputFirstName.text = user.FirstName;
                inputLastName.text = user.LastName;
                inputEmail.text = user.Email;
                inputUserName.text = user.UserName;
                inputPassword.text = inputConfirmPassword.text = user != null ? user.Password : "";
                dropdownDepartment.value = user.Department.Ordinal + 1;
                EventSystem.current.SetSelectedGameObject(inputFirstName.gameObject);
                inputFirstName.caretPosition = inputFirstName.text.Length - 1;
                UpdateButtons();
            }
        }

        /// <summary>
        /// 启用/禁用用户输入
        /// </summary>
        /// <param name="interactable"></param>
        private void InteractiveInput(bool interactable)
        {
            inputFirstName.interactable = inputLastName.interactable = inputEmail.interactable = interactable;
            inputUserName.interactable = inputPassword.interactable = inputConfirmPassword.interactable = interactable;
            dropdownDepartment.interactable = interactable;
        }

        /// <summary>
        /// 更新按钮状态
        /// </summary>
        private void UpdateButtons()
        {
            if (btnUpdateUser != null)
            {
                btnUpdateUser.interactable = (inputFirstName.text.Length > 0 && inputPassword.text.Length > 0 && inputPassword.text.Equals(inputConfirmPassword.text));
            }
        }
        
        /// <summary>
        /// 更新用户数据按钮点击调用
        /// </summary>
        void BtnUpdateUserClick()
        {
            m_user = new UserVO(
                inputUserName.text, inputFirstName.text,
                inputLastName.text, inputEmail.text,
                inputPassword.text, new DeptEnum(dropdownDepartment.captionText.text, dropdownDepartment.value -1));

            if (m_user.IsValid)
            {
                if (m_mode == UserFormMode.ADD)
                {
                    if (null != AddUser)
                    {
                        AddUser();
                    }
                }
                else
                {
                    if (null != UpdateUser)
                    {
                        UpdateUser();
                    }
                }
            }
        }

        /// <summary>
        /// 取消按钮点击回调
        /// </summary>
        void BtnCancelClick()
        {
            if (null != CancelUser)
            {
                CancelUser();
            }
        }

        /// <summary>
        /// 输入变更回调
        /// </summary>
        /// <param name="value"></param>
        void OnInputChanged(string value)
        {
            UpdateButtons();
        }
    }
}
