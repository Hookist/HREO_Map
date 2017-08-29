Login: superadmin007@gmail.com
password: Qwerty1!

First you should execute some commands in Package Manager Console :

1) Install-Package EntityFramework -IncludePrerelease

2) Enable-Migrations  -ContextTypeName AddressesMap.Models.DBModels.AddressesMapModel

3) Update-Database 
 
MsSql script located in folder App_Data of the project