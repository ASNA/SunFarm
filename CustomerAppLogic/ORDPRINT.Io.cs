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
    
    public partial class Ordprint
    {
        private FixedDecimal<_9, _0> CMCUSTNO;
        private FixedString<_40> CMNAME;
        private FixedString<_35> CMADDR1;
        private FixedString<_35> CMADDR2;
        private FixedString<_30> CMCITY;
        private FixedString<_2> CMSTATE;
        private FixedString<_10> CMPOSTCODE;
        private FixedString<_1> CMACTIVE;
        private FixedDecimal<_10, _0> CMFAX;
        private FixedString<_20> CMPHONE;
        private FixedString<_5> CMUSRFLGS;
        private FixedString<_40> CMCONTACT;
        private FixedString<_40> CMCONEMAL;
        private FixedString<_1> CMYN01;
        private FixedString<_1> CMYN02;
        private FixedString<_1> CMYN03;
        private FixedDecimal<_9, _0> ITEMNUMBER;
        private FixedString<_50> ITEMDESC;
        private FixedString<_20> ITEMSHRTDS;
        private FixedDecimal<_5, _0> ITEMCATGRY;
        private FixedDecimal<_9, _4> ITEMPRICE;
        private FixedDecimal<_7, _4> ITEMWEIGHT;
        private FixedString<_10> ITEMUNIT;
        private FixedDecimal<_12, _4> ITEMQTYAVL;
        private FixedDecimal<_12, _4> ITEMQTYORD;
        private FixedDecimal<_9, _0> ORDNUM;
        private FixedDecimal<_9, _0> ORDCUST;
        private FixedDate<_ISO, _Default> ORDDATE;
        private FixedDate<_ISO, _Default> ORDSHPDATE;
        private FixedDecimal<_8, _0> ORDDELDATE;
        private FixedDecimal<_3, _0> ORDSHPVIA;
        private FixedDecimal<_13, _4> ORDAMOUNT;
        private FixedDecimal<_13, _4> ORDWEIGHT;
        private FixedDecimal<_9, _2> ORDSHPCHG;
        private FixedDecimal<_9, _0> ORDLINNUM;
        private FixedDecimal<_9, _0> ORDNUMBER;
        private FixedDecimal<_9, _0> ORDITEMNUM;
        private FixedDecimal<_9, _3> ORDLQTY;
        private FixedDecimal<_9, _3> ORDLQTYBKO;
        private FixedDecimal<_9, _3> ORDLQTYSHP;
        private FixedDecimal<_9, _3> ORDLQTYDEL;
        private FixedDecimal<_3, _0> CARRIERCOD;
        private FixedString<_30> CARRIERDES;
        private FixedDate<_ISO, _Default> wDateUSA1;
        private FixedDate<_ISO, _Default> wDateUSA2;
        private static Dictionary<string, string> CUSTOMERL1FormatIDs = new Dictionary<string, string>()
        {
            { "RCUSTOMER", "6su4S42+ard0ZHitdjHOFT1WPw0=" }
        };
        private static Dictionary<string, string> ITEMMASTL1FormatIDs = new Dictionary<string, string>()
        {
            { "ITEM", "MYptlBMMlFkTrB+iC4clV88B0MA=" }
        };
        private static Dictionary<string, string> ORDERHDRL2FormatIDs = new Dictionary<string, string>()
        {
            { "RORDRHDR", "HGKtY5i8LuqjN1v8eIQidC7WD6Q=" }
        };
        private static Dictionary<string, string> ORDERLINL2FormatIDs = new Dictionary<string, string>()
        {
            { "RORDLINE", "ZgLaFmi11VaM21ivXYmcwM/P/Bo=" }
        };
        private static Dictionary<string, string> SHIPPINGFormatIDs = new Dictionary<string, string>()
        {
            { "SHIP", "Q6zxbCCsNWjkgTEOfwEN+1R7BGw=" }
        };
        private static Dictionary<string, string> QPRINTFormatIDs = new Dictionary<string, string>()
        {
            { "PrtNmeLine", "xaKeeR3HgCej8bXGcKCjYsdCV3E=" },
            { "PrtAdrLine", "Ok8DSHqldbnYHZTodtCAI7sTPzI=" },
            { "PrtHdrLine", "zWpVClNNbmCA3/xIY45/cnXnSGY=" },
            { "PrtDtlLineF", "iB8MLHcywo6PGAI6grHf4fjE2ZY=" },
            { "PrtTotLine", "YCTtlEXduWbC6bREEuK4aQUjweQ=" }
        };

        private void PopulateBufferCUSTOMERL1(string _, AdgDataSet _dataSet)
        {
            var _table = _dataSet.GetAdgTable("*FILE");
            System.Data.DataRow _row = _table.Row;
            _row["CMCUSTNO"] = ((decimal)(CMCUSTNO));
            _row["CMNAME"] = ((string)(CMNAME));
            _row["CMADDR1"] = ((string)(CMADDR1));
            _row["CMADDR2"] = ((string)(CMADDR2));
            _row["CMCITY"] = ((string)(CMCITY));
            _row["CMSTATE"] = ((string)(CMSTATE));
            _row["CMPOSTCODE"] = ((string)(CMPOSTCODE));
            _row["CMACTIVE"] = ((string)(CMACTIVE));
            _row["CMFAX"] = ((decimal)(CMFAX));
            _row["CMPHONE"] = ((string)(CMPHONE));
            _row["CMUSRFLGS"] = ((string)(CMUSRFLGS));
            _row["CMCONTACT"] = ((string)(CMCONTACT));
            _row["CMCONEMAL"] = ((string)(CMCONEMAL));
            _row["CMYN01"] = ((string)(CMYN01));
            _row["CMYN02"] = ((string)(CMYN02));
            _row["CMYN03"] = ((string)(CMYN03));
        }
        private void PopulateFieldsCUSTOMERL1(string _, AdgDataSet _dataSet)
        {
            var _table = _dataSet.SetActive("*FILE");
            System.Data.DataRow _row = _table.Row;
            CMCUSTNO = ((decimal)(_row["CMCUSTNO"]));
            CMNAME = ((string)(_row["CMNAME"]));
            CMADDR1 = ((string)(_row["CMADDR1"]));
            CMADDR2 = ((string)(_row["CMADDR2"]));
            CMCITY = ((string)(_row["CMCITY"]));
            CMSTATE = ((string)(_row["CMSTATE"]));
            CMPOSTCODE = ((string)(_row["CMPOSTCODE"]));
            CMACTIVE = ((string)(_row["CMACTIVE"]));
            CMFAX = ((decimal)(_row["CMFAX"]));
            CMPHONE = ((string)(_row["CMPHONE"]));
            CMUSRFLGS = ((string)(_row["CMUSRFLGS"]));
            CMCONTACT = ((string)(_row["CMCONTACT"]));
            CMCONEMAL = ((string)(_row["CMCONEMAL"]));
            CMYN01 = ((string)(_row["CMYN01"]));
            CMYN02 = ((string)(_row["CMYN02"]));
            CMYN03 = ((string)(_row["CMYN03"]));
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
        private void PopulateBufferORDERHDRL2(string _, AdgDataSet _dataSet)
        {
            var _table = _dataSet.GetAdgTable("*FILE");
            System.Data.DataRow _row = _table.Row;
            _row["ORDNUM"] = ((decimal)(ORDNUM));
            _row["ORDCUST"] = ((decimal)(ORDCUST));
            _row["ORDDATE"] = ((System.DateTime)(ORDDATE));
            _row["ORDSHPDATE"] = ((System.DateTime)(ORDSHPDATE));
            _row["ORDDELDATE"] = ((decimal)(ORDDELDATE));
            _row["ORDSHPVIA"] = ((decimal)(ORDSHPVIA));
            _row["ORDAMOUNT"] = ((decimal)(ORDAMOUNT));
            _row["ORDWEIGHT"] = ((decimal)(ORDWEIGHT));
            _row["ORDSHPCHG"] = ((decimal)(ORDSHPCHG));
        }
        private void PopulateFieldsORDERHDRL2(string _, AdgDataSet _dataSet)
        {
            var _table = _dataSet.SetActive("*FILE");
            System.Data.DataRow _row = _table.Row;
            ORDNUM = ((decimal)(_row["ORDNUM"]));
            ORDCUST = ((decimal)(_row["ORDCUST"]));
            ORDDATE = ((System.DateTime)(_row["ORDDATE"]));
            ORDSHPDATE = ((System.DateTime)(_row["ORDSHPDATE"]));
            ORDDELDATE = ((decimal)(_row["ORDDELDATE"]));
            ORDSHPVIA = ((decimal)(_row["ORDSHPVIA"]));
            ORDAMOUNT = ((decimal)(_row["ORDAMOUNT"]));
            ORDWEIGHT = ((decimal)(_row["ORDWEIGHT"]));
            ORDSHPCHG = ((decimal)(_row["ORDSHPCHG"]));
        }
        private void PopulateBufferORDERLINL2(string _, AdgDataSet _dataSet)
        {
            var _table = _dataSet.GetAdgTable("*FILE");
            System.Data.DataRow _row = _table.Row;
            _row["ORDLINNUM"] = ((decimal)(ORDLINNUM));
            _row["ORDNUMBER"] = ((decimal)(ORDNUMBER));
            _row["ORDITEMNUM"] = ((decimal)(ORDITEMNUM));
            _row["ORDLQTY"] = ((decimal)(ORDLQTY));
            _row["ORDLQTYBKO"] = ((decimal)(ORDLQTYBKO));
            _row["ORDLQTYSHP"] = ((decimal)(ORDLQTYSHP));
            _row["ORDLQTYDEL"] = ((decimal)(ORDLQTYDEL));
        }
        private void PopulateFieldsORDERLINL2(string _, AdgDataSet _dataSet)
        {
            var _table = _dataSet.SetActive("*FILE");
            System.Data.DataRow _row = _table.Row;
            ORDLINNUM = ((decimal)(_row["ORDLINNUM"]));
            ORDNUMBER = ((decimal)(_row["ORDNUMBER"]));
            ORDITEMNUM = ((decimal)(_row["ORDITEMNUM"]));
            ORDLQTY = ((decimal)(_row["ORDLQTY"]));
            ORDLQTYBKO = ((decimal)(_row["ORDLQTYBKO"]));
            ORDLQTYSHP = ((decimal)(_row["ORDLQTYSHP"]));
            ORDLQTYDEL = ((decimal)(_row["ORDLQTYDEL"]));
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
        private void PopulateBufferQPRINTPrtNmeLine(AdgDataSet _dataSet)
        {
            var _table = _dataSet.GetAdgTable("PrtNmeLine");
            System.Data.DataRow _row = _table.Row;
            _row["wUserId"] = ((string)(wUserId));
            _row["wORDNBR"] = ((string)(wORDNBR));
            _row["wCUSTNO"] = ((string)(wCUSTNO));
        }
        private void PopulateFieldsQPRINTPrtNmeLine(AdgDataSet _dataSet)
        {
        }
        private void PopulateBufferQPRINTPrtAdrLine(AdgDataSet _dataSet)
        {
            var _table = _dataSet.GetAdgTable("PrtAdrLine");
            System.Data.DataRow _row = _table.Row;
            _row["wPRTADDR"] = ((string)(wPRTADDR));
        }
        private void PopulateFieldsQPRINTPrtAdrLine(AdgDataSet _dataSet)
        {
        }
        private void PopulateBufferQPRINTPrtHdrLine(AdgDataSet _dataSet)
        {
            var _table = _dataSet.GetAdgTable("PrtHdrLine");
            System.Data.DataRow _row = _table.Row;
            _row["wDateUSA1"] = ((System.DateTime)(wDateUSA1));
            _row["wDateUSA2"] = ((System.DateTime)(wDateUSA2));
            _row["CARRIERDES"] = ((string)(CARRIERDES));
            _row["wORDAMOUNT"] = ((string)(wORDAMOUNT));
        }
        private void PopulateFieldsQPRINTPrtHdrLine(AdgDataSet _dataSet)
        {
        }
        private void PopulateBufferQPRINTPrtDtlLineF(AdgDataSet _dataSet)
        {
            var _table = _dataSet.GetAdgTable("PrtDtlLineF");
            System.Data.DataRow _row = _table.Row;
            _row["wLINNUM"] = ((string)(wLINNUM));
            _row["wITEMNUM"] = ((decimal)(wITEMNUM));
            _row["ITEMSHRTDS"] = ((string)(ITEMSHRTDS));
            _row["ORDLQTY"] = ((decimal)(ORDLQTY));
            _row["ORDLQTYBKO"] = ((decimal)(ORDLQTYBKO));
            _row["ORDLQTYSHP"] = ((decimal)(ORDLQTYSHP));
            _row["ITEMPRICE"] = ((decimal)(ITEMPRICE));
            _row["wEXTAMT"] = ((decimal)(wEXTAMT));
        }
        private void PopulateFieldsQPRINTPrtDtlLineF(AdgDataSet _dataSet)
        {
        }
        private void PopulateBufferQPRINTPrtTotLine(AdgDataSet _dataSet)
        {
            var _table = _dataSet.GetAdgTable("PrtTotLine");
            System.Data.DataRow _row = _table.Row;
            _row["wCountLne"] = ((decimal)(wCountLne));
            _row["wORDNBR"] = ((string)(wORDNBR));
        }
        private void PopulateFieldsQPRINTPrtTotLine(AdgDataSet _dataSet)
        {
        }
        private void PopulateBufferQPRINT(string _recordFormatName, AdgDataSet _dataSet)
        {
            if (string.Equals(_recordFormatName, "PrtNmeLine", System.StringComparison.CurrentCultureIgnoreCase))
            {
                this.PopulateBufferQPRINTPrtNmeLine(_dataSet);
            }
            else
            {
                if (string.Equals(_recordFormatName, "PrtAdrLine", System.StringComparison.CurrentCultureIgnoreCase))
                {
                    this.PopulateBufferQPRINTPrtAdrLine(_dataSet);
                }
                else
                {
                    if (string.Equals(_recordFormatName, "PrtHdrLine", System.StringComparison.CurrentCultureIgnoreCase))
                    {
                        this.PopulateBufferQPRINTPrtHdrLine(_dataSet);
                    }
                    else
                    {
                        if (string.Equals(_recordFormatName, "PrtDtlLineF", System.StringComparison.CurrentCultureIgnoreCase))
                        {
                            this.PopulateBufferQPRINTPrtDtlLineF(_dataSet);
                        }
                        else
                        {
                            if (string.Equals(_recordFormatName, "PrtTotLine", System.StringComparison.CurrentCultureIgnoreCase))
                            {
                                this.PopulateBufferQPRINTPrtTotLine(_dataSet);
                            }
                        }
                    }
                }
            }
        }
        private void PopulateFieldsQPRINT(string _recordFormatName, AdgDataSet _dataSet)
        {
            if (string.Equals(_recordFormatName, "PrtNmeLine", System.StringComparison.CurrentCultureIgnoreCase))
            {
                this.PopulateFieldsQPRINTPrtNmeLine(_dataSet);
            }
            else
            {
                if (string.Equals(_recordFormatName, "PrtAdrLine", System.StringComparison.CurrentCultureIgnoreCase))
                {
                    this.PopulateFieldsQPRINTPrtAdrLine(_dataSet);
                }
                else
                {
                    if (string.Equals(_recordFormatName, "PrtHdrLine", System.StringComparison.CurrentCultureIgnoreCase))
                    {
                        this.PopulateFieldsQPRINTPrtHdrLine(_dataSet);
                    }
                    else
                    {
                        if (string.Equals(_recordFormatName, "PrtDtlLineF", System.StringComparison.CurrentCultureIgnoreCase))
                        {
                            this.PopulateFieldsQPRINTPrtDtlLineF(_dataSet);
                        }
                        else
                        {
                            if (string.Equals(_recordFormatName, "PrtTotLine", System.StringComparison.CurrentCultureIgnoreCase))
                            {
                                this.PopulateFieldsQPRINTPrtTotLine(_dataSet);
                            }
                        }
                    }
                }
            }
        }
    }
}
