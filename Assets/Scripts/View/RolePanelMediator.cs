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

namespace Demo.PureMVC.EmployeeAdmin.View
{
    public class RolePanelMediator : Mediator, IMediator
    {
        private RoleProxy roleProxy;

        public new const string NAME = "RolePanelMediator";

        public RolePanelMediator(RolePanel viewComponent)
            : base(NAME, viewComponent)
        {
            RolePanel.AddRole += RolePanel_AddRole;
            RolePanel.RemoveRole += RolePanel_RemoveRole;
        }

        public override void OnRegister()
        {
            base.OnRegister();
            roleProxy = (RoleProxy)Facade.RetrieveProxy(RoleProxy.NAME);
        }

        private RolePanel RolePanel
        {
            get { return (RolePanel)ViewComponent; }
        }

        void RolePanel_RemoveRole()
        {
            roleProxy.RemoveRoleFromUser(RolePanel.User, RolePanel.SelectedRole);
        }

        void RolePanel_AddRole()
        {
            roleProxy.AddRoleToUser(RolePanel.User, RolePanel.RoleListSelectedRole);
        }

        public override IList<string> ListNotificationInterests()
        {
            IList<string> list = new List<string>();
            list.Add(NotiConst.NEW_USER);
            list.Add(NotiConst.USER_ADDED);
            list.Add(NotiConst.USER_DELETED);
            list.Add(NotiConst.CANCEL_SELECTED);
            list.Add(NotiConst.USER_SELECTED);
            list.Add(NotiConst.ADD_ROLE);
            list.Add(NotiConst.DEL_ROLE);
            return list;
        }

        public override void HandleNotification(INotification note)
        {
            UserVO user;
            RoleVO role;
            string userName;

            switch (note.Name)
            {
                case NotiConst.NEW_USER:
                    RolePanel.ClearForm();
                    break;
                case NotiConst.USER_ADDED:
                    user = (UserVO)note.Body;
                    userName = user == null ? "" : user.UserName;
                    role = new RoleVO(userName);
                    roleProxy.AddItem(role);
                    RolePanel.ClearForm();
                    break;
                case NotiConst.USER_UPDATED:
                    RolePanel.ClearForm();
                    break;
                case NotiConst.USER_DELETED:
                    RolePanel.ClearForm();
                    break;
                case NotiConst.CANCEL_SELECTED:
                    RolePanel.ClearForm();
                    break;
                case NotiConst.USER_SELECTED:
                    user = (UserVO)note.Body;
                    userName = user == null ? "" : user.UserName;
                    RolePanel.ShowUser(user, roleProxy.GetUserRoles(userName));
                    break;
                case NotiConst.ADD_ROLE:
                    userName = RolePanel.User == null ? "" : RolePanel.User.UserName;
                    RolePanel.ShowUserRoles(roleProxy.GetUserRoles(userName));
                    break;
                case NotiConst.DEL_ROLE:
                    userName = RolePanel.User == null ? "" : RolePanel.User.UserName;
                    RolePanel.ShowUserRoles(roleProxy.GetUserRoles(userName));
                    break;
            }
        }
    }
}
