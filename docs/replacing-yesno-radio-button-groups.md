---
layout: page
title: Replacing YES/NO fields with Radio-button Groups
permalink: /replacing-yesno-with-radio-button-groups/
---

Certain fields are expected to have a **value** which is *one* of *many* discreet values. We have seen the simpler case where *two* values are allowed: `YES`, `NO` (or *true* *false*) — and we used a `Checkbox` element for that scenario —.

When *more* that *two* values of a discreet collection is expected, a *Radio Button Group* may be the best User Interface.

>&#128161; IBM i Legacy `DDS` referred to this User Interface as [MLTCHCFLD or multiple-choice selection field](https://www.ibm.com/support/knowledgecenter/en/ssw_ibm_i_72/rzakc/rzakcmstmltchcf.htm).

An important distinction, when comparing *Checkbox Groups* with *Radio Button Groups* is that, in addition to listing the available choices, *selecting* one choice automatically *de-selects* the others. In other words, choices are *mutually* exclusive.

ASNA Monarch Nomad&reg; provides a TagHelper to simplify the production of *Radio Button Groups* named `DdsRadioGroupField`.

To explore the use of `DdsRadioGroupField` we will replace the `DdsCheckboxField`  [described in this Guide before]({{ site.rooturl }}/replacing-yesno-with-checkboxes/).

The *field definition* in the `Model` file does **not** need to change.

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

| Attribute    | Definition 
| ------------ | ---
| *Text*       | The Label for the Group of Radio Buttons
| *ValuesText* | A comma separated list of labels for each of the Radio Buttons

Please note that:
1. The order of items in the `ValuesText` attribute is **important**.
2. There should be a one-to-one correspondence between the `Values` attribute in the `Model` field's definition to each text in the `ValuesText` attribute in the Markup.
3. Individual labels in the `ValuesText` attribute are separated by *comma* and quoted using *single* quotes.

Run the Website again, and the **Customer Maintenance Page** should now look like the following image:

![YES/NO Field presented as a Radio Group](/images/page-two-radio-grp-01.png/)

Notice that as you click on a radio button, for example on **"No"** choice, the dark circle on the **"Yes"** choice disappears - making the selection *mutually* exclusive -. 

## Keyboard operation

When *keyboard focus* is one of the choices in the *Radio Group*, for example in **"Yes"** radio button:

1. The *Right arrow* moves to the **next** choice.
2. The *Left arrow* moves to the **previous** choice.
3. Continue to move *right* or *left*, past the last or first choice, *rolls around* the choice group.

>The Radio Group interface is normally used when the possible choices are more than two, but for the sake of keeping this Guide *simple*, we use the same `SFYN01` field as we did before for the [Checkboxes in this Guide before]({{ site.rooturl }}/replacing-yesno-with-checkboxes/).
  

## Using Radio button Group fields with decimal fields

`DdsRadioButtonGroupField` TagHelper is not limited `Char` fields. Just as we did for [Checkboxes in this Guide before]({{ site.rooturl }}/replacing-yesno-with-checkboxes/), if the field we want to present as a *group of radio buttons* is defined as a `Decimal`, we can still use it to *bind* to this TagHelper.

More specifically, a field definition in the `Model`, such as:

```cs
[Dec(1, 0)]
[Values(typeof(decimal), 1, 0)]
public decimal DECSNDCONF { get; set; }
```

May be *bound* to the following (*unmodified*) Markup:

```html
<DdsRadioButtonGroupField Col="47" Text="Send Confirmation:" ValuesText="'Yes','No'" For="CUSTREC.DECSNDCONF" VirtualRowCol="18,27" />
```

>*At runtime*:

1. Going out to the Browser, `DECSNDCONF` with a value of `1` will check the *Radio button* with label `‘Yes’` — and if the value were `0` (*zero*), the radio button with `‘No’` would be checked instead —.
2. A change on the state of the *Radio buttons* for field `DECSNDCONF` requested from the Browser would be translated to `1` or `0` (*zero*) Decimal value, depending on which *radio button* was left **checked**.

>No changes to the Model nor the Application logic are needed.

<br>
<br>
<br>
[Continue ...]({{ site.rooturl }}/replacing-prompt-with-icon/)

[^1]: Commit "Replacing YES/NO field with a Radio Button Group”