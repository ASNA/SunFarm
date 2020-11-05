// Translated from AVR to C# on 11/5/2020 at 11:57:57 AM by ASNA Serengeti® version 16.0.3.0
// ASNA Monarch(R) version 10.0.24.0 at 11/4/2020
// Migrated source location: library NUTSNBOLTS, file QRPGSRC, member CUSTPRTS

using ASNA.QSys;
using ASNA.DataGate.Common;
using SunFarm.Customers.Application_Job;

using System;


namespace SunFarm.Customers
{
    [ASNA.QSys.HostServices.ActivationGroup("*DFTACTGRP")]
    [ProgramEntry("_ENTRY")]
    public partial class Custprts : ASNA.QSys.HostServices.Program
    {
        protected Indicator _INLR;
        protected Indicator _INRT;
        protected IndicatorArray<Len<_1, _0, _0>> _IN;
        DatabaseFile CSMASTERL1;
        DatabaseFile CUSTOMERL1;
        PrintFile QPRINT;
        internal const decimal QPRINT_PrintLineHeight = 50; // Notes: Units are LOMETRIC (one hundredth of a centimeter). The constant used came from the Global Directive defaults.
        //********************************************************************
        //   U1 Print sales
        //   U2 Print credits
        //********************************************************************
        DataStructure pNumberAlf = new DataStructure(9);
        FixedDecimal<_9, _0> pNumber { get => pNumberAlf.GetZoned(0, 9, 0); set => pNumberAlf.SetZoned(value, 0, 9, 0); } 

        class pNumberAlf_Class : LocalScopeDS
        {
            internal FixedDecimal<_9, _0> pNumber { get => DS.GetZoned(0, 9, 0); set => DS.SetZoned(value, 0, 9, 0); } 
            internal pNumberAlf_Class() : base(9){}
        }

        FixedDecimal<_7, _0> wCount;
        FixedDecimal<_4, _0> wPrevYr;
        FixedDecimal<_4, _0> wPrtYr;
        FixedString<Len<_1, _2, _0>> wUnderline;
        short X;
        DataStructure CUSTSL;
        FixedDecimalArrayInDS<_12, _11, _2> SlsArray; 

#region Program Status Data Structure
        DataStructure _DS3 = new DataStructure(10);
        FixedString<_10> sUserId { get => _DS3.GetString(0, 10); set => _DS3.SetString(((string)value).AsSpan(), 0, 10); } 

#endregion
        //**********************************************************************
        //**********************************************************************
#region Constructor and Dispose 
        public Custprts()
        {
            _IN = new IndicatorArray<Len<_1, _0, _0>>((char[])null);
            _instanceInit();
            CSMASTERL1.Overrider = Job;
            CSMASTERL1.Open(Job.Database, AccessMode.Read, false, false, ServerCursors.Default);
            CUSTOMERL1.Overrider = Job;
            CUSTOMERL1.Open(Job.Database, AccessMode.Read, false, false, ServerCursors.Default);
            QPRINT.Printer = "Microsoft Print to PDF";
            QPRINT.Overrider = Job;
            QPRINT.ManuscriptPath = ASNA.QSys.HostServices.Program.Spooler.GetNewFilePath(QPRINT.DclPrintFileName);
            QPRINT.Open(Job.PrinterDB);
        }

        override public void Dispose(bool disposing)
        {
            if (disposing)
            {
                
                CSMASTERL1.Close();
                CUSTOMERL1.Close();
                QPRINT.Close();
            }
            base.Dispose(disposing);
        }


#endregion
        void StarEntry(int _pc_parms)
        {
            do
            {
                //----------------------------------------------------------------------
                _IN[80] = CUSTOMERL1.Chain(true, pNumber) ? '0' : '1';
                if ((bool)_IN[80])
                    CMNAME = "????????".MoveLeft(CMNAME);
                QPRINT.Write("PrtHeading", _IN.Array);
                _INOF = (Indicator)QPRINT.InOverflow;
                CSMASTERL1.Seek(SeekMode.SetLL, pNumber);
                _INLR = CSMASTERL1.ReadNextEqual(true, pNumber) ? '0' : '1';
                //----------------------------------------------------------------------
                while (_INLR == '0')
                {
                    if (CSYEAR == wPrevYr)
                    {
                        wPrtYr = 0;
                    }
                    else
                    {
                        wPrtYr = CSYEAR;
                        wPrevYr = CSYEAR;
                    }
                    ChkTheInfo();
                    _INLR = CSMASTERL1.ReadNextEqual(true, pNumber) ? '0' : '1';
                }
                //----------------------------------------------------------------------
                QPRINT.Write("PrtCount", _IN.Array);
                _INOF = (Indicator)QPRINT.InOverflow;
                //**********************************************************************
                //**********************************************************************
            } while (!(bool)_INLR);
        }
        void ChkTheInfo()
        {
            for (X = 1; X <= 12; X++)
            {
                if (Job.GetSwitch(1) == '0' && (SlsArray[(int)X - 1] > 0))
                    SlsArray[(int)X - 1] = 0;
                if (Job.GetSwitch(2) == '0' && (SlsArray[(int)X - 1] < 0))
                    SlsArray[(int)X - 1] = 0;
            }
            // IS THERE ANYTHING TO PRINT? -----------------------------------------
            for (X = 1; X <= 12; X++)
            {
                if (SlsArray[(int)X - 1] != 0)
                {
                    QPRINT.Write("PrtDetail", _IN.Array);
                    _INOF = (Indicator)QPRINT.InOverflow;
                    wCount += 1;
                    break;
                }
            }
        }
        //**********************************************************************
        //**********************************************************************

        Indicator _INOF;
#region Entry and activation methods for *ENTRY
        public static object _classFactory()
        {
            return new Custprts();
        }

        int _parms;

        void __ENTRY(out Indicator __inLR, bool _isNew, object _pNumberAlf)
        {
            int _pc_parms = 1;
            bool _cleanup = true;
            if (_pNumberAlf is IMODS)
                pNumberAlf.Load((_pNumberAlf as IMODS).DumpAll());
            else if (_pNumberAlf is IDS)
                pNumberAlf.Load((_pNumberAlf as IDS).Dump());
            else
                pNumberAlf.Load(_pNumberAlf.ToString());
            __inLR = '0';
            try
            {
                _parms = _pc_parms;
                StarEntry(_pc_parms);
            }
            catch(Return)
            {
            }
            catch(System.Threading.ThreadAbortException)
            {
                _cleanup = false;
                __inLR = '1';
            }
            finally
            {
                if (_cleanup)
                {
                    if (_pNumberAlf is IMODS)
                        (_pNumberAlf as IMODS).LoadAll(pNumberAlf.Dump());
                    else if (_pNumberAlf is IDS)
                        (_pNumberAlf as IDS).Load(pNumberAlf.Dump());
                    else
                        _pNumberAlf = pNumberAlf.Dump();
                    __inLR = _INLR;
                }
            }
        }

        public static void _ENTRY(ICaller _caller, out Indicator __inLR, object pNumberAlf)
        {
            IActivationManager _manager = ProcedureSupport.ActivationManager;
            bool _isNew = false;
            Custprts _instance = null;
            __inLR = '0';
            _instance = _manager.GetInstance(typeof(Custprts), _classFactory, _caller, out _isNew) as Custprts;
            try
            {
                _instance.__ENTRY(out __inLR, _isNew, pNumberAlf);
            }
            catch
            {
                __inLR = '1';
                throw;
            }
            finally
            {
                _manager.DisposeInstance(_instance, __inLR);
            }
        }
#endregion

        void _instanceInit()
        {
            X = 0;
            wUnderline = new string('-', 120);
            wPrtYr = 9999;
            wPrevYr = 9999;
            wCount = 0;
            CSMASTERL1 = new DatabaseFile(PopulateBufferCSMASTERL1, PopulateFieldsCSMASTERL1, null, "CSMASTERL1", "*LIBL/CSMASTERL1", CSMASTERL1FormatIDs)
            { IsDefaultRFN = true };
            CUSTOMERL1 = new DatabaseFile(PopulateBufferCUSTOMERL1, PopulateFieldsCUSTOMERL1, null, "CUSTOMERL1", "*LIBL/CUSTOMERL1", CUSTOMERL1FormatIDs)
            { IsDefaultRFN = true };
            QPRINT = new PrintFile(PopulateBufferQPRINT, "QPRINT", "IRONMGRPRT\\CUSTPRTS", QPRINTFormatIDs);
            CUSTSL = new DataStructure(CUSTSL_000, CUSTSL_001, CUSTSL_002, CUSTSL_003, CUSTSL_004, CUSTSL_005, CUSTSL_006, 
                CUSTSL_007, CUSTSL_008, CUSTSL_009, CUSTSL_010, CUSTSL_011, CUSTSL_012, CUSTSL_013, CUSTSL_014);
            SlsArray = new FixedDecimalArrayInDS<_12, _11, _2>(CUSTSL, LayoutType.Packed, 10);
        }
    }

}
