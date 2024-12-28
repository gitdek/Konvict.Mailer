$Id: README.txt 9118 2007-04-04 22:16:34Z robert $

To use PowerMTA's .NET API, your project needs to reference the
port25.pmta.api.submitter.dll.

On the command prompt, compile with the /reference: switch.
For C#:
    C:\> csc /reference:c:\pmta\api\dotnet\port25.pmta.submitter.dll YourFile.cs
For Visual Basic:
    C:\> vbc /reference:c:\pmta\api\dotnet\port25.pmta.submitter.dll YourFile.vb

In Visual Studio, click the "Project" menu, then the "Add Reference..." item.
In the upcoming dialog, click Browse and pick port25.pmta.api.submitter.dll
from the install directory c:\pmta\api\dotnet\
