---
layout: page
title: Merging two Screens
permalink: /merging-two-screens/
---

Oftentimes after reorganizing and cleaning Green-Screen elements on **Display Pages** — *particularly when preparing for Desktop Browser or large Tablets* — we end up with **lots** of unused space where more information *can* be displayed.

## *We could go from this look:*
![Screen with Unused Space](/images/page-two-04_b.png)

<br>

## *To this richer look:*
![Two Screens merged](/images/page-two-09.png)

The two Green-Screens we will merge are:
1. *Customer Maintenance* Screen.
2. *Display Sales* Screen.

The **Display Sales** green-screen looks like the following image:

![Two Screens merged](/images/legacy-display-sales-screen.png)

The **Display Sales** green-screen shows only two new data items which *can easily* fit in the **Customer Maintenance** Display Page we have been enhancing.  

Furthermore, the database contains much more useful information about the Sales (not shown in Legacy Application) that we could now exploit.

If we look at database records (logical file **CSMASTERL1**), we can see that for each customer we keep monthly sales and returns information on a given year (sales records have the **CSTYPE** code ‘1’ and returns the code ‘2’).

![Database Sales Info](/images/sales-returns-log-file.png)

Displaying the total sales (of all recorded years) is too limited.  

Depending on the needs of SunFarm company we could argue that it would be more beneficial to show:  

1. Detailed *sales* and *returns* for the last registered year.
2. Totals for that period.
3. Sales trend (last month vs first month of that year)
4. A chart showing how sales progressed throughout the year.

# Business Logic Changes

The Legacy Business Logic, was implemented in two different programs:

1. **CUSTINQ** -- Customer Inquiry Interactive Program.
2. **CUSTCALC** -- Customer Calculations (i.e. Total Sales and Returns) Batch Program.

As an Interactive Program, **CUSTINQ** uses a Displayfile, in this case **CUSTDSPF** (which migrated to a *Display Page* with same name with *cshtml* extension, on the Website).

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

>&#128161; We don't get the benefits of RPG -- *we are not in Kansas anymore* ;) -- 

RPG developers were accustomed to database language facilities that do not exist in a *General Purpose* Language such as C#.

In particular, declaring files in RPG would make available *global* fields that provided access to active records defined in these files (Database or Workstation).

ASNA Monarch Runtime uses [Partial classes](https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/classes-and-structs/partial-classes-and-methods) to *complete* the equivalent database facilities built-into RPG Language.
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

1. Declare fields as defined by the record schema of database file added.

2. Initialize the new DatabaseFile @ _instanceInit() - where we connect with methods to populate fields in-out (*of dataset*).

3. Implement populate field's in-out (*of dataset*) methods in the IO partial class.

4. Set new file Job’s overrider class.

5. Open and Close the new database file.

## Declare fields (as defined by the record schema)  
The easiest, and less error-prone method to declare fields as defined in the record schema, is to use a Tool. It can be done manually, but it may be too cumbersome and error-prone.

Notice how each Program listed under the CustomerAppLogic in the Visual Studio Solution shows an icon that can be used to expand the Program node in the tree, to show lated source files (in this case partial class sources).

For example, expanding node CUSTINQ.cs on the Project tree, will show the following sources:

~~~
CUSTINQ.cs
   CUSTINQ.Io.cs      
   CUSTINQ.Io.xfu
~~~

The **IO** (*Input* / *Output*) sources are the implementation of the *partial classes* that complete the Program with the RPG database facilities mentioned above.

These files are *generated* files and should be re-generated using [Serengeti Tools](https://)

## Initialize the new Database File  
><sub>TO-DO: Describe step.</sub>

## Implement populate field's in-out (*of dataset*) methods  
><sub>TO-DO: Describe step.</sub>

## Set new file Job’s overrider class  
><sub>TO-DO: Describe step.</sub>

## Open and Close the new database file.  
><sub>TO-DO: Describe step.</sub>

<br>
<br>
<br>
[Continue ...]({{ site.rooturl }}/merging-two-screens/)
