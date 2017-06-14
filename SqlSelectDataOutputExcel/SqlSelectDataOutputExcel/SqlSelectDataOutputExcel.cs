using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SqlSelectDataOutputExcel.Entity;
using SqlServer;
using SqlSelectDataOutputExcel.Models;
using System.IO;
using CommonProc.File;
using SqlSelectDataOutputExcel.Bizlogic;

namespace SqlSelectDataOutputExcel
{
	/// <summary>
	/// SQL SERVER 実行結果を csv または xlsx ファイル出力
	/// </summary>
	public static class SqlSelectDataOutputExcel
	{
		/// <summary>
		/// csv出力
		///	データ取得
		///	出力ファイルパス作成
		///	csv書込み
		/// </summary>
		/// <param name="outputDirPath">出力フォルダパス</param>
		/// <param name="fileName">出力ファイル名</param>
		/// <param name="connStr">DB接続文字列</param>
		/// <param name="sql">実行するSQL文</param>
		/// <param name="param">バインドパラメータのDictionary[key:バインドパラメータ,val:置き換えるデータ値]</param>
		public static void OutputCsv(string outputDirPath, string fileName, string connStr, string sql, Dictionary<string, string> param = default(Dictionary<string, string>))
		{
			try
			{
				if (!Directory.Exists(outputDirPath))
				{
					throw new DirectoryNotFoundException($"{outputDirPath} というフォルダは存在しません");
				}

				var data = getData(connStr, sql, param);

				var filePath = $"{Path.Combine(outputDirPath, fileName)}.csv";
				filePath = MakePath.CreateUniqueFileName(filePath);

				File.AppendAllText(filePath, data.CsvData, Encoding.UTF8);
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		/// <summary>
		/// xlsx出力
		///	データ取得
		///	出力ファイルパス作成
		///	csv書込み
		/// </summary>
		/// <param name="outputDirPath">出力フォルダパス</param>
		/// <param name="fileName">出力ファイル名</param>
		/// <param name="connStr">DB接続文字列</param>
		/// <param name="sql">実行するSQL文</param>
		/// <param name="param">バインドパラメータのDictionary</param>
		public static void OutputXlsx(string outputDirPath, string fileName, string connStr, string sql, Dictionary<string, string> param = default(Dictionary<string, string>))
		{
			try
			{
				if (!Directory.Exists(outputDirPath))
				{
					throw new DirectoryNotFoundException($"{outputDirPath} というフォルダは存在しません");
				}

				var data = getData(connStr, sql, param);

				var filePath = $"{Path.Combine(outputDirPath, fileName)}.xlsx";
				filePath = MakePath.CreateUniqueFileName(filePath);

				WriteXlsx.Write(filePath, data);
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		/// <summary>
		/// データ取得
		/// </summary>
		/// <param name="connStr">DB接続文字列</param>
		/// <param name="sql">実行するSQL文</param>
		/// <param name="param">バインドパラメータのDictionary</param>
		/// <returns></returns>
		private static DataList getData(string connStr, string sql, Dictionary<string, string> param)
		{
			try
			{
				var conn = new Connect(connStr);

				using (conn.conn)
				using (var transaction = conn.conn.BeginTransaction())
				{
					return DatabaseOperation.Select(conn.conn, transaction, sql, param);
				}
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}
	}
}
