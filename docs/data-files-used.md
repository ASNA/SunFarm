---
layout: page
title: Data Files Used
permalink: /data-files-used/
---

The Sample Customer Application uses existing files created on IBMi.

SunFarm Migration expects those files to have been previously Migrated to Microsoft SQL Database.

As you can verify by looking at the [MyJob.cs](https://github.com/ASNA/SunFarm/blob/master/CustomerAppLogic/MyJob.cs) file, the database used is named **NancySql**


~~~   
 public Database MyDatabase = new Database("NancySql");
~~~

## Assumption
This Guide assumes that your have a Microsoft SQL Installation and have migrated (or obtained a copy of the migrated data) from the Legacy Sample Customer Application.

<br>
<br>
<br>
[Continue ...]({{ site.rooturl }}/logo-branding/)
