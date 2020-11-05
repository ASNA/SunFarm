﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using ASNA.DataGate.Client;
using ASNA.DataGate.Common;
using ASNA.QSys;
using System;
using System.Collections.Generic;


namespace SunFarm.Customers
{
    
    public partial class Custprmpt
    {
        private FixedDecimal<_4, _0> SFLRRN;
        private FixedDecimal<_1, _0> SFLSEL;
        private FixedString<_5> SFLVALUE;
        private FixedString<_32> SFLDESC;
        private FixedDecimal<_3, _0> CARRIERCOD;
        private FixedString<_30> CARRIERDES;
        private static Dictionary<string, string> SHIPPINGFormatIDs = new Dictionary<string, string>()
        {
            { "SHIP", "Q6zxbCCsNWjkgTEOfwEN+1R7BGw=" }
        };

        private void PopulateBufferCUSTPRMPMYWINDOW(AdgDataSet _dataSet)
        {
            var _table = _dataSet.GetAdgTable("MYWINDOW");
            System.Data.DataRow _row = _table.Row;
            _row["WINTITLE"] = ((string)(WinTitle));
        }
        private void PopulateFieldsCUSTPRMPMYWINDOW(AdgDataSet _dataSet)
        {
        }
        private void PopulateBufferCUSTPRMPSFLC(AdgDataSet _dataSet)
        {
            var _table = _dataSet.GetAdgTable("SFLC");
            System.Data.DataRow _row = _table.Row;
            _row["SFLRRN"] = ((decimal)(SFLRRN));
        }
        private void PopulateFieldsCUSTPRMPSFLC(AdgDataSet _dataSet)
        {
            var _table = _dataSet.GetAdgTable("SFLC");
            System.Data.DataRow _row = _table.Row;
            SFLRRN = ((decimal)(_row["SFLRRN"]));
        }
        private void PopulateBufferCUSTPRMPSFL1(AdgDataSet _dataSet)
        {
            var _table = _dataSet.GetAdgTable("SFL1");
            System.Data.DataRow _row = _table.Row;
            _row["SFLSEL"] = ((decimal)(SFLSEL));
            _row["SFLVALUE"] = ((string)(SFLVALUE));
            _row["SFLDESC"] = ((string)(SFLDESC));
        }
        private void PopulateFieldsCUSTPRMPSFL1(AdgDataSet _dataSet)
        {
            var _table = _dataSet.GetAdgTable("SFL1");
            System.Data.DataRow _row = _table.Row;
            SFLSEL = ((decimal)(_row["SFLSEL"]));
            SFLVALUE = ((string)(_row["SFLVALUE"]));
            SFLDESC = ((string)(_row["SFLDESC"]));
        }
        private void PopulateBufferCUSTPRMPDUMMY(AdgDataSet _dataSet)
        {
        }
        private void PopulateFieldsCUSTPRMPDUMMY(AdgDataSet _dataSet)
        {
        }
        private void PopulateBufferCUSTPRMP(string _recordFormatName, AdgDataSet _dataSet)
        {
            if (string.Equals(_recordFormatName, "MYWINDOW", System.StringComparison.CurrentCultureIgnoreCase))
            {
                this.PopulateBufferCUSTPRMPMYWINDOW(_dataSet);
            }
            else
            {
                if (string.Equals(_recordFormatName, "SFLC", System.StringComparison.CurrentCultureIgnoreCase))
                {
                    this.PopulateBufferCUSTPRMPSFLC(_dataSet);
                }
                else
                {
                    if (string.Equals(_recordFormatName, "SFL1", System.StringComparison.CurrentCultureIgnoreCase))
                    {
                        this.PopulateBufferCUSTPRMPSFL1(_dataSet);
                    }
                    else
                    {
                        if (string.Equals(_recordFormatName, "DUMMY", System.StringComparison.CurrentCultureIgnoreCase))
                        {
                            this.PopulateBufferCUSTPRMPDUMMY(_dataSet);
                        }
                    }
                }
            }
        }
        private void PopulateFieldsCUSTPRMP(string _recordFormatName, AdgDataSet _dataSet)
        {
            if (string.Equals(_recordFormatName, "MYWINDOW", System.StringComparison.CurrentCultureIgnoreCase))
            {
                this.PopulateFieldsCUSTPRMPMYWINDOW(_dataSet);
            }
            else
            {
                if (string.Equals(_recordFormatName, "SFLC", System.StringComparison.CurrentCultureIgnoreCase))
                {
                    this.PopulateFieldsCUSTPRMPSFLC(_dataSet);
                }
                else
                {
                    if (string.Equals(_recordFormatName, "SFL1", System.StringComparison.CurrentCultureIgnoreCase))
                    {
                        this.PopulateFieldsCUSTPRMPSFL1(_dataSet);
                    }
                    else
                    {
                        if (string.Equals(_recordFormatName, "DUMMY", System.StringComparison.CurrentCultureIgnoreCase))
                        {
                            this.PopulateFieldsCUSTPRMPDUMMY(_dataSet);
                        }
                    }
                }
            }
        }
        private void PopulateBufferSHIPPING(string _, AdgDataSet _dataSet)
        {
            var _table = _dataSet.GetAdgTable("*FILE");
            System.Data.DataRow _row = _table.Row;
            _row["CARRIERCOD"] = ((decimal)(CARRIERCOD));
            _row["CARRIERDES"] = ((string)(CARRIERDES));
        }
        private void PopulateFieldsSHIPPING(string _, AdgDataSet _dataSet)
        {
            var _table = _dataSet.SetActive("*FILE");
            System.Data.DataRow _row = _table.Row;
            CARRIERCOD = ((decimal)(_row["CARRIERCOD"]));
            CARRIERDES = ((string)(_row["CARRIERDES"]));
        }
    }
}
