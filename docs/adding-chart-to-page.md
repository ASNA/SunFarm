---
layout: page
title: Adding Chart To Page
permalink: /adding-chart-to-page/
---

You may have noticed that when reorganizing the data on the [Customer Maintenance]({{ site.rooturl }}/customer-maintenance-enhancement/) Page, we left a distinctive area on the upper-right hand corder of the page, looking something like:

![Space for Chart](/images/page-two-chart-01.png/)

We want to display two pieces of data:
1. The *Customer Status* (“Active”, “Closed”, “Over limit”, “Refer” and “Suspended”) at the top of that box, *and*
2. A *Sales Chart* that makes it easier to understand the Sales performance trend.

## Green-screen cryptic codes may now be displayed descriptively

The original green-screen page would show:

~~~
Status: A (F4)
~~~

`A` code is too *cryptic*, only experienced operators would remember that it means `Active`. Without changing the Business Logic, we can improve the presentation of that information by displaying full descriptions: 

>&#128161; We have plenty of space now!

While changing the presentation of the `status` we do not want to loose the *Prompting* ability for that field. To avoid affecting too much the Business Logic, let’s work on a presentation *trick*.

First, put back the `CUSTREC.SFSTATUS` field back on the Page. For convenience, show it at column position `1`.

Markup for Row “2”:

```html
<div Row="2">
    <DdsConstant Col="8" Text="Account number" />
    <DdsDecField class="left-aligned-field" Col="20" For="CUSTREC.SFCUSTNO" 
       VirtualRowCol="5,27" Color="DarkBlue" EditCode="Z" Comment="CUSTOMER NUMBER" />
    <DdsCharField Col="1" For="CUSTREC.SFSTATUS" VirtualRowCol=“15,27" PositionCursor="44"/>
</div>
```

Run the Website Application (*no need to recompile business logic this time*):

You can still get to the Status code (see image below).

![Customer Status wrong position](/images/page-two-chart-02_a.png)

And prompt to change its value (see image below).

![Customer Status Prompting](/images/page-two-chart-02_b.png)

Now let’s add a standard `HTML <span>` element to display the Status description on a proper place:

```html
<div Row="2">
    <DdsConstant Col="8" Text="Account number" />
    <DdsDecField class="left-aligned-field" Col="20" For="CUSTREC.SFCUSTNO" VirtualRowCol="5,27" Color="DarkBlue" EditCode="Z" Comment="CUSTOMER NUMBER" />
    <DdsCharField Col="1" For="CUSTREC.SFSTATUS" VirtualRowCol="15,27" PositionCursor="44" tabIndex=10 />
    <span class="box-highlight-center-field" ExpoGridCol="72/90">@Model.CUSTREC.SF_STATUS_NAME</span>
</div>
```

You may recognize that CSS Style box-highlight-center-field, has not been defined, and you are right, here it is (you can add it to site.css file):

```css
.box-highlight-center-field {
    background-color: black;
    color: white;
    text-align: center;
}
```

Nothing fancy, a white on black box with the text centered.

## What is `@Model.CUSTREC.SF_STATUS_NAME`?

We have been using `For=“”` notation to reference fields in attributes for Expo *tagHelpers*, but when we use standard HTML elements - *where we want to refer to the content of an HTML element* - we need to let Razor know our intent. 

Using `@` symbol will let use switch to C# code, were we can refer to *content* on symbols defined on the Model. `@Model.CUSTREC` will get us to the instance of the CUSTREC DdsRecord, but field SF_STATUS_NAME does not *yet* exists.

We can fix that, by defining it this way:

~~~
CustomerAppSite\Areas\CustomerAppViews\Pages\CUSTDSPF.cshtml.cs
~~~

```cs
[Char(1)]
public string SFSTATUS { get; set; }

public string SF_STATUS_NAME { get { return statusCodeToName(); } }

private string statusCodeToName()
{
    switch (SFSTATUS)
    {
        case "A":
            return "Active";
        case "C":
            return "Closed";
        case "O":
            return "Over Limit";
        case "R":
            return "Refer";
        case "S":
            return "Suspended";
    }

    return "(Undefined Status)";
}
```

Notice how we don’t qualify the new properties with [Char(nn)] because we are referring to C# *standard* string properties — additionally, we don’t want to add it to the Workstation *DataSet*— This is just **presentation** logic.

Run the Website Application (and make sure the **CSS** cache is refreshed), and you should see the following output (image below)[^1].

![Descriptive Status](/images/page-two-chart-02_c.png/)

We can trick the user by overlapping the original Status code field on top of the descriptive new presentation field, and make it invisible.

Let’s declare a new CSS Style:

~~~
CustomerAppSite\wwwroot\css\site.css
~~~

```css
.present-no-visible-field {
    opacity:0.1;
}
```
Change our `Row=“2”` such that both the original *Status code* and the new *Status Descriptions* are written to the page, but where the *Status code* is not **visible**:

~~~
CustomerAppSite\Areas\CustomerAppViews\Pages\CUSTDSPF.cshtml
~~~

```html
<div Row="2">
    <DdsConstant Col="8" Text="Account number" />
    <DdsDecField class="left-aligned-field" Col="20" For="CUSTREC.SFCUSTNO" 
      VirtualRowCol="5,27" Color="DarkBlue" EditCode="Z" Comment="CUSTOMER NUMBER" />
    <span class="box-highlight-center-field" ExpoGridCol="72/90">@Model.CUSTREC.SF_STATUS_NAME</span>
    <DdsCharField class="present-no-visible-field" Col="72" ColSpan="18" For="CUSTREC.SFSTATUS" VirtualRowCol="15,27" PositionCursor="44" />
</div>
```

>Note: The *rendering* order is important. 

When overlapping elements and one is *input capable*, the later must be on top of the former. The Browser renders elements in the order in which they are described in 'HTML', in this case we want '“CUSTREC.SFSTATUS"' rendered on top of '@Model.CUSTREC.SF_STATUS_NAME'

Run the Website Application again, and you can verify that the *Status Description* may still be *clickable* and prompting to change its value still works.[^2]

## Chart Placeholder

They Chart will be placed vertically at the same level as `Row=“3”` on a box with `width= 300` pixels and `height= 200` pixels. The box will have a light colored gray border.

We can start with standard `DIV HTML` element. We will position it like we have been positioning other elements before, under a particular `Row` with a horizontal displacement indicated by a `Column` attribute.

The starting Markup is:

```html
<div Row="3">
    <img id="customer-icon" ExpoCol="8" src="~/customer-icon.svg" />
    <div id="custrec-chart" ExpoCol="67">My Chart</div>
</div>
```
With the custrec-chart style defined in *CSS*:

```css
#custrec-chart {
    position: absolute;
    background-color: whitesmoke;
    width: 300px;
    height: 200px;
    border-style: solid;
    border-color: lightgrey;
    border-width: medium;
}
```

Such element shows like the image below[^3].

![Chart Placeholder element](/images/page-two-chart-03.png/)

