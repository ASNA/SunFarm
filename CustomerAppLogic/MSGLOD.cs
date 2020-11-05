// Translated from AVR to C# on 11/5/2020 at 11:57:57 AM by ASNA Serengeti® version 16.0.3.0
// ASNA Monarch(R) version 10.0.24.0 at 11/4/2020
// Migrated source location: library NUTSNBOLTS, file QCLSRC, member MSGLOD

using ASNA.QSys;
using ASNA.DataGate.Common;
using SunFarm.Customers.Application_Job;

using System;
using System.Collections;
using System.Collections.Specialized;
using ASNA.QSys.HostServices;



namespace SunFarm.Customers
{
    [ProgramEntry("_ENTRY")]
    public partial class Msglod : CLProgram
    {
        protected Indicator _INLR;
        protected Indicator _INRT;
        protected IndicatorArray<Len<_1, _0, _0>> _IN;

        FixedString<_7> _MSGID;
        FixedString<_30> _MSGTXT;

        //------------------------------------------------------------------------------ 
        //  "*Entry" Mainline Code (Monarch generated)
        //------------------------------------------------------------------------------ 
        void StarEntry(int _pc_parms)
        {
            _INLR = '1';


            if (!_MSGID.IsBlanks())
            {
                if (((string)_MSGID).Substring(0, 3) == "CST")
                    SendProgramMessage(_MSGID, "CUSTMSGF", _MSGTXT);
                else
                    SendProgramMessage(_MSGID, "ITEMMSGF", _MSGTXT);
            }


        }

#region Entry and activation methods for *ENTRY
        public static object _classFactory()
        {
            return new Msglod();
        }

        int _parms;

        void __ENTRY(out Indicator __inLR, bool _isNew, ref FixedString<_7> __MSGID, ref FixedString<_30> __MSGTXT)
        {
            int _pc_parms = 2;
            bool _cleanup = true;
            _MSGTXT = __MSGTXT;
            _MSGID = __MSGID;
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
                    __MSGID = _MSGID;
                    __MSGTXT = _MSGTXT;
                    __inLR = _INLR;
                }
            }
        }

        public static void _ENTRY(ICaller _caller, out Indicator __inLR, ref FixedString<_7> _MSGID, ref FixedString<_30> _MSGTXT)
        {
            IActivationManager _manager = ProcedureSupport.ActivationManager;
            bool _isNew = false;
            Msglod _instance = null;
            __inLR = '0';
            _instance = _manager.GetInstance(typeof(Msglod), _classFactory, _caller, out _isNew) as Msglod;
            try
            {
                _instance.__ENTRY(out __inLR, _isNew, ref _MSGID, ref _MSGTXT);
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

        public Msglod()
        {
            _IN = new IndicatorArray<Len<_1, _0, _0>>((char[])null);
            _instanceInit();
        }

        void _instanceInit()
        {
        }
    }

}
