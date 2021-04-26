---
layout: page
title: Replacing Prompt (F4) with clickable icon
permalink: /replacing-prompt-with-icon/
---
| Quick How-to: 
|:-------------
| [Adding icon to simulate F4 prompt](https://github.com/ASNA/SunFarm/search?q=Adding+icon+to+simulate+F4+prompt&type=commits)

<br>
Prompting for possible values of fields (also known as *drilling-down* on a user interface) is very common.

There are several ways to enhance a Display Page with this functionality, such as using *drop-down* lists. 

In this Guide we will assume we want to keep the **Window** record feature of the Legacy `DDS` and *pop-up* records on the screen to present valid field values.

The Legacy Application used to present a *constant label* — to the right of the given field — with the content `(F4)` to *hint* to the user that pressing **Function 4** on the keyboard at specific fields would present the list of *valid values*.

In particular, the SunFarm Application menu option `Update` on the main Display Page would show the **Customer Maintenance** screen — which we have been enhancing in this Guide —. 

The **Customer Maintenance** Display Page contains two *Prompts*:

1. Prompting for `States` (to update *Address* Information) *and*
2. Prompting for `Status` (to update Customer *Status*).

The **Window** Records presented when `F4` key is pressed at the proper fields are shown by the following images:

Prompting for **States**

![State Prompt](/images/page-two-state-prompt.png/) 

Prompting for **Customer Status Codes**

![Status Prompt](/images/page-two-status-prompt.png/)
  
<br>
<br>  
## Adding a Button with a magnifying glass icon to the right of the State field.

The following is the *TagHelper* added to the `CUSTREC` record in file:

~~~
CustomerAppSite\Areas\CustomerAppViews\Pages\CUSTDSPF.cshtml
~~~

```html
<div Row="7">
    <DdsConstant Col="20" Text="City" />
    <DdsCharField Col="27" ColSpan="10" For="CUSTREC.SFCITY" Lower="true" VirtualRowCol="10,27" PositionCursor="42" tabIndex=5 />
    <DdsCharField Col="37" For="CUSTREC.SFSTATE" VirtualRowCol="11,27" PositionCursor="43" tabIndex=6 />
    <DdsButton Col="40" IconId="search" ButtonStyle="Icon" AidKey="F4" FocusField="CUSTREC.SFSTATE"/>
    <DdsCharField Col="42" For="CUSTREC.SFPOSTCODE" VirtualRowCol="11,37" tabIndex=7 />
</div>
```

Focus on the new *TagHelper* added, `DdsButton`[^1].

That simple markup line would do the trick nicely, **WITHOUT** having to change *a single line* in the `Model`’s code or `Application Logic`.

Let’s see how it looks (all the details will be explained later at the end of this document) 

![State Prompting Icon](/images/page-two-state-prompt-icon.png/)

When the mouse pointer is moved *close* to the **magnifying glass** icon — also known as *hovering over* it — the shape of the pointer changes to a **Hand** with finger pointing to it. This is a *hint* to the user that the icon may be **clicked**.

>When the icon is clicked:

1. The keyboard focus is changed to the field `CUSTREC.SFSTATE`
2. The `AidKey` with the value `F4` is *programmatically* set in the *keyboard buffer* right before the Display Page is *submitted*: just as if the User had navigated to the field in question and the key `F4` in the keyboard had been pressed.

## DdsButton TagHelper

Let’s look *deeper* into how the Markup that produced the clickable icon for prompting is defined.

```html
<DdsButton Col="40" ButtonStyle="Icon" IconId="search" AidKey="F4" FocusField="CUSTREC.SFSTATE"/>
```

The attribute `Col` should already be familiar. It specifies the horizontal position within the row (*seven* in this case) where the button should be rendered.

`ButtonStyle` is an attribute that defines the different styles supported for clickable buttons:

1. `Button` - A push button with a text label centered (Default)
2. `Image` - An image that is identified by a user file as a web resource.
3. `Link` - A hyperlink (text label that executes an action)  
4. `Icon` - A named stock image (more on this below)

If the `ButtonStyle` attribute is set to `Icon` (which is what we use in this Guide), the attribute `IconId` must be provided to the name of an existing *Icon Shape*.

*Icon Shapes* are rendered with square dimensions (*width* equals to *height*) and are scaled to fit in a **cell** of the height of the font and the `fore-color` of the `CSS` text, (think of a character with a *graphic shape*).

`AidKey` is an attribute that takes the name of an IBM i *Aid* key (`F1` ...  `F24`, `Clear`, `Help`, `PageUp`, `PageDown`, `Print`, `Home`, `Enter`, `Attn` and `Reset` ).

`FocusField` is an attribute that is set to the *field-name* of the record written to the page where we want to simulate user navigation *right before* the Display Page is *auto-submitted*.

## Icon Payload efficiency

*ASNA Monarch Base* rendering engine is optimized to include in the Page payload only **ONE** copy of a particular *Icon Shape*. If you plan to add `DdsButton` TahHelpers in records of a subfile, you can be assured that the Page’s *payload* will **NOTE** grow. When more than one instance of a particular shape is rendered on a Page, there is a single copy in memory of the shape and multiple *references* to it.

## `IconId` available Names

*ASNA Monarch* provides a collection of *Icon Shapes* in the form of [SVG Images](https://en.wikipedia.org/wiki/Scalable_Vector_Graphics) with commonly used shapes that are inspired by the [Font Awesome free library](https://fontawesome.com/plans).

The *Icon Shapes* are simple and clean, monochrome (color can be selected), scaled without loss and respect the background (use *transparent* background).

*ASNA Monarch&reg;* Icon library contains 250 named shapes, which should be sufficient for most of the DdsButtons you application may need.

[Browse the Icon library]({{ site.rooturl }}/asna-icon-library-reference/)

## Button Icon for Status prompt

Adding a clickable icon for the second prompt can be accomplished the same way. We want to show the icon at the right of the Chart’s title where we are displaying the Customer Status (Active for the customer we have been using as a sample).

The last field was positioned at column 72 with a span of 18, that is: the last position is 72+18 = 90. Let’s add the icon at column position 91. The field we want to emulate pressing is `SFSTATUS` on the record `CUSTREC`.

The following is the markup we need:

```html
<div Row="2">
    <DdsConstant Col="8" Text="Account number" />
    <DdsDecField class="left-aligned-field" Col="20" For="CUSTREC.SFCUSTNO" 
       VirtualRowCol="5,27" Color="DarkBlue" EditCode="Z" Comment="CUSTOMER NUMBER" />
    <span class="box-highlight-center-field" 
       ExpoGridCol="72/90">@Model.CUSTREC.SF_STATUS_NAME</span>
    <DdsCharField class="present-no-visible-field" Col="72" ColSpan="18" 
       For="CUSTREC.SFSTATUS" VirtualRowCol="15,27" PositionCursor="44" />
    <DdsButton Col="91" IconId="search" ButtonStyle="Icon" AidKey="F4" FocusField="CUSTREC.SFSTATUS"/>
</div>
```

## Button Title as a Visual Hint for the User

Not all icon shapes are universal and when the same shape appears in more than one place on the Page, it may not be immediately apparent as to its purpose.

We can add a small text hint that shows on Desktop Browsers when the mouse pointer hovers on top of the Icon.

Let’s add the following two text hints to our prompts:
1. "Prompt for State Codes"
2. "Prompt for Status Codes"


Simple addition of these attributes to our TagHelpers:

`IconTitle="Prompt for State Codes"`

`IconTitle="Prompt for Status Codes"`

The prompts with their corresponding titles should now look like the following image:

![State Prompt Title](/images/page-two-state-prompt-icon-title.png/)

<br>
<br>
<br>
## *Last Page of this Guide*
<br>
<br>

[^1]: Commit "Adding icon to simulate F4 prompt"

