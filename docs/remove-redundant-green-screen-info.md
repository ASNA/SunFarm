---
layout: page
title: Remove Redundant Green-screen Info
permalink: /remove-redundant-green-screen-info/
---
| Quick How-to: 
|:-------------
| [Subfile selection options as pull-down options](https://github.com/ASNA/SunFarm/search?q=Subfile+selection+options+as+pull-down+options&type=commits)
| [Replacing Page Title](https://github.com/ASNA/SunFarm/search?q=Replacing+Page+Title&type=commits)
| [Fourteen records subfile](https://github.com/ASNA/SunFarm/search?q=Fourteen+records+subfile&type=commits)

<br>

The *Canvas* used to render Pages on Modern Browsers is totally different than the fixed-position grid layout used by a Page on the IBM i.

Not only is the real state larger and varying in its dimensions but the color schema is almost incomparable.

Typical color effects such as reverse-video colors are often quite distracting. In addition, we *may* identify typical information as irrelevant, such as:

* Display Current Date and Time
* Display Username
* Display name of Program associated with Page.

These items of information are readily available as part of the *Operating System*. Removing these types of redundant items from Legacy Pages, improves significantly their *Modern* look

# First Step: Remove Reverse Video effect

DDS on the IBM i usually renders text on a terminal with dark background. Text screen attributes such as REVERSE-IMAGE produced a subtle lighter green background which some considered pleasant effect to highlight text, such as records on a subfile.

[Reverse-video](https://www.ibm.com/support/knowledgecenter/en/ssw_ibm_i_73/rzakc/rzakcmstdfdspat.htm) effect on a typically white/pale background Browser Page would produce a *very heavy* block of text when rendered. 

As you can see on the out-of-the-box Migration of the First Page, the subfile records are rendered with a color that is displeasing to the eye.

The reverse-video (*or Reverse Image*) display attribute DDS keyword:

`DSPATR(RI)` 

 
gets translated to:

```html
InvertFontColors="*True"
```

`TagHelper` attribute.

Removing such attribute produces a much more pleasant output.

Removing `InvertFontColors="*True"` from the subfile `SFL1` in file:

~~~
CustomerAppSite\Areas\CustomerAppViews\Pages\CUSTDSPF.cshtm
~~~

*(Lines `51`, `52` and `53`)*

```html
<div Row="8" RowSpan="@SFLC_SubfilePage">
    @for (int rrn=0,; rrn < Model.SFLC.SFL1.Count; rrn++)
    {
        int  row = 8 + rrn;
        <DdsSubfileRecord RecordNumber="rrn" For="SFLC.SFL1">
            <div IsGridRow>
                <DdsCharField Col="2" For="SFLC.SFL1[rrn].SFCOLOR" VisibleCondition="*False" VirtualRowCol="@row,2" tabIndex=1 />
                <DdsDecField Col="4" For="SFLC.SFL1[rrn].SFSEL" VirtualRowCol="@row,4" EditCode="Z" ValuesText="'0','2','3','5','7','9','10','11'" tabIndex=2 />
                <DdsDecField Col="7+1" For="SFLC.SFL1[rrn].SFCUSTNO" VirtualRowCol="@row,7" Color="Green : !61 , DarkBlue : 61" InvertFontColors="*True" EditCode="Z" Comment="CUSTOMER NUMBER" />
                <DdsCharField Col="14+1" For="SFLC.SFL1[rrn].SFNAME1" VirtualRowCol="@row,14" Color="Green : !61 , DarkBlue : 61" InvertFontColors="*True" />
                <DdsCharField Col="55+1" For="SFLC.SFL1[rrn].SFCSZ" VirtualRowCol="@row,55" Color="Green : !61 , DarkBlue : 61" InvertFontColors="*True" Comment="CITY-STATE-ZIP" />
            </div>
        </DdsSubfileRecord>
    }
</div>
```

Simplifies the markup and produces a nicer effect:

```html
<div Row="8" RowSpan="@SFLC_SubfilePage">
    @for (int rrn=0; rrn < Model.SFLC.SFL1.Count; rrn++)
    {
        int row = 8 + rrn;
        <DdsSubfileRecord RecordNumber="rrn" For="SFLC.SFL1">
            <div IsGridRow>
                <DdsCharField Col="2" For="SFLC.SFL1[rrn].SFCOLOR" VisibleCondition="*False" VirtualRowCol="@row,2" tabIndex=1 />
                <DdsDecField Col="4" For="SFLC.SFL1[rrn].SFSEL" VirtualRowCol="@row,4" EditCode="Z" ValuesText="'0','2','3','5','7','9','10','11'" tabIndex=2 />
                <DdsDecField Col="7+1" For="SFLC.SFL1[rrn].SFCUSTNO" VirtualRowCol="@row,7" Color="Green : !61 , DarkBlue : 61"  EditCode="Z" Comment="CUSTOMER NUMBER" />
                <DdsCharField Col="14+1" For="SFLC.SFL1[rrn].SFNAME1" VirtualRowCol="@row,14" Color="Green : !61 , DarkBlue : 61" />
                <DdsCharField Col="55+1" For="SFLC.SFL1[rrn].SFCSZ" VirtualRowCol="@row,55" Color="Green : !61 , DarkBlue : 61" Comment="CITY-STATE-ZIP" />
            </div>
        </DdsSubfileRecord>
    }
</div>
```

## Removing Reverse Video effect:
<br>

![No Reverse-Video](/images/no-reverse-image.png)

<br>

## Compare to original:

<br>

![Original Page One](/images/out-of-box-page-one.png)

# Second Step: Remove Irrelevant Green-Screen Info

1. Remove User, Date and Time fields.
2. Remove F3=Exit constant.
3. Move the constant "2=Update  3=Display sales …" to the `SFLC.SFL1[rrn].SFSEL` field as Value-Text (or pull down selection option labels)

Removing is always easy.
Open file:

~~~
CustomerAppSite\Areas\CustomerAppViews\Pages\CUSTDSPF.cshtml
~~~

Identify the Division for *Row=1* on record "SFLC":

```html
<div Row="1">
    <DdsConstant Col="2" Text="*USER" />
    <DdsConstant Col="31+1" Text="M5 Customer Inquiry" Color="DarkBlue" />
    <DdsConstant Col="64+1" Text="*DATE" />
    <DdsConstant Col="73+1" Text="*TIME" />
</div>
```

Remove the constants with Text "*USER", "*DATE" and "*TIME".

Identify the Division for `Row=4` and `Row=5` on record "SFLC":

```html
<div Row="4">
    <DdsConstant Col="3" Text="2=Update   3=Display sales    5=Delivery Addresses    7=Create sales record     9=Print" Color="Blue" />
</div>
<div Row="5">
    <DdsConstant Col="3" Text="sales (Online)  10=Print sales (Batch)    11=Orders" Color="Blue" />
</div>
```

Remove both Rows.

Identify the Division for `Row=23` on record "KEYS":

```html
<DdsRecord For="KEYS" KeyNames="ENTER 'Enter'; ">
    <div Row="23">
        <DdsConstant Col="3" Text="F3=Exit  F9=Spool Files" Color="Blue" />
    </div>
</DdsRecord>
```

Remove `Row=23`

So far the *markup* for the identified rows is reduced to:

```html
<div Row="1">
    <DdsConstant Col="31+1" Text="M5 Customer Inquiry" Color="DarkBlue" />
</div>

                    Note: div's for Rows 4 and 5 are gone


<DdsRecord For="KEYS" KeyNames="ENTER 'Enter'; ">
</DdsRecord>

                    Note: KEYS record has no visible rows.
```

Completing (3) regarding the constant `"2=Update 3=Display sales` ... etc deserves further explanation.

### We have two pieces of information:

<ol type="a">
    <li>The option code (char value), that is: &lt;nothing&gt;, 2, 5, 7, 9, 10 and 11.</li>
    <li>The corresponding labels, such as: "Update", "Display sales", "Delivery Addresses" etc..</li>
</ol>


Each ASP.NET Razor Page is defined by two files: the Markup file (extension `.cshtml`) and the corresponding Model file (*extension* `.cshtml.cs`).

><sub>Note: For convenience, Visual Studio Solution Explorer, shows the Model file under the Markup file in the Website file structure.</sub>


Visual Studio intellisense,  allows to jump back and forth, between symbols defined in the Markup and the Model. For example, positioning the cursor in the markup on top of DdsDecField `For="SFLC.SFL1[rrn].SFSEL"` and pressing `F12` (*Go To Definition*), will take you to the Model’s definition for the `SFLC` (Subfile record Controller)’s field `SFLSEL`.

### Model's Code

~~~
File: CustomerAppSite\Areas\CustomerAppViews\Pages\CUSTDSPF.cshtml.cs
~~~

```cs
public class SFL1_Model : SubfileRecordModel
{
    [Char(1, Protect = "*True")]
    public string SFCOLOR { get; set; }

    [Values(typeof(Decimal),"00","02","03","05","07","09","10","11")]
    [Dec(2, 0)]
    public decimal SFSEL { get; set; }

    [Dec(6, 0)]
    public decimal SFCUSTNO { get; private set; } // CUSTOMER NUMBER

    [Char(40)]
    public string SFNAME1 { get; private set; }

    [Char(25)]
    public string SFCSZ { get; private set; } // CITY-STATE-ZIP
}
```

Focus your attention on the **SFSEL** definition:

```cs
[Values(typeof(Decimal),"00","02","03","05","07","09","10","11")]
[Dec(2, 0)]
public decimal SFSEL { get; set; }
```

Note that, in addition to the C# decimal definition for the type, the field `SFLSEL` is decorated with **Dec** and **Values** attributes.

**Dec** attribute further defines the decimal as one with *fixed precision* and *decimal positions*.
The **Values** attribute defines the *valid* values for the field. The positions on the list of valid values is **important**, since it will be matched with the ValuesText *TagHelper* attribute in the markup.

Back on the Markup


File: `…\Areas\CustomerAppViews\Pages\CUSTDSPF.cshtml`

```html
<DdsDecField Col="4" For="SFLC.SFL1[rrn].SFSEL" VirtualRowCol="@row,4" EditCode="Z" ValuesText="'0','2','3','5','7','9','10','11'" tabIndex=2 />
```

Let's change the *ValuesText* to:

```html
ValuesText="' ','Update','Display sales','Delivery Addresses','Create sales record','Printsales (Online)','Print sales (Batch)','Orders'" 
```

Build, run and look at the result [^1]:

![Pulldown options text](/images/pulldown-options-step1.png)

<br>

Now we need to push the rest of the fields to the *right*, to make it look nicer.

<br>

There are several ways to accomplish this. What we will use, is the *trial-and-error* technique.

To avoid having to recalculate the column positions for the fields in the subfile, we can add a constant numeric value to each of the fields to the right: `SFLC.SFL1[rrn].SFCUSTNO`, `SFLC.SFL1[rrn].SFNAME1` and `SFLC.SFL1[rrn].SFCSZ`. 

Let’s add *`10+`* … no, *`15+`* … no *`12+`*

The markup would look like this (for clarity I eliminated part of each line, replaced by …):

```html
<DdsSubfileRecord RecordNumber="rrn" For="SFLC.SFL1">
    <div IsGridRow>
        <DdsCharField Col="2" For="SFLC.SFL1[rrn].SFCOLOR" ... 
        <DdsDecField Col="4" For="SFLC.SFL1[rrn].SFSEL" ...
        <DdsDecField Col="12+7+1" For="SFLC.SFL1[rrn].SFCUSTNO" ... Comment="CUSTOMER NUMBER" />
        <DdsCharField Col="12+14+1" For="SFLC.SFL1[rrn].SFNAME1" ...  />
        <DdsCharField Col="12+55+1" For="SFLC.SFL1[rrn].SFCSZ" ... Comment="CITY-STATE-ZIP" />
    </div>
</DdsSubfileRecord>
```

><sub>As you change the markup, you may refresh the running page in the Browser, until it looks how you want it.</sub>


><sub>Note also, that the Col attribute tag-helper, already came with an expression. Cocoon Displayfile Migration Agent took advantage of the expression to indicate the original column value (from DDS and adjustments it had to do to prevent overlaps — due to HTML borders and padding—)
</sub>

Look at the result, and it should look now, like this:

![Pulldown options text](/images/pulldown-options-step2.png)


<br>

Before we continue, let’s get rid of the vertical gap, between Position to name: (*Row=2*) and the headings of the Subfile (*Row=7*).

<br>
<br>

We accomplish this by changing `Row="7"` to `Row="3"` and `Row="8"` to `Row="4"`

Next, we will align the column headings on the Subfile...

But first, read the Topic: [Avoiding constants stretching]({{ site.rooturl }}/avoid-constant-stretching/)

# Third Step: Align Constants to their proper place

For alignment, we have three challenges:

1. Show the Page Title with modern style.
2. Subfile Column headings should align to their corresponding fields.
3. Improve usability for Search input field artifact.


### Show the Page Title with Modern style
The Legacy Application used Centering as a way to make the constant stand out as a Page Title. We could do the same, but we have more resources when working on Browser pages, such as *Larger* font with text effects such as **bold**.

Let's forget that the Title is a DdsConstant with a specific column position, and think of it as a Text block within an HTML division of a particular style.

Add the following `CSS` style to file:

~~~
CustomerAppSite\wwwroot\css\site.css
~~~

```css
#page-title {
    font-size: large;
    padding-left: 4.0em;
    padding-top: 1em;
    font-weight: bold;
}
```

The definition of the style is self-explanatory: large font size, with a certain padding to the left and top, using the **bold** text effect. We use that style only once on the page, in the item with `id="page-title"`.

In the markup, we remove the old DdsConstant specification with the same purpose (formerly on row one):

Remove:

```html
<div Row="1">
  <DdsConstant Col="31+1" Text="M5 Customer Inquiry" Color="DarkBlue" />
</div>

```

Add in its place [^2]:

```html
<div id="page-title">Customer Inquiry</div>
```

Once you have CSS styles defined, you can take advantage of Modern Browsers *Developer Tools* to experiment with different styles, even with **text-align: center** if you width to preserve the original developer design.

<sub>Note: If you wish to experiment with Browser's **Developer Tools** you can find excellent learning resources online.</sub>

### A Taste of Browser's Developer Tools:
<br>
![Browser's Developer Tools](/images/developer-tools-element-style.png)

<br>
<br>

## Subfile Column headings should align to their corresponding fields.

Field’s starting positions are very accurately identified on the page based on the original DDS row and col positions. But identifying ending column positions is harder.

The default Font for *Monarch Base* Display Pages is of the type *variable-pitch*, meaning that the physical width of characters varies according to the Font’s designer’s stroke used. When using **variable-pitch** fonts, letter occupy different width, notably **thinner** letters, such as `i` use a lot less character width than wider letters, such as upper-case `M`.

Green-screen page designers used a Font that is of the type *fixed-pitch*, meaning that the width of ALL characters is **the same**.

A green-screen label starting at column 5 with the constant **"THIS CONSTANT"** (thirteen characters) is guaranteed to end at column position 17. That is, the end-column position can be precisely computed by the formula:

~~~
(starting-position + (field-length-1))
~~~

This **no longer** works on Browser fonts (even with those so-called *"Monospace"*).

*Monarch&reg; Display Page Agent* will use the length of the field of constant to approximate the *Grid Column Span* (ending position), by the use of a *fudge-factor* to account for the variability of *Web fonts*.

We can apply a technique to correct this approximation.

Consider the column for SFNAME1 Subfile on the **Customer Inquiry** Page.

Use the *Developer Tools* to inspect the column width allocated to the Customer Name on the subfile.

![Grid Column Span](/images/grid-col-span.png)

Notice how the Legacy Application was allocating *forty* characters for Customer Names (its field length), but when using *variable-pitch* fonts this *span* results wasteful, showing an un-used -*yet valuable*- area on the page.

We can override the *Column Span* calculation by adding the *TagHelper* attribute **ColSpan** as follows:

```html
<DdsCharField Col="12+14+1" ColSpan="30" For="SFLC.SFL1[rrn].SFNAME1" VirtualRowCol="@row,14" Color="Green : !61 , DarkBlue : 61" />
```
	 
This way we can reduce the Grid column position on the last subfile field. Using the method that we have applied before, position the constants that make the subfile column headings as well as the position for the "Position to name:" constant, according to the image below. (You can also compare against the changes in the public GitHub Repository [^3])

![Narrower Customer Name Column](/images/narrower-name-column.png)

## Position to name input field

You may have noticed in the image above, that we grew the width of the input box (DdsCharField TagHelper) for field, beyond the field's length:

```html
<DdsCharField Col="63+1" For="SFLC.SETNAME" ColSpan="20" VirtualRowCol="2,70" />
```
We chose to give it a rather wide *twenty* **ColSpan** value. Compare it to the length definition (in the Model):

```cs
[Char(10, OutputData=false)]
public string SETNAME { get; set; }
```
We made the text box *twice* as wide (compared to the length). The reason we changed it that way, was because we claim it is *aesthetically* more pleasing. For no other reason.

>The width of input fields in  HTML is independent of the *text* length allowed.

This is **very** important and valuable feature to improve the *Look* of Pages. If the width of an input is too small (compared to the *maximum-length* allowed) the text may scroll horizontally within the box. If the width of the text box if wider than the *maximum-length* allowed, typing text will stop at the limit, but we can leave a visual gap to the right.

Our visual improvements should be very noticeable so far. There is, however plenty of room at the bottom of the Page to show more records in the subfile.

The Legacy DDS had the Subfile Page set at 14 records. Modern computer devices that run Browsers today, are capable of high definition displays, with Fonts that show clearly at rather small sizes, which leaves plenty of room on the page for many more subfile records than the equivalent green-screen application.

<br>
<br>

[Continue ...]({{ site.rooturl }}/enhancements-affecting-logic/)

<br>
<br>

[^1]: [Commit: "Subfile selection options as pull-down options"](https://github.com/ASNA/SunFarm/search?q=Subfile+selection+options+as+pull-down+options&type=commits)
[^2]: [Commit: "Replacing Page Title"](https://github.com/ASNA/SunFarm/search?q=Replacing+Page+Title&type=commits)
[^3]: [Commit: "Fourteen records subfile"](https://github.com/ASNA/SunFarm/search?q=Fourteen+records+subfile&type=commits)