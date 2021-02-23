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

>>Note: The *rendering* order is important. 

When overlapping elements and one is *input capable*, the later must be on top of the former. The Browser renders elements in the order in which they are described in 'HTML', in this case we want '“CUSTREC.SFSTATUS"' rendered on top of '@Model.CUSTREC.SF_STATUS_NAME'

Run the Website Application again, and you can verify that the *Status Description* may still be *clickable* and prompting to change its value still works.[^2]

<br>
<br>
<br>
[Continue ...]({{ site.rooturl }}/adding-chart-to-page/)

[^1]: Commit: “Descriptive Customer Status field”
[^2]: Commit “Chart heading showing the Status Description Centered”

