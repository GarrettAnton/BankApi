using BankApiInterfaces.Interfaces.Excel;
using BankApiInterfaces.Models;
using log4net;
using Microsoft.Office.Interop.Excel;
using System.Collections.Generic;

namespace ExcelLib.ExcelWriter
{
    public abstract class BaseExcelProcessor : IExcelProcessor
    {
        internal ILog _log;
        internal Application _app;
        public BaseExcelProcessor(ILog iLog)
        {
            _log = iLog;
            _app = new Application();
        }

        public abstract void CloseExcelFile();
        public abstract void CreateExcelFile(string fullPath);
        public abstract void OpenExcelFile(string fullPath);
        public abstract void SaveExcelFile();
        public abstract ExcelCell GetCell(CellsAddress address);
        public abstract List<ExcelCell> GetCells(List<CellsAddress> addresses);
        public abstract void WriteCellIntoTheFile(ExcelCell cell);
        public abstract void WriteCellsIntoTheFile(List<ExcelCell> cells);
        public abstract void Dispose();
    }
}
