﻿using System.Collections.Generic;
using GemBox.Spreadsheet;

namespace ExcelCsvReaderApp.Util
{
    public class ExcelReader
    {
        private readonly string _excelFilePath;

        public ExcelReader(string excelFilePath)
        {
            _excelFilePath = excelFilePath;
        }

        public string GetCellValue(string sheetName, int rowNumber, int columnNumber)
        {
            // If using Professional version, put your serial key below
            SpreadsheetInfo.SetLicense("FREE-LIMITED-KEY");

            // Load Excel workbook from file path
            var workbook = ExcelFile.Load(_excelFilePath);

            // Select the worksheet by name
            var worksheet = workbook.Worksheets[sheetName];

            // Display sheet's name
            // Console.WriteLine("SheetName is: " + worksheet.Name);

            // Select the row by row number
            var row = worksheet.Rows[rowNumber - 1];

            // Select the cell by row and column number
            var cell = row.Cells[columnNumber - 1];

            return cell.Value.ToString();
        }

        public int GetRowNumberByCellValue(string sheetName, string expectedCellValue, int columnNumber)
        {
            SpreadsheetInfo.SetLicense("FREE-LIMITED-KEY");
            var workbook = ExcelFile.Load(_excelFilePath);
            var worksheet = workbook.Worksheets[sheetName];
            var numberOfRows = worksheet.Rows.Count;

            var rowNumber = 0;

            for (var i = 1; i <= numberOfRows; i++)
            {
                var cellValue = worksheet.Cells[(i - 1), (columnNumber - 1)].Value;
                if (cellValue != null && cellValue.ToString().Equals(expectedCellValue))
                {
                    rowNumber = i;
                    break;
                }
            }

            if (rowNumber == 0)
            {
                throw new KeyNotFoundException(
                    "Failed to find '" + expectedCellValue + "' in sheet '" + sheetName + "'");
            }

            return rowNumber;
        }

        public int GetNumberOfDuplicatesByCellValue(string sheetName, string expectedCellValue, int columnNumber)
        {
            SpreadsheetInfo.SetLicense("FREE-LIMITED-KEY");
            var workbook = ExcelFile.Load(_excelFilePath);
            var worksheet = workbook.Worksheets[sheetName];
            var numberOfRows = worksheet.Rows.Count;

            var count = 0;

            for (var i = 1; i <= numberOfRows; i++)
            {
                var cellValue = worksheet.Cells[(i - 1), (columnNumber - 1)].Value;
                if (cellValue != null && cellValue.ToString().Equals(expectedCellValue))
                {
                    count++;
                }
            }

            return count;
        }
    }
}