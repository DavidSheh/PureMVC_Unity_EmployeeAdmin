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

        public UserListMediator(UserList userList)
                : base(NAME, userList)
        {
            Debug.Log("UserListMediator()");
            userList.NewUser += userList_NewUser;
            userList.DeleteUser += userList_DeleteUser;
            userList.SelectUser += userList_SelectUser;
        }

        public override void OnRegister()
        {
            Debug.Log("UserListMediator.OnRegister()");
            base.OnRegister();
            userProxy = Facade.RetrieveProxy(UserProxy.NAME) as UserProxy;
            UserList.LoadUsers(userProxy.Users);
        }

        private UserList UserList
        {
            get { return (UserList)ViewComponent; }
        }

        void userList_NewUser()
        {
            UserVO user = new UserVO();
            SendNotification(NotiConst.NEW_USER, user);
        }

        void userList_DeleteUser()
        {
            SendNotification(NotiConst.DELETE_USER, UserList.SelectedUserData);
        }

        void userList_SelectUser()
        {
            SendNotification(NotiConst.USER_SELECTED, UserList.SelectedUserData);
        }

        public override IList<string> ListNotificationInterests()
        {
            IList<string> list = new List<string>();
            list.Add(NotiConst.CANCEL_SELECTED);
            list.Add(NotiConst.USER_UPDATED);
            return list;
        }

        public override void HandleNotification(INotification note)
        {
            switch (note.Name)
            {
                case NotiConst.CANCEL_SELECTED:
                    UserList.Deselect();
                    break;

                case NotiConst.USER_UPDATED:
                    UserList.Deselect();
                    UserList.LoadUsers(userProxy.Users);
                    break;
            }
        }
    }
}
