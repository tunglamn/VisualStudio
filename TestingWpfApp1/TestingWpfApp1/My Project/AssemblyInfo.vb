﻿Imports System
Imports System.Reflection
Imports System.Runtime.InteropServices
Imports System.Globalization
Imports System.Resources
Imports System.Windows

' Общие сведения об этой сборке предоставляются следующим набором 
' атрибутов. Отредактируйте значения этих атрибутов, чтобы изменить
' общие сведения об этой сборке.

' Проверьте значения атрибутов сборки

<Assembly: AssemblyTitle("TestingWpfApp1")> 
<Assembly: AssemblyDescription("")> 
<Assembly: AssemblyCompany("")> 
<Assembly: AssemblyProduct("TestingWpfApp1")> 
<Assembly: AssemblyCopyright("Copyright ©  2015")> 
<Assembly: AssemblyTrademark("")> 
<Assembly: ComVisible(false)>

'Для построения приложений с поддержкой локализации установите 
'<UICulture>ТребуемоеЗначение</UICulture> в файле .vbproj
'внутри <PropertyGroup>. Например, при использовании американского английского 
'в исходных файлах установите значение <UICulture> равным "en-US". Затем снимите комментарий
'с атрибута NeutralResourceLanguage (ниже). Замените "en-US" в расположенной ниже
'строке значением, соответствующим параметру UICulture в файле проекта.

'<Assembly: NeutralResourcesLanguage("en-US", UltimateResourceFallbackLocation.Satellite)> 


'Атрибут ThemeInfo указывает расположение словарей ресурсов для конкретной темы и словарей общих ресурсов.
'1-й параметр: расположение словарей ресурсов для конкретной темы
'(используется если ресурс не найден на странице 
' или в словарях ресурсов приложения)

'2-й параметр: расположение словаря общих ресурсов
'(используется если ресурс не найден на странице, 
'в приложении и в словарях ресурсов для конкретной темы)
<Assembly: ThemeInfo(ResourceDictionaryLocation.None, ResourceDictionaryLocation.SourceAssembly)>



'Следующий GUID служит для идентификации библиотеки типов, если этот проект будет видимым для COM
<Assembly: Guid("a8cbff02-fec0-4d2e-a91d-91b71ec592e2")> 

' Сведения о версии сборки состоят из следующих четырех значений:
'
'      Основной номер версии
'      Дополнительный номер версии 
'      Номер построения
'      Редакция
'
' Можно задать все значения или принять номер построения и номер редакции по умолчанию, 
' используя "*", как показано ниже:
' <Assembly: AssemblyVersion("1.0.*")> 

<Assembly: AssemblyVersion("1.0.0.0")> 
<Assembly: AssemblyFileVersion("1.0.0.0")> 
