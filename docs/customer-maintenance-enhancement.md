---
layout: page
title: Customer Maintenance Enhancement
permalink: /customer-maintenance-enhancement/
---
Let’s apply the same techniques like we did for the [Customer Inquiry Page]({{ site.rooturl }}/remove-redundant-green-screen-info/).

*Same Markup file*:

~~~
CustomerAppSite\Areas\CustomerAppViews\Pages\CUSTDSPF.cshtml
~~~

*Different Record*:
```html
<DdsRecord For="CUSTREC"
```

1. Remove redundant **Program Name**, **User**, **Date** and **Time**
2. Improve KeyNames (Navigation Menu): ‘Submit’ ‘Exit' ‘Prompt’ ‘New Customer’ ‘Remove Customer’ and ‘Back’.
3. Reset StretchConstantText to false (record level)
4. Replace **Page Title** by standard HTML and CSS

With these changes, the Page should look like the following image[^1]:

![Customer Maintenance Prep](/images/page-two-00.png/)

&#128161; When fields and constants have different styles, the colon *(:)* constant separator adds unnecessary visual clutter

>The default **ASNA Expo** colors scheme uses a white box with no borders for the field and transparent background color for the text constants. That is enough visual separation for the two elements, making the use of the colon unnecessary. 

&#128161; When numbers are used for non-numeric data, left alignment looks best.

>The read-only field **CUSTREC.SFCUSTNO** (*customer number*) is rendered as a right-aligned numeric field, but in this case it represents a code, which looks better left-aligned.
Just like we did for right-aligned-constant before let’s define a CSS style for left-aligned-field

~~~
CustomerAppSite\wwwroot\css\site.css
~~~

```css
.left-aligned-field {
    text-align: left;
}
```

Change the markup to show the Customer number data on row 2 (used to be "5"), aligned accordingly:[^2]

```html
<div Row="2">
    <DdsConstant Col="8" Text="Account number" />
    <DdsDecField class="left-aligned-field" 
        Col="20" For="CUSTREC.SFCUSTNO" 
        VirtualRowCol="5,27" 
        Color="DarkBlue" 
        EditCode="Z" Comment="CUSTOMER NUMBER" />
</div>
```

The **Account Number** label and field value should look like the following image:

![Left Aligned Account Number](/images/page-two-01.png/)

&#128161; Input-capable fields with equal display width
> The input box on Web pages distinctively shows a box with a particular background. Making fields of equal display width produces pleasing results. Expanding	 (or contracting) the display width using the ColSpan attribute does not violate the field length definition. The data entry will either stop at the limit (even when there is more visual space), or scroll horizontally when the visual width is smaller than the length of the data field.

Let’s make **CUSTREC.SFNAME**, **CUSTREC.SFADDR1** and **CUSTREC.SFADDR2** column spans the same:  25 positions. On the next row, we can fit **City**, **State** and **Zip code** information. Also move the Rows for this data section to Rows: 4, 5, 6 and 7 [^3]

<sub>Note how we are leaving an area on the left to add a photo for the Customer contact.</sub>

The new Markup is:

```html
<div Row="4">
   <DdsConstant Col="20" Text="Name" />
   <DdsCharField Col="27" ColSpan="25" For="CUSTREC.SFNAME" Lower="true" … PositionCursor="40"/>
</div>

<div Row="5">
   <DdsConstant Col="20" Text="Address 1" />
   <DdsCharField Col="27" ColSpan="25" For="CUSTREC.SFADDR1" Lower="true" … PositionCursor="41"/>
</div>

<div Row="6">
   <DdsConstant Col="20" Text="Address 2" />
   <DdsCharField Col="27" ColSpan="25" For="CUSTREC.SFADDR2" Lower="true" … />
</div>

<div Row="7">
    <DdsConstant Col="20" Text="City" />
    <DdsCharField Col="27" ColSpan="10" For="CUSTREC.SFCITY" Lower="true" … PositionCursor="42"/>
    <DdsCharField Col="37" For="CUSTREC.SFSTATE" … PositionCursor="43" tabIndex=6 />
    <DdsCharField Col="42" For="CUSTREC.SFPOSTCODE" VirtualRowCol="11,37" tabIndex=7 />
</div>
```

Notice also that we have given **CUSTREC.SFCITY** field a ColSpan of 10 *- while the definition of the field is Char(30) -*. We confidently reduced it, because most US cities will fit in the width provided. It is safe to reduce field's width when all (or most) of the possible values are small. In the extreme case, where a larger should appear, the field scroll horizontally *by default*, allowing for the 30 positions max field length.

(Since we removed the **"F4"** constant, we should add a push button to indicate that State is *"promptable"* -adding such element is described in a [later Chapter of this Guide](/replacing-prompt-with-icon/)).

Move the rest of fields up and around the Customer Photo area

What used to be Rows **13, 14, 15, 16, 17** and **18** is now Rows *8*, *9* and *10* [^4]

![Fields around Photo Area](/images/page-two-03.png/)


**Note:** The use of **Y/N** fields as User-Interface Artifacts is **discouraged** in Modern Applications. It may be confusing to users never exposed to *Legacy* Applications. There are better paradigms we can apply, such as *Checkboxes*. We will replace **Y/N** fields [later in this Guide](/replacing-yesno-with-checkboxes/).



<br>
<br>
<br>
[Continue ...]({{ site.rooturl }}/adding-image-to-page/)



[^1]: Commit "Customer Maintenance Preparation"
[^2]: Commit "Left aligned decimal field"
[^3]: Commit "Input capable field block"
[^4]: Commit "Rest of fields up and around Customer photo area”
