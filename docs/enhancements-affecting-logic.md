---
layout: page
title: Enhancements affecting Business Logic
permalink: /enhancements-affecting-logic/
---

[In the last section]({{ site.rooturl }}/enhancements-affecting-logic/), we left the *Customer Inquiry* Page, looking like the following image:

![Fourteen records per Page](/images/narrower-name-column.png)

There comes a time when to continue enhancing a page, it is necessary to change the Business Logic.

Since we have been able to compress the screen while aligning elements and eliminating redundant items, we are left with unused real-estate.

The Business Logic is writing fourteen records at a time. But now we can fit much more, at least *twenty* (a nice whole number).

Let’s assume we want to increase the Page size, from fourteen to twenty records per-page.

## Two changes are needed:
1. Write twenty records to the subfile in the Business Rules.
2. Expand the record count to show, in the Razor Page’s Subfile Controller.

We will use blue color for the code that needs to be added. Red strikeout for code that needs to be removed.

To change the Business Logic, we need to change the following class (program):

~~~
...\CustomerAppLogic\CUSTINQ.cs
~~~

```cs
public partial class Custinq : ASNA.QSys.HostServices.Program
{
    const int SFLC_SubfilePage = 20;

    protected Indicator _INLR;
.
.
.
    void LoadSfl() // Line 519
    {
        _IN[61] = '0'; //Start with green.
        _IN[90] = '1'; //Clear the subfile.
        CUSTDSPF.Write("SFLC", _IN.Array);
        _IN[76] = '0'; //Display records.
        _IN[90] = '0';
        sflrrn = 0;
        _IN[77] = CUSTOMERL2.ReadNext(true) ? '0' : '1';
        //----------------------------------------------------------
        while (!(bool)_IN[77] && (sflrrn < 14 SFLC_SubfilePage))
        {
.
.
.
//*********************************************************************
//  Read Backwards for a PageDown
//*********************************************************************
        void ReadBack() // Line 558
        {
            _IN[76] = '0';
            _IN[77] = '0';
            X = 0;
            CUSTDSPF.ChainByRRN("SFL1", 1, _IN.Array); //Get the top name and
            CMNAME = SFNAME1; // number.
            CMCUSTNO = (decimal)SFCUSTNO;
            CUSTOMERL2.Chain(true, CMNAME, CMCUSTNO);
            _IN[76] = CUSTOMERL2.ReadPrevious(true) ? '0' : '1';
            while (!(bool)_IN[76] && (X < 14 SFLC_SubfilePage))
            {
                /* EOF or full s/f. */
                X += 1;
                _IN[76] = CUSTOMERL2.ReadPrevious(true) ? '0' : '1';
            }
            if ((bool)_IN[76])
                //Any records found?
                CUSTOMERL2.Seek(SeekMode.SetLL, new string(char.MinValue, 40));
        }
```

## In the Business Logic
There are two places in **CUSTINQ** where the hard-coded value *14* (representing the records to write to the subfile) is used.

We define a constant symbol, and use it in two places so we can adjust that number to twenty *(and make it easy to adjust it in a single place to any other value we may want)*.

## In the Website Layout
To expand the number of records that need to be displayed in the Subfile controller, we need to affect two files:

1. **Model File**: ...\CustomerAppSite\Areas\CustomerAppViews\Pages\CUSTDSPF.cshtml.cs
2. **Markup File**: ...\CustomerAppSite\Areas\CustomerAppViews\Pages\CUSTDSPF.cshtml



