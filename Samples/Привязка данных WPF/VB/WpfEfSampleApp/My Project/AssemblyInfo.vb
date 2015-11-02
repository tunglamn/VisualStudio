Imports System
Imports System.Reflection
Imports System.Runtime.InteropServices
Imports System.Globalization
Imports System.Resources
Imports System.Windows

' Управление общими сведениями о сборке осуществляется с помощью 
' набора атрибутов. Отредактируйте эти значения атрибутов, чтобы изменить сведения,
' сопоставленные со сборкой.

' Проверьте значения атрибутов сборки

<Assembly: AssemblyTitle("WpfEfSampleApp")> 
<Assembly: AssemblyDescription("")> 
<Assembly: AssemblyCompany("Microsoft Corporation")> 
<Assembly: AssemblyProduct("WpfEfSampleApp")> 
<Assembly: AssemblyCopyright("Copyright @ Microsoft Corporation 2009")> 
<Assembly: AssemblyTrademark("")> 
<Assembly: ComVisible(false)>

'Чтобы начать построение локализованных приложений, задайте 
'<UICulture>CultureYouAreCodingWith</UICulture> в файле .vbproj
'внутри <PropertyGroup>. Например, при использовании американского английского 
'в исходных файлах установите значение <UICulture> равным "en-US".  Затем снимите комментарий
'с атрибута NeutralResourceLanguage (ниже).  Замените "en-US" в расположенной ниже
'строке значением, соответствующим параметру UICulture в файле проекта.

'<Assembly: NeutralResourcesLanguage("en-US", UltimateResourceFallbackLocation.Satellite)> 


'Атрибут ThemeInfo указывает расположение словарей ресурсов для конкретной темы и словарей общих ресурсов.
'1-й параметр: расположение словарей ресурсов для конкретной темы
'(используется, если ресурс не найден на странице 
' или в словарях ресурсов приложения)

'2-й параметр: расположение словаря общих ресурсов
'(используется, если ресурс не найден на странице 
'в приложении и в словарях ресурсов для конкретной темы)
<Assembly: ThemeInfo(ResourceDictionaryLocation.None, ResourceDictionaryLocation.SourceAssembly)>



'Следующий GUID служит идентификатором (ID) библиотеки типов, если этот проект видим для COM
<Assembly: Guid("9a93b15c-606c-4b5c-bfe4-20c76b741665")> 

' Сведения о версии сборки состоят из указанных ниже четырех значений:
'
'      Основной номер версии
'      Дополнительный номер версии 
'      Номер построения
'      Редакция
'
' Можно задать все значения или принять номер построения и номер редакции по умолчанию, 
' с помощью знака '*', как показано ниже:
' <Assembly: AssemblyVersion("1.0.*")> 

<Assembly: AssemblyVersion("1.0.0.0")> 
<Assembly: AssemblyFileVersion("1.0.0.0")> 
