/* 
    作者：Sheh伟伟
    博客：http://www.jianshu.com/users/fd3eec0ab0f2/latest_articles
    Github：https://github.com/DavidSheh
*/

using System.Collections.Generic;

using PureMVC.Patterns;
using PureMVC.Interfaces;
using Demo.PureMVC.EmployeeAdmin.Model.VO;
using Demo.PureMVC.EmployeeAdmin.Model.Enum;

namespace Demo.PureMVC.EmployeeAdmin.Model
{
	public class RoleProxy : Proxy, IProxy
	{
		public new const string NAME = "RoleProxy";

		public RoleProxy()
			: base(NAME, new List<RoleVO>())
		{
            // generate some test data
            AddItem(new RoleVO("Sheh伟伟",
                new RoleEnum[] { RoleEnum.ADMIN }));

            AddItem(new RoleVO("lstooge",
				new RoleEnum[] { RoleEnum.PAYROLL, RoleEnum.EMP_BENEFITS }));

			AddItem(new RoleVO("cstooge",
				new RoleEnum[] { RoleEnum.ACCT_PAY, RoleEnum.ACCT_RCV, RoleEnum.GEN_LEDGER }));

			AddItem(new RoleVO("mstooge",
				new RoleEnum[] { RoleEnum.INVENTORY, RoleEnum.PRODUCTION, RoleEnum.SALES, RoleEnum.SHIPPING }));
		}

		/// <summary>
		/// get the data property cast to the appropriate type 
		/// </summary>
		public IList<RoleVO> Roles
		{
			get { return (IList<RoleVO>) Data; }
		}

		/// <summary>
		/// add an item to the data
		/// </summary>
		/// <param name="role"></param>
		public void AddItem(RoleVO role)
		{
			Roles.Add(role);
		}

		/// <summary>
		/// delete an item from the data
		/// </summary>
		/// <param name="user"></param>
		public void DeleteItem(UserVO user)
		{
			for (int i = 0; i < Roles.Count; i++)
			{
				if (Roles[i].UserName.Equals(user.UserName))
				{
					Roles.RemoveAt(i);
					break;
				}
			}
		}

		/// <summary>
		/// determine if the user has a given role
		/// </summary>
		/// <param name="user"></param>
		/// <param name="role"></param>
		/// <returns></returns>
		public bool DoesUserHaveRole(UserVO user, RoleEnum role)
		{
			bool hasRole = false;

			for (int i = 0; i < Roles.Count; i++)
			{
				if (Roles[i].UserName.Equals(user.UserName))
				{
					IList<RoleEnum> userRoles = Roles[i].Roles;

					foreach (RoleEnum curRole in userRoles)
					{
						if (curRole.Equals(role))
						{
							hasRole = true;
							break;
						}
					}
				}
			}

			return hasRole;
		}

		/// <summary>
		/// add a role to this user 
		/// </summary>
		/// <param name="user"></param>
		/// <param name="role"></param>
		public void AddRoleToUser(UserVO user, RoleEnum role)
		{
			bool result = false;

			if (!DoesUserHaveRole(user, role))
			{
				for (int i = 0; i < Roles.Count; i++)
				{
					if (Roles[i].UserName.Equals(user.UserName))
					{
						IList<RoleEnum> userRoles = Roles[i].Roles;
						userRoles.Add(role);
						result = true;
						break;
					}
				}
			}

			SendNotification(NotiConst.ADD_ROLE, result);
		}

		// remove a role from the user
		public void RemoveRoleFromUser(UserVO user, RoleEnum role)
		{
			if (DoesUserHaveRole(user, role))
			{
				for (int i = 0; i < Roles.Count; i++)
				{
					if (Roles[i].UserName.Equals(user.UserName))
					{
						IList<RoleEnum> userRoles = Roles[i].Roles;

						foreach (RoleEnum curRole in userRoles)
						{
							if (curRole.Equals(role))
							{
								userRoles.Remove(role);

                                SendNotification(NotiConst.DEL_ROLE, role);
                                break;
							}
						}

						break;
					}
				}
			}
		}

		// get a user"s roles
		public IList<RoleEnum> GetUserRoles(string username)
		{
			IList<RoleEnum> userRoles = new List<RoleEnum>();

			for (int i = 0; i < Roles.Count; i++)
			{
				if (Roles[i].UserName.Equals(username))
				{
					userRoles = Roles[i].Roles;
					break;
				}
			}

			return userRoles;
		}
	}
}
