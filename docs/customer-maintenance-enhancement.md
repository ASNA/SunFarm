---
layout: page
title: Customer Maintenance Enhancement
permalink: /customer-maintenance-enhancement/
---
Let’s apply the same techniques like we did for the [Customer Inquiry Page]({{ site.rooturl }}/remove-redundant-green-screen-info/).

*Same Markup file*:

~~~
CustomerAppSite\Areas\CustomerAppViews\Pages\CUSTDSPF.cshtml
~~~

*Different Record*:
```html
<DdsRecord For="CUSTREC"
```

1. Remove redundant **Program Name**, **Date** and **Time**
2. Improve KeyNames (Navigation Menu): ‘Submit’ ‘Exit' ‘Prompt’ ‘New Customer’ ‘Remove Customer’ and ‘Back’.
3. Reset StretchConstantText to false (record level)
4. Replace **Page Title** by standard HTML and CSS

With these changes, the Page should look like the following image:

![Customer Maintenance Prep](/images/page-two-00.png/)

<br>
<br>
<br>
[Continue ...]({{ site.rooturl }}/adding-image-to-page/)
