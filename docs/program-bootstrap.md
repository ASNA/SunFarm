---
layout: page
title: Program Bootstrapping
permalink: /program-bootstrap/
---

To support the [IBMi RPG Developer's Model](https://asnaqsys.github.io/concepts/background/ibmi-developer-model.html), every time a `QSys Program` is called there is logic that runs which may be important to understand. We will refer in this topic to this code as **Program Bootstrap** code.

[ASNA QSys docs Concepts](https://asnaqsys.github.io/concepts/enhancements/nomad-tools.html) mentions the need for explicit Boilerplate code *on every program*, given that C# is a [General Purpose Programming Language](https://en.wikipedia.org/wiki/General-purpose_programming_language). This Boilerplate code implementing the *Program Bootstrap* logic is produced during migration using the same `<program-name>.cs` source file (mostly at the end of the file).

For the purposes of this Guide, we will isolate that code into its own source file, using the same technique used by [QSys DatabaseFile Support](https://asnaqsys.github.io/concepts/program-structure/qsys-databasefile), in which [C# partial classes](https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/classes-and-structs/partial-classes-and-methods) facilitates the isolation of - *otherwise* - internal implementation details.

On the Logic Project, let's add a new source file.

Sourcefile: `CustomerAppLogic\CUSTINQ.Bootstrap.cs`

```cs
using ASNA.QSys;

namespace SunFarm.Customers
{
    public partial class Custinq : ASNA.QSys.HostServices.Program
    {
    }
}
```

The initial contents are just the empty *partial class* with its required using statement.

## Dynamically calling a QSys Program

The following image shows the complete SunFarm Application *Call Graph*[^1] 

![Application Call Graph](/images/app-call-graph.png)

> Notice how *Interactive* programs are shown with **green** background, while *Batch* programs are shown with **blue** background.

The first `CALL` that happens in the Application is the one from `CUSTINQC` to `CUSTINQ` (the naming convention used by the legacy developers was to use 'C' for the last letter on programs that were written in **CL** language).

If we start with the first program in the Graph, CUSTINQC, the Main C-Specs or *Entry implementation is:

```cs
void StarEntry(int _pc_parms)
{
    Indicator _LR = '0';
    _INLR = '1';
    AddMsgToIgnore(typeof(CPF2103)); /* LIBRARY SO_AND_SO ALREADY IN LIBL */

    TryAddLibLEntry("SunFarm");
    DynamicCaller_.CallD("SunFarm.Customers.CUSTINQ", out _LR);
}
```

The whole mission of CUSTINQC Program is to prepare for the call to the `SunFarm.Customers.CUSTINQ`, the first Interactive program driving the first Display Page. The preparation consists of:

1. Mask the exception code CPF2103, which may be thrown in case the library we are about to add already exists.
2. Add the *library* `SunFarm` to the [library list](https://www.ibm.com/docs/en/i/7.3?topic=ssw_ibm_i_73/cl/addlible.htm), such that database files can be opened without the explicit library reference.
 
After that, the program calls `SunFarm.Customers.CUSTINQ` (and throws away the resulting LR indicator).

Calling `SunFarm.Customers.CUSTINQ` is done as a **Dynamic** way, meaning that it uses the [Procedure Activation Manager](https://asnaqsys.github.io/concepts/program-structure/qsys-program).

Ignore for a moment (we will come back to it later), the instantiation of `DynamicCaller_` object. Suffice for now that the *Dynamic* calling process starts here.

>&#128161; You may want to set a **breakpoint** on this line of code in your live debugging session, to follow the following description.

Move the two `??ENTRY` methods, from source `CUSTINQ.cs` to `CUSTINQ.Bootstrap.cs`, the new code should look like the following:

Sourcefile: `CustomerAppLogic\CUSTINQ.Bootstrap.cs`

```cs
using ASNA.QSys;

namespace SunFarm.Customers
{
    public partial class Custinq : ASNA.QSys.HostServices.Program
    {
        void __ENTRY(out Indicator __inLR, bool _isNew)
        {
            int _pc_parms = 0;
            bool _cleanup = true;
            __inLR = '0';
            try
            {
                _parms = _pc_parms;
                if (_isNew)
                    PROCESS_STAR_INZSR();
                StarEntry(_pc_parms);
            }
            catch (Return)
            {
            }
            catch (System.Threading.ThreadAbortException)
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
            Custinq _instance = null;
            __inLR = '0';
            _instance = _manager.GetInstance(typeof(Custinq), _classFactory, _caller, out _isNew) as Custinq;
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
    }
}
```

Two phases of the Program Bootstrap are invoked:

1. **Find** the Program instance (ENTRY with *one* underscore prefix).
2. **Process** the instance (ENTRY with *two* underscore prefix).

Note how in the [IBMi Developer Model](https://asnaqsys.github.io/concepts/background/ibmi-developer-model.html) it is possible that the instance of the Program called, may be *Active*, in this case, the **Find** phase will be successful and there is no need to initialize the Program.

When there is **no** *Active* instance (as would be the case for the first time the Application runs), the following steps are executed:

1. The new  instance is allocated, and the normal C# constructor is called. 
2. A call to `PROCESS_STAR_INZSR` (which may or may not exist) is made.
3. Remember how many parameters were used to call the Program (implementation for legacy [%PARMS](https://www.ibm.com/docs/en/i/7.3?topic=functions-parms-return-number-parameters)).
4. Monitor how the call will execute (C# try catch block).
5. Call the *Entry method (`StarEntry`).
6. If the program ran thru completion (not terminated abruptly), update the **LR* indicator, for the next program call.

### Implicit Cycle

All programs implement the Main C-Specs as the method `StarEntry`.

> Note that Boilerplate runtime fields start with underscore (`_`) symbol. This is a common practice naming convention for runtime support. Whenever possible abstain from naming your own local fields with such prefix.

All `QSys Programs` (except for when the legacy source was CL language), use the following  Boilerplate code. The RPG legacy main C-Specs are migrated into the ellipsis as presented below:

```cs
void StarEntry(int _pc_parms)
{
    Indicator _LR = '0';

    do
    {
        .
        .
        .
    } while (!(bool)_INLR);
}
```

This Boilerplate code is known as the *Implicit Cycle*. The \*InLR flag, short from **L**ast **R**ecord indicator, came from the Punch-card era, where the duration of the program depended upon a stack of Punch-cards fed to a *Hopper*. Cards were taken of the top of the *Hopper*, processed one by one, until the **last** one.

Notice the peculiar implementation of **LR** indicator.

To simplify the description (and isolate Boilerplate generated code), move the following class field declarations from `CUSTINQ.cs` to `CUSTINQ.Bootstrap.cs`

```cs
// Indicators available to Program:
protected Indicator _INLR;
protected Indicator _INRT;
protected IndicatorArray<Len<_1, _0, _0>> _IN;

// Program parameter count.
int _parms;

protected dynamic DynamicCaller_; // CallD Support
```

The Bootstrap partial class, should now start with code like the following:

```cs
namespace SunFarm.Customers
{
    public partial class Custinq : ASNA.QSys.HostServices.Program
    {
        // Indicators available to Program:
        protected Indicator _INLR;
        protected Indicator _INRT;
        protected IndicatorArray<Len<_1, _0, _0>> _IN;

        // Program parameter count.
        int _parms;

        protected dynamic DynamicCaller_; // CallD Support
        .
        .
        .

```
All indicators are declared inside the Program class. You can say, each program works with its own copy of the indicators (and they are passed around, even to the *DataSet* to reach the Website Model classes). The main reason for implementing it this way is to simplify the application code. Access to this indicators is as direct as you would access a member field variable.

> Note also that the prefix `*` (asterisk) used for RPG system global variables is not available to the C# syntax. The `_` (underscore) is used instead.

We have in the class:

1. [Last Record Indicator](https://www.ibm.com/docs/en/i/7.3?topic=indicators-last-record-indicator-lr) `_INLR`.
2. [Return Indicator](https://www.ibm.com/docs/en/i/7.3?topic=specifications-return-indicator-rt) `_INRT`.
3. Ninety nine general purpose [Internal Indicators](https://www.ibm.com/docs/en/i/7.3?topic=specifications-internal-indicators) `_IN` array.[^2]
4. We have mentioned `_parms` before.
5. There is also the object declaration: `DynamicCaller_` that deserves its own section description ("Calling other Programs from this class" below).

As you can see from the *Implicit Cycle* above, that there is another `_LR` (and you may se more of these as local fields in methods).
The `_LR` local field declarations (note: no `IN` prefix) are used when calling other programs, sending this *temporary* indicator passed as an **out** C# reference. This technique avoids affecting the *program global* `_INLR` (unless specific code exists to synchronize both).

The last part of the implementation *puzzle* is the declaration of the *object* `DynamicCaller_`. 

### Calling other Programs from this class

.NET 5 offers a mechanism to [Bind Objects at Runtime](https://docs.microsoft.com/en-us/dotnet/api/system.dynamic.idynamicmetaobjectprovider?view=net-5.0). This is an advanced mechanism that was part of IBM i (from the early days) which gives the flexibility to de-couple runnable objects, allowing reducing the dependency of source (less recompilation), and the possibility to replace at runtime the actual object that is called (program overriding).

RPG `CALL` is a dynamic call. 

Move the `instanceInit` method (last to move in this topic), from `CUSTINQ.cs` to `CUSTINQ.Bootstrap.cs`, a code similar ro the folloing:

```cs
void _instanceInit()
{
    .
    .
    .
    DynamicCaller_ = new DynamicCaller(this);

    // All file(s) instantiation: workstation, database and printfile.
    CUSTDSPF = new WorkstationFile(PopulateBufferCUSTDSPF, PopulateFieldsCUSTDSPF, null, "CUSTDSPF", "/CustomerAppViews/CUSTDSPF");
    CUSTDSPF.Open();
    CUSTOMERL2 = new DatabaseFile(PopulateBufferCUSTOMERL2, PopulateFieldsCUSTOMERL2, null, "CUSTOMERL2", "*LIBL/CUSTOMERL2", CUSTOMERL2FormatIDs)
    { IsDefaultRFN = true };
    CUSTOMERL1 = new DatabaseFile(PopulateBufferCUSTOMERL1, PopulateFieldsCUSTOMERL1, null, "CUSTOMERL1", "*LIBL/CUSTOMERL1", CUSTOMERL1FormatIDs, blockingFactor: 0)
    { IsDefaultRFN = true };

    CSMASTERL1 = new DatabaseFile(PopulateBufferCSMASTERL1, PopulateFieldsCSMASTERL1, null, "CSMASTERL1", "*LIBL/CSMASTERL1", CSMASTERL1FormatIDs)
    { IsDefaultRFN = true };

    // Data-structure (bound to file fields) instantiation
    CUSTDS = new DataStructure(CUSTDS_000, CUSTDS_001, CUSTDS_002, CUSTDS_003, CUSTDS_004, CUSTDS_005, CUSTDS_006,
        CUSTDS_007, CUSTDS_008, CUSTDS_009, CUSTDS_010, CUSTDS_011, CUSTDS_012, CUSTDS_013, CUSTDS_014, CUSTDS_015);
}
```

Focus first on `DynamicCaller_ = new DynamicCaller(this)`. The DynamicCaller is a `ASNA.QSys` class that implements the .NET 5 interface `IDynamicMetaObjectProvider`.

Without getting into too deep an explanation, the `DynamicCaller_` object will allow:

`DynamicCaller_.CallD(` *program-by-string-name*, *<variable>* parameter-list )

Remember the call:

![Call from CL to RPG](call-from-cl-to-rpg.png)

With code:

```cs
DynamicCaller_.CallD("SunFarm.Customers.CUSTINQ", out _LR);
```

> This *dynamic* call was the one that took us from one program to another. 

Notice:
1. The name of the *qualified* program is a string. The object binding happens at **runtime** (not compile time).
2. The parameters match the declaration of matches the declared parameters in `public static void _ENTRY(ICaller _caller, out Indicator __inLR)`. This is specific to `SunFarm.Customers.CUSTINQ`.

> There are all kinds of supported parameter passing (copy in/ copy out) as [supported by legacy RPG](https://www.ibm.com/docs/en/i/7.1?topic=procedures-passing-data-ile-rpg-program-procedure). 

## Putting all together

The concepts described in this topic are advanced topics, but general understanding is crucial when maintaining Nomad migrating Applications.

Since we have isolated the **Bootstrap** Boilerplate code into its own little source file, let's run a step by step *Debugging* session to put all these ideas together and make some sense of it.

TBD ... (To be continued)

<br>
<br>
<br>


[^1]: Some programs not call directly by the Application have been excluded.
[^2]: There are in fact 100 indicators, but the first (index zero) is not used. This is done such that _IN[1] can be read as the first indicator, avoiding the subtraction from one, like: _IN[1-1], or _IN[0].