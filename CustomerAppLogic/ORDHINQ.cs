﻿// Translated from AVR to C# on 11/5/2020 at 11:57:57 AM by ASNA Monarch® Nomad version 16.0.3.0
// ASNA Monarch(R) version 10.0.24.0 at 11/4/2020
// Migrated source location: library RPNUBO, file QRPGSRC, member ORDHINQ

using ASNA.QSys.Runtime;
using ASNA.DataGate.Common;

using System;


namespace SunFarm.Customers
{
    [ASNA.QSys.Runtime.JobSupport.ActivationGroup("*DFTACTGRP")]
    [ProgramEntry("_ENTRY")]
    public partial class Ordhinq : ASNA.QSys.Runtime.JobSupport.Program
    {
        protected Indicator _INLR;
        protected Indicator _INRT;
        protected IndicatorArray<Len<_1, _0, _0>> _IN;
        protected dynamic DynamicCaller_;
        WorkstationFile ORDHDSPF;
        DatabaseFile OrderHdrL2;
        DatabaseFile OrderHdrL1;
        DatabaseFile CustomerL1;
        //********************************************************************
        DataStructure _DS8 = new DataStructure(3);
        FixedDecimal<_3, _0> _lb_pNbrs { get => _DS8.GetZoned(0, 3, 0); set => _DS8.SetZoned(value, 0, 3, 0); } 

        FixedDecimalArray<_20, _9, _0> pNumbers;
        FixedString<_7> MID;
        FixedString<_30> MTX;
        FixedString<_1> LockRec;
        FixedDecimal<_5, _0> X;
        FixedDecimal<_9, _0> SVOrdnum;
        FixedDecimal<_13, _2> TempWgt;
        // Order      DS
        DataStructure OrdhDS;

        //********************************************************************
        // Fields defined in main C-Specs declared now as Global fields (Monarch generated)
        FixedString<_1> AddUpd;
        FixedDecimal<_8, _0> CKorddate;
        FixedDecimal<_9, _0> CustNo;
        FixedString<_1> Delall;
        FixedString<_10> pCsrFld;
        FixedString<_10> pResult;
        FixedDecimal<_4, _0> savrrn;
        FixedDecimal<_4, _0> sflrrn;
        FixedDecimal<_9, _0> TEMPNO;
        FixedString<_1> WorkTemp1;

        // PLIST(s) relocated by Monarch
        
        // KLIST(s) relocated by Monarch
        
#region Constructor and Dispose 
        public Ordhinq()
        {
            _IN = new IndicatorArray<Len<_1, _0, _0>>((char[])null);
            _instanceInit();
            // Initialization of Data Structure fields (Monarch generated)
            Reset__lb_pNbrs();
            CustomerL1.Overrider = Job;
            CustomerL1.Open(Job.Database, AccessMode.Read, false, false, ServerCursors.Default);
            OrderHdrL1.Overrider = Job;
            OrderHdrL1.Open(Job.Database, AccessMode.RWCD, false, false, ServerCursors.Default);
            OrderHdrL2.Overrider = Job;
            OrderHdrL2.Open(Job.Database, AccessMode.Read, false, false, ServerCursors.Default);
        }

        override public void Dispose(bool disposing)
        {
            if (disposing)
            {
                
                ORDHDSPF.Close();
                OrderHdrL2.Close();
                OrderHdrL1.Close();
                CustomerL1.Close();
            }
            base.Dispose(disposing);
        }


#endregion
        void StarEntry(int _pc_parms)
        {
            Indicator _LR = '0';
            int _RrnTmp = 0;
            FixedDecimal<_9, _0> _ORDNUMProxy = 0;
            do
            {
                // KLIST "KeyMastL2" moved by Monarch to global scope.
                //********************************************************************
                CustomerL1.Chain(true, CustNo);
                SCRCUST = EditCode.Apply(CMCUSTNO, 0, 9, EditCodes.Z, "").Trim() + " " + CMNAME;
                SCRPHONE = ((string)CMPHONE).Trim();
                if (CMFAX != 0)
                {
                    SCRFAX = CMFAX.MoveLeft(SCRFAX);
                    SCRFAX = "(" + ((string)SCRFAX).Substring(0, 3) + ") " + ((string)SCRFAX).Substring(3, 3) + "-" + ((string)SCRFAX).Substring(6, 4);
                }
                //--------------------------------------------------------------------
                do
                {
                    _IN[90] = '0';
                    _IN[77] = '0';
                    ORDHDSPF.Write("MSGSFC", _IN.Array);
                    ORDHDSPF.Write("KEYS", _IN.Array);
                    ORDHDSPF.ExFmt("SFLC", _IN.Array);
                    SFFILESTAT = "";
                    //--------------------------------------------------------------------
                    if ((bool)_IN[12])
                    {
                        _INLR = '1';
                        break;
                        // PageUp-RollDown
                    }
                    else if ((bool)_IN[50])
                    {
                        OrderHdrL2.Seek(SeekMode.SetLL, CustNo, ORDNUM);
                        LoadSfl();
                    }
                    else if ((bool)_IN[51])
                    {
                        // PageDown-RollUp
                        ReadBack();
                        LoadSfl();
                    }
                    else if (SETORDNUM != 0)
                    {
                        ORDNUM = SETORDNUM;
                        OrderHdrL2.Seek(SeekMode.SetLL, CustNo, ORDNUM);
                        LoadSfl();
                    }
                    else if ((bool)_IN[6])
                    {
                        SVOrdnum = ORDNUM;
                        OrderHdrL1.Seek(SeekMode.SetGT, 999999999m);
                        OrderHdrL1.ReadPrevious(false);
                        TEMPNO = ORDNUM + 10;
                        ORDNUM = TEMPNO;
                        ORDCUST = CustNo;
                        ORDDATE = DateTime.Now.TimestampToDate();
                        ORDSHPDATE = DateTime.Now.TimestampToDate();
                        ORDDELDATE = 0;
                        ORDSHPVIA = 0;
                        ORDAMOUNT = 0;
                        OrderHdrL1.Write();
                        AddUpd = "A";
                        DTORDNUM = TEMPNO;
                        SFORDNUM = TEMPNO;
                        DTORDDATE = ORDDATE;
                        DTSHPDATE = ORDSHPDATE;
                        DTDELDATE = ORDDELDATE;
                        DTSHPVIA = ORDSHPVIA;
                        _IN[30] = '1';
                        RcdUpdate();
                    }
                    else
                    {
                        _IN[30] = '0';
                        _IN[66] = '0';
                        savrrn = sflrrn;
                        pNumbers.Initialize(0);
                        _lb_pNbrs = 0;
                        do
                        {
                            _RrnTmp = (int)sflrrn;
                            _IN[66] = ORDHDSPF.ReadNextChanged("SFL1", ref _RrnTmp, _IN.Array) ? '0' : '1';
                            sflrrn = _RrnTmp;
                            if (!(bool)_IN[66])
                            {
                                if (SFSEL == 6)
                                {
                                    ORDNUM = SFORDNUM;
                                    _ORDNUMProxy = ORDNUM;
                                    // Print the order.
                                    DynamicCaller_.CallD("SunFarm.Customers.ORDPRINT", out _LR, ref CustNo, ref _ORDNUMProxy);
                                    ORDNUM = _ORDNUMProxy;
                                    LockRec = "N";
                                    OrdChk();
                                    ClearSel();
                                }
                                else if (SFSEL == 3)
                                {
                                    ORDNUM = SFORDNUM;
                                    Delall = "";
                                    _ORDNUMProxy = ORDNUM;
                                    // Maintainance.
                                    DynamicCaller_.CallD("SunFarm.Customers.ORDDTLINQ", out _LR, ref CustNo, ref _ORDNUMProxy, ref Delall);
                                    ORDNUM = _ORDNUMProxy;
                                    LockRec = "N";
                                    OrdChk();
                                    SFORDAMT = ORDAMOUNT;
                                    ClearSel();
                                }
                                else if (SFSEL == 2)
                                {
                                    // Maintainance.
                                    ORDHDSPF.ChainByRRN("SFL1", (int)sflrrn, _IN.Array);
                                    AddUpd = "U";
                                    RcdUpdate();
                                }
                            }
                        } while (!((bool)_IN[66]));
                        sflrrn = savrrn;
                    }
                } while (!((bool)_IN[12]));
                ///SPACE 3
                //*********************************************************************
                // UPDATE THE ORDER RECORD
                //*********************************************************************
            } while (!(bool)_INLR);
        }
        void RcdUpdate()
        {
            Indicator _LR = '0';
            FixedDecimal<_9, _0> _ORDNUMProxy = 0;
            if (!(bool)_IN[6])
                // If not a new order.
                ClearSel();
            ORDNUM = SFORDNUM;
            LockRec = "N";
            OrdChk();
            DTORDNUM = ORDNUM;
            DTORDDATE = ORDDATE;
            DTSHPDATE = ORDSHPDATE;
            DTDELDATE = ORDDELDATE;
            DTSHPVIA = ORDSHPVIA;
            TempWgt = ORDWEIGHT / 16; // Convert to pounds.
            DTWEIGHT = EditCode.Apply(TempWgt, 2, 13, EditCodes.One, "").Trim() + " Lbs";
            _IN[40] = '1'; // Place the cursor on
            _IN[41] = '0'; //  the name field.
            _IN[42] = '0';
            _IN[43] = '0';
            _IN[44] = '0';
            _IN[99] = '0';
            //-------------------------------------------------------
            do
            {
                ORDHDSPF.Write("MSGSFC", _IN.Array);
                ORDHDSPF.Write("MYWINDOW", _IN.Array);
                ORDHDSPF.ExFmt("ORDHREC", _IN.Array);
                _IN[40] = '0';
                _IN[41] = '0';
                _IN[42] = '0';
                _IN[43] = '0';
                _IN[44] = '0';
                ClearMsgs();
                _IN[30] = '0';
                if ((bool)_IN[12])
                    break;
                else if ((bool)_IN[4])
                {
                    pCsrFld = "SHIPVIA";
                    DynamicCaller_.CallD("SunFarm.Customers.CUSTPRMPT", out _LR, ref pCsrFld, ref pResult);
                    WorkTemp1 = (string)pResult;
                    DTSHPVIA = WorkTemp1.MoveRight(DTSHPVIA);
                    // Delete  Order
                    // Delete  The order line items first
                }
                else if ((bool)_IN[11])
                {
                    ORDNUM = SFORDNUM;
                    Delall = "Y";
                    _ORDNUMProxy = ORDNUM;
                    DynamicCaller_.CallD("SunFarm.Customers.ORDDTLINQ", out _LR, ref CustNo, ref _ORDNUMProxy, ref Delall);
                    ORDNUM = _ORDNUMProxy;
                    LockRec = "Y";
                    OrdChk();
                    if (!(bool)_IN[80])
                        OrderHdrL1.Delete();
                    ORDHDSPF.ChainByRRN("SFL1", (int)sflrrn, _IN.Array);
                    SFORDDATE = DateTime.MinValue;
                    SFSHPDATE = DateTime.MinValue;
                    SFDELDATE = 0;
                    SFORDAMT = 0;
                    SFSHPVIA = 0;
                    SFFILESTAT = "*Del";
                    // Set the color
                    _IN[60] = '1';
                    ORDHDSPF.Update("SFL1", _IN.Array);
                    _IN[60] = '0';
                    // Delete msg
                    MID = "ORD0003";
                    MTX = ORDNUM.MoveRight(MTX);
                    DynamicCaller_.CallD("SunFarm.Customers.MSGLOD", out _LR, ref MID, ref MTX);
                    break;
                    // Add/update on the Enter key
                }
                else
                {
                    if (AddUpd == "A")
                    {
                        EditFlds();
                        if ((bool)_IN[99])
                            // Any errors?
                            continue;
                        OrderHdrL1.Chain(true, TEMPNO);
                        UpdDbFlds();
                        OrderHdrL1.Update();
                        OrderHdrL2.Seek(SeekMode.SetLL, CustNo);
                        LoadSfl();
                        // Added message
                        MID = "ORD0001";
                        MTX = ORDNUM.MoveLeft(MTX);
                        DynamicCaller_.CallD("SunFarm.Customers.MSGLOD", out _LR, ref MID, ref MTX);
                        break;
                        // Update the database
                    }
                    else
                    {
                        EditFlds();
                        if ((bool)_IN[99])
                            // Any errors?
                            continue;
                        LockRec = "Y";
                        OrdChk(); //Reread the record.
                        UpdDbFlds();
                        OrderHdrL1.Update();
                        ORDHDSPF.ChainByRRN("SFL1", (int)sflrrn, _IN.Array);
                        SFORDNUM = ORDNUM;
                        SFORDDATE = ORDDATE;
                        SFSHPDATE = ORDSHPDATE;
                        SFDELDATE = ORDDELDATE;
                        SFORDAMT = ORDAMOUNT;
                        SFSHPVIA = ORDSHPVIA;
                        SFFILESTAT = "";
                        ORDHDSPF.Update("SFL1", _IN.Array);
                        _IN[12] = '1';
                        // Updated message
                        MID = "ORD0002";
                        MTX = ORDNUM.MoveLeft(MTX);
                        DynamicCaller_.CallD("SunFarm.Customers.MSGLOD", out _LR, ref MID, ref MTX);
                    }
                    // Re-open for the next update
                    LockRec = "N";
                    OrdChk();
                }
            } while (!((bool)_IN[12]));
        }
        //*********************************************************************
        //  EDIT THE SCREEN FIELDS
        //*********************************************************************
        void EditFlds()
        {
            Indicator _LR = '0';
            _IN[40] = '0'; //Clear all error inds
            _IN[41] = '0';
            _IN[42] = '0';
            _IN[43] = '0';
            _IN[44] = '0';
            _IN[99] = '0';
            CKorddate = DTORDDATE.MoveRight(CKorddate);
            if (CKorddate == 0)
            {
                _IN[40] = '1';
                _IN[99] = '1';
                MID = "ORD0004"; // Blank field.
                MTX = "Order Date";
            }
            else if (DTSHPVIA == 0)
            {
                _IN[42] = '1';
                _IN[99] = '1';
                MID = "Ord0005"; // Blank field.
                MTX = "Ship Via";
            }
            if ((bool)_IN[99])
            {
                DynamicCaller_.CallD("SunFarm.Customers.MSGLOD", out _LR, ref MID, ref MTX);
            }
        }
        //*********************************************************************
        //  UPDATE THE DATABASE FIELDS
        //*********************************************************************
        void UpdDbFlds()
        {
            ORDNUM = DTORDNUM;
            ORDDATE = DTORDDATE;
            ORDSHPDATE = DTSHPDATE;
            ORDDELDATE = DTDELDATE;
            ORDSHPVIA = DTSHPVIA;
        }
        //********************************************************************
        //   CHECK THE ORDER NUMBER
        //********************************************************************
        void OrdChk()
        {
            if (LockRec == "N")
                _IN[80] = OrderHdrL1.Chain(false, ORDNUM) ? '0' : '1';
            else
                _IN[80] = OrderHdrL1.Chain(true, ORDNUM) ? '0' : '1';
        }
        //*********************************************************************
        //  Load Sfl Subroutine
        //*********************************************************************
        void LoadSfl()
        {
            _IN[61] = '0'; //Start with green.
            _IN[90] = '1'; //Clear the subfile.
            ORDHDSPF.Write("SFLC", _IN.Array);
            _IN[76] = '0'; //Display records.
            _IN[90] = '0';
            SFFILESTAT = "";
            sflrrn = 0;
            _IN[77] = OrderHdrL2.ReadNextEqual(true, CustNo) ? '0' : '1';
            //----------------------------------------------------------
            while (!(bool)_IN[77] && (sflrrn < 9999))
            {
                SFORDNUM = ORDNUM;
                SFORDDATE = ORDDATE;
                SFSHPDATE = ORDSHPDATE;
                SFDELDATE = ORDDELDATE;
                SFORDAMT = ORDAMOUNT;
                SFSHPVIA = ORDSHPVIA;
                if ((bool)_IN[61])
                    //Save the color of
                    SFCOLOR = "W"; // the row.
                else
                    SFCOLOR = "G";
                sflrrn += 1;
                ORDHDSPF.WriteSubfile("SFL1", (int)sflrrn, _IN.Array);
                _IN[61] = (Indicator)(!(bool)_IN[61]); //Reverse the color.
                _IN[77] = OrderHdrL2.ReadNextEqual(true, CustNo) ? '0' : '1';
            }
            // Any records found?
            if (sflrrn == 0)
            {
                sflrrn = 1;
                SFORDNUM = 0;
                ORDNUM = 0;
                SFORDDATE = DateTime.MinValue;
                SFSHPDATE = DateTime.MinValue;
                SFDELDATE = 0;
                SFFILESTAT = "NoOr";
                SFORDAMT = 0;
                SFSHPVIA = 0;
                ORDHDSPF.WriteSubfile("SFL1", (int)sflrrn, _IN.Array);
                SFFILESTAT = "";
            }
        }
        //*********************************************************************
        //  Read Backwards for a PageDown
        //*********************************************************************
        void ReadBack()
        {
            _IN[76] = '0';
            _IN[77] = '0';
            X = 0;
            ORDHDSPF.ChainByRRN("SFL1", 1, _IN.Array); //Get the top ord nbr
            ORDNUM = SFORDNUM;
            OrderHdrL2.Chain(true, CustNo, ORDNUM);
            _IN[76] = OrderHdrL2.ReadPreviousEqual(true, CustNo) ? '0' : '1';
            while (!(bool)_IN[76] && (X < 10))
            {
                /* EOF or full s/f. */
                X += 1;
                _IN[76] = OrderHdrL2.ReadPreviousEqual(true, CustNo) ? '0' : '1';
            }
            if ((bool)_IN[76])
            {
                ORDNUM = 0;
                OrderHdrL2.Seek(SeekMode.SetLL, CustNo, ORDNUM);
            }
        }
        //*********************************************************************
        // CLEAR THE SELECTION NUMBER
        //*********************************************************************
        void ClearSel()
        {
            SFSEL = 0;
            _IN[61] = (Indicator)(SFCOLOR == "W");
            ORDHDSPF.Update("SFL1", _IN.Array);
        }
        //*********************************************************************
        // CLEAR THE MESSAGE QUEUE
        //*********************************************************************
        void ClearMsgs()
        {
            Indicator _LR = '0';
            DynamicCaller_.CallD("SunFarm.Customers.MSGCLR", out _LR);
            MID = "";
        }
        //*********************************************************************
        //     * Init Subroutine
        //*********************************************************************
        void PROCESS_STAR_INZSR()
        {
            Indicator _LR = '0';
            OrderHdrL2.Seek(SeekMode.SetLL, CustNo);
            LoadSfl();
            _IN[75] = '1';
            MID = "";
            _at_PGMQ = "*";
            DynamicCaller_.CallD("SunFarm.Customers.MSGCLR", out _LR);
        }

        // Message handling parm list
        // PLIST "#PLMSG" moved by Monarch to global scope.
        void Reset__lb_pNbrs()
        {
            _lb_pNbrs = 0;
        }

#region Entry and activation methods for *ENTRY
        public static object _classFactory()
        {
            return new Ordhinq();
        }

        int _parms;

        void __ENTRY(out Indicator __inLR, bool _isNew, ref FixedDecimal<_9, _0> _CustNo)
        {
            int _pc_parms = 1;
            bool _cleanup = true;
            CustNo = _CustNo;
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
                    _CustNo = CustNo;
                    __inLR = _INLR;
                }
            }
        }

        public static void _ENTRY(ICaller _caller, out Indicator __inLR, ref FixedDecimal<_9, _0> CustNo)
        {
            IActivationManager _manager = ProcedureSupport.ActivationManager;
            bool _isNew = false;
            Ordhinq _instance = null;
            __inLR = '0';
            _instance = _manager.GetInstance(typeof(Ordhinq), _classFactory, _caller, out _isNew) as Ordhinq;
            try
            {
                _instance.__ENTRY(out __inLR, _isNew, ref CustNo);
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
            pNumbers = new FixedDecimalArray<_20, _9, _0>((FixedDecimal<_9, _0>[])null);
            DynamicCaller_ = new DynamicCaller(this);
            ORDHDSPF = new WorkstationFile(PopulateBufferORDHDSPF, PopulateFieldsORDHDSPF, null, "ORDHDSPF", "/CustomerAppViews/ORDHDSPF");
            ORDHDSPF.Open();
            OrderHdrL2 = new DatabaseFile(PopulateBufferOrderHdrL2, PopulateFieldsOrderHdrL2, null, "OrderHdrL2", "*LIBL/OrderHdrL2", OrderHdrL2FormatIDs)
            { IsDefaultRFN = true };
            OrderHdrL1 = new DatabaseFile(PopulateBufferOrderHdrL1, PopulateFieldsOrderHdrL1, null, "OrderHdrL1", "*LIBL/OrderHdrL1", OrderHdrL1FormatIDs, blockingFactor : 0)
            { IsDefaultRFN = true };
            CustomerL1 = new DatabaseFile(PopulateBufferCustomerL1, PopulateFieldsCustomerL1, null, "CustomerL1", "*LIBL/CustomerL1", CustomerL1FormatIDs)
            { IsDefaultRFN = true };
            OrdhDS = new DataStructure(OrdhDS_000, OrdhDS_001, OrdhDS_002, OrdhDS_003, OrdhDS_004, OrdhDS_005, OrdhDS_006, 
                OrdhDS_007, OrdhDS_008);
        }
    }

    //Error  There are 2 NoLock Read operations. Using *NoLock on a file opened for *Update is not supported with DSS .NET
}
