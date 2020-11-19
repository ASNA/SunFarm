﻿using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using ASNA.QSys.ExpoModel;

// Migrated on 11/4/2020 at 12:31 PM by ASNA Monarch(R) version 10.0.24.0
// Legacy location: library ERNUBO, file QDDSSRC, member CUSTDSPF


namespace SunFarm.Customers.CustomerAppViews
{
    [
        BindProperties,
        DisplayPage(FunctionKeys = "F3 03"),
        ExportSource(CCSID = 37)
    ]
    public class CUSTDSPF : DisplayPageModel
    {
        public SFLC_Model SFLC { get; set; }
        public CUSTREC_Model CUSTREC { get; set; }
        public SALESREC_Model SALESREC { get; set; }
        public KEYS_Model KEYS { get; set; }
        public MSGSFC_Model MSGSFC { get; set; }

        public CUSTDSPF()
        {
            SFLC = new SFLC_Model();
            CUSTREC = new CUSTREC_Model();
            SALESREC = new SALESREC_Model();
            KEYS = new KEYS_Model();
            MSGSFC = new MSGSFC_Model();
        }

        [
            SubfileControl(ClearRecords : "90",
                FunctionKeys = "PageUp 51:!76;PageDown 50:!77",
                DisplayFields = "!90",
                DisplayRecords = "!90",
                Size = 20,
                IsExpandable = false,
                EraseFormats = "CUSTREC SALESREC"
            )
        ]
        public class SFLC_Model : SubfileControlModel
        {
            public List<SFL1_Model> SFL1 { get; set; } = new List<SFL1_Model>();

            [Char(10, InputOnly=true)]
            public string SETNAME { get; set; }

            public class SFL1_Model : SubfileRecordModel
            {
                [Char(1, Protect = "*True")]
                public string SFCOLOR { get; set; }

                [Values(typeof(Decimal),"00","02","03","05","07","09","10","11")]
                [Dec(2, 0)]
                public decimal SFSEL { get; set; }

                [Dec(6, 0)]
                public decimal SFCUSTNO { get; private set; } // CUSTOMER NUMBER

                [Char(40)]
                public string SFNAME1 { get; private set; }

                [Char(25)]
                public string SFCSZ { get; private set; } // CITY-STATE-ZIP

            }

        }

        [
            Record(FunctionKeys = "F4 04;F6 06:!30;F11 11:!30;F12 12",
                EraseFormats = "SFLC KEYS SALESREC"
            )
        ]
        public class CUSTREC_Model : RecordModel
        {
            [Char(10)]
            private string CSRREC
            {
                get => CursorLocationFormatName;
                set { }
            }

            [Char(10)]
            private string CSRFLD
            {
                get => CursorLocationFieldName;
                set { }
            }

            [Char(10)]
            public string SCPGM { get; private set; }

            [Dec(6, 0)]
            public decimal SFCUSTNO { get; private set; } // CUSTOMER NUMBER

            [Char(40)]
            public string SFOLDNAME { get; private set; }

            [Char(40)]
            public string SFNAME { get; set; }

            [Char(35)]
            public string SFADDR1 { get; set; }

            [Char(35)]
            public string SFADDR2 { get; set; }

            [Char(30)]
            public string SFCITY { get; set; }

            [Char(2)]
            public string SFSTATE { get; set; }

            [Char(10)]
            public string SFPOSTCODE { get; set; }

            [Dec(10, 0)]
            public decimal SFFAX { get; set; }

            [Char(20)]
            public string SFPHONE { get; set; }

            [Char(1)]
            public string SFSTATUS { get; set; }

            [Char(40)]
            public string SFCONTACT { get; set; }

            [Char(40)]
            public string SFCONEMAL { get; set; }

            [Char(1)]
            public string SFYN01 { get; set; }

            [Dec(11, 2)]
            public decimal CSSALES01 { get; private set; }

            [Dec(11, 2)]
            public decimal CSSALES02 { get; private set; }

            [Dec(11, 2)]
            public decimal CSSALES03 { get; private set; }

            [Dec(11, 2)]
            public decimal CSSALES04 { get; private set; }

            [Dec(11, 2)]
            public decimal CSSALES05 { get; private set; }

            [Dec(11, 2)]
            public decimal CSSALES06 { get; private set; }

            [Dec(11, 2)]
            public decimal CSSALES07 { get; private set; }

            [Dec(11, 2)]
            public decimal CSSALES08 { get; private set; }

            [Dec(11, 2)]
            public decimal CSSALES09 { get; private set; }

            [Dec(11, 2)]
            public decimal CSSALES10 { get; private set; }

            [Dec(11, 2)]
            public decimal CSSALES11 { get; private set; }

            [Dec(11, 2)]
            public decimal CSSALES12 { get; private set; }
        }

        [
            Record(FunctionKeys = "F12 12",
                EraseFormats = "SFLC KEYS CUSTREC"
            )
        ]
        public class SALESREC_Model : RecordModel
        {
            [Char(10)]
            public string SCPGM { get; private set; }

            [Dec(6, 0)]
            public decimal SFCUSTNO { get; private set; } // CUSTOMER NUMBER

            [Char(40)]
            public string SFNAME { get; private set; }

            [Dec(13, 2)]
            public decimal SFSALES { get; private set; }

            [Dec(13, 2)]
            public decimal SFRETURNS { get; private set; }

        }

        [
            Record(EraseFormats = "CUSTREC SALESREC")
        ]
        public class KEYS_Model : RecordModel
        {
        }

        [
            SubfileControl(ClearRecords : "",
                ProgramQ = "@PGMQ",
                DisplayFields = "*True",
                DisplayRecords = "*True",
                InitializeRecords = "*True",
                Size = 10
            )
        ]
        public class MSGSFC_Model : SubfileControlModel
        {
            public List<MSGSF_Model> MSGSF { get; set; } = new List<MSGSF_Model>();

            [Char(10, Alias = "@PGMQ")]
            public string aPGMQ { get; private set; }

            [
                SubfileRecord(IsMessageSubfile = true)
            ]
            public class MSGSF_Model : SubfileRecordModel
            {
                [Char(4, Alias = "@MSGKY")]
                public string aMSGKY { get; private set; }

                [Char(10, Alias = "@PGMQ")]
                public string aPGMQ { get; private set; }

                [Char(128)]
                public string MessageText { get; private set; }

            }

        }

    }
}
