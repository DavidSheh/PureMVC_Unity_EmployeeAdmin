/* 
    作者：Sheh伟伟
    博客：http://www.jianshu.com/users/fd3eec0ab0f2/latest_articles
    Github：https://github.com/DavidSheh
*/

using System;
using UnityEngine;
using System.Collections.Generic;
using Demo.PureMVC.EmployeeAdmin.Model.VO;
using UnityEngine.UI;

namespace Demo.PureMVC.EmployeeAdmin.View.Components
{
    public partial class UserList : MonoBehaviour
    {
        public Text txtUserCount;
        public ToggleGroupExtension toggleGroup;
        public Button btnNew;
        public Button btnDelete;
        public UserItem itemTemplete;
        List<UserItem> itemList = new List<UserItem>();//Item临时缓存列表

        public Action NewUser;//添加用户事件
        public Action DeleteUser;//删除用户事件
        public Action SelectUser;//选择用户事件

        public UserVO SelectedUserData;//列表中选择好的用户
        private IList<UserVO> m_currentUsers;//当前选择的用户

        void Start()
        {
            itemTemplete.gameObject.SetActive(false);
            toggleGroup.onValueChanged += OnItemSelect;
            btnDelete.onClick.AddListener(BtnDeleteClick);
            btnNew.onClick.AddListener(BtnNewClick);

            UpdateDeleteBtn();

            //var list = new List<UserVO>();
            //list.Add(new UserVO());
            //list.Add(new UserVO());
            //list.Add(new UserVO());
            //list.Add(new UserVO());
            //LoadUsers(list);
        }

        /// <summary>
        /// 加载用户
        /// </summary>
        /// <param name="list"></param>
        public void LoadUsers(IList<UserVO> list)
        {
            m_currentUsers = list;
            RefreshUI(list);
        }
        
        /// <summary>
        /// 点击Delete按钮
        /// </summary>
        void BtnDeleteClick()
        {
            Debug.Log("BtnDeleteClick");
            if (null != DeleteUser)
            {
                DeleteUser();
            }
        }

        /// <summary>
        /// 点击New按钮
        /// </summary>
        void BtnNewClick()
        {
            Debug.Log("BtnNewClick");
            if (null != NewUser)
            {
                NewUser();
            }
        }

        /// <summary>
        /// User Item Selected
        /// </summary>
        /// <param name="itemToggle"></param>
        void OnItemSelect(Toggle itemToggle)
        {
            if (null == itemToggle)
            {
                return;
            }

            Debug.Log(itemToggle.gameObject.name);

            UserItem item = itemToggle.GetComponent<UserItem>();
            this.SelectedUserData = item.userData;
            UpdateDeleteBtn();
            if (null != SelectUser)
            {
                SelectUser();
            }
        }

        /// <summary>
        /// 取消选择
        /// </summary>
        public void DeselectItem()
        {
            toggleGroup.SetAllTogglesOff();
            this.SelectedUserData = null;
            UpdateDeleteBtn();
        }

        /// <summary>
        /// 刷新UI
        /// </summary>
        /// <param name="datas"></param>
        void RefreshUI(IList<UserVO> datas)
        {
            ClearItems();
            foreach (var data in datas)
            {
                UserItem item = CreateItem();
                item.UpdateData(data);
                itemList.Add(item);
            }

            SetUserCount(datas.Count);
        }

        /// <summary>
        /// 设置用户数量
        /// </summary>
        /// <param name="count"></param>
        private void SetUserCount(int count)
        {
            txtUserCount.text = count.ToString();
        }
        
        /// <summary>
        /// 创建Item
        /// </summary>
        /// <returns></returns>
        private UserItem CreateItem()
        {
            UserItem item = GameObject.Instantiate<UserItem>(itemTemplete);
            item.transform.SetParent(itemTemplete.transform.parent);
            item.gameObject.SetActive(true);
            item.transform.localScale = Vector3.one;
            item.transform.localPosition = Vector3.zero;

            toggleGroup.Add(item.GetComponent<Toggle>());

            return item;
        }

        /// <summary>
        /// 清空列表
        /// </summary>
        void ClearItems()
        {
            foreach (var item in itemList)
            {
                Destroy(item.gameObject);
            }

            toggleGroup.Clear();
            itemList.Clear();
        }
        
        /// <summary>
        /// 更新删除按钮的状态
        /// </summary>
        private void UpdateDeleteBtn()
        {
            btnDelete.interactable = (SelectedUserData != null);
        }
    }
}
