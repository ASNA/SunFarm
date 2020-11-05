// Translated from AVR to C# on 11/5/2020 at 11:57:57 AM by ASNA Serengeti® version 16.0.3.0
// ASNA Monarch(R) version 10.0.24.0 at 11/4/2020
// Migrated source location: library NUTSNBOLTS, file QRPGSRC, member CUSTCALC

using ASNA.QSys;
using ASNA.DataGate.Common;
using SunFarm.Customers.Application_Job;

using System;


namespace SunFarm.Customers
{
    [ASNA.QSys.HostServices.ActivationGroup("*DFTACTGRP")]
    [ProgramEntry("_ENTRY")]
    public partial class Custcalc : ASNA.QSys.HostServices.Program
    {
        protected Indicator _INLR;
        protected Indicator _INRT;
        protected IndicatorArray<Len<_1, _0, _0>> _IN;
        //********************************************************************
        // JB   8/30/2004   Added option to display billing info.

        //********************************************************************
        DatabaseFile CUSTOMERL1;
        DatabaseFile CSMASTERL1;
        //********************************************************************
        //  ** ENTRY Parm List **
        FixedDecimal<_9, _0> Cust_lb_;
        FixedDecimal<_13, _2> Sales;
        FixedDecimal<_13, _2> TempAmt;
        FixedDecimal<_13, _2> Returns;
        FixedString<_9> Cust_lb_Ch;
        FixedString<_13> SalesCh;
        FixedString<_13> ReturnsCh;

        FixedDecimal<_1, _0> SaleEvent;
        FixedDecimal<_1, _0> ReturnEvent;

        DataStructure CUSTSL;
        FixedDecimalArrayInDS<_12, _11, _2> SlsArray; 

        //********************************************************************
#region Constructor and Dispose 
        public Custcalc()
        {
            _IN = new IndicatorArray<Len<_1, _0, _0>>((char[])null);
            _instanceInit();
            CSMASTERL1.Overrider = Job;
            CSMASTERL1.Open(Job.Database, AccessMode.Read, false, false, ServerCursors.Default);
            CUSTOMERL1.Overrider = Job;
            CUSTOMERL1.Open(Job.Database, AccessMode.Read, false, false, ServerCursors.Default);
        }

        override public void Dispose(bool disposing)
        {
            if (disposing)
            {
                
                CUSTOMERL1.Close();
                CSMASTERL1.Close();
            }
            base.Dispose(disposing);
        }


#endregion
        void StarEntry(int _pc_parms)
        {
            do
            {
                Sales = 0;
                Returns = 0;

                // Get Customer Master Record

                Cust_lb_ = Cust_lb_Ch.MoveRight(Cust_lb_);
                _IN[90] = CUSTOMERL1.Chain(true, Cust_lb_) ? '0' : '1';
                //* Position Sales File to Customer
                if (!(bool)_IN[90])
                {
                    CSMASTERL1.Seek(SeekMode.SetLL, Cust_lb_);
                    _IN[3] = CSMASTERL1.ReadNextEqual(true, Cust_lb_) ? '0' : '1';
                    //*Read Sales Records
                    while (!(bool)_IN[3])
                    {
                        //*Sales
                        TempAmt = SlsArray.Sum();
                        if (CSTYPE == SaleEvent)
                            Sales = Sales + TempAmt;
                        //*Returns
                        if (CSTYPE == ReturnEvent)
                            Returns = Returns + TempAmt;
                        //*Read Next
                        _IN[3] = CSMASTERL1.ReadNextEqual(true, Cust_lb_) ? '0' : '1';
                    }
                }
                SalesCh = Sales.MoveRight(SalesCh);
                ReturnsCh = Returns.MoveRight(ReturnsCh);
                _INLR = '1';
                ///SPACE 3
                // * * * * * * * * * * ** *
                //  Initialize Event Fields
                // * * * * * * * * * * ** *
            } while (!(bool)_INLR);
        }
        void PROCESS_STAR_INZSR()
        {
            SaleEvent = 1;
            ReturnEvent = 2;
            _IN[3] = '0';
            Sales = 0;
            Returns = 0;
        }

#region Entry and activation methods for *ENTRY
        public static object _classFactory()
        {
            return new Custcalc();
        }

        int _parms;

        void __ENTRY(out Indicator __inLR, bool _isNew, ref FixedString<_9> _Cust_lb_Ch, ref FixedString<_13> _SalesCh, ref FixedString<_13> _ReturnsCh)
        {
            int _pc_parms = 3;
            bool _cleanup = true;
            ReturnsCh = _ReturnsCh;
            SalesCh = _SalesCh;
            Cust_lb_Ch = _Cust_lb_Ch;
            __inLR = '0';
            try
            {
                _parms = _pc_parms;
                if (_isNew)
                    PROCESS_STAR_INZSR();
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
                    _Cust_lb_Ch = Cust_lb_Ch;
                    _SalesCh = SalesCh;
                    _ReturnsCh = ReturnsCh;
                    __inLR = _INLR;
                }
            }
        }

        public static void _ENTRY(ICaller _caller, out Indicator __inLR, ref FixedString<_9> Cust_lb_Ch, ref FixedString<_13> SalesCh, ref FixedString<_13> ReturnsCh)
        {
            IActivationManager _manager = ProcedureSupport.ActivationManager;
            bool _isNew = false;
            Custcalc _instance = null;
            __inLR = '0';
            _instance = _manager.GetInstance(typeof(Custcalc), _classFactory, _caller, out _isNew) as Custcalc;
            try
            {
                _instance.__ENTRY(out __inLR, _isNew, ref Cust_lb_Ch, ref SalesCh, ref ReturnsCh);
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
            CUSTOMERL1 = new DatabaseFile(PopulateBufferCUSTOMERL1, PopulateFieldsCUSTOMERL1, null, "CUSTOMERL1", "*LIBL/CUSTOMERL1", CUSTOMERL1FormatIDs)
            { IsDefaultRFN = true };
            CSMASTERL1 = new DatabaseFile(PopulateBufferCSMASTERL1, PopulateFieldsCSMASTERL1, null, "CSMASTERL1", "*LIBL/CSMASTERL1", CSMASTERL1FormatIDs)
            { IsDefaultRFN = true };
            CUSTSL = new DataStructure(CUSTSL_000, CUSTSL_001, CUSTSL_002, CUSTSL_003, CUSTSL_004, CUSTSL_005, CUSTSL_006, 
                CUSTSL_007, CUSTSL_008, CUSTSL_009, CUSTSL_010, CUSTSL_011, CUSTSL_012, CUSTSL_013, CUSTSL_014);
            SlsArray = new FixedDecimalArrayInDS<_12, _11, _2>(CUSTSL, LayoutType.Packed, 10);
        }
    }

}
