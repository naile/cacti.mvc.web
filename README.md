Cacti asp.net mvc extensions
=============

A set of common use extensions, filters and helpers for asp.net MVC


Installation
------------

    Install-Package Cacti.Mvc.Web

To avoid importing namespaces in your views add `Cacti.Mvc.Web` to your Views/Web.config
```xml
<system.web.webPages.razor>
   <namespaces>
      <add namespace="Cacti.Mvc.Web"/>
   </namespaces>
</system.web.webPages.razor>
```
HTML helpers
------------
* DropDownGroupFor
* DescriptionFor
* CleanTextBoxFor

Filters
-------
* AutoRefreshAttribute


Attributes
----------
* MaxFileSizeAttribute


Routes
------
* NotEqual constraint


Extensions
----------
* DateTime (ToUnix, ToJavascript)