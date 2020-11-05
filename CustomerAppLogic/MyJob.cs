// Translated from AVR to C# on 11/5/2020 at 11:57:57 AM by ASNA Serengeti® version 16.0.3.0
// ASNA Monarch(R) version 10.0.24.0 at 11/4/2020
using ASNA.QSys;
using ASNA.DataGate.Common;
using System;
using SunFarm.Customers;
namespace SunFarm.Customers.Application_Job
{

    public partial class MyJob : ASNA.QSys.HostServices.WebJob
    {
        protected Indicator _INLR;
        protected Indicator _INRT;
        protected IndicatorArray<Len<_1, _0, _0>> _IN;
        protected dynamic DynamicCaller_;
        public Database MyDatabase = new Database("NancySQL");
        // public Database MyPrinterDB = new Database("cypress");

        override protected Database getDatabase()
        {
            return MyDatabase;
        }

        //override protected Database getPrinterDB()
        //{
        //    return MyPrinterDB;
        //}

        override public void Dispose(bool disposing)
        {
            if (disposing)
            {
                MyDatabase.Close();
                // MyPrinterDB.Close();
            }
            base.Dispose(disposing);
        }

        MyJob(ASNA.QSys.JobSupport.IJobServices JobServices)
            : base(JobServices)
        {
            _IN = new IndicatorArray<Len<_1, _0, _0>>((char[])null);
            _instanceInit();
        }

        public static MyJob JobFactory()
        {
            ASNA.QSys.HostServices.Spooler spooler = new ASNA.QSys.HostServices.Spooler("C:\\MonarchQueues\\OutputQueues", "QPRINT");
            ASNA.QSys.HostServices.DocumentLibraryObject dlo = new ASNA.QSys.HostServices.DocumentLibraryObject("QDLS");
            ASNA.QSys.HostServices.IntergratedFileSystem ifs = new ASNA.QSys.HostServices.IntergratedFileSystem("//MyServer/MyShare");
            MyJob job = null;

            job = new MyJob(new ASNA.QSys.HostServices.JobServices(spooler, dlo, ifs));
            job.JobQueueBaseQueuesPath = "C:\\MonarchQueues\\JobQueues";
            return job;
        }

        override protected void ExecuteStartupProgram()
        {
            Indicator _LR = '0';
            MyDatabase.Open();
            // MyPrinterDB.Open();

            DynamicCaller_.CallD("SunFarm.Customers.Custinqc", out _LR);
        }


        void _instanceInit()
        {
            DynamicCaller_ = new DynamicCaller(this);
        }
    }

}