We will be using [AMCHARTS Version 4](https://www.amcharts.com/) - commercial third party - JavaScript Library.

>Note: The *free-trial* usage of [AMCHARTS Version 4](https://www.amcharts.com/) shows a [watermark](https://en.wikipedia.org/wiki/Watermark) icon. If you like this library, and plan to use it in production, you can eliminate the [watermark](https://en.wikipedia.org/wiki/Watermark) icon buying a license.

We will not delve in all the details on how to use [AMCHARTS Library](https://www.amcharts.com/), this is outside the *Scope* for this **Guide**, but we will provide sufficient explanation to demonstrate how *third-party* JavaScript Libraries may be used on ASNA Nomad&reg; Migrated Applications.

The Chart we want is an `XY line chart` that will plot *one value* for *every* one of the **twelve months** in a given year connecting these points with straight `lines`. 

The effect is to *visually* see the Sales *Trend*. Are **Sales** *Growing*? or are they *Declining* - month by month - also visualizing the *Shape* throughout the year.

## Basic Chart Terminology:
1. Chart type: `XYChart`
2. A `Series` is a collection of data points.
3. `Category X` data is: **months**.
4. `Category Y` values is: **Sales**.
5. `Value Title` is: **“Sales”**
  
The [AMCHARTS](https://www.amcharts.com/) *engine* is fed by a JavaScript *entry-point*, which we will add at the end of our `CUSTDSPF.chtml` markup file.

The *entry-point* **Script** algorithm is:
1. Find the `HTML div` placeholder element by `ID`.
2. If the element exists, we can fairly assume that the `CUSTREC` record is *Active* on the Display, therefore we will create the `Chart`.
3. Establish data-fields `Category` items as **“month”**
4. Establish `Value axis` as **“sales”**
5. Establish `Value series` as collection of fields with monthly *Sales* (point) data.
6. Provide the `Chart Data` in [JSON](https://www.json.org/json-en.html) data format.

Append the following script(s) at the end of the Markup file:

~~~
CustomerAppSite/Areas/CustomerAppViews/Pages/CUSTDSPF.cshtml
~~~

```html
<script src="https://cdn.amcharts.com/lib/4/core.js"></script>
<script src="https://cdn.amcharts.com/lib/4/charts.js"></script>
<script>
    const CHART_ID = 'custrec-chart';
    const chartEl = document.getElementById(CHART_ID);

    if (chartEl !== null) {
        let salesJsonData = chartEl.innerHTML;
        let chart = am4core.create(CHART_ID, am4charts.XYChart);
        let categoryAxis = chart.xAxes.push(new am4charts.CategoryAxis());
        categoryAxis.dataFields.category = "month";

        let valueAxis = chart.yAxes.push(new am4charts.ValueAxis());
        valueAxis.title.text = "Sales";

        let series = chart.series.push(new am4charts.LineSeries());
        series.name = "Sales";
        series.stroke = am4core.color("#CDA2AB");
        series.strokeWidth = 3;
        series.dataFields.valueY = "sales";
        series.dataFields.categoryX = "month";

        const chartData = JSON.parse(salesJsonData);
        chart.data = chartData.data;
    }
</script>
```

The two-first `<script>` lines, refer to the [cdn address](https://en.wikipedia.org/wiki/Content_delivery_network) for the [AMCHARTS production JavaScript Library](https://www.amcharts.com/). 

The third `<script>` JavaScript *block* is the implementation of the *Custom* Algorithm described above.

If you run the **Website Application** now, you will get something like the following image[^4]:

![Empty AMCHARTS](/images/page-two-chart-04.png/)

>Note: the Chart is empty (we have not provided valid [JSON](https://www.json.org/json-en.html) data points). Also note that we got our **free** penalty, the AMCHARTS watermark icon.


The *plumbing* of the Chart is now in place, all we need to do is: set the *data-points* as the text inside the `DIV` html element. The *data-points* are expected to be placed as text, following the [JSON](https://www.json.org/json-en.html) syntax.

## Producing the Chart data in JSON format

Change the Markup for `Row=“3”`, such that the contents of our chart placeholder has a reference to a new property on our server-side `Model`:

```html
<div Row="3">
    <img id="customer-icon" ExpoCol="8" src="~/customer-icon.svg" />
    <div id="custrec-chart" ExpoCol="67">@Model.CUSTREC.SALES_CHART_DATA</div>
</div>
```

The `<script>` we provided to drive the [AMCHARTS JavaScript Library](https://www.amcharts.com/) has the following code:

```cs
        let salesJsonData = chartEl.innerHTML;
            .
            .
            .
        const chartData = JSON.parse(salesJsonData);
        chart.data = chartData.data;
```

You can read this as:

1. Extract the `HTML` string content from our chart `HTML div` element (our placeholder).
2. Using the `DOM JSON` object, parse the string text and produce a JavaScript object, keeping a reference in the constant `chartData`.
3. `chartData` has a property called **“data”** with the *data-points* expected by [AMCHARTS JavaScript Library](https://www.amcharts.com/) such that the rendering can execute.

## Implementing SALES_CHART_DATA getter property

Let’s add the following *read-only* property on our `CUSTREC` Model. Add the following *read-only* property to the Model, right after `PERCENT_CHANGE_RETURNS`:

```cs
[Char(20)]
public string PERCENT_CHANGE_RETURNS { get; private set; }
    .
    .
    .
public string SALES_CHART_DATA { get { return FormatChartData(); } }
```
>Note that the type is *C# string* type.

The **getter** method calls `FormatChartData` method. 

We can safely assume that **when** `SALES_CHART_DATA` getter is executed, the *Sales* properties (`CSSALES01` thru `CSSALES12`) have been *populated* with current values (for the Selected Customer). 

Add the following implementation for `FormatChartData` new method:

```cs
private string FormatChartData()
{
    SalesChartData chart = new SalesChartData();
    chart.data = new SalesSeriesPoint[12];

    chart.data[0] = new SalesSeriesPoint("Jan", CSSALES01);
    chart.data[1] = new SalesSeriesPoint("Feb", CSSALES02);
    chart.data[2] = new SalesSeriesPoint("Mar", CSSALES03);
    chart.data[3] = new SalesSeriesPoint("Apr", CSSALES04);
    chart.data[4] = new SalesSeriesPoint("May", CSSALES05);
    chart.data[5] = new SalesSeriesPoint("Jun", CSSALES06);
    chart.data[6] = new SalesSeriesPoint("Jul", CSSALES07);
    chart.data[7] = new SalesSeriesPoint("Aug", CSSALES08);
    chart.data[8] = new SalesSeriesPoint("Sep", CSSALES09);
    chart.data[9] = new SalesSeriesPoint("Oct", CSSALES10);
    chart.data[10] = new SalesSeriesPoint("Nov", CSSALES11);
    chart.data[11] = new SalesSeriesPoint("Dec", CSSALES12);

    JsonSerializerOptions serializerOptions = new JsonSerializerOptions();
    return JsonSerializer.Serialize<SalesChartData>(chart, serializerOptions);
}
```

## Before adding the rest of implementation, let’s stop here to explain.

`FormatChartData` starts by creating an *instance* of `SalesChartData` class (code will follow later).

The *object chart* just created, has an *uninitialized* property called **“data”**. 

This is a *collection* of `SalesSeriesPoint` *objects* (code will follow later). We allocate 12 members for this new collection.

*Next*, for *every* element in the collection, we create an *object* that represents the `Sales` series point, that is, the `Category` name and its *value*. The *value* comes from the `CSSALESnn` property collection (which comes from the Workstation, table CUSTREC in the *DataSet*).

Once the twelve month’s data have been loaded, we use **.Net** `System.Text.Json` class to *serialize* our C# objects into a JSON object in string *form*. This is what the JavaScript expects to find as the `HTML` *content* in our `Chart` placeholder.

Since we are using `Text.Json`, let’s add at the top of our C# source file the following [.NET assembly](https://docs.microsoft.com/en-us/dotnet/standard/assembly/) reference.

```cs
using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using ASNA.QSys.ExpoModel;
using System.Text.Json;       // New reference added
```

*Lastly*, here is the implementation for the small Utility classes:

```cs
class SalesSeriesPoint
{
    public SalesSeriesPoint(string month, decimal sales) 
      { this.month = month; this.sales = sales; }

    public string month { get; set; }
    public decimal sales { get; set; }
}

class SalesChartData
{
    public SalesSeriesPoint[] data { get; set; }
}
```

Run the Website Application with the latest changes, the following final Page should show (see image below)[^5]

![Chart with Data points](/images/enhanced-page-two.png)

With the Chart you can quickly see that the Customer 46000 increased sales in 1997, and had a very good month in the middle of the year (June).

>&#128161; What a difference!

*For the sake of completeness*, you may want to break-up the property `SALES_CHART_DATA`, such that you can add a breakpoint (before *getter* returns*) and visualize the data points formatted using `JSON syntax` (you can look at the string representation as well as how the Visual Studio built-in `JSON visualizer` presents the data).

![JSON Visualizer Debugger](/images/page-two-chart-JSON-visualizer.png/)

<br>
<br>
<br>
[Continue ...]({{ site.rooturl }}/replacing-yesno-with-checkboxes/)

[^1]: Commit: “Descriptive Customer Status field”
[^2]: Commit “Chart heading showing the Status Description Centered”
[^3]: Commit: “Chart Placeholder”
[^4]: Commit “Empty Chart”
[^5]: Commit “Chart showing Sales data”
