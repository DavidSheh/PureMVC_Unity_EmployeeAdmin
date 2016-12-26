/* 
    作者：Sheh伟伟
    博客：http://www.jianshu.com/users/fd3eec0ab0f2/latest_articles
    Github：https://github.com/DavidSheh
*/

using System;
using System.Collections.Generic;

using PureMVC.Patterns;
using PureMVC.Interfaces;

using Demo.PureMVC.EmployeeAdmin.Model;
using Demo.PureMVC.EmployeeAdmin.Model.VO;
using Demo.PureMVC.EmployeeAdmin.View.Components;
using UnityEngine;

namespace Demo.PureMVC.EmployeeAdmin.View
{
    public class UserListMediator : Mediator, IMediator
    {
        private UserProxy userProxy;

        public new const string NAME = "UserListMediator";

        private UserList View
        {
            get { return (UserList)ViewComponent; }
        }

        public UserListMediator(UserList userList)
                : base(NAME, userList)
        {
            Debug.Log("UserListMediator()");
            userList.OnUserNew += OnNewUser;
            userList.OnUserDelete += OnDeleteUser;
            userList.OnUserSelect += OnSelectUser;
        }

        public override void OnRegister()
        {
            Debug.Log("UserListMediator.OnRegister()");
            base.OnRegister();
            userProxy = Facade.RetrieveProxy(UserProxy.NAME) as UserProxy;
            View.LoadUsers(userProxy.Users);
        }

        void OnNewUser()
        {
            UserVO user = new UserVO();
            SendNotification(NotiConst.NEW_USER, user);
        }

        void OnDeleteUser()
        {
            SendNotification(NotiConst.DELETE_USER, View.SelectedUserData);
        }

        void OnSelectUser()
        {
            SendNotification(NotiConst.USER_SELECTED, View.SelectedUserData);
        }

        public override IList<string> ListNotificationInterests()
        {
            IList<string> list = new List<string>();
            list.Add(NotiConst.USER_DELETED);
            list.Add(NotiConst.CANCEL_SELECTED);
            list.Add(NotiConst.USER_ADDED);
            list.Add(NotiConst.USER_UPDATED);
            return list;
        }

        public override void HandleNotification(INotification notification)
        {
            switch (notification.Name)
            {
                case NotiConst.USER_DELETED:
                    View.DeselectItem();
                    View.LoadUsers(userProxy.Users);
                    break;
                case NotiConst.CANCEL_SELECTED:
                    View.DeselectItem();
                    break;
                case NotiConst.USER_ADDED:
                    View.DeselectItem();
                    View.LoadUsers(userProxy.Users);
                    break;
                case NotiConst.USER_UPDATED:
                    View.DeselectItem();
                    View.LoadUsers(userProxy.Users);
                    break;
            }
        }
    }
}
