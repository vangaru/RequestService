namespace RequestService.ExcelWriter.Services;

/// <summary>
/// Service for writing requests data to excel.
/// </summary>
public interface IExcelWriterService
{
    /// <summary>
    /// Writes all requests from database to excel file.
    /// </summary>
    /// <param name="excelPath">Path to the excel file to write records to.</param>
    public void WriteRequestsFromDb(string excelPath);
    
    /// <summary>
    /// Writes requests summary to the excel file.
    /// </summary>
    /// <param name="excelPath">Path to the excel file to write records to.</param>
    public void WriteRequestsSummary(string excelPath);

    /// <summary>
    /// Writes generated requests to the database.
    /// </summary>
    /// <param name="excelPath">Path to the excel file to write records to.</param>
    public void WriteGeneratedSummary(string excelPath);
}