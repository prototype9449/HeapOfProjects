<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="WebAppLessons.Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div style="border-width: medium; background-color: #FFFFFF; position: absolute;">
            <asp:TextBox runat="server" ID="PasswordBox" Width="200px" Font-Size="Large" Height="22px"></asp:TextBox>
            <br/>
            <asp:Button ID="ButtonNext" runat="server" Text="Продолжить" Width="200px" BorderColor="White" Font-Bold="True" Font-Italic="False" Font-Size="Medium" OnClick="ButtonNext_OnClick"/>
        </div>
    </form>
</body>
</html>
