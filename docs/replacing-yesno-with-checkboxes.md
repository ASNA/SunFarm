---
layout: page
title: Replacing YES/NO fields with Check-boxes
permalink: /replacing-yesno-with-checkboxes/
---

**Green-screen** Displayfiles typically used *boolean* fields with a *one-char* field that expects the value `‘Y’` or `’N’`. 

The Markup for Page [Customer Maintenance]({{ site.rooturl }}/enhancements-affecting-logic/), has a field named: `CUSTREC.SFYN01`

It is rendered as:

![Send Confirmation YES/NO](/images/page-two-checkbox-01.png/)

&#128161; Using character fields for booleans presents several problems, among them are:

1. Fields need a constant to the right to show the possible *expected* values, in this example `(Y/N)`. Not only does it waste valuable screen real-estate , but also requires the constant to be properly aligned.
2. Having a character input field that allows more than two possible values, makes the interface *error prone* and therefore more validation will be required.

Checkboxes on Web pages are usually presented with the **“tick”** button to the left of the label, with a specific *spacing* between the button and the label. It should make sense then to include a constant label, such as `“Send Confirmation”` as part of the tagHelper.

What we want to produce is the following (see image below)

![Send Confirmation checkbox](/images/page-two-checkbox-02.png/)

>Notice the *Send Confirmation* `checkbox`.

When the “tick” is checked, the application logic wants the field with a character value of `“Y”` and when it does not a `“N”` (or a *blank*)

The Markup can be simplified *too*, from three lines:

```html
  <DdsConstant Col="47" Text="Send Confirmation" />
  <DdsCharField Col="58" ColSpan="2" For="CUSTREC.SFYN01" VirtualRowCol="18,27" />
  <DdsConstant Col="61" Text="(Y/N)" />
```

Down to one line:

```html
<DdsCheckboxField Col="47" Text="Send Confirmation" For="CUSTREC.SFYN01" VirtualRowCol="18,27" />
```

On the `Model` source file, we *decorate* the field, providing information for the value we want to mean **checked** (or true), and which for **unchecked** (or false). 

The `Values` attribute for `DdsCheckboxField` expects the first value to be the **checked** and the second the **unchecked**.

```cs
    [Char(1)]
    [Values(typeof(string), “Y”, “N”)]
    public string SFYN01 { get; set; }
```

>Note: `DdsCheckboxField` can be used on `Decimal` workstation fields *too*.

<br>
<br>
<br>
[Continue ...]({{ site.rooturl }}/replacing-yesno-radio-button-group/)
