// Translated from AVR to C# on 11/5/2020 at 11:57:57 AM by ASNA Monarch® Nomad version 16.0.3.0
// ASNA Monarch(R) version 10.0.24.0 at 11/4/2020
// Migrated source location: library NUTSNBOLTS, file QCLSRC, member MSGCLR

using ASNA.QSys.Runtime;
using ASNA.QSys.Runtime.JobSupport;

namespace SunFarm.Customers
{
    [ProgramEntry("_ENTRY")]
    public partial class Msgclr : CLProgram
    {
        protected Indicator _INLR;
        protected Indicator _INRT;
        protected IndicatorArray<Len<_1, _0, _0>> _IN;


        //------------------------------------------------------------------------------ 
        //  "*Entry" Mainline Code (Monarch generated)
        //------------------------------------------------------------------------------ 
        void StarEntry(int _pc_parms)
        {
            _INLR = '1';

            RemoveMessage("*ALL");
            return;


        }

#region Entry and activation methods for *ENTRY
        public static object _classFactory()
        {
            return new Msgclr();
        }

        int _parms;

        void __ENTRY(out Indicator __inLR, bool _isNew)
        {
            int _pc_parms = 0;
            bool _cleanup = true;
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

        public static void _ENTRY(ICaller _caller, out Indicator __inLR)
        {
            IActivationManager _manager = ProcedureSupport.ActivationManager;
            bool _isNew = false;
            Msgclr _instance = null;
            __inLR = '0';
            _instance = _manager.GetInstance(typeof(Msgclr), _classFactory, _caller, out _isNew) as Msgclr;
            try
            {
                _instance.__ENTRY(out __inLR, _isNew);
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

        public Msgclr()
        {
            _IN = new IndicatorArray<Len<_1, _0, _0>>((char[])null);
            _instanceInit();
        }

        void _instanceInit()
        {
        }
    }

}
