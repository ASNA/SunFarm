---
layout: page
title: Replacing YES/NO fields with Check-boxes
permalink: /replacing-yesno-with-checkboxes/
---

**Green-screen** Displayfiles typically used *boolean* fields with a *one-char* field that expects the value `‘Y’` or `’N’`. 

The Markup for Page [Customer Maintenance]({{ site.rooturl }}/merging-two-screens//), has a field named: `CUSTREC.SFYN01`

It is rendered as:

![Send Confirmation YES/NO](/images/page-two-checkbox-01.png/)

&#128161; Using character fields for booleans presents several problems, among them are:

1. Fields need a constant to the right to show the possible *expected* values, in this example `(Y/N)`. Not only does it waste valuable screen real-estate , but also requires the constant to be properly aligned.
2. Having a character input field that allows more than two possible values, makes the interface *error prone* and therefore more validation will be required.

Checkboxes on Web pages are usually presented with the **“tick”** button to the left of the label, with a specific *spacing* between the button and the label. It should make sense then to include a constant label, such as `“Send Confirmation”` as part of the TagHelper.

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

The `Values` attribute for `DdsCheckboxField` expects the first value to be the **checked** and the second the **unchecked**[^1].

```cs
[Char(1)]
[Values(typeof(string), "Y", "N")]
public string SFYN01 { get; set; }
```

>Note: `DdsCheckboxField` can be used on `Decimal` workstation fields *too*.

## Using Checkbox with decimal fields

Let’s artificially modify our application to show how `DdsCheckboxField` TagHelper could be used to bind a `Decimal` field in our `Model` (which could be extended to a database field defined as decimal too).

To avoid changing too much our Application, let’s add a new field to the `Model` defined as follows:

~~~
CustomerAppSite\Areas\CustomerAppViews\Pages\CUSTDSPF.cshtml.cs:
~~~

```cs
[Dec(1, 0)]
[Values(typeof(decimal), 1, 0)]
public decimal DECSNDCONF { get; set; }
```

The new field reads: *“Decimal Send Conformation” and uses 1 digit, where 1 means Yes and 0 (zero) means No*.

For convenience we will declare it right after `PERCENT_CHANGE_RETURNS` (around line 250 - see commit file differences).

In the markup, replace: `CUSTREC.SFYN01` with `CUSTREC.DECSNDCONF`:

~~~
CustomerAppSite\Areas\CustomerAppViews\Pages\CUSTDSPF.cshtml
~~~

Before:

```html
<DdsCheckboxField Col="47" Text="Send Confirmation" For="CUSTREC.SFYN01" VirtualRowCol="18,27" />
```

After:

```html
<DdsCheckboxField Col="47" Text="Send Confirmation" For="CUSTREC.DECSNDCONF" VirtualRowCol=“18,27" />
```

If we had `DECSNDCONF` in the database declared as decimal (1,0) that would be **all** we need to do, but we will **not** go to the trouble of changing the database schema in this Guide.

*Instead*, we will trick the Logic code such that `Y/N` in field `SFYN01` will update `DECSNDCONF` when going *out* to the screen, and then posted new value for `DECSNDCONF` with value `1` or `0` will update `SFYN01` on the way *in*. 

This should be simple to do.

Let’s  first run the [Serengeti Tools](https://asna.githubio.SerengetiTools) to update the Display Page *DataSet* and make `DECSNDCONF` available to the Program `CUSTINQ`.

Locate the sourcefile `CustomerAppLogic/CUSTINQ.cs` using *Visual Studio Solution Explorer* and invoke the *context-menu* options and execute: `Refresh XFU` and `Run Custom Tools` Serengeti commands.

To keep in *Sync* the Decimal field `DECSNDCONF` with the database field `SFYN01` all we need to do in the Business Logic - Program `CUSTINQ` - is:

*Before* going *out* to the screen, that is, before: `CUSTDSPF.ExFmt("CUSTREC", _IN.Array);` update `DECSNDCONF` with the value in `SFYN01`, with the following code:

```cs
DECSNDCONF = (SFYN01 == "Y") ? 1 : 0;
```

*After* coming back from the screen to the Business Logic, that us, after: `CUSTDSPF.ExFmt("CUSTREC", _IN.Array);`, update `SFYN01` value.

```cs
SFYN01 = DECSNDCONF == 1 ? "Y" : "N";
```
Build CustomerAppLogic Project and run the Website[^2].

Updating Customer records by changing the state of the “Send Confirmation” checkbox will work *the same way* as before, but you can debug to see how the `DECSNDCONF` decimal field values changes from `0` to `1` values.

<br>
<br>
<br>
[Continue ...]({{ site.rooturl }}/replacing-yesno-with-radio-button-groups/)


[^1]: Commit “Replaced Y/N field with Checkbox”
[^2]: Commit “Using Decimal Fields with Checkboxes”