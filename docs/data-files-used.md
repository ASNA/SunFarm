---
layout: page
title: Data Files Used
permalink: /data-files-used/
---

| Quick How-to: 
|:-------------
| [Get data files](https://github.com/ASNA/SunFarmData)

<br>

The Customer Sample Application uses existing files created on IBM i.

SunFarm Migration expects those files to have been previously Migrated to Microsoft SQL Database.[^1]

As you can verify by looking at the [MyJob.cs](https://github.com/ASNA/SunFarm/blob/master/CustomerAppLogic/MyJob.cs) file, the database used is named **SunFarm**


~~~   
 public Database MyDatabase = new Database("SunFarm");
~~~

## Assumption
This Guide assumes that your have a Microsoft SQL Installation and have migrated (or obtained a copy of the migrated data[^1]) from the Legacy Sample Customer Application.

<br>

## Configuration with local system PATH references.

Before running the cloned Repository, please update the following references to your local system:

1. Route for `empty` page to the website.
2. Path to the `Message File` folder on your system.
3. Path to the Application's `Job` class.

### Route for `empty` page to the website.
Edit the sourcefile `..\SunFarm\CustomerAppSite\Startup.cs`

Update line `61` the **AddAreaPageRoute** line. The folder name to the *Views* may be different in your copy.

```cs
services.AddRazorPages(razorOptions =>
{
    razorOptions.Conventions.AddAreaPageRoute("CustomerAppViews", "/CUSTDSPF", "");
}).AddMvcOptions (mvcOptions =>
{
    mvcOptions.ValueProviderFactories.Insert(0, new EditedValueProviderFactory());
});
```

<br>

### Path to the `Message File` folder on your system.
Edit the sourcefile `..\SunFarm\CustomerAppSite\App.config`.

Make sure Line `4` has the correct path:

```html
<?xml version="1.0" encoding="utf-8"?>
<configuration>
  <appSettings>
    <add key="MonarchMessageFileFolder" value="C:\Projects\ASNA\SunFarm\CustomerAppSite\MessageFiles" />
  </appSettings>
  <startup>
    <supportedRuntime version="v4.0" sku=".NETFramework,Version=v4.5" />
  </startup>
</configuration>
```

<br>

### Path to the Application **Job** class.
Edit sourcefile `C:\Projects\ASNA\SunFarm\CustomerAppSite\appsettings.json`

Make sure the `AssemblyPath` attribute has the correct value for to the output of the Logic Project:

```json
"JobDescriptor": {
"AssemblyPath": "C:\\Projects\\ASNA\\SunFarm\\CustomerAppLogic\\bin\\Debug\\net5.0\\SunFarm.Customers.Application.dll",
"Class": "SunFarm.Customers.Application_Job.MyJob",
"Name": "SunFarmJob"
},
```


<br>
<br>
<br>
[Continue ...]({{ site.rooturl }}/logo-branding/)

<br>
<br>

[^1]: The Repository [SunFarmData](https://github.com/ASNA/SunFarmData) may be used to restore the Customer Sample SQL Data Migration.