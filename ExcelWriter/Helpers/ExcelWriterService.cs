using System.Data;
using OfficeOpenXml;
using RequestService.Common.Models;

namespace RequestService.ExcelWriter.Helpers;

/// <summary>
/// Writes specified data to excel.
/// </summary>
public static class ExcelWriterService
{
    public static void Write(IEnumerable<Request> requests, string excelPath)
    {
        using var excelPackage = new ExcelPackage();
        using var dataTable = new DataTable();
        PopulateRequestColumns(dataTable);
        PopulateRequestRows(dataTable, requests);
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
}