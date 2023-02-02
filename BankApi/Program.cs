using BankApiInterfaces.Interfaces;
using BankApiInterfaces.Interfaces.Configuration;
using BankApiInterfaces.Interfaces.Excel;
using BankApiInterfaces.Interfaces.RestApi;
using BankApiInterfaces.Models;
using BankInterfacesBuilder.Builders;
using System;
using System.IO;

namespace BankApi
{
    internal class Program
    {
        const int maxRowValue = 1048576;
        static void Main(string[] args)
        {
            IBankInterfacesBuilder bankInterfacesBuilder = new InterfacesBuilder();
            IConfigurationProvider cp = bankInterfacesBuilder.GetConfigurationProvider();
            using (IExchangeRateReciever reciever = bankInterfacesBuilder.GetExchangeRateReciever(cp))
            using (IExcelProcessor excelProcessor = bankInterfacesBuilder.GetExcelProcessor())
            {
                try
                {
                    if (!IsFileExist(Environment.CurrentDirectory + "\\" + cp.ExcelFileName))
                    {
                        FirstOneFileCreation(excelProcessor, cp);
                    }
                    else
                    {
                        excelProcessor.OpenExcelFile(Environment.CurrentDirectory + "\\" + cp.ExcelFileName);
                    }
                    var dayCell = GetFirstEmptyCell(excelProcessor, "A");
                    dayCell.Value = DateTime.Now.ToString("dd.MM.yyyy");
                    excelProcessor.WriteCellIntoTheFile(dayCell);
                    if (cp.IsUSD)
                    {
                        var dollarCell = GetFirstEmptyCell(excelProcessor, "B");
                        dollarCell.Value = reciever.USD.Replace(",", ".");
                        excelProcessor.WriteCellIntoTheFile(dollarCell);
                    }
                    if (cp.IsEUR)
                    {
                        var euroCell = GetFirstEmptyCell(excelProcessor, "C");
                        euroCell.Value = reciever.EUR.Replace(",", "."); ;
                        excelProcessor.WriteCellIntoTheFile(euroCell);
                    }
                    excelProcessor.SaveExcelFile();
                }
                catch (Exception e)
                {
                    excelProcessor.Dispose();
                    reciever.Dispose();
                }
            }
        }

        public static bool IsFileExist (string path)
        {
            return File.Exists(path);
        }

        public static ExcelCell GetFirstEmptyCell(IExcelProcessor excelProcessor, string column)
        {
            int i = 2;
            while (i <= maxRowValue)
            {
                var cell = excelProcessor.GetCell(new CellsAddress() { Column = column, Row = i });
                if (string.IsNullOrEmpty(cell.Value))
                {
                    return cell;
                }
                i++;
            }
            return null;
        }

        public static void FirstOneFileCreation(IExcelProcessor excelProcessor, IConfigurationProvider cp)
        {
            excelProcessor.CreateExcelFile(Environment.CurrentDirectory + "\\" + cp.ExcelFileName);
            ExcelCell dayTitle = new ExcelCell()
            {
                Address = new CellsAddress()
                {
                    Column = "A",
                    Row = 1
                },
                Value = "Day"
            };
            excelProcessor.WriteCellIntoTheFile(dayTitle);
            if (cp.IsUSD)
            {
                ExcelCell dollarTitle = new ExcelCell()
                {
                    Address = new CellsAddress()
                    {
                        Column = "B",
                        Row = 1
                    },
                    Value = "Dollar"
                };
                excelProcessor.WriteCellIntoTheFile(dollarTitle);
            }
            if (cp.IsEUR)
            {
                ExcelCell euroTitle = new ExcelCell()
                {
                    Address = new CellsAddress()
                    {
                        Column = "C",
                        Row = 1
                    },
                    Value = "Euro"
                };
                excelProcessor.WriteCellIntoTheFile(euroTitle);
            }
        }
    }
}
