/* 
    作者：Sheh伟伟
    博客：http://www.jianshu.com/users/fd3eec0ab0f2/latest_articles
    Github：https://github.com/DavidSheh
*/

using PureMVC.Patterns;
using PureMVC.Interfaces;

using Demo.PureMVC.EmployeeAdmin.Model.VO;
using Demo.PureMVC.EmployeeAdmin.Model;

namespace Demo.PureMVC.EmployeeAdmin.Controller
{
	public class DeleteUserCommand : SimpleCommand, ICommand
	{
		/// <summary>
		/// retrieve the user and role proxies and delete the user
		/// and his roles. then send the USER_DELETED notification
		/// </summary>
		/// <param name="notification"></param>
		public override void Execute(INotification notification)
		{
			UserVO user = (UserVO) notification.Body;
			UserProxy userProxy = (UserProxy) Facade.RetrieveProxy(UserProxy.NAME);
			RoleProxy roleProxy = (RoleProxy) Facade.RetrieveProxy(RoleProxy.NAME);
			userProxy.DeleteItem(user);
			roleProxy.DeleteItem(user);
			SendNotification(NotiConst.USER_DELETED);
		}
	}
}
