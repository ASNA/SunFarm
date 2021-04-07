---
layout: page
title: Merging two Screens
permalink: /merging-two-screens/
---

Oftentimes after reorganizing and cleaning up Green-Screen elements on **Display Pages** — *particularly when preparing for Desktop Browser or large Tablets* — we end up with **lots** of unused space where more information *can* be displayed.

## *We could go from this look:*
![Screen with Unused Space](/images/page-two-04_b.png)

<br>

## *To this richer look:*
![Two Screens merged](/images/page-two-09.png)

The two Green-Screens we will merge are:
1. *Customer Maintenance* Screen.
2. *Display Sales* Screen.

The *Legacy* **Display Sales** green-screen looks like the following image:

![Two Screens merged](/images/legacy-display-sales-screen.png)

The *Legacy* **Display Sales** green-screen shows only two new data items which *can easily* fit into the improved **Customer Maintenance** Display Page.  

Furthermore, the database contains much more useful information about the Sales (not shown in the *Legacy* version) that we could now exploit.

If we look at database records (logical file **CSMASTERL1**), we can see that for each customer we keep monthly sales and returns information on a given year (sales records have the **CSTYPE** code ‘1’ and returns the code ‘2’).

![Database Sales Info](/images/sales-returns-log-file.png)

Displaying the total sales (of all recorded years) is too limited (original *Legacy* Design).  

Depending on the needs of SunFarm company we could argue that it would be more beneficial to show:  

1. Detailed *sales* and *returns* for the last registered year.
2. Totals for that period.
3. Sales trend (last month vs first month of that year)
4. A chart showing how sales progressed throughout the year.

# Business Logic Changes

The Legacy Business Logic, was implemented in two different programs:

1. **CUSTINQ** -- Customer Inquiry *Interactive* Program.
2. **CUSTCALC** -- Customer Calculations (i.e. Total Sales and Returns) *Batch* Program.

As an *Interactive* Program, **CUSTINQ** uses a Displayfile, in this case **CUSTDSPF** (which migrated to a *Display Page* with same name with *cshtml* extension, on the Website).

All Programs were migrated and converted by **ASNA Nomad&reg;** to C# under the *CustomerAppLogic* Project in the Visual Studio Solution. 

Locate CUSTINQ.cs source file in CustomerAppLogic\CUSTINQ.cs, and pay attention to the files being referred:

```c#
namespace SunFarm.Customers
{
    [ASNA.QSys.HostServices.ActivationGroup("*DFTACTGRP")]
    [ProgramEntry("_ENTRY")]
    public partial class Custinq : ASNA.QSys.HostServices.Program
    {
.
.
.
        WorkstationFile CUSTDSPF;
        DatabaseFile CUSTOMERL2;
        DatabaseFile CUSTOMERL1;
```

Three files are referred:
1. Workstation File: **CUSTDSPF**
2. Database Logical File: **CUSTOMERL2**
3. Database Logical File: **CUSTOMERL1**

Both Database Logical File(s): **CUSTOMERL1** and **CUSTOMERL2** are two different views on the **CUSTOMER** physical file, but the information About the Sales and Returns is not in the **CUSTOMER** physical file.

The second program we have mentioned above, the **CUSTCALC** batch Program, *knows* how to get to Sales and Returns records, to compute those Totals.

If you look at the C# source code for CUSTCALC.cs, you will find:

```cs
namespace SunFarm.Customers
{
    [ASNA.QSys.HostServices.ActivationGroup("*DFTACTGRP")]
    [ProgramEntry("_ENTRY")]
    public partial class Custcalc : ASNA.QSys.HostServices.Program
    {
        protected Indicator _INLR;
        protected Indicator _INRT;
        protected IndicatorArray<Len<_1, _0, _0>> _IN;

        DatabaseFile CUSTOMERL1;
        DatabaseFile CSMASTERL1; // Sales and Returns file
```

There is reference to the **CSMASTER** file - thru its Logical file **CSMASTERL1** -

We can copy code from **CUSTCALC.cs** to **CUSTINQ.cs**

## Declaring DatabaseFiles in C# does not make fields available to Program

>&#128161; We don't get the benefits of RPG -- [we are not in Kansas anymore](https://wordhistories.net/2019/06/25/kansas-anymore/) -- 

RPG developers were accustomed to database language facilities that do not exist in a *General Purpose* Language such as C#.

In particular, declaring files in RPG would make available *global* fields that provided access to active records defined in these files (Database or Workstation).

