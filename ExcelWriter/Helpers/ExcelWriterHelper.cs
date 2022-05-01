using System.Data;
using System.Globalization;
using OfficeOpenXml;
using RequestService.Common.Models;

namespace RequestService.ExcelWriter.Helpers;

/// <summary>
/// Writes specified data to excel.
/// </summary>
public static class ExcelWriterHelper
{
    public static void Write(IEnumerable<Request> requests, string excelPath)
    {
        using var excelPackage = new ExcelPackage();
        using var dataTable = new DataTable();
        PopulateRequestColumns(dataTable);
        PopulateRequestRows(dataTable, requests);
        Save(dataTable, excelPackage, excelPath);
    }

    private static void PopulateRequestColumns(DataTable dataTable)
    {
        // Could have used reflection but im tired.
        dataTable.Columns.Add("Id", typeof(string));
        dataTable.Columns.Add("Origin", typeof(int));
        dataTable.Columns.Add("Destination", typeof(int));
        dataTable.Columns.Add("Seats count", typeof(int));
        dataTable.Columns.Add("DateTime", typeof(string));
    }

    private static void PopulateRequestRows(DataTable dataTable, IEnumerable<Request> requests)
    {
        foreach (Request request in requests)
        {
            dataTable.Rows.Add(
                request.Id,
                request.Route?.Origin,
                request.Route?.Destination,
                request.SeatsCount,
                request.RequestDateTime);
        }
    }

    public static void Write(IEnumerable<RequestsPerHourSummary> summaries, string excelPath)
    {
        
    }

    private static void Save(DataTable dataTable, ExcelPackage excelPackage, string excelPath)
    {
        ExcelWorksheet worksheet =
            excelPackage.Workbook.Worksheets.Add(DateTime.Now.ToLocalTime().ToString(CultureInfo.InvariantCulture));
        worksheet.Cells["A1"].LoadFromDataTable(dataTable, true);
        var file = new FileInfo(excelPath);
        excelPackage.SaveAs(file);
    }
}