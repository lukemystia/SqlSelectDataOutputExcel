using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClosedXML.Excel;
using SqlSelectDataOutputExcel.Models;

namespace SqlSelectDataOutputExcel.Bizlogic
{
	/// <summary>
	/// xlsx ファイル書込み
	/// </summary>
	internal static class WriteXlsx
	{
		/// <summary>
		/// 書込み
		/// </summary>
		/// <param name="filePath"></param>
		/// <param name="dl"></param>
		public static void Write(string filePath, DataList dl)
		{
			try
			{
				using (var workbook = new XLWorkbook(XLEventTracking.Disabled))
				using (var worksheet = workbook.Worksheets.Add("DataSheet"))
				{
					var workRow = 0;
					var workCell = worksheet.Cell(++workRow, 1);

					// カラム名
					workCell = writeData(workCell, dl.Columns, true);
					workCell = worksheet.Cell(++workRow, 1);

					// データ
					dl.Datas.ForEach(x =>
					{
						workCell = writeData(workCell, x, false);
						workCell = worksheet.Cell(++workRow, 1);
					});

					// セルサイズ自動調整
					worksheet.ColumnsUsed().AdjustToContents();
					workbook.SaveAs(filePath);
				}
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		/// <summary>
		/// 書込み
		/// </summary>
		/// <param name="workCell"></param>
		/// <param name="dataList"></param>
		/// <param name="color"></param>
		private static IXLCell writeData(IXLCell workCell, List<object> dataList, bool color)
		{
			try
			{
				dataList.ForEach(x =>
				{
					// 書込み
					workCell.Value = x;

					// 見た目
					if (color)
					{
						workCell.Style.Fill.BackgroundColor = XLColor.LightGreen;
					}

					workCell.Style
						.Border.SetTopBorder(XLBorderStyleValues.Thin)
						.Border.SetBottomBorder(XLBorderStyleValues.Thin)
						.Border.SetLeftBorder(XLBorderStyleValues.Thin)
						.Border.SetRightBorder(XLBorderStyleValues.Thin);

					// 作業セルを右に
					workCell = workCell.CellRight();
				});

				return workCell;
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}
	}
}
