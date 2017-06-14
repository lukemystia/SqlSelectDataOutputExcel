using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SqlSelectDataOutputExcel.Models
{
	/// <summary>
	/// 取得データとカラム名
	/// </summary>
	internal class DataList
	{
		/// <summary>
		/// 取得データ
		/// </summary>
		public List<List<object>> Datas { get; set; } = new List<List<object>>();

		/// <summary>
		/// カラム名
		/// </summary>
		public List<object> Columns { get; set; } = new List<object>();

		/// <summary>
		/// csvデータ
		/// </summary>
		public string CsvData => string.Join(",", Columns) + "\r\n" + string.Join("\r\n", Datas.Select(x => string.Join(",", x)));
	}
}
