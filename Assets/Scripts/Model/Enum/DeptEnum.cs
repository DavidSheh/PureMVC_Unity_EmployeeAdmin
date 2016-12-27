/* 
    作者：Sheh伟伟
    博客：http://www.jianshu.com/users/fd3eec0ab0f2/latest_articles
    Github：https://github.com/DavidSheh
*/

using System.Collections.Generic;

namespace Demo.PureMVC.EmployeeAdmin.Model.Enum
{
	public class DeptEnum
	{
		public static readonly DeptEnum NONE_SELECTED = new DeptEnum("--None Selected--", -1);
		public static readonly DeptEnum ACCT = new DeptEnum("Accounting", 0);
		public static readonly DeptEnum SALES = new DeptEnum("Sales", 1);
		public static readonly DeptEnum PLANT = new DeptEnum("Plant", 2);
		public static readonly DeptEnum SHIPPING = new DeptEnum("Shipping", 3);
		public static readonly DeptEnum QC = new DeptEnum("Quality Control", 4);

		public int Ordinal
		{
			get { return m_ordinal; }
		}
		private int m_ordinal;

		public string Value
		{
			get { return m_value; }
		}
		private string m_value;

		public DeptEnum(string value, int ordinal)
		{
			m_value = value;
			m_ordinal = ordinal;
		}

		public static IList<DeptEnum> List
		{
			get
			{
				List<DeptEnum> l = new List<DeptEnum>();
				l.Add(ACCT);
				l.Add(SALES);
				l.Add(PLANT);
                l.Add(SHIPPING);
                l.Add(QC);
                return l;
			}
		}

		public static IList<DeptEnum> ComboList
		{
			get
			{
				IList<DeptEnum> l = List;
				l.Insert(0, NONE_SELECTED);
				return l;
			}
		}

		public bool Equals(DeptEnum e)
		{
			if (e == null) return false;
			return (Ordinal == e.Ordinal && Value == e.Value);
		}

		public override string ToString()
		{
			return Value;
		}
	}
}
