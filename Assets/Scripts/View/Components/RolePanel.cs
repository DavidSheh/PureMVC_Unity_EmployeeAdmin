/* 
    作者：Sheh伟伟
    博客：http://www.jianshu.com/users/fd3eec0ab0f2/latest_articles
    Github：https://github.com/DavidSheh
*/

using System;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Demo.PureMVC.EmployeeAdmin.Model.Enum;
using Demo.PureMVC.EmployeeAdmin.Model.VO;
using UnityEngine.UI;

namespace Demo.PureMVC.EmployeeAdmin.View.Components
{
    public partial class RolePanel : MonoBehaviour
    {
        public Text txtFullName;
        public ToggleGroupExtension toggleGroup;
        public Dropdown dropdownRoleList;
        public Button btnAdd;
        public Button btnRemove;
        
        public GameObject itemTemplete;
        List<GameObject> itemList = new List<GameObject>();//Item临时缓存列表

        #region Events
        public event Action AddRole;
        public event Action RemoveRole;
        #endregion

        void Start()
        {
            itemTemplete.gameObject.SetActive(false);
            toggleGroup.onValueChanged += roleList_SelectionChanged;
            dropdownRoleList.onValueChanged.AddListener(userRoles_SelectionChanged);
            List<Dropdown.OptionData> options = new List<Dropdown.OptionData>();
            var roleList = RoleEnum.ComboList;
            for (int i = 0; i < roleList.Count; i++)
            {
                Dropdown.OptionData data = new Dropdown.OptionData(roleList[i].Value);
                options.Add(data);
            }
            dropdownRoleList.AddOptions(options);

            btnAdd.onClick.AddListener(BtnAddClick);
            btnRemove.onClick.AddListener(BtnRemoveClick);
        }

        #region Clear form

        public void ClearForm()
        {
            ClearItems();

            m_user = null;
            m_roles = null;

            m_selectedRole = null;

            txtFullName.text = "";
            dropdownRoleList.value = 0;
            dropdownRoleList.interactable = false;
            UpdateButtons();
        }

        #endregion

        public void ShowUser(UserVO user, IList<RoleEnum> roles)
        {
            if (user == null)
            {
                ClearForm();
            }
            else
            {
                m_user = user;
                m_roles = roles;
                
                ShowUserRoles(roles);
                dropdownRoleList.value = 0;
                dropdownRoleList.interactable = true;
                UpdateButtons();
            }
        }


        public void ShowUserRoles(IList<RoleEnum> roles)
        {
            ClearItems();
            foreach (var data in roles)
            {
                GameObject item = CreateItem();
                item.GetComponentInChildren<Text>().text = data.Value;
                itemList.Add(item);
            }

            UpdateButtons();
        }

        /// <summary>
        /// 清空列表
        /// </summary>
        void ClearItems()
        {
            foreach (var item in itemList)
            {
                Destroy(item);
            }

            toggleGroup.Clear();
            itemList.Clear();
        }

        /// <summary>
        /// 创建Item
        /// </summary>
        /// <returns></returns>
        private GameObject CreateItem()
        {
            GameObject item = GameObject.Instantiate<GameObject>(itemTemplete);
            item.transform.SetParent(itemTemplete.transform.parent);
            item.SetActive(true);
            item.transform.localScale = Vector3.one;
            item.transform.localPosition = Vector3.zero;

            toggleGroup.Add(item.GetComponent<Toggle>());

            return item;
        }

        /// <summary>
        /// Add按钮点击调用
        /// </summary>
        protected void BtnAddClick()
        {
            if (null != AddRole)
            {
                AddRole();
            }
        }

        /// <summary>
        /// Remove按钮点击调用
        /// </summary>
        protected virtual void BtnRemoveClick()
        {
            if (null != RemoveRole)
            {
                RemoveRole();
            }
        }

        #region Properties

        public UserVO User
        {
            get { return m_user; }
        }
        private UserVO m_user;

        public IList<RoleEnum> Roles
        {
            get { return m_roles; }
        }
        private IList<RoleEnum> m_roles;

        private RoleEnum m_selectedRole;
        public RoleEnum SelectedRole
        {
            get
            {
                return m_selectedRole;
            }
        }

        public RoleEnum RoleListSelectedRole
        {
            get
            {
                return RoleEnum.ComboList[dropdownRoleList.value];
            }
        }
        #endregion

        #region Button updating

        private void UpdateButtons()
        { 
            if (btnRemove != null)
                btnRemove.interactable = SelectedRole != null;
            if (btnAdd != null)
                btnAdd.interactable = dropdownRoleList.value != 0;
        }

        #endregion

        #region Event handling

        private void UserControl_Loaded()
        {
            UpdateButtons();
        }

        private void roleList_SelectionChanged(Toggle toggle)
        {
            int index = 0;
            for (int i = 0; i < itemList.Count; i++)
            {
                Toggle t = itemList[i].GetComponent<Toggle>();
                if(t == toggle)
                {
                    index = i;
                    break;
                }
            }

            m_selectedRole = Roles[index];

            UpdateButtons();
        }

        /// <summary>
        /// dropdown回调
        /// </summary>
        private void userRoles_SelectionChanged(int value)
        {
            UpdateButtons();
        }
        #endregion
    }
}
