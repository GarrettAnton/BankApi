using BankApiInterfaces.Models;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ExcelLib.ExcelWriter
{
    public class ExcelProcessor : BaseExcelProcessor
    {
        public ExcelProcessor(ILog iLog) : base(iLog)
        {
        }

        #region Operation with files
        public override void CreateExcelFile(string fullPath)
        {
            try
            {
                _log.Info($"Try to create file {fullPath}");
                _app.Workbooks.Add();
                _app.ActiveWorkbook.SaveAs(fullPath);
            }
            catch (Exception e)
            {
                _log.Error($"The error was ocured with the message '{e.Message}'");
                throw;
            }
        }

        public override void OpenExcelFile(string fullPath)
        {
            try
            {
                _log.Info($"Try to open file {fullPath}");
                _app.Workbooks.Open(fullPath);
            }
            catch (Exception e)
            {
                _log.Error($"The error was ocured with the message '{e.Message}'");
                throw;
            }
        }
        public override void SaveExcelFile()
        {
            try
            {
                _log.Info($"Try to save active file");
                _app.ActiveWorkbook.Save();
            }
            catch (Exception e)
            {
                _log.Error($"The error was ocured with the message '{e.Message}'");
                throw;
            }
        }

        public override void CloseExcelFile()
        {
            try
            {
                _log.Info($"Try to close active file");
                _app.ActiveWorkbook.Close();
            }
            catch (Exception e)
            {
                _log.Error($"The error was ocured with the message '{e.Message}'");
                throw;
            }
        }
        #endregion

        #region Get Cell
        public override ExcelCell GetCell(CellsAddress address)
        {
            try
            {
                _log.Info($"Try to return cell with address {address.Column}:{address.Row}");
                var cell = _app.ActiveWorkbook.ActiveSheet.Cells[address.Row, address.Column];
                return new ExcelCell()
                {
                    Address = address,
                    Value = cell.Value == null ? null : _app.ActiveWorkbook.ActiveSheet.Cells[address.Row, address.Column].Value.ToString()
                };
            }
            catch (Exception e)
            {
                _log.Error($"The error was ocured with the message '{e.Message}'");
                throw;
            }
        }

        public override List<ExcelCell> GetCells(List<CellsAddress> addresses)
        {
            if (addresses == null || !addresses.Any())
            {
                _log.Warn("The list of addresses is empty or null");
                throw new Exception("The list of addresses is empty or null");
            }
            var result = new List<ExcelCell>();
            addresses.ForEach(_ => result.Add(GetCell(_)));
            return result;
        }
        #endregion

        #region Write Cell
        public override void WriteCellIntoTheFile(ExcelCell cell)
        {
            try
            {
                _log.Info($"Try to write cell with address {cell.Address.Row}:{cell.Address.Column} and value {cell.Value}");
                _app.ActiveWorkbook.ActiveSheet.Cells[cell.Address.Row, cell.Address.Column].Value = cell.Value;
            }
            catch (Exception e)
            {
                _log.Error($"The error was ocured with the message '{e.Message}'");
                throw;
            }
        }

        public override void WriteCellsIntoTheFile(List<ExcelCell> cells)
        {
            if (cells == null || !cells.Any())
            {
                _log.Warn("The list of cells is empty or null");
                throw new Exception("The list of cells is empty or null");
            }
            try
            {
                foreach (var cell in cells)
                {
                    WriteCellIntoTheFile(cell);
                }
            }
            catch (Exception e)
            {
                _log.Error($"The error was ocured with the message '{e.Message}'");
                throw;
            }
        }
        #endregion

        public override void Dispose()
        {
            if (_app.ActiveWorkbook != null)
            {
                _log.Info($"Close all opened workbooks");
                _app.ActiveWorkbook.Close(false);
                _log.Info($"Opened workbooks were closed");
            }
        }
    }
}
