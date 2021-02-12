---
layout: page
title: Remove Redundant Green-screen Info
permalink: /remove-redundant-green-screen-info/
---

The *Canvas* used to render Pages on Modern Browsers is totally different than the fixed-position grid layout used by a Page on the IBMi

Not only does the real state is larger and varying in its dimensions but the color schema is almost incomparable.

Typical color effects in IBMi such as *reverse-video* look **horrendous** on a Modern Browser, in additional, we can identify typical information as obsolete, such as:

* Display Current Date and Time
* Display Username
* Display name of Program associated with Page.

These items of information are readily available as part of the *Operating System*. Removing these types of redundant items from Legacy Pages, improves significantly their *Modern* look

# First Step: Remove Reverse Image effect

DDS on the IBMi usually renders text on a terminal with dark background. Text screen attributes such as REVERSE-IMAGE produced a subtle lighter green background which some considered pleasant effect to highlight text, such as records on a subfile.

REVERSE-IMAGE effect on a typically white/pale background Browser Page would produce a *very heavy* block of text when rendered. 

As you can see on the out-of-the-box Migration of the First Page, the subfile records are rendered with a color that is displeasing to the eye.

The reverse-image display attribute DDS keyword:

~~~
DSPATR(RI) 
~~~
 
gets translated to:

```html
InvertFontColors=“*True"
```
TagHelper attribute.

Removing such attribute produces a much more pleasant output.

Removing InvertFontColors=“*True" from the subfile **SFL1** in file:

~~~
CustomerAppSite\Areas\CustomerAppViews\Pages\CUSTDSPF.cshtm
~~~

*(Lines 51, 52 and 53)*

~~~
<div Row="8" RowSpan="@SFLC_SubfilePage * @SFLC_SubfileRowsPerRecord">
    @for (int rrn=0, row = 8; rrn < Model.SFLC.SFL1.Count; rrn++, row += @SFLC_SubfileRowsPerRecord)
    {
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
~~~

Simplifies the markup and produces a nicer effect:

~~~
<div Row="8" RowSpan="@SFLC_SubfilePage * @SFLC_SubfileRowsPerRecord">
    @for (int rrn=0, row = 8; rrn < Model.SFLC.SFL1.Count; rrn++, row += @SFLC_SubfileRowsPerRecord)
    {
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
~~~

![No Reverse-Image](/images/no-reverse-image.png)

<br>

# Compare to original:

![Original Page One](/images/enhanced-page-one.png)

<br>
<br>
<br>
[Continue ...]({{ site.rooturl }}/more-natural-font/)