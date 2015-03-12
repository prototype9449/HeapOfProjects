<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="WebEmptyApp.WebForm1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:Label ID="labelMy" runat="server">Hi</asp:Label>
            <br/>
            <%
                for (int i = 0; i < 10; i++)
                {
                    Response.Write(i +"<br/>");
                }
            %>
            Hi
            <br/>
            <%= DateTime.Now %>
        </div>
    </form>
</body>
</html>
