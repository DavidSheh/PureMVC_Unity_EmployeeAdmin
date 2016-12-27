/* 
    作者：Sheh伟伟
    博客：http://www.jianshu.com/users/fd3eec0ab0f2/latest_articles
    Github：https://github.com/DavidSheh
*/
using PureMVC.Patterns;
using PureMVC.Interfaces;
using UnityEngine;

namespace Demo.PureMVC.EmployeeAdmin.Controller
{
	public class AddRoleResultCommand : SimpleCommand, ICommand
	{
		public override void Execute(INotification notification)
		{
			bool result = (bool) notification.Body;

			if (!result) {
				Debug.LogWarning("Role already exists for this user! Please Click Add User Role");
			}
		}
	}
}
