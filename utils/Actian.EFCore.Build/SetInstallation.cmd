set II_INSTALLATION=%1
set II_SYSTEM=C:\Data\ActianX\Ingres%II_INSTALLATION%
set LIB=%II_SYSTEM%\ingres\lib
set INCLUDE=%II_SYSTEM%\ingres\files
set ACTIAN_TEST_CONNECTION_STRING=Server=localhost;Port=%II_INSTALLATION%7;User ID=efcore_test;Password=efcore_test;Persist Security Info=true
set path=%II_SYSTEM%\ingres\bin;%II_SYSTEM%\ingres\utility;%PATH%
