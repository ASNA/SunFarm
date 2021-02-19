---
layout: page
title: Adding Image to Page
permalink: /adding-image-to-page/
---
**Monarch Display Pages** are Microsoft Razor Pages where *Standard HTML elements* may also be used.

You may have noticed that we have been using **Row** attributes on *Standard HTML "div" elements* to indicate that div container becomes a **One-row Grid Display Element** where we can position other elements converting **Col** and **ColSpan** attributes to **grid-column** CSS styles.

You may **Visualize** this *Anatomy* with the help of the Browser Developer tools, as indicated by the following image: 

![Developer Tool's Row View](/images/developer-tools-element-view.png)

Furthermore, using Browser Developer tools to inspect **Display Page Fields** provides Visualization as to how *Starting* and *Ending* **Grid** positions are translated to **Standard CSS STyles**.

Observe in the following image, how the field **CUSTREC.SFNAME** is defined by the Browser in *Standard* HTML:

![Developer Tool's Row View](/images/developer-tools-element-style.png)

## The Vertical Positions may Flow freely

Migrated **DDS** Displayfiles - *now in the form of ASNA Display Pages* - preserve the *Legacy* (row, column) specification in the form of **Row** "div" attribute and **Col** or **ColSpan** attributes on fields inside the "div".

When inspecting directly the rendered **HTML** using Browser's Developer Tools, there is a **"data-asna-row"** custom HTML attribute that contains the value of the **Row** attribute in the Page Markup. This *attribute* has two purposes:

1. *Annotate* in the rendered HTML the intended row in the markup.

2. *Open* Vertical space for when there are row gaps *(skipping the continuously ascending numbering in the markup)*. After the Page loads completely, JavaScript code will run to insert rows with the CSS style **dds-grid-empty-row** which basically defines vertical spacing to fill the gap.

Once you have understood the basic technique, you can then realize that:

&#128161; Adding other *Standard HTML* elements outside the **div Row** containers, is not only perfectly valid, but *encouraged*.

The same is true for *Standard HTML* elements added *inside* **div Row** containers. Doing so, will make sure elements are positioned at “Row” vertical boundaries and starting at the top-left position specified by a another attribute called **ExpoCol** (similar to the **Col** attribute on DdsCharField, DdsConstant and DdsDecField tagHelpers you have been using all along).  

Let’s add a Customer Photo placeholder on this page.

```html
<div Row="3">
    <img id="customer-icon" ExpoCol="8" src="~/customer-icon.svg" />
</div>
```
  
We have our image in the proper *vertical* position, but it has the wrong *horizontal* position. We are also missing the **width** and **height** image specifications.

The next image shows how **customer-icon** would be attempted to be rendered by the Browser:

![Customer Icon Vertical Placement](/images/page-two-04.png)

We need to complete the position and dimensions of the image by adding the following CSS style to our site.css (in wwwroot/css folder as we did before):

```css
#customer-icon {
    position: relative;
    width: 109px;
    border-color: gray;
    border-width: thin;
    border-style: solid;
    background-color: white;
}
```

>&#128161; Notice how **Row=“3”** became as tall as the height of the image, pushing all the rest of the rows down. This may or not be what we intended.

![Customer Icon Shows](/images/page-two-04_a.png)