>Please read [RPG Database Language Support](https://asnaqsys.github.io/concepts/program-structure/rpg-language-support.html) to understand how Database access is supported by ASNA QSys.

ASNA QSys Runtime uses [Partial classes](https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/classes-and-structs/partial-classes-and-methods) to *complete* the equivalent database facilities built-into RPG Language.

>Note: When adding new database file references to a C# Program, the implementation of the *partial* IO classes needs to be re-generated. We will show later in this chapter how that is done.   

### Declaring Sales and Returns Database file

Let’s add the following database file declaration on Program CUSTINQC:

```c#
namespace SunFarm.Customers
{
    [ASNA.QSys.HostServices.ActivationGroup("*DFTACTGRP")]
    [ProgramEntry("_ENTRY")]
    public partial class Custinq : ASNA.QSys.HostServices.Program
    {
.
.
.
        WorkstationFile CUSTDSPF;
        DatabaseFile CUSTOMERL2;
        DatabaseFile CUSTOMERL1;
        DatabaseFile CSMASTERL1; // Copied from program CUSTCALC
```
As we have been discussing above, declaring CSMASTERL1 is not enough, we need to:

1. Instance the new DatabaseFile CSMASTERL1 field.

2. Set new file Job’s overrider class.

3. Open and Close the new database file.

## Instance the new DatabaseFile CSMASTERL1 field.
The code we need to *instance* the CSMASTERL1 field we just added to the program, is code we already have in program CUSTCALC.

If we follow the `Custcalc()` constructor we can see how it calls private method `_instanceInit()` and then performs two more method calls related to this new file (including opening it).

**Source file:** `CustomerAppLogic/CUSTCALC.cs`
```cs
void _instanceInit()
{
        . 
        . 
        . 
  
    CSMASTERL1 = new DatabaseFile(PopulateBufferCSMASTERL1, PopulateFieldsCSMASTERL1, null, "CSMASTERL1", "*LIBL/CSMASTERL1", CSMASTERL1FormatIDs){ IsDefaultRFN = true };
  
        .
        .
        .
}
```

We copy that line into CUSTINQ.cs `_instanceInit()` *( right after instancing **CUSTOMERL1** )*:

**Source file:** `CustomerAppLogic/CUSTINQ.cs`
```cs
void _instanceInit()
{
        .
        .
        .
    
    CUSTDSPF = new WorkstationFile(PopulateBufferCUSTDSPF, PopulateFieldsCUSTDSPF, null, "CUSTDSPF", "/CustomerAppViews/CUSTDSPF");
    CUSTDSPF.Open();
    CUSTOMERL2 = new DatabaseFile(PopulateBufferCUSTOMERL2, PopulateFieldsCUSTOMERL2, null, "CUSTOMERL2", "*LIBL/CUSTOMERL2", CUSTOMERL2FormatIDs){ IsDefaultRFN = true };
    CUSTOMERL1 = new DatabaseFile(PopulateBufferCUSTOMERL1, PopulateFieldsCUSTOMERL1, null, "CUSTOMERL1", "*LIBL/CUSTOMERL1", CUSTOMERL1FormatIDs, blockingFactor : 0){ IsDefaultRFN = true };

    CSMASTERL1 = new DatabaseFile(PopulateBufferCSMASTERL1, PopulateFieldsCSMASTERL1, null, "CSMASTERL1", "*LIBL/CSMASTERL1", CSMASTERL1FormatIDs){ IsDefaultRFN = true };

        .
        .
        .
}
```
>Note: `PopulateFieldsCSMASTERL1` and `CSMASTERL1FormatIDs` are not defined yet. We'll add them later.

<br>
## Set new file Job’s overrider class  
Every **DatabaseFile** field instance has a property to set the *Overrider* Job instance. This property is used by the `QSys` runtime support to implement [Overriding](https://www.ibm.com/support/knowledgecenter/en/ssw_ibm_i_72/rzasc/redfio.htm). You don't need to be concerned if your don't understand right now what *Overriding* means, just know that this reference is required.

**Source file:** `CustomerAppLogic/CUSTINQ.cs`
```cs
public Custinq()
{
        ,
        ,
        ,
          
    CUSTOMERL1.Overrider = Job;
    CUSTOMERL1.Open(Job.Database, AccessMode.RWCD, false, false, ServerCursors.Default);
    CUSTOMERL2.Overrider = Job;
    CUSTOMERL2.Open(Job.Database, AccessMode.Read, false, false, ServerCursors.Default);

    CSMASTERL1.Overrider = Job; // Line copied from CUSTCALC's constructor
}
```

## Open and Close the new database file.  
Opening the `CSMASTERL1` database file and closing it, is done - *as expected* - during *construction* and *disposal* of the Program instance.

Copy lines related to Open and Close from `CUSTCALC.cs` to `CUSTINQ.cs`, the complete **CUSTINQ** *Constructor* and *Disposal* methods should look like the following code [^1]:

**Source file:** `CustomerAppLogic/CUSTINQ.cs`
```cs
public Custinq()
{
    _IN = new IndicatorArray<Len<_1, _0, _0>>((char[])null);
    _instanceInit();
    // Initialization of Data Structure fields (Monarch generated)
    Reset__lb_pNbrs();
    CUSTOMERL1.Overrider = Job;
    CUSTOMERL1.Open(Job.Database, AccessMode.RWCD, false, false, ServerCursors.Default);
    CUSTOMERL2.Overrider = Job;
    CUSTOMERL2.Open(Job.Database, AccessMode.Read, false, false, ServerCursors.Default);

    CSMASTERL1.Overrider = Job;
    CSMASTERL1.Open(Job.Database, AccessMode.Read, false, false, ServerCursors.Default);
}

override public void Dispose(bool disposing)
{
    if (disposing)
    {
        
        CUSTDSPF.Close();
        CUSTOMERL2.Close();
        CUSTOMERL1.Close();

        CSMASTERL1.Close();
    }
    base.Dispose(disposing);
}
```

To make available fields to the Program as specified by the record's database schema, we need to do the following:

1. Declare fields (as defined by the record schema).
2. Implement populate field's in-out (*of dataset*) methods in the IO partial class.


## Declare fields (as defined by the record schema)  
Once we have copied the declaration of database file `CSMASTERL1` from CUSTCALC.cs to CUSTINQ.cs, and updated its Instantiation and Open/Close code, we proceed to complete its setup by declaring fields (as defined by the record schema).

The easiest, and less error-prone method to declare fields (as defined in the record schema), is to use a Tool. It can be done manually, but it may be too cumbersome and error-prone.

Notice how each Program listed under the CustomerAppLogic in the Visual Studio Solution shows an icon that can be used to expand the Program node in the tree to show related source files (in this case partial class sources).

For example, expanding node CUSTINQ.cs on the Project tree, will show the following sources:

~~~
CUSTINQ.cs
   CUSTINQ.Io.cs      
   CUSTINQ.Io.xfu
~~~

The **IO** (*Input* / *Output*) sources are the implementation of the *partial classes* that complete the Program with the RPG database facilities mentioned above.

These files are *generated* files and should be re-generated using [Nomad Tools](https://asnaqsys.github.io/concepts/enhancements/nomad-tools.html)

All we need is access to the **Nomad Tools**, which can be reached in Visual Studio 2019 Solution Explorer, by selecting a program - such as `CUSTCALC` - and opening the *Context Menu* options using *right-mouse* click.

![Nomad Tools Context Menu Options](/images/serengeti-context-menus.jpg)

Serengeti Context Menu Tools:
1. Refresh XFU
2. Run Custom Tool

## Refresh XFU
If you look at the contents of `CUSTINQ.Io.xfu` you will find a stand-alone [XML](https://en.wikipedia.org/wiki/XML) document which contains external description of all database files used by the program `CUSTINQ.cs`. Since we have added one more Database file to `CUSTINQ` we need to *regenerate* or *refresh* the contents of the XML file.

Run **Refresh XFU**

## Run Custom Tool
Once we have the `CUSTINQ.Io.xfu` up-to-date (after running **Refresh XFU**), we need to execute the second Nomad Tool: **Run Custom Tool**

What **Run Custom Tool** does is *re-generate*:

a. Field declarations of files according to the latest definition in `CUSTINQ.Io.xfu` 

b. Implementation of methods *PopulateBuffer* and *PopulateFields* in source file `CUSTINQ.Io.cs` for every single file referred to by `CUSTINQ.cs` 

>Once we run the Nomad Tools, our *CustomerAppLogic* Project should compile. We need to move to to the *Interactive* section of the program, that is, work on the Website Pages.

# Displaying new data on Customer Maintenance Page

We added the ability to Program `CUSTINQ` to deal with *Sales and Returns* information for a customer (previously only in `CUSTCALC`).

Let’s now focus in the Front-End. 

Open the `CustomerAppSite/Areas/CustomerAppViews/Pages/CUSTDSPF.cshtml` markup file and locate `DdsRecord For="CUSTREC"` TagHelper.

So far, we have used *Rows* 2 thru 10. We have room in *Rows* 12 thru 20.

The following image shows what we are about to add to the Page:

![New Data for Customer Maintenance Page](/images/new-data-page-two_01.png/)

We have several new constants, and around thirty new fields. Let’s concentrate on the new fields we want to display. Fields are referred to by the tag-helpers using the attribute For=

First let’s start by displaying Sales information. Rows 12 thru 15:

```html
<div Row="12">
    <DdsConstant Col="8" Text="Last registered sales" />
</div>
<div Row="13">
    <DdsConstant Col="12" Text="Jan" />
    <DdsDecField Col="15" For="CUSTREC.CSSALES01" EditCode=One />
    <DdsConstant Col="30" Text="Feb" />
    <DdsDecField Col="35" For="CUSTREC.CSSALES02" EditCode=One />
    <DdsConstant Col="48" Text="Mar" />
    <DdsDecField Col="51" For="CUSTREC.CSSALES03" EditCode=One />
    <DdsConstant Col="66" Text="Apr" />
    <DdsDecField Col="69" For="CUSTREC.CSSALES04" EditCode=One />
</div>
<div Row="14">
    <DdsConstant Col="12" Text="May" />
    <DdsDecField Col="15" For="CUSTREC.CSSALES05" EditCode=One />
    <DdsConstant Col="30" Text="Jun" />
    <DdsDecField Col="35" For="CUSTREC.CSSALES06" EditCode=One />
    <DdsConstant Col="48" Text="Jul" />
    <DdsDecField Col="51" For="CUSTREC.CSSALES07" EditCode=One />
    <DdsConstant Col="66" Text="Aug" />
    <DdsDecField Col="69" For="CUSTREC.CSSALES08" EditCode=One />
</div>
<div Row="15">
    <DdsConstant Col="12" Text="Sep" />
    <DdsDecField Col="15" For="CUSTREC.CSSALES09" EditCode=One />
    <DdsConstant Col="30" Text="Oct" />
    <DdsDecField Col="35" For="CUSTREC.CSSALES10" EditCode=One />
    <DdsConstant Col="48" Text="Nov" />
    <DdsDecField Col="51" For="CUSTREC.CSSALES11" EditCode=One />
    <DdsConstant Col="66" Text="Dec" />
    <DdsDecField Col="69" For="CUSTREC.CSSALES12" EditCode=One />
</div>
```

If we ignore for a moment the display attributes and concentrate on the new data-fields we have twelve new fields:

```html
    For="CUSTREC.CSSALES01"
    For="CUSTREC.CSSALES02"
    For="CUSTREC.CSSALES03"
    For="CUSTREC.CSSALES04"
    For="CUSTREC.CSSALES05"
    For="CUSTREC.CSSALES06"
    For="CUSTREC.CSSALES07"
    For="CUSTREC.CSSALES08"
    For="CUSTREC.CSSALES09"
    For="CUSTREC.CSSALES10"
    For="CUSTREC.CSSALES11"
    For="CUSTREC.CSSALES12"
```
You may have noticed that Visual Studio Razor Page smart editor is highlighting names with the reference to known CUSTREC Model property as `undefined`.

Let's proceed to define the *twelve* sales fields.

All fields used in the **Markup** should be defined in the *Model* class for the record format in question.

In this particular case, we need to add fields to `CUSTREC` record in the Model.

Open the Model C# source file:

~~~
CustomerAppSite\Areas\CustomerAppViews\Pages\CUSTDSPF.cshtml.cs
~~~

Locate the `CUSTREC_Model` class.

>&#128161; All *Display Record Format*'s Model class names end with `_Model`

```cs
[
    Record(FunctionKeys = "F4 04;F6 06:!30;F11 11:!30;F12 12",
        EraseFormats = "SFLC KEYS SALESREC"
    )
]
public class CUSTREC_Model : RecordModel
{
        .
        .
        .
    [Char(10)]
    public string SCPGM { get; private set; }

    [Dec(6, 0)]
    public decimal SFCUSTNO { get; private set; } // CUSTOMER NUMBER

    [Char(40)]
    public string SFOLDNAME { get; private set; }
        .
        .
        .
    [Char(1)]
    [Values(typeof(string), "Y", "N")]
    public string SFYN01 { get; set; }
}
```

Each field is declared as a property of the class with particular access. 

[Bracket C# notation](https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/concepts/attributes/) is used to *decorate* additional *RPG-like* attributes to describe in detail the *type* and *valid* values expected by *DataSet* use to communicate with the *Business Rules Logic* (the Programs).

The last field defined in class in the class `CUSTREC_Model` is `SFYN01`.

The following field definitions are added, right after `SFYN01`.

```cs
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
```
>Note: We used Dec(11,2) because we know the schema definition for the *Sales and Returns* physical file: `CSMASTER`. 

## Refreshing XFU after adding fields to Markup 

We have addedd fields to the class `CUSTREC_Model` because we want use them in the *Markup* to be displayed when the Page renders by the Browser.

The value of fields needs to come from Program `CUSTINQ` before the Page is rendered.

The new value (*for input-capable fields*) entered by end-users, when *valid* needs to be transferred to the Program `CUSTINQ` for processing.

A middle-tier data layer called the *DataSet* is used to move values from `CUSTINQ` Program fields to the `CUSTREC_Model` fields, and back from `CUSTREC_Model` fields to `CUSTINQ` Program fields.

The following graph shows a simplified representation of the *DataSet* communication buffer:

![Data Set model](/images/data-set-model.png/)

The *DataSet* holds the values of all *active* records. Similarly to the way RPG application works, the control flow starts with a *Program* writing records and then executing formats. The *schema* of the records in the DataSet must match the *schema* of the fields declared in the Program as well as the fields declared in the Model.

Similarly to how we executed [Nomad Tools](https://asnaqsys.github.io/concepts/enhancements/nomad-tools.html) earlier in this Chapter to update the *IO* **XFU** file, we need to repeat the process every time we modify the *Model* to keep the *DataSet* in sync.

* Run **Refresh XFU**
* Run Custom Tool

If no errors are generated by [Nomad Tools](https://asnaqsys.github.io/concepts/enhancements/nomad-tools.html), compile the Business Logic Project and run the Website. 

Selecting any of the records on the Customer list and opting to "Update", will present the Customer Maintenance Page will present a year worth of *Sales information*, but all values will be **zero**.

### Populating Sales fields from Database file fields

CUSTINQ Program is responsible for populating the fields `CSSALES01` ... `CSSALES12`, before *executing* Display Record Format "CUSTREC".

We need to add code before `CUSTDSPF.ExFmt("CUSTREC", _IN.Array);` runs in method `RcdUpdate()`
Source file: `SunFarm/CustomerAppLogic/CUSTINQ.cs`

```cs
void RcdUpdate()
{
    Indicator _LR = '0';
    ClearSel();

        .
        .
        .
    //--------------------------------------------------
    do
    {
        CUSTDSPF.Write("MSGSFC", _IN.Array);
        CUSTDSPF.ExFmt("CUSTREC", _IN.Array);
        .
        .
        .

    } while (!((bool)_IN[12]));
}        
```

Let's add a call to a new method that we will name `LoadLastSalesAndReturns` which will take care for populating the new fields `CSSALES01` ... `CSSALES12`.

The new method implements the following logic:

```cs
private void LoadLastSalesAndReturns()
{
    CSSALES01 = CSSALES02 = CSSALES03 = CSSALES04 = CSSALES05 = CSSALES06 = 0;
    CSSALES07 = CSSALES08 = CSSALES09 = CSSALES10 = CSSALES11 = CSSALES12 = 0;

    FixedDecimal<_9, _0> CustomerNumber = new FixedDecimal<_9, _0>();

    CustomerNumber = CMCUSTNO.MoveRight(CustomerNumber);
    if ( CSMASTERL1.Seek(SeekMode.SetLL, CustomerNumber) )
        CSMASTERL1.ReadNextEqual(false, CustomerNumber);
}
```

We start by clearing the values of fields `CSSALES01` ... `CSSALES12`. Then we perform database file operations to find the record using the current `CustomerNumber`. If the search is successful, we **Read** the *Sales and Returns* record.

Thanks to the **IO* partial class implementation in `CUSTINQ.Io.cs` the fields `CSSALES01` ... `CSSALES12` are populated with the values read from the database logical file `CSMASTERL1`.

>&#128161; Examine generated code in `CUSTINQ.Io.cs` to understand how method `PopulateFieldsCSMASTERL1()` takes care of populating fields `CSSALES01` ... `CSSALES12` while `CSMASTERL1.ReadNextEqual` runs. You may even set breakpoints to step thru the code while this happens.

Compile the Business Logic Project and run the Website. 

Selecting any of the records on the Customer list and opting to "Update", will present the *Customer Maintenance* Page will present a year worth of *Sales information*, with the first year of data values. (Note: the label says: "Last registered sales" we will fix that later) [^2].

The following image shows progress:

![Last registered Sales](/images/page-two-05.png/)
  
<br>

## Add *Returns* Data to the Page

Similar to how we added *Sales data* to the Page, we will add *Returns data*.

We start by adding the *Returns* fields to the Markup:

Open Markup file:  

~~~
CustomerAppSite\Areas\CustomerAppViews\Pages\CUSTDSPF.cshtml
~~~

Locate `For="CUSTREC"` and **Add* the following *Rows* (17 thru 20) to the Display Record:

```html
<div Row="17">
    <DdsConstant Col="8" Text="Last registered returns" />
</div>

<div Row="18">
    <DdsConstant Col="12" Text="Jan" />
    <DdsDecField Col="15" For="CUSTREC.CSRETURN01" EditCode=One Color="red" />
    <DdsConstant Col="30" Text="Feb" />
    <DdsDecField Col="35" For="CUSTREC.CSRETURN02" EditCode=One Color="red" />
    <DdsConstant Col="48" Text="Mar" />
    <DdsDecField Col="51" For="CUSTREC.CSRETURN03" EditCode=One Color="red" />
    <DdsConstant Col="66" Text="Apr" />
    <DdsDecField Col="69" For="CUSTREC.CSRETURN04" EditCode=One Color="red" />
</div>
<div Row="19">
    <DdsConstant Col="12" Text="May" />
    <DdsDecField Col="15" For="CUSTREC.CSRETURN05" EditCode=One Color="red" />
    <DdsConstant Col="30" Text="Jun" />
    <DdsDecField Col="35" For="CUSTREC.CSRETURN06" EditCode=One Color="red" />
    <DdsConstant Col="48" Text="Jul" />
    <DdsDecField Col="51" For="CUSTREC.CSRETURN07" EditCode=One Color="red" />
    <DdsConstant Col="66" Text="Aug" />
    <DdsDecField Col="69" For="CUSTREC.CSRETURN08" EditCode=One Color="red" />
</div>
<div Row="20">
    <DdsConstant Col="12" Text="Sep" />
    <DdsDecField Col="15" For="CUSTREC.CSRETURN09" EditCode=One Color="red" />
    <DdsConstant Col="30" Text="Oct" />
    <DdsDecField Col="35" For="CUSTREC.CSRETURN10" EditCode=One Color="red" />
    <DdsConstant Col="48" Text="Nov" />
    <DdsDecField Col="51" For="CUSTREC.CSRETURN11" EditCode=One Color="red" />
    <DdsConstant Col="66" Text="Dec" />
    <DdsDecField Col="69" For="CUSTREC.CSRETURN12" EditCode=One Color="red" />
</div>
```
>Note: we are setting the Color to `red` instead of showing *negative* sign for the *Returns* values. In the Business Logic, we will convert *negative* values to *absolute* values.

Next, **Add** to the `CUSTREC_Model` the definitions for the *Returns* fields - just as we did before to the `CSSALESnn` fields (above) -.

```cs
[Dec(11, 2)]
public decimal CSRETURN01 { get; private set; }

[Dec(11, 2)]
public decimal CSRETURN02 { get; private set; }

[Dec(11, 2)]
public decimal CSRETURN03 { get; private set; }

[Dec(11, 2)]
public decimal CSRETURN04 { get; private set; }

[Dec(11, 2)]
public decimal CSRETURN05 { get; private set; }

[Dec(11, 2)]
public decimal CSRETURN06 { get; private set; }

[Dec(11, 2)]
public decimal CSRETURN07 { get; private set; }

[Dec(11, 2)]
public decimal CSRETURN08 { get; private set; }

[Dec(11, 2)]
public decimal CSRETURN09 { get; private set; }

[Dec(11, 2)]
public decimal CSRETURN10 { get; private set; }

[Dec(11, 2)]
public decimal CSRETURN11 { get; private set; }

[Dec(11, 2)]
public decimal CSRETURN12 { get; private set; }
```

Re-run [Nomad Tools](https://asnaqsys.github.io/concepts/enhancements/nomad-tools.html) on the `CUSTINQ` Program, in the `CustomerAppLogic` Project.[^3]

Re-run the Website and navigate to a Customer "Update" Page:

![Returns Initial Rendering](/images/page-two-06.png/)

We got values for all Sales *Returns* for one year, but all values are *zero*.

You should figure out by now, that we are missing some code in the program logic.

Get back to Program `CUSTINQ` and *add* the code to `LoadLastSalesAndReturns` private method, to match the following listing:

```cs
private void LoadLastSalesAndReturns()
{
    CSSALES01 = CSSALES02 = CSSALES03 = CSSALES04 = CSSALES05 = CSSALES06 = 0;
    CSSALES07 = CSSALES08 = CSSALES09 = CSSALES10 = CSSALES11 = CSSALES12 = 0;

    CSRETURN01 = CSRETURN02 = CSRETURN03 = CSRETURN04 = CSRETURN05 = CSRETURN06 = 0;
    CSRETURN07 = CSRETURN08 = CSRETURN09 = CSRETURN10 = CSRETURN11 = CSRETURN12 = 0;

    FixedDecimal< _9, _0> CustomerNumber = new FixedDecimal<_9, _0>();

    CustomerNumber = CMCUSTNO.MoveRight(CustomerNumber);
    if (!CSMASTERL1.Seek(SeekMode.SetLL, CustomerNumber))
        return;

    Dictionary<decimal, YearData> salesForCustomer = new Dictionary<decimal, YearData>();
    Dictionary<decimal, YearData> returnsForCustomer = new Dictionary<decimal, YearData>();
    decimal lastYearSales = decimal.MinValue;
    decimal lastYearReturns = decimal.MinValue;

    while (CSMASTERL1.ReadNextEqual(false, CustomerNumber))
    {
        if (CSTYPE == 1)
        {
            lastYearSales = CSYEAR;
            salesForCustomer.Add(lastYearSales, new YearData(CSSALES01, CSSALES02, CSSALES03, CSSALES04, CSSALES05, CSSALES06, CSSALES07, CSSALES08, CSSALES09, CSSALES10, CSSALES11, CSSALES12));
        }
        else
        {
            lastYearReturns = CSYEAR;
            returnsForCustomer.Add(lastYearReturns, new YearData(CSSALES01, CSSALES02, CSSALES03, CSSALES04, CSSALES05, CSSALES06, CSSALES07, CSSALES08, CSSALES09, CSSALES10, CSSALES11, CSSALES12));
        }
    }

    if (lastYearSales > decimal.MinValue)
    {
        CSSALES01 = salesForCustomer[lastYearSales].month[0];
        CSSALES02 = salesForCustomer[lastYearSales].month[1];
        CSSALES03 = salesForCustomer[lastYearSales].month[2];
        CSSALES04 = salesForCustomer[lastYearSales].month[3];
        CSSALES05 = salesForCustomer[lastYearSales].month[4];
        CSSALES06 = salesForCustomer[lastYearSales].month[5];
        CSSALES07 = salesForCustomer[lastYearSales].month[6];
        CSSALES08 = salesForCustomer[lastYearSales].month[7];
        CSSALES09 = salesForCustomer[lastYearSales].month[8];
        CSSALES10 = salesForCustomer[lastYearSales].month[9];
        CSSALES11 = salesForCustomer[lastYearSales].month[10];
        CSSALES12 = salesForCustomer[lastYearSales].month[11];
    }

    if (lastYearReturns > decimal.MinValue)
    {
        CSRETURN01 = returnsForCustomer[lastYearReturns].month[0];
        CSRETURN02 = returnsForCustomer[lastYearReturns].month[1];
        CSRETURN03 = returnsForCustomer[lastYearReturns].month[2];
        CSRETURN04 = returnsForCustomer[lastYearReturns].month[3];
        CSRETURN05 = returnsForCustomer[lastYearReturns].month[4];
        CSRETURN06 = returnsForCustomer[lastYearReturns].month[5];
        CSRETURN07 = returnsForCustomer[lastYearReturns].month[6];
        CSRETURN08 = returnsForCustomer[lastYearReturns].month[7];
        CSRETURN09 = returnsForCustomer[lastYearReturns].month[8];
        CSRETURN10 = returnsForCustomer[lastYearReturns].month[9];
        CSRETURN11 = returnsForCustomer[lastYearReturns].month[10];
        CSRETURN12 = returnsForCustomer[lastYearReturns].month[11];
    }
}    
```

## Algorithm to Calculate Last Year's Sales and Returns
1. Start `lastYearSales` and `lastYearReturns` with the *minimum* value.
2. Loop reading records for the *Customer* of interest until the **Customer Number** is different from what we are processing.
3. In the Loop in (2), depending on the *Record Type*, we collect *Sales* or *Returns* in a dictionary collection that uses **Year** as key. We also keep the lastYearSales and lastYearReturns updated. (We know that the records are sorted by year within a Customer).
4. After Loop in (2) is complete, we can **Update** the `CSSALESnn` field group and/or `CSRETURNnn` field group, by extracting from the Dictionary the lastYear’s data.
  
> Note: `CSTYPE` database field defines the *Record Type*. If the `CSTYPE` has the value `1`, then the record has *Sales* information, otherwise (i.e. `CSTYPE` has the value `2`), the record has *Returns* information.

If you try to compile the `CustomerAppLogic` you may have noticed that we are missing the implementation of *YearData* class.

## Adding standard C# Helper classes to Business Logic

Even when the code we are working on, came from a machine *Migration* and follows certain conventions to assist with the *Legacy* RPG developer's mental model, there is nothing that prevents us from using *modern* C# code with access to the full **.NET** framework.

Our algorithm above, uses a [.NET memory collection](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.dictionary-2?view=net-5.0) to hold an instance of a class that holds a year full of data values. The *Dictionary* collection is keyed by any *type* we want, in this case, we conveniently use a *Year* value.

All we are missing now is the implementation of such *YearData* class.

Add the following class implementation at the end of the source file `CUSTINQ`.

```cs
internal class YearData
{
    const int MONTHS_IN_YEAR = 12;
    public decimal[] month;

    public YearData()
    {
        month = new decimal[MONTHS_IN_YEAR];
    }

    public YearData(decimal jan, decimal feb, decimal mar, decimal apr, decimal may, decimal jun, decimal jul, decimal aug, decimal sep, decimal oct, decimal nov, decimal dec) : this()
    {
        month[0] = jan;
        month[1] = feb;
        month[2] = mar;
        month[3] = apr;
        month[4] = may;
        month[5] = jun;
        month[6] = jul;
        month[7] = aug;
        month[8] = sep;
        month[9] = oct;
        month[10] = nov;
        month[11] = dec;
    }

    public decimal Sum()
    {
        decimal result = 0;
        foreach (var amt in month)
            result += amt;

        return result;
    }
}
```
Compile CustomerAppLogic and run Website. Navigate to any customer to *Update* its records. You should get a Page similar to the following:


![Last Registered Returns](/images/page-two-07.png/)


## Additional Table heading data: Year, Total and Trend

Let’s define one more CSS Style - to make Sales and Returns label stronger -.

Add following style at the end of file: `CustomerAppSite\wwwroot\css\site.css`

```css
.large-bold-text {
    font-size: large !important;
    font-weight: bold;
}
```

On the row for the Table Heading "Last registered sales" we will add:
1. `Year` for which the sales data was found in the database.
2. `Total Sales`.
3. `Sales change` in percent for December compared to January for that Year.

The new Model properties are:

```cs
[Char(20)]
public string YEAR_SALES { get; private set; }

[Dec(13, 2)]
public decimal TOTAL_SALES { get; private set; }

[Char(20)]
public string PERCENT_CHANGE_SALES { get; private set; }
```
>Note how we define the properties as *output-only* (public get, private set). We define `TOTAL_SALES` as decimal (so we can take advantage of EditCode formatter). But since we want an uncommon formatting for the `Year` and `Sales` change in percent for December, we will use business logic with nice `.Net` framework formatting classes to format this data as a string and use it as such.
  
As we did before, navigate to the `CUSTINQ` Program in the CustomerAppLogic Project using *Visual Studio Solution Explorer* and run the [Nomad Tools](https://asnaqsys.github.io/concepts/enhancements/nomad-tools.html). With the updated *DataSet*, we should have the fields `YEAR_SALES`, `TOTAL_SALES` and `PERCENT_CHANGE_SALES` available for us to use (and populate) from within the `CUSTINQ` Program logic.

Add the code that deals with `YEAR_SALES`, `TOTAL_SALES` and `PERCENT_CHANGE_SALES` calculations, by updating the method `LoadLastSalesAndReturns()`, matching this listing:

```cs
private void LoadLastSalesAndReturns()
{
    CSSALES01 = CSSALES02 = CSSALES03 = CSSALES04 = CSSALES05 = CSSALES06 = 0;
    CSSALES07 = CSSALES08 = CSSALES09 = CSSALES10 = CSSALES11 = CSSALES12 = 0;

    CSRETURN01 = CSRETURN02 = CSRETURN03 = CSRETURN04 = CSRETURN05 = CSRETURN06 = 0;
    CSRETURN07 = CSRETURN08 = CSRETURN09 = CSRETURN10 = CSRETURN11 = CSRETURN12 = 0;

    YEAR_SALES = string.Empty;
    TOTAL_SALES = 0;
    PERCENT_CHANGE_SALES = string.Empty;

    YEAR_RETURNS = string.Empty;
    TOTAL_RETURNS = 0;
    PERCENT_CHANGE_RETURNS = string.Empty;

    FixedDecimal< _9, _0> CustomerNumber = new FixedDecimal<_9, _0>();

    CustomerNumber = CMCUSTNO.MoveRight(CustomerNumber);
    if (!CSMASTERL1.Seek(SeekMode.SetLL, CustomerNumber))
        return;

    Dictionary<decimal, YearData> salesForCustomer = new Dictionary<decimal, YearData>();
    Dictionary<decimal, YearData> returnsForCustomer = new Dictionary<decimal, YearData>();
    decimal lastYearSales = decimal.MinValue;
    decimal lastYearReturns = decimal.MinValue;

    while (CSMASTERL1.ReadNextEqual(false, CustomerNumber))
    {
        if (CSTYPE == 1)
        {
            lastYearSales = CSYEAR;
            salesForCustomer.Add(lastYearSales, new YearData(CSSALES01, CSSALES02, CSSALES03, CSSALES04, CSSALES05, CSSALES06, CSSALES07, CSSALES08, CSSALES09, CSSALES10, CSSALES11, CSSALES12));
        }
        else
        {
            lastYearReturns = CSYEAR;
            returnsForCustomer.Add(lastYearReturns, new YearData(CSSALES01, CSSALES02, CSSALES03, CSSALES04, CSSALES05, CSSALES06, CSSALES07, CSSALES08, CSSALES09, CSSALES10, CSSALES11, CSSALES12));
        }
    }

    if (lastYearSales > decimal.MinValue)
    {
        CSSALES01 = salesForCustomer[lastYearSales].month[0];
        CSSALES02 = salesForCustomer[lastYearSales].month[1];
        CSSALES03 = salesForCustomer[lastYearSales].month[2];
        CSSALES04 = salesForCustomer[lastYearSales].month[3];
        CSSALES05 = salesForCustomer[lastYearSales].month[4];
        CSSALES06 = salesForCustomer[lastYearSales].month[5];
        CSSALES07 = salesForCustomer[lastYearSales].month[6];
        CSSALES08 = salesForCustomer[lastYearSales].month[7];
        CSSALES09 = salesForCustomer[lastYearSales].month[8];
        CSSALES10 = salesForCustomer[lastYearSales].month[9];
        CSSALES11 = salesForCustomer[lastYearSales].month[10];
        CSSALES12 = salesForCustomer[lastYearSales].month[11];

        YEAR_SALES = $"(Year {lastYearSales})";
        TOTAL_SALES = salesForCustomer[lastYearSales].Sum();

        if (CSSALES12 > CSSALES01 && CSSALES12 > 0)
        {
            decimal calc = (CSSALES01 * 100) / CSSALES12;
            PERCENT_CHANGE_SALES = $"↑ +{Math.Round(calc, 1)}%";
        }
        else if (CSSALES12 < CSSALES01 && CSSALES01 > 0)
        {
            decimal calc = (CSSALES12 * 100) / CSSALES01;
            PERCENT_CHANGE_SALES = $"↓ +{Math.Round(calc, 1)}%";
        }
    }

    if (lastYearReturns > decimal.MinValue)
    {
        CSRETURN01 = returnsForCustomer[lastYearReturns].month[0];
        CSRETURN02 = returnsForCustomer[lastYearReturns].month[1];
        CSRETURN03 = returnsForCustomer[lastYearReturns].month[2];
        CSRETURN04 = returnsForCustomer[lastYearReturns].month[3];
        CSRETURN05 = returnsForCustomer[lastYearReturns].month[4];
        CSRETURN06 = returnsForCustomer[lastYearReturns].month[5];
        CSRETURN07 = returnsForCustomer[lastYearReturns].month[6];
        CSRETURN08 = returnsForCustomer[lastYearReturns].month[7];
        CSRETURN09 = returnsForCustomer[lastYearReturns].month[8];
        CSRETURN10 = returnsForCustomer[lastYearReturns].month[9];
        CSRETURN11 = returnsForCustomer[lastYearReturns].month[10];
        CSRETURN12 = returnsForCustomer[lastYearReturns].month[11];

        YEAR_RETURNS = $"(Year {lastYearReturns})";
        TOTAL_RETURNS = returnsForCustomer[lastYearReturns].Sum();

        decimal janReturns = Math.Abs(CSRETURN01);
        decimal decReturns = Math.Abs(CSRETURN12);

        if (decReturns > janReturns && decReturns > 0)
        {
            decimal calc = (janReturns * 100) / decReturns;
            PERCENT_CHANGE_RETURNS = $"↑ +{Math.Round(calc, 1)}%";
        }
        else if (decReturns < janReturns && CSRETURN01 > 0)
        {
            decimal calc = (decReturns * 100) / janReturns;
            PERCENT_CHANGE_RETURNS = $"↓ +{Math.Round(calc, 1)}%";
        }
    }
}
```

Compile `CustomerAppLogic` and Run the Website Application. Navigate to any Customer to **Update** its record information and you should get a Page similar to the following:

![Totals and Trend](/images/page-two-08_a.png/)

The `Sales heading` is now more useful, but the `Total Sales` for the year are not aligned how we would want it. 

Recall that `CUSTREC.TOTAL_SALES` is a Decimal Field. Decimal fields are aligned to the *right* by default.

Let’s change its *CSS style* to correct the problem. We have solved this problem before. We already have a *CSS Style* called left-aligned-field.

The field `For="CUSTREC.TOTAL_SALES"` in the Markup already has a class="large-bold-text". Standard *CSS* is by definition a *cascading* technology, meaning that several styles may be applied. The syntax is simple, we can just add one *more* style separated with a space:

```html
<div Row="12">
    <DdsConstant Col="8" class="large-bold-text" Text="Last registered sales" />
    <DdsCharField Col="27" class="large-bold-text" For="CUSTREC.YEAR_SALES" />
    <DdsDecField Col="63" class="large-bold-text left-aligned-field" 
      For="CUSTREC.TOTAL_SALES" EditCode=One />
    <DdsCharField Col="75" class="large-bold-text" For="CUSTREC.PERCENT_CHANGE_SALES" Color="green" />
</div>
```

Re-run the Website Application, the heading is now complete[^4]:

![Totals and Trend Aligned](/images/page-two-08_b.png/)

We can repeat the steps to add a proper header to the "Last registered returns table". We will show only the new markup, you can use the GitHub project history to look at the rest of the code.

The Markup for Row 17 is:

```html
<div Row="17">
    <DdsConstant Col="8" class="large-bold-text" Text="Last registered returns" />
    <DdsCharField Col="27" class="large-bold-text" For="CUSTREC.YEAR_RETURNS" />
    <DdsDecField Col="63" class="large-bold-text left-aligned-field" 
      For="CUSTREC.TOTAL_RETURNS" EditCode=One Color="red" />
    <DdsCharField Col="75" class="large-bold-text" For="CUSTREC.PERCENT_CHANGE_RETURNS" Color="red" />
</div>
```

The two headings should now show like the following image[^5]:

![Proper heading CSS Style](/images/page-two-09.png/)

<br>
<br>
<br>
[Continue ...]({{ site.rooturl }}/adding-chart-to-page/)

[^1]: Commit "Declaration of Sales Returns file in CUSTINQ.cs"
[^2]: Commit "First Sales information added to Page"
[^3]: Commit "Adding Returns to the CUSTREC_Model DataSet"
[^4]: Commit "Last Registered Sales Heading"
[^5]: Commit "Last registered returns heading complete"
