---
layout: page
title: Data Files Used
permalink: /data-files-used/
---

The Customer Sample Application uses existing files created on IBM i.

SunFarm Migration expects those files to have been previously Migrated to Microsoft SQL Database.[^1]

As you can verify by looking at the [MyJob.cs](https://github.com/ASNA/SunFarm/blob/master/CustomerAppLogic/MyJob.cs) file, the database used is named **SunFarm**


~~~   
 public Database MyDatabase = new Database("SunFarm");
~~~

## Assumption
This Guide assumes that your have a Microsoft SQL Installation and have migrated (or obtained a copy of the migrated data[^1]) from the Legacy Sample Customer Application.

<br>
<br>
<br>
[Continue ...]({{ site.rooturl }}/logo-branding/)

<br>
<br>

[^1]: The Repository [SunFarmData](https://github.com/ASNA/SunFarmData) may be used to restore the Customer Sample SQL Data Migration.