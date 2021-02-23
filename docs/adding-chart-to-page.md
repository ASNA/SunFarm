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


<br>
<br>
<br>
[Continue ...]({{ site.rooturl }}/adding-chart-to-page/)

