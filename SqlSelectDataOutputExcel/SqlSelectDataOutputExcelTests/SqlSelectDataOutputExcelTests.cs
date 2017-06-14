using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SqlSelectDataOutputExcel.Tests
{
	[TestClass()]
	public class SqlSelectDataOutputExcelTests
	{
		[TestMethod()]
		public void OutputCsvTest()
		{
			var connStr = "Data Source=localhost;Initial Catalog=db_name;User Id=user_id;Password=password;";
			var sql = "SELECT * FROM hoge_table WHERE id = @id OR id = @id2";

			var param = new Dictionary<string, string>();
			param.Add("@id", "1");
			param.Add("@id2", "2");

			var dirPath = @"C:\SqlSelectDataOutputExcelTests";
			var fileName = "test";

			SqlSelectDataOutputExcel.OutputCsv(dirPath, fileName, connStr, sql, param);

			Assert.AreEqual(System.IO.File.Exists($@"{dirPath}\{fileName}.csv"), true);
		}

		[TestMethod()]
		public void OutputCsvTest2()
		{
			var connStr = "Data Source=localhost;Initial Catalog=db_name;User Id=user_id;Password=password;";
			var sql = "SELECT * FROM hoge_table";

			var dirPath = @"C:\SqlSelectDataOutputExcelTests";
			var fileName = "test";

			SqlSelectDataOutputExcel.OutputCsv(dirPath, fileName, connStr, sql);

			Assert.AreEqual(System.IO.File.Exists($@"{dirPath}\{fileName}.csv"), true);
		}

		[TestMethod()]
		public void OutputXlsxTest()
		{
			var connStr = "Data Source=localhost;Initial Catalog=db_name;User Id=user_id;Password=password;";
			var sql = "SELECT * FROM hoge_table WHERE id = @id OR id = @id2";

			var param = new Dictionary<string, string>();
			param.Add("@id", "1");
			param.Add("@id2", "2");

			var dirPath = @"C:\SqlSelectDataOutputExcelTests";
			var fileName = "test";

			SqlSelectDataOutputExcel.OutputXlsx(dirPath, fileName, connStr, sql, param);

			Assert.AreEqual(System.IO.File.Exists($@"{dirPath}\{fileName}.xlsx"), true);
		}
	}
}