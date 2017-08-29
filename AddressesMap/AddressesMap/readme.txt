Login: superadmin007@gmail.com
password: Qwerty1!

Сперва нужно запустить миграции с помощью команд в Package Manager Console :

Enable-Migrations  -ContextTypeName AddressesMap.Models.DBModels.AddressesMapModel

Update-Database
 
ms sql cкрипт данных для базы данных находиться в App_Data