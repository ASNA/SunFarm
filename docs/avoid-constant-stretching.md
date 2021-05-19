---
layout: page
title: Constant Label Text Stretching effect
permalink: /avoid-constant-stretching/
---
If you had not noticed, the text on the constants is rendered with more white space between characters (*than normal*).

Particularly noticeable is the subfile column heading "City / State / Zip".

![Subfile Column Headings with extra spacing](/images/out-of-box-page-one.png)


ASNA Expo DdsConstant TagHelper has an attribute called *StretchConstantText* which defaults to true. That attribute can be overridden at the record-level.

That attribute exists to match better the constant text alignment that *green-screen* developers used, particularly when splitting words in multiple DDS constants, or when right justifying (manually) text on the screen.

# Look at other pages where this becomes more apparent

## Legacy developer intended to align labels to the right:

![Subfile Column Headings with extra spacing](/images/stretch-original-intention.png)

<br>
<br>

## Same starting constant positions, without stretching text:

![Subfile Column Headings with extra spacing](/images/stretch-off.png)

## The Stretching effect on Constant text can be turned *on* or *off* by changing the attribute:

~~~
StretchConstantText=
~~~

With boolean values *true* or *false*.

The Default value (if the attribute is not given) is *true*.

>The attribute can be specified at the *record* or at the *constant* level.

For example, we want "CUSTREC" to **NOT* Stretch the text in the constants:

```html
<DdsRecord For="CUSTREC" StretchConstantText=false KeyNames="ENTER ‘Submit'; …
```


# Preferred technique to Align text to the right

ASNA *Cocoon for Base* will not try to guess *right-alignment* intent. (Unfortunately, DDS does not have a place to indicate such intent).

The *Stretching effect* is not perfect. There are differences in the rendering of Font families and rendering engines which vary according to the Browser vendor.

One way to control the alignment of text, is thru the use of CSS.

To align Text to the right, we need to specify the original **cell** width intended on the green-screen character grid layout. Usually this width is the length of the constant text (given that green-screen used a predictable fixed width font).

Matching the column span is achieved by adding ColSpan attribute with the special value ="-1” indicating that we want exactly the length of the constant text to be used.

Lastly, we add a CSS class attribute to all the DdsConstant tag-helpers in the record set to "right-aligned-constant":

```html
<div Row="7">
    <DdsConstant Col="20" ColSpan="-1" class="right-aligned-constant" Text="Name:" />
    .
    .
    .
</div>
```

Where the style right-aligned-constant is the following simple CSS added to the CustomerAppSite\wwwroot\css\site.css file:

```css
.right-aligned-constant {
    text-align: right;
}
```

&#128161; Experiment with this approach on your own, and the imperfections of the StretchConstantText effect will disappear.
