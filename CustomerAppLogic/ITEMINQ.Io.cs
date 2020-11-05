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
    
    public partial class Iteminq
    {
        private FixedString<_20> SETITMDSC;
        private FixedString<_1> SFCOLOR;
        private FixedDecimal<_2, _0> SFSEL;
        private FixedDecimal<_9, _0> SFITEMNUM;
        private FixedString<_20> SFITEMDESC;
        private FixedDecimal<_9, _4> SFITMPRIC;
        private FixedDecimal<_12, _4> SFITMAVAIL;
        private FixedString<_10> CSRREC;
        private FixedString<_10> CSRFLD;
        private FixedString<_10> SCPGM;
        private FixedString<_20> SFOLDDESC;
        private FixedDecimal<_9, _0> SFITEM;
        private FixedString<_50> SFLNGDESC;
        private FixedString<_20> SFDESC;
        private FixedDecimal<_9, _4> SFPRICE;
        private FixedString<_10> SFUNIT;
        private FixedDecimal<_7, _4> SFWEIGHT;
        private FixedDecimal<_12, _4> SFAVAIL;
        private FixedDecimal<_12, _4> SFONORDR;
        private FixedString<_10> _at_PGMQ;
        private FixedString<_4> _at_MSGKY;
        private FixedString<Len<_1, _2, _8>> MESSAGETEXT;
        static ILayout ITEMDS_000 = Layout.Packed(9, 0);
        static ILayout ITEMDS_001 = Layout.FixedString(50);
        static ILayout ITEMDS_002 = Layout.FixedString(20);
        static ILayout ITEMDS_003 = Layout.Zoned(5, 0);
        static ILayout ITEMDS_004 = Layout.Packed(9, 4);
        static ILayout ITEMDS_005 = Layout.Packed(7, 4);
        static ILayout ITEMDS_006 = Layout.FixedString(10);
        static ILayout ITEMDS_007 = Layout.Packed(12, 4);
        static ILayout ITEMDS_008 = Layout.Packed(12, 4);
        private static Dictionary<string, string> ITEMMASTL2FormatIDs = new Dictionary<string, string>()
        {
            { "ITEM", "MYptlBMMlFkTrB+iC4clV88B0MA=" }
        };
        private static Dictionary<string, string> ITEMMASTL1FormatIDs = new Dictionary<string, string>()
        {
            { "ITEM", "MYptlBMMlFkTrB+iC4clV88B0MA=" }
        };

        private FixedDecimal<_9, _0> ITEMNUMBER
        {
            get
            {
                return this.ITEMDS.GetPacked(0, 9, 0);
            }
            set
            {
                this.ITEMDS.SetPacked(value, 0, 9, 0);
            }
        }
        private FixedString<_50> ITEMDESC
        {
            get
            {
                return this.ITEMDS.GetString(5, 50);
            }
            set
            {
                this.ITEMDS.SetString(((string)(value)).AsSpan(), 5, 50);
            }
        }
        private FixedString<_20> ITEMSHRTDS
        {
            get
            {
                return this.ITEMDS.GetString(55, 20);
            }
            set
            {
                this.ITEMDS.SetString(((string)(value)).AsSpan(), 55, 20);
            }
        }
        private FixedDecimal<_5, _0> ITEMCATGRY
        {
            get
            {
                return this.ITEMDS.GetZoned(75, 5, 0);
            }
            set
            {
                this.ITEMDS.SetZoned(value, 75, 5, 0);
            }
        }
        private FixedDecimal<_9, _4> ITEMPRICE
        {
            get
            {
                return this.ITEMDS.GetPacked(80, 9, 4);
            }
            set
            {
                this.ITEMDS.SetPacked(value, 80, 9, 4);
            }
        }
        private FixedDecimal<_7, _4> ITEMWEIGHT
        {
            get
            {
                return this.ITEMDS.GetPacked(85, 7, 4);
            }
            set
            {
                this.ITEMDS.SetPacked(value, 85, 7, 4);
            }
        }
        private FixedString<_10> ITEMUNIT
        {
            get
            {
                return this.ITEMDS.GetString(89, 10);
            }
            set
            {
                this.ITEMDS.SetString(((string)(value)).AsSpan(), 89, 10);
            }
        }
        private FixedDecimal<_12, _4> ITEMQTYAVL
        {
            get
            {
                return this.ITEMDS.GetPacked(99, 12, 4);
            }
            set
            {
                this.ITEMDS.SetPacked(value, 99, 12, 4);
            }
        }
        private FixedDecimal<_12, _4> ITEMQTYORD
        {
            get
            {
                return this.ITEMDS.GetPacked(106, 12, 4);
            }
            set
            {
                this.ITEMDS.SetPacked(value, 106, 12, 4);
            }
        }
        private void PopulateBufferITEMDSPFSFLC(AdgDataSet _dataSet)
        {
        }
        private void PopulateFieldsITEMDSPFSFLC(AdgDataSet _dataSet)
        {
            var _table = _dataSet.GetAdgTable("SFLC");
            System.Data.DataRow _row = _table.Row;
            SETITMDSC = ((string)(_row["SETITMDSC"]));
        }
        private void PopulateBufferITEMDSPFSFL1(AdgDataSet _dataSet)
        {
            var _table = _dataSet.GetAdgTable("SFL1");
            System.Data.DataRow _row = _table.Row;
            _row["SFCOLOR"] = ((string)(SFCOLOR));
            _row["SFSEL"] = ((decimal)(SFSEL));
            _row["SFITEMNUM"] = ((decimal)(SFITEMNUM));
            _row["SFITEMDESC"] = ((string)(SFITEMDESC));
            _row["SFITMPRIC"] = ((decimal)(SFITMPRIC));
            _row["SFITMAVAIL"] = ((decimal)(SFITMAVAIL));
        }
        private void PopulateFieldsITEMDSPFSFL1(AdgDataSet _dataSet)
        {
            var _table = _dataSet.GetAdgTable("SFL1");
            System.Data.DataRow _row = _table.Row;
            SFCOLOR = ((string)(_row["SFCOLOR"]));
            SFSEL = ((decimal)(_row["SFSEL"]));
            SFITEMNUM = ((decimal)(_row["SFITEMNUM"]));
            SFITEMDESC = ((string)(_row["SFITEMDESC"]));
            SFITMPRIC = ((decimal)(_row["SFITMPRIC"]));
            SFITMAVAIL = ((decimal)(_row["SFITMAVAIL"]));
        }
        private void PopulateBufferITEMDSPFITEMREC(AdgDataSet _dataSet)
        {
            var _table = _dataSet.GetAdgTable("ITEMREC");
            System.Data.DataRow _row = _table.Row;
            _row["CSRREC"] = ((string)(CSRREC));
            _row["CSRFLD"] = ((string)(CSRFLD));
            _row["SCPGM"] = ((string)(SCPGM));
            _row["SFOLDDESC"] = ((string)(SFOLDDESC));
            _row["SFITEM"] = ((decimal)(SFITEM));
            _row["SFLNGDESC"] = ((string)(SFLNGDESC));
            _row["SFDESC"] = ((string)(SFDESC));
            _row["SFPRICE"] = ((decimal)(SFPRICE));
            _row["SFUNIT"] = ((string)(SFUNIT));
            _row["SFWEIGHT"] = ((decimal)(SFWEIGHT));
            _row["SFAVAIL"] = ((decimal)(SFAVAIL));
            _row["SFONORDR"] = ((decimal)(SFONORDR));
        }
        private void PopulateFieldsITEMDSPFITEMREC(AdgDataSet _dataSet)
        {
            var _table = _dataSet.GetAdgTable("ITEMREC");
            System.Data.DataRow _row = _table.Row;
            CSRREC = ((string)(_row["CSRREC"]));
            CSRFLD = ((string)(_row["CSRFLD"]));
            SFLNGDESC = ((string)(_row["SFLNGDESC"]));
            SFDESC = ((string)(_row["SFDESC"]));
            SFPRICE = ((decimal)(_row["SFPRICE"]));
            SFUNIT = ((string)(_row["SFUNIT"]));
            SFWEIGHT = ((decimal)(_row["SFWEIGHT"]));
            SFAVAIL = ((decimal)(_row["SFAVAIL"]));
            SFONORDR = ((decimal)(_row["SFONORDR"]));
        }
        private void PopulateBufferITEMDSPFKEYS(AdgDataSet _dataSet)
        {
        }
        private void PopulateFieldsITEMDSPFKEYS(AdgDataSet _dataSet)
        {
        }
        private void PopulateBufferITEMDSPFMSGSFC(AdgDataSet _dataSet)
        {
            var _table = _dataSet.GetAdgTable("MSGSFC");
            System.Data.DataRow _row = _table.Row;
            _row["@PGMQ"] = ((string)(_at_PGMQ));
        }
        private void PopulateFieldsITEMDSPFMSGSFC(AdgDataSet _dataSet)
        {
            var _table = _dataSet.GetAdgTable("MSGSFC");
            System.Data.DataRow _row = _table.Row;
            _at_PGMQ = ((string)(_row["@PGMQ"]));
        }
        private void PopulateBufferITEMDSPFMSGSF(AdgDataSet _dataSet)
        {
            var _table = _dataSet.GetAdgTable("MSGSF");
            System.Data.DataRow _row = _table.Row;
            _row["@MSGKY"] = ((string)(_at_MSGKY));
            _row["@PGMQ"] = ((string)(_at_PGMQ));
            _row["MESSAGETEXT"] = ((string)(MESSAGETEXT));
        }
        private void PopulateFieldsITEMDSPFMSGSF(AdgDataSet _dataSet)
        {
            var _table = _dataSet.GetAdgTable("MSGSF");
            System.Data.DataRow _row = _table.Row;
            _at_MSGKY = ((string)(_row["@MSGKY"]));
            _at_PGMQ = ((string)(_row["@PGMQ"]));
            MESSAGETEXT = ((string)(_row["MESSAGETEXT"]));
        }
        private void PopulateBufferITEMDSPF(string _recordFormatName, AdgDataSet _dataSet)
        {
            if (string.Equals(_recordFormatName, "SFLC", System.StringComparison.CurrentCultureIgnoreCase))
            {
                this.PopulateBufferITEMDSPFSFLC(_dataSet);
            }
            else
            {
                if (string.Equals(_recordFormatName, "SFL1", System.StringComparison.CurrentCultureIgnoreCase))
                {
                    this.PopulateBufferITEMDSPFSFL1(_dataSet);
                }
                else
                {
                    if (string.Equals(_recordFormatName, "ITEMREC", System.StringComparison.CurrentCultureIgnoreCase))
                    {
                        this.PopulateBufferITEMDSPFITEMREC(_dataSet);
                    }
                    else
                    {
                        if (string.Equals(_recordFormatName, "KEYS", System.StringComparison.CurrentCultureIgnoreCase))
                        {
                            this.PopulateBufferITEMDSPFKEYS(_dataSet);
                        }
                        else
                        {
                            if (string.Equals(_recordFormatName, "MSGSFC", System.StringComparison.CurrentCultureIgnoreCase))
                            {
                                this.PopulateBufferITEMDSPFMSGSFC(_dataSet);
                            }
                            else
                            {
                                if (string.Equals(_recordFormatName, "MSGSF", System.StringComparison.CurrentCultureIgnoreCase))
                                {
                                    this.PopulateBufferITEMDSPFMSGSF(_dataSet);
                                }
                            }
                        }
                    }
                }
            }
        }
        private void PopulateFieldsITEMDSPF(string _recordFormatName, AdgDataSet _dataSet)
        {
            if (string.Equals(_recordFormatName, "SFLC", System.StringComparison.CurrentCultureIgnoreCase))
            {
                this.PopulateFieldsITEMDSPFSFLC(_dataSet);
            }
            else
            {
                if (string.Equals(_recordFormatName, "SFL1", System.StringComparison.CurrentCultureIgnoreCase))
                {
                    this.PopulateFieldsITEMDSPFSFL1(_dataSet);
                }
                else
                {
                    if (string.Equals(_recordFormatName, "ITEMREC", System.StringComparison.CurrentCultureIgnoreCase))
                    {
                        this.PopulateFieldsITEMDSPFITEMREC(_dataSet);
                    }
                    else
                    {
                        if (string.Equals(_recordFormatName, "KEYS", System.StringComparison.CurrentCultureIgnoreCase))
                        {
                            this.PopulateFieldsITEMDSPFKEYS(_dataSet);
                        }
                        else
                        {
                            if (string.Equals(_recordFormatName, "MSGSFC", System.StringComparison.CurrentCultureIgnoreCase))
                            {
                                this.PopulateFieldsITEMDSPFMSGSFC(_dataSet);
                            }
                            else
                            {
                                if (string.Equals(_recordFormatName, "MSGSF", System.StringComparison.CurrentCultureIgnoreCase))
                                {
                                    this.PopulateFieldsITEMDSPFMSGSF(_dataSet);
                                }
                            }
                        }
                    }
                }
            }
        }
        private void PopulateBufferITEMMASTL2(string _, AdgDataSet _dataSet)
        {
            var _table = _dataSet.GetAdgTable("*FILE");
            System.Data.DataRow _row = _table.Row;
            _row["ITEMNUMBER"] = ((decimal)(ITEMNUMBER));
            _row["ITEMDESC"] = ((string)(ITEMDESC));
            _row["ITEMSHRTDS"] = ((string)(ITEMSHRTDS));
            _row["ITEMCATGRY"] = ((decimal)(ITEMCATGRY));
            _row["ITEMPRICE"] = ((decimal)(ITEMPRICE));
            _row["ITEMWEIGHT"] = ((decimal)(ITEMWEIGHT));
            _row["ITEMUNIT"] = ((string)(ITEMUNIT));
            _row["ITEMQTYAVL"] = ((decimal)(ITEMQTYAVL));
            _row["ITEMQTYORD"] = ((decimal)(ITEMQTYORD));
        }
        private void PopulateFieldsITEMMASTL2(string _, AdgDataSet _dataSet)
        {
            var _table = _dataSet.SetActive("*FILE");
            System.Data.DataRow _row = _table.Row;
            ITEMNUMBER = ((decimal)(_row["ITEMNUMBER"]));
            ITEMDESC = ((string)(_row["ITEMDESC"]));
            ITEMSHRTDS = ((string)(_row["ITEMSHRTDS"]));
            ITEMCATGRY = ((decimal)(_row["ITEMCATGRY"]));
            ITEMPRICE = ((decimal)(_row["ITEMPRICE"]));
            ITEMWEIGHT = ((decimal)(_row["ITEMWEIGHT"]));
            ITEMUNIT = ((string)(_row["ITEMUNIT"]));
            ITEMQTYAVL = ((decimal)(_row["ITEMQTYAVL"]));
            ITEMQTYORD = ((decimal)(_row["ITEMQTYORD"]));
        }
        private void PopulateBufferITEMMASTL1(string _, AdgDataSet _dataSet)
        {
            var _table = _dataSet.GetAdgTable("*FILE");
            System.Data.DataRow _row = _table.Row;
            _row["ITEMNUMBER"] = ((decimal)(ITEMNUMBER));
            _row["ITEMDESC"] = ((string)(ITEMDESC));
            _row["ITEMSHRTDS"] = ((string)(ITEMSHRTDS));
            _row["ITEMCATGRY"] = ((decimal)(ITEMCATGRY));
            _row["ITEMPRICE"] = ((decimal)(ITEMPRICE));
            _row["ITEMWEIGHT"] = ((decimal)(ITEMWEIGHT));
            _row["ITEMUNIT"] = ((string)(ITEMUNIT));
            _row["ITEMQTYAVL"] = ((decimal)(ITEMQTYAVL));
            _row["ITEMQTYORD"] = ((decimal)(ITEMQTYORD));
        }
        private void PopulateFieldsITEMMASTL1(string _, AdgDataSet _dataSet)
        {
            var _table = _dataSet.SetActive("*FILE");
            System.Data.DataRow _row = _table.Row;
            ITEMNUMBER = ((decimal)(_row["ITEMNUMBER"]));
            ITEMDESC = ((string)(_row["ITEMDESC"]));
            ITEMSHRTDS = ((string)(_row["ITEMSHRTDS"]));
            ITEMCATGRY = ((decimal)(_row["ITEMCATGRY"]));
            ITEMPRICE = ((decimal)(_row["ITEMPRICE"]));
            ITEMWEIGHT = ((decimal)(_row["ITEMWEIGHT"]));
            ITEMUNIT = ((string)(_row["ITEMUNIT"]));
            ITEMQTYAVL = ((decimal)(_row["ITEMQTYAVL"]));
            ITEMQTYORD = ((decimal)(_row["ITEMQTYORD"]));
        }
    }
}
