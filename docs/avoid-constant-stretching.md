---
layout: page
title: Constant Label Text Stretching effect
permalink: /avoid-constant-stretching/
---
If you had not noticed, the text on the constants is rendered with more white space between characters (*than normal*).

Particularly noticeable is the subfile column heading “City / State / Zip”.

![Subfile Column Headings with extra spacing](/images/out-of-box-page-one.png)


ASNA Expo DdsConstant TagHelper has an attribute called *StretchConstantText* which defaults to true. That attribute can be overridden at the record-level.

That attribute exists to match better the constant text alignment that *green-screen* developers used, particularly when splitting words in multiple DDS constants, or when right justifying (manually) text on the screen.

# Look at other pages where this need becomes apparent

## Legacy developer intended to align labels to the right:

![Subfile Column Headings with extra spacing](/images/stretch-original-intention.png)

<br>
<br>

## Same starting constant positions, without stretching text:

![Subfile Column Headings with extra spacing](/images/stretch-off.png)


<br>
<br>
<br>
