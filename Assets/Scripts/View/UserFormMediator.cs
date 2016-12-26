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
    public class UserFormMediator : Mediator, IMediator
    {
        private UserProxy userProxy;

        public new const string NAME = "UserFormMediator";

        public UserFormMediator(UserForm viewComponent)
            : base(NAME, viewComponent)
        {
            //UserForm.AddUser += new EventHandler(UserForm_AddUser);
            //UserForm.UpdateUser += new EventHandler(UserForm_UpdateUser);
            //UserForm.CancelUser += new EventHandler(UserForm_CancelUser);
        }

        public override void OnRegister()
        {
            base.OnRegister();
            userProxy = (UserProxy)Facade.RetrieveProxy(UserProxy.NAME);
        }

        private UserForm UserForm
        {
            get { return (UserForm)ViewComponent; }
        }

        void UserForm_AddUser(object sender, EventArgs e)
        {
            //UserVO user = UserForm.User;
            //userProxy.AddItem(user);
            //SendNotification(ApplicationFacade.USER_ADDED, user);
            //UserForm.ClearForm();
        }

        void UserForm_UpdateUser(object sender, EventArgs e)
        {
            //UserVO user = UserForm.User;
            //userProxy.UpdateItem(user);
            //SendNotification(ApplicationFacade.USER_UPDATED, user);
            //UserForm.ClearForm();
        }

        void UserForm_CancelUser(object sender, EventArgs e)
        {
            SendNotification(NotiConst.CANCEL_SELECTED);
            //UserForm.ClearForm();
        }

        public override IList<string> ListNotificationInterests()
        {
            IList<string> list = new List<string>();
            list.Add(NotiConst.NEW_USER);
            list.Add(NotiConst.USER_DELETED);
            list.Add(NotiConst.USER_SELECTED);
            return list;
        }

        public override void HandleNotification(INotification note)
        {
            UserVO user;

            switch (note.Name)
            {
                case NotiConst.NEW_USER:
                    user = (UserVO)note.Body;
                    //UserForm.ShowUser(user, UserFormMode.ADD);
                    break;

                case NotiConst.USER_DELETED:
                    //UserForm.ClearForm();
                    break;

                case NotiConst.USER_SELECTED:
                    user = (UserVO)note.Body;
                    //UserForm.ShowUser(user, UserFormMode.EDIT);
                    break;

            }
        }
    }
}
