---
layout: page
title: Application Logo Branding
permalink: /logo-branding/
---

We want to show the SunFarm Logo on all pages of the Enhanced Customer Application.

# Logo Anatomy 
The SunFarm Logo consisits of:

* An image of a sunrise.
* Two lines of text:
  * Company name ("sunfarm")
  * Company Motto ("always fresh")

The image we use is names "sun-farm", it is a *raster* **png** format image, designed with transparency:

![Logo image](/images/logo.png)

# First step: Add the image to the website assets

The *Website assets* are static content that may be resuested by Pages, such as images, styles and scripting.

The folder that contains *Website Assets* is called in ASP.NET **wwwroot**

When an ASP.Net website is created the folder **wwwroot** exists and has a few folders inside:

  * css
  * js
  * lib

Complete the **wwwroot** folder structure with an *images* folder, where we copy the raster image named "sun-farm.png" [^1].

![www root file structure](/images/wwwroot-images.png)

# Second step: Add styles for the logo image.

The new Website template adds a file named **site.css** where we add custom styles. The original file is empty (may contain only a comment).

We add the following *styles* for elements we are interested on [^1]:

```css  
#logo-banner {
    height: auto;
    background-color: green;
}

#logo {
    padding-left: 1em;
}

#logo-text {
    display: inline-block;
    vertical-align: top;
    margin-left: -41px;
}

#logo-title {
    color: orange;
    font-size: xx-large;
    font-family: system-ui;
    font-weight: bolder;
}

#logo-subtitle {
    text-align: center;
    color: lightgreen;
    font-size: medium;
    opacity: 0.5;
}
```   

Application Pages are Migrated to the *Areas* subfolder within the *CustomerAppSite*. Given that we want **ALL** Pages (in the Customer Area) to display the Logo header, the place where we want to define it, is:

~~~   
CustomerAppSite\Areas\CustomerAppViews\_ViewStart.cshtml
~~~   

This file defines the layout shared by all Pages in the view **CustomerAppViews**

```html
@{
    Layout = "_Layout";
}

<div id="logo-banner">
    <img id="logo" src="~/images/sun-farm.png" />
    <div id="logo-text">
        <div id="logo-title">sunfarm</div>
        <div id="logo-subtitle">always fresh</div>
    </div>
</div>
```   

The top header for all pages will contain a *division* designated for the Logo Banner which contains:
1. An image
2. A division for two legends, positioned to produce the complete Logo.

![Logo image](/images/logo-banner.png)



<br>
<br>
<br>

[Continue ...]({{ site.url }}{{ site.baseurl}}/navigation-menu)


[^1]: Commit: "Logo resources"

