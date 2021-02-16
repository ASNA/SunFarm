---
layout: page
title: Remove Redundant Green-screen Info
permalink: /enhancements-affecting-logic/
---

[In the last section]({{ site.rooturl }}/enhancements-affecting-logic/), we left the *Customer Inquiry* Page, looking like this image:

![Narrower Customer Name Column](/images/narrower-name-column.png)

There comes a time when to continue enhancing a page, it is necessary to change the Business Logic.

Since we have been able to compress the screen while aligning elements and eliminating redundant items, we are left with unused real-estate.

The Business Logic is writing fourteen records at a time, now we can fit at least twenty (a nice rounding number) or even more if the majority of users may have higher resolution devices.

Let’s assume we want to increase the record count from fourteen to twenty.

