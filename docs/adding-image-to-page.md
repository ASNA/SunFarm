---
layout: page
title: Adding Image to Page
permalink: /adding-image-to-page/
---

| Quick How-to: 
|:-------------
| [Adding an Image to the Page](https://github.com/ASNA/SunFarm/search?q=Adding+an+Image+to+the+Page&type=commits)

<br>

**Monarch Display Pages** are Microsoft Razor Pages where *Standard HTML elements* may also be used.

You may have noticed that we have been using **Row** attributes on *Standard HTML "div" elements* to indicate that div container becomes a **One-row Grid Display Element** where we can position other elements converting **Col** and **ColSpan** attributes to **grid-column** CSS styles.

You may **Visualize** this *Anatomy* with the help of the Browser Developer tools, as indicated by the following image: 

![Developer Tool's Row View](/images/developer-tools-element-view.png)

Furthermore, using Browser Developer tools to inspect **Display Page Fields** provides Visualization as to how *Starting* and *Ending* **Grid** positions are translated to **Standard CSS Styles**.

Observe in the following image, how the field **CUSTREC.SFNAME** is defined by the Browser in *Standard* HTML:

![Developer Tool's Row View](/images/developer-tools-element-style.png)

<br>
## The Vertical Positions may Flow freely

Migrated **DDS** Displayfiles - *now in the form of ASNA Display Pages* - preserve the *Legacy* (row, column) specification in the form of **Row** "div" attribute and **Col** or **ColSpan** attributes on fields inside the "div".

When inspecting directly the rendered **HTML** using Browser's Developer Tools, there is a **"data-asna-row"** custom HTML attribute that contains the value of the **Row** attribute in the Page Markup. This *attribute* has two purposes:

1. *Annotate* in the rendered HTML the intended row in the markup.

2. *Open up* **Vertical Space** for when there are row gaps *(skipping the continuously ascending numbering in the markup)*. After the Page loads completely, JavaScript code will run to insert rows with the CSS style **dds-grid-empty-row** which basically defines vertical spacing to fill the gap.

Once you have understood the basic technique, you can then realize that:

&#128161; Adding other *Standard HTML* elements outside the **div Row** containers, is not only perfectly valid, but *encouraged*.

The same is true for *Standard HTML* elements added *inside* **div Row** containers. Doing so, will make sure elements are positioned at "Row" vertical boundaries and starting at the top-left position specified by a another attribute called **ExpoCol** (similar to the **Col** attribute on DdsCharField, DdsConstant and DdsDecField *TagHelpers* you have been using all along).  

Let’s add a Customer Photo placeholder on this page.

*First*, let's create the [SVG](https://en.wikipedia.org/wiki/Scalable_Vector_Graphics) image. 

Copy the XML source from next paragraph and save it to new file: `CustomerAppSite\wwwroot\customer-icon.svg`

```xml
<?xml version="1.0" encoding="iso-8859-1"?>
<svg version="1.1" id="Layer_1" xmlns="http://www.w3.org/2000/svg" xmlns:xlink="http://www.w3.org/1999/xlink" x="0px" y="0px"
	 viewBox="0 0 508.609 508.609" style="enable-background:new 0 0 508.609 508.609;" xml:space="preserve">
    <g>
        <circle style="fill:#EEB490;" cx="163.942" cy="159.364" r="36.959"/>
        <circle style="fill:#EEB490;" cx="344.328" cy="159.364" r="36.959"/>
    </g>
    <circle style="fill:#56545F;" cx="254.135" cy="115.285" r="115.285"/>
    <path style="fill:#FF667C;" d="M338.564,284.821H169.706c-77.309,0-140.037,62.728-140.037,140.037v83.751H478.94v-83.751 C478.601,347.55,415.873,284.821,338.564,284.821z"/>
    <path style="fill:#D34A5E;" d="M166.993,284.821v116.98c0,48.148,38.993,87.142,87.142,87.142s87.142-38.993,87.142-87.142v-116.98	H166.993z"/>
    <circle style="fill:#EEB490;" cx="254.135" cy="271.936" r="62.728"/>
    <g>
        <circle style="fill:#F1F3F7;" cx="218.872" cy="341.785" r="26.109"/>
        <circle style="fill:#F1F3F7;" cx="289.399" cy="341.785" r="26.109"/>
    </g>
    <circle style="fill:#D6D6D8;" cx="254.135" cy="341.785" r="19.666"/>
    <path style="fill:#FACCB4;" d="M330.766,74.257c-10.172,17.971-40.689,31.195-76.63,31.195s-66.458-13.224-76.63-31.195 c-11.189,15.597-17.632,34.585-17.632,54.93v69.171c0,52.217,42.4,94.262,94.262,94.262c52.217,0,94.262-42.384,94.262-94.262 v-68.832C348.397,108.842,341.955,89.854,330.766,74.257z"/>
</svg>
```

*Next*, add a reference to the new image by adding Row="3" to the record "CUSTREC" in the markup file:

`CustomerAppSite\Areas\CustomerAppViews\Pages\CUSTDSPF.cshtml`

With this HTML segment:

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

> The `#customer-icon` style sets the **width** of the image element, yet the SVG definition is defined to **preserve** aspect-ratio, meaning, the *height* will also follow the *width* to preserve the proportions.

Notice how **Row="3"** became as tall as the height of the image, pushing all the rest of the rows down. This may or not be what we intended.

![Customer Icon Shows](/images/page-two-04_a.png)

>If you still don't see the image with proper width, clear the Browser's CSS cache.

If we prefer not *to push* the rest of rows down, we can change the position style from: "relative" to: "absolute"[^1].

```css
#customer-icon {
    position: absolute;
    width: 109px;
    border-color: gray;
    border-width: thin;
    border-style: solid;
    background-color: white;
}
```

![Customer Icon Intended Placement](/images/page-two-04_b.png)

<br>
<br>

[Continue ...]({{ site.rooturl }}/merging-two-screens/)

<br>
<br>

[^1]: [Commit: "Adding an Image to the Page"](https://github.com/ASNA/SunFarm/search?q=Adding+an+Image+to+the+Page&type=commits)