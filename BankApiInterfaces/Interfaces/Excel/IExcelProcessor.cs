using BankApiInterfaces.Models;
using System;
using System.Collections.Generic;

namespace BankApiInterfaces.Interfaces.Excel
{
    public interface IExcelProcessor : IDisposable
    {
        void OpenExcelFile(string fullPath);
        void CreateExcelFile(string fullPath);
        void SaveExcelFile();
        void CloseExcelFile();
        void WriteCellsIntoTheFile(List<ExcelCell> cells);
        void WriteCellIntoTheFile(ExcelCell cell);
        List<ExcelCell> GetCells(List<CellsAddress> addresses);
        ExcelCell GetCell(CellsAddress address);
        
    }
}
