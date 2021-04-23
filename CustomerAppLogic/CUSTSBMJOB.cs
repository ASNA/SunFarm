// Translated from AVR to C# on 11/5/2020 at 11:57:57 AM by ASNA Monarch® Nomad version 16.0.3.0
// ASNA Monarch(R) version 10.0.24.0 at 11/4/2020
// Migrated source location: library NUTSNBOLTS, file QRPGSRC, member CUSTSBMJOB

using ASNA.QSys;
using ASNA.DataGate.Common;
using SunFarm.Customers.Application_Job;

using System;


namespace SunFarm.Customers
{
    [ASNA.QSys.HostServices.ActivationGroup("*DFTACTGRP")]
    [ProgramEntry("_ENTRY")]
    public partial class Custsbmjob : ASNA.QSys.HostServices.Program
    {
        protected Indicator _INLR;
        protected Indicator _INRT;
        protected IndicatorArray<Len<_1, _0, _0>> _IN;
        protected dynamic DynamicCaller_;
        //********************************************************************
        // This program updates multiple customers without sending a msg.    *
        //********************************************************************
        // JB   8/30/2004   Created.
        //********************************************************************
        DataStructure _DS4 = new DataStructure(9);
        FixedDecimal<_9, _0> wkNumber9 { get => _DS4.GetZoned(0, 9, 0); set => _DS4.SetZoned(value, 0, 9, 0); } 
        FixedString<_9> wkAlpha9 { get => _DS4.GetString(0, 9); set => _DS4.SetString(((string)value).AsSpan(), 0, 9); } 

        FixedDecimalArray<_20, _9, _0> pNumbers;
        FixedStringArray<_20, _1> pTypes;
        //********************************************************************

        // Fields defined in main C-Specs declared now as Global fields (Monarch generated)
        FixedDecimal<_15, _5> pCmdLen;
        FixedString<_80> pString;
        FixedDecimal<_3, _0> X;

#region Constructor and Dispose 
        public Custsbmjob()
        {
            _IN = new IndicatorArray<Len<_1, _0, _0>>((char[])null);
            _instanceInit();
        }


#endregion
        void StarEntry(int _pc_parms)
        {
            Indicator _LR = '0';
            do
            {
                pCmdLen = 80;
                _INLR = '1';
                //--------------------------------------------------------------------
                for (X = 1; X <= 20; X++)
                {
                    if (pNumbers[(int)(X - 1)] != 0)
                    {
                        wkNumber9 = pNumbers[(int)(X - 1)];
                        if (pTypes[(int)(X - 1)] == "C")
                            pString = "SbmJob Cmd(CALL CUSTCRTS Parm(\'" + wkAlpha9 + "\')) Job(CustCrt) ";
                        else
                            pString = "SbmJob Cmd(CALL CUSTPRTS Parm(\'" + wkAlpha9 + "\')) Job(CustPrint) ";
                        DynamicCaller_.CallD("?.QCMDEXC", out _LR, ref pString, ref pCmdLen);
                    }
                }
            } while (!(bool)_INLR);
        }
        //********************************************************************

#region Entry and activation methods for *ENTRY
        public static object _classFactory()
        {
            return new Custsbmjob();
        }

        int _parms;

        void __ENTRY(out Indicator __inLR, bool _isNew, FixedDecimalArray<_20, _9, _0> _pNumbers, FixedStringArray<_20, _1> _pTypes)
        {
            int _pc_parms = 2;
            bool _cleanup = true;
            pTypes = _pTypes;
            pNumbers = _pNumbers;
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
                    __inLR = _INLR;
            }
        }

        public static void _ENTRY(ICaller _caller, out Indicator __inLR, FixedDecimalArray<_20, _9, _0> pNumbers, FixedStringArray<_20, _1> pTypes)
        {
            IActivationManager _manager = ProcedureSupport.ActivationManager;
            bool _isNew = false;
            Custsbmjob _instance = null;
            __inLR = '0';
            _instance = _manager.GetInstance(typeof(Custsbmjob), _classFactory, _caller, out _isNew) as Custsbmjob;
            try
            {
                _instance.__ENTRY(out __inLR, _isNew, pNumbers, pTypes);
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
            pTypes = new FixedStringArray<_20, _1>((FixedString<_1>[])null);
            pNumbers = new FixedDecimalArray<_20, _9, _0>((FixedDecimal<_9, _0>[])null);
            DynamicCaller_ = new DynamicCaller(this);
        }
    }

}
