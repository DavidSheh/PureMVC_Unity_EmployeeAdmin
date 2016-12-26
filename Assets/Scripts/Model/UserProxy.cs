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
	public class UserProxy : Proxy, IProxy
	{
		public new const string NAME = "UserProxy";

		public UserProxy()
			: base(NAME, new List<UserVO>())
		{
            // generate some test data	
            AddItem(new UserVO("Sheh伟伟", "伟伟", "Sheh", "QQ群253999688", "sheh", DeptEnum.QC));
            AddItem(new UserVO("lstooge", "Larry", "Stooge", "larry@stooges.com", "ijk456", DeptEnum.ACCT));
			AddItem(new UserVO("cstooge", "Curly", "Stooge", "curly@stooges.com", "xyz987", DeptEnum.SALES));
			AddItem(new UserVO("mstooge", "Moe", "Stooge", "moe@stooges.com", "abc123", DeptEnum.PLANT));
		}

		/// <summary>
		/// Return data property cast to proper type
		/// </summary>
		public IList<UserVO> Users
		{
			get { return (IList<UserVO>) Data; }
		}

		/// <summary>
		/// add an item to the data
		/// </summary>
		/// <param name="user"></param>
		public void AddItem(UserVO user)
		{
			Users.Add(user);
		}

		/// <summary>
		/// update an item in the data
		/// </summary>
		/// <param name="user"></param>
		public void UpdateItem(UserVO user)
		{
			for (int i = 0; i < Users.Count; i++)
			{
				if (Users[i].UserName.Equals(user.UserName))
				{
					Users[i] = user;
					break;
				}
			}
		}

		/// <summary>
		/// delete an item in the data
		/// </summary>
		/// <param name="user"></param>
		public void DeleteItem(UserVO user)
		{
			for (int i = 0; i < Users.Count; i++)
			{
				if (Users[i].UserName.Equals(user.UserName))
				{
					Users.RemoveAt(i);
					break;
				}
			}
		}
	}
}
