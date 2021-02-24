---
layout: page
title: Replacing YES/NO fields with Radio-button Groups
permalink: /replacing-yesno-with-radio-button-groups/
---

Certain fields are expected to have a **value** which is *one* of *many* discreet values. We have seen the simpler case where *two* values are allowed: `YES`, `NO` (or *true* *false*) — and we used a `Checkbox` element for that scenario —.

When *more* that *two* values of a discreet collection is expected, a *Radio Button Group* may be the best User Interface.

>&#128161; IBMi Legacy `DDS` referred to this User Interface as [MLTCHCFLD or multiple-choice selection field](https://www.ibm.com/support/knowledgecenter/en/ssw_ibm_i_72/rzakc/rzakcmstmltchcf.htm).

An important distinction, when comparing *Checkbox Groups* with *Radio Button Groups* is that, in addition to listing the available choices, *selecting* one choice automatically *de-selects* the others. In other words, choices are *mutually* exclusive.

ASNA Monarch Nomad&reg; provides a tagHelper to simplify the production of *Radio Button Groups* named `DdsRadioGroupField`.

To explore the use of `DdsRadioGroupField` we will replace the `DdsCheckboxField`  [described in this Guide before]({{ site.rooturl }}/replacing-yesno-with-checkboxes/).

* The *field definition* in the `Model` file does **not** need to change.

*For reference*, here is the definition used for Checkboxes:

Sourcefile: `CustomerAppSite\Areas\CustomerAppViews\Pages\CUSTDSPF.cshtml.cs`

```cs
[Char(1)]
[Values(typeof(string), "Y", "N")]
public string SFYN01 { get; set; }
```

What we need to change is the *Markup*, such that `Row 10` is defined as follows:

Markup File: `CustomerAppSite\Areas\CustomerAppViews\Pages\CUSTDSPF.cshtml`[^1]

```html
<div Row="10">
   <DdsConstant Col="10" Text="Phone" />
   <DdsCharField Col="20" ColSpan="10" class="left-aligned-field" For="CUSTREC.SFPHONE" VirtualRowCol="14,27"  />
   <DdsConstant Col="33" Text="Fax" />
   <DdsDecField Col="36" ColSpan="10" class="left-aligned-field" For="CUSTREC.SFFAX" VirtualRowCol="13,27" EditWord="(   )   -    " tabIndex=8 />
   <DdsRadioButtonGroupField Col="47" Text="Send Confirmation:" ValuesText="'Yes','No'" For="CUSTREC.SFYN01" VirtualRowCol="18,27" />
</div>
```

Focusing on the new `DdsRadioButtonGroupField`, let's describe the attributes used:

| **Attribute** | **Definition** 
| *Text* | The Label for the Group of Radio Buttons
| *ValuesText* | A comma separated list of labels for each of the Radio Buttons

<br>
<br>
<br>
[Continue ...]({{ site.rooturl }}/replacing-yesno-radio-button-groups/)

[^1]: Commit “Replacing YES/NO field with a Radio Button Group”