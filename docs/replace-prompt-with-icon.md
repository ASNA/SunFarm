---
layout: page
title: Replacing Prompt (F4) with clickable icon
permalink: /replacing-prompt-with-icon/
---

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

Focus on the new tagHelper added, `DdsButton`[^1].

That simple markup line would do the trick nicely, **WITHOUT** having to change *a single line* in the `Model`’s code or `Application Logic`.

Let’s see how it looks (all the details will be explained later at the end of this document) 

![State Prompting Icon](/images/page-two-state-prompt-icon.png/)

When the mouse pointer is moved *close* to the **magnifying glass** icon — also known as *hovering over* it — the shape of the pointer changes to a **Hand** with finger pointing to it. This is a *hint* to the user that the icon may be **clicked**.

>When the icon is clicked:

1. The keyboard focus is changed to the field `CUSTREC.SFSTATE`
2. The `AidKey` with the value `F4` is *programmatically* set in the *keyboard buffer* right before the Display Page is *submitted*: just as if the User had navigated to the field in question and the key `F4` in the keyboard had been pressed.

## DdsButton TagHelper

<br>
<br>
<br>
[Continue ...]({{ site.rooturl }}/replacing-prompt-with-icon/)

[^1]: Commit “Adding icon to simulate F4 prompt”

