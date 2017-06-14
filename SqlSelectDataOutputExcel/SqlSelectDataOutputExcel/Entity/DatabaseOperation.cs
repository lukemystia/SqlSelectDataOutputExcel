using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SqlSelectDataOutputExcel.Models;

namespace SqlSelectDataOutputExcel.Entity
{
	/// <summary>
	/// DB操作関連
	/// </summary>
	internal static class DatabaseOperation
	{
		/// <summary>
		/// データとカラム名取得
		/// </summary>
		/// <param name="conn"></param>
		/// <param name="transaction"></param>
		/// <param name="sql"></param>
		/// <returns></returns>
		public static DataList Select(SqlConnection conn, SqlTransaction transaction, string sql, Dictionary<string, string> param)
		{
			try
			{
				var data = new DataList();

				using (var cmd = new SqlCommand(sql, conn, transaction))
				{
					if (param != null)
					{
						foreach (var item in param)
						{
							cmd.Parameters.Add(new SqlParameter(item.Key, item.Value));
						}
					}

					using (var reader = cmd.ExecuteReader())
					{
						if (!reader.HasRows)
						{
							return data;
						}

						while (reader.Read())
						{
							// データ
							data.Datas.Add(getValue(reader));
							if (data.Columns.Count >= 1)
							{
								continue;
							}

							// 列名
							data.Columns = getColumnName(reader);
						}
					}
				}

				return data;
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		/// <summary>
		/// データ取得
		/// </summary>
		/// <param name="reader"></param>
		/// <returns></returns>
		private static List<object> getValue(SqlDataReader reader)
		{
			try
			{
				var temp = new List<object>();

				for (int i = 0; i < reader.FieldCount; i++)
				{
					temp.Add(reader.GetValue(i));
				}

				return temp;
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		/// <summary>
		/// 列名取得
		/// </summary>
		/// <param name="reader"></param>
		/// <returns></returns>
		private static List<object> getColumnName(SqlDataReader reader)
		{
			try
			{
				var temp = new List<object>();

				for (int i = 0; i < reader.FieldCount; i++)
				{
					temp.Add(reader.GetName(i));
				}

				return temp;
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}
	}
}
