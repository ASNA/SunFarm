// Translated from AVR to C# on 11/5/2020 at 11:57:57 AM by ASNA Monarch® Nomad version 16.0.3.0
// ASNA Monarch(R) version 10.0.24.0 at 11/4/2020
// Migrated source location: library NUTSNBOLTS, file QRPGSRC, member CUSTDELIV

using ASNA.QSys;
using ASNA.DataGate.Common;
using SunFarm.Customers.Application_Job;

using System;


namespace SunFarm.Customers
{
#warning Field name CUSTDELIV renamed to avoid a clash with the class name.
    [ASNA.QSys.HostServices.ActivationGroup("*DFTACTGRP")]
    [ProgramEntry("_ENTRY")]
    public partial class Custdeliv : ASNA.QSys.HostServices.Program
    {
        protected Indicator _INLR;
        protected Indicator _INRT;
        protected IndicatorArray<Len<_1, _0, _0>> _IN;
        //********************************************************************
        // JB   8/31/2004   Created.

        //********************************************************************
        DatabaseFile CUSTOMERL1;
        DatabaseFile CAMASTER;
        WorkstationFile _fCUSTDELIV;
        //********************************************************************
        // Customer DS
        DataStructure CUSTDS;

        //********************************************************************
        // Fields defined in main C-Specs declared now as Global fields (Monarch generated)
        FixedDecimal<_9, _0> pNumber;
        FixedDecimal<_4, _0> savrrn;
        FixedDecimal<_4, _0> sflrrn;

#region Constructor and Dispose 
        public Custdeliv()
        {
            _IN = new IndicatorArray<Len<_1, _0, _0>>((char[])null);
            _instanceInit();
            CAMASTER.Overrider = Job;
            CAMASTER.Open(Job.Database, AccessMode.Read, false, false, ServerCursors.Default);
            CUSTOMERL1.Overrider = Job;
            CUSTOMERL1.Open(Job.Database, AccessMode.Read, false, false, ServerCursors.Default);
        }

        override public void Dispose(bool disposing)
        {
            if (disposing)
            {
                
                CUSTOMERL1.Close();
                CAMASTER.Close();
                _fCUSTDELIV.Close();
            }
            base.Dispose(disposing);
        }


#endregion
        void StarEntry(int _pc_parms)
        {
            int _RrnTmp = 0;
            do
            {
                while (!(bool)_IN[12])
                {
                    _IN[90] = '0';
                    _fCUSTDELIV.Write("KEYS", _IN.Array);
                    _fCUSTDELIV.ExFmt("SFLC", _IN.Array);
                    // exit
                    if ((bool)_IN[12])
                    {
                        _INLR = '1';
                        break;
                    }
                    _IN[30] = '0';
                    savrrn = sflrrn;
                    _RrnTmp = (int)sflrrn;
                    _IN[40] = _fCUSTDELIV.ReadNextChanged("SFL1", ref _RrnTmp, _IN.Array) ? '0' : '1';
                    sflrrn = _RrnTmp;
                    while (!(bool)_IN[40])
                    {
                        if (SFLSEL == "1")
                        {
                            _fCUSTDELIV.ChainByRRN("SFL1", (int)sflrrn, _IN.Array);
                            Something();
                            SFLSEL = "";
                            _fCUSTDELIV.Update("SFL1", _IN.Array);
                        }
                        _RrnTmp = (int)sflrrn;
                        _IN[40] = _fCUSTDELIV.ReadNextChanged("SFL1", ref _RrnTmp, _IN.Array) ? '0' : '1';
                        sflrrn = _RrnTmp;
                    }
                    sflrrn = savrrn;
                }
                //****************************************

                //****************************************
            } while (!(bool)_INLR);
        }
        void Something()
        {
        }
        //**********************
        //  Load Sfl Subroutine
        //**********************
        void LoadSfl()
        {
            _IN[99] = CAMASTER.ReadNextEqual(true, pNumber) ? '0' : '1';
            while (!(bool)_IN[99])
            {
                SFLCUST_lb_ = CACUSTNO;
                SFLCUST = (string)CANAME;
                SFLCITY = CACITY.TrimEnd() + ", " + CASTATE;
                SFLZIP = (string)CAZIP;
                sflrrn += 1;
                _fCUSTDELIV.WriteSubfile("SFL1", (int)sflrrn, _IN.Array);
                _IN[99] = CAMASTER.ReadNextEqual(true, pNumber) ? '0' : '1';
            }
            // end of file
            if (sflrrn == 0)
            {
                sflrrn += 1;
                CMCUSTNO = 0;
                SFLCUST = "No Address Records Found";
                SFLCITY = "";
                SFLZIP = "";
                _fCUSTDELIV.WriteSubfile("SFL1", (int)sflrrn, _IN.Array);
            }
        }
        //*********************
        // Init Subroutine
        //*********************
        void PROCESS_STAR_INZSR()
        {
            sflrrn = 0;
            _IN[90] = '1';
            _fCUSTDELIV.Write("SFLC", _IN.Array);
            CAMASTER.Seek(SeekMode.SetLL, pNumber);
            LoadSfl();
        }

#region Entry and activation methods for *ENTRY
        public static object _classFactory()
        {
            return new Custdeliv();
        }

        int _parms;

        void __ENTRY(out Indicator __inLR, bool _isNew, ref FixedDecimal<_9, _0> _pNumber)
        {
            int _pc_parms = 1;
            bool _cleanup = true;
            pNumber = _pNumber;
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
                    _pNumber = pNumber;
                    __inLR = _INLR;
                }
            }
        }

        public static void _ENTRY(ICaller _caller, out Indicator __inLR, ref FixedDecimal<_9, _0> pNumber)
        {
            IActivationManager _manager = ProcedureSupport.ActivationManager;
            bool _isNew = false;
            Custdeliv _instance = null;
            __inLR = '0';
            _instance = _manager.GetInstance(typeof(Custdeliv), _classFactory, _caller, out _isNew) as Custdeliv;
            try
            {
                _instance.__ENTRY(out __inLR, _isNew, ref pNumber);
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
            CAMASTER = new DatabaseFile(PopulateBufferCAMASTER, PopulateFieldsCAMASTER, null, "CAMASTER", "*LIBL/CAMASTER", CAMASTERFormatIDs)
            { IsDefaultRFN = true };
            _fCUSTDELIV = new WorkstationFile(PopulateBuffer_fCUSTDELIV, PopulateFields_fCUSTDELIV, null, "_fCUSTDELIV", "/CustomerAppViews/CUSTDELIV");
            _fCUSTDELIV.Open();
            CUSTDS = new DataStructure(CUSTDS_000, CUSTDS_001, CUSTDS_002, CUSTDS_003, CUSTDS_004, CUSTDS_005, CUSTDS_006, 
                CUSTDS_007, CUSTDS_008, CUSTDS_009, CUSTDS_010, CUSTDS_011, CUSTDS_012, CUSTDS_013, CUSTDS_014, CUSTDS_015);
        }
    }

}
