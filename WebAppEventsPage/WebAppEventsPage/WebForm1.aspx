<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebForm1.aspx.cs" Inherits="WebAppEventsPage.WebForm1" MaintainScrollPositionOnPostback="false" %>
<title>Hello world</title>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:Panel runat="server" ID="panel"></asp:Panel>
            <asp:PlaceHolder runat="server" ID="Place"></asp:PlaceHolder>
            <asp:Button runat="server" ID="btn" OnClick="OnClick"/>
            <br />
            <asp:HyperLink runat="server" Text="link" BackColor="Red" NavigateUrl="http://yandex.ru"/>
            <asp:LinkButton runat="server" Text="Linkbutton"></asp:LinkButton>
            <br />
            <asp:BulletedList runat="server" ID="bulletList" DisplayMode="LinkButton" BulletStyle="LowerAlpha">
                <asp:ListItem Text="1"/>
                <asp:ListItem Text="2"/>
                <asp:ListItem Text="3"/>
                <asp:ListItem Text="4"/>
                <asp:ListItem Text="5"/>
            </asp:BulletedList>
            <br />
            <br />
            <asp:RadioButtonList runat="server" EnableViewState="False">
                <asp:ListItem Text="1" />
                <asp:ListItem Text="2" />
                <asp:ListItem Text="3" />
                <asp:ListItem Text="4" />
                <asp:ListItem Text="5" />
            </asp:RadioButtonList>
            <br />
            <asp:CheckBoxList runat="server">
                <asp:ListItem Text="1" />
                <asp:ListItem Text="2" />
                <asp:ListItem Text="3" />
                <asp:ListItem Text="4" />
                <asp:ListItem Text="5" />
            </asp:CheckBoxList>
            <br />
            <asp:CheckBoxList runat="server">
                <asp:ListItem Text="1" />
                <asp:ListItem Text="2" />
                <asp:ListItem Text="3" />
                <asp:ListItem Text="4" />
                <asp:ListItem Text="5" />
            </asp:CheckBoxList>
            <br />
            <asp:CheckBoxList runat="server">
                <asp:ListItem Text="1" />
                <asp:ListItem Text="2" />
                <asp:ListItem Text="3" />
                <asp:ListItem Text="4" />
                <asp:ListItem Text="5" />
            </asp:CheckBoxList>
            <asp:CheckBoxList runat="server">
                <asp:ListItem Text="1" />
                <asp:ListItem Text="2" />
                <asp:ListItem Text="3" />
                <asp:ListItem Text="4" />
                <asp:ListItem Text="5" />
            </asp:CheckBoxList>
            <br />
            <asp:CheckBoxList runat="server">
                <asp:ListItem Text="1" />
                <asp:ListItem Text="2" />
                <asp:ListItem Text="3" />
                <asp:ListItem Text="4" />

            </asp:CheckBoxList>
            <asp:CheckBoxList runat="server">
                <asp:ListItem Text="1" />
                <asp:ListItem Text="2" />
                <asp:ListItem Text="3" />
                <asp:ListItem Text="4" />
                <asp:ListItem Text="5" />
            </asp:CheckBoxList>
             <asp:CheckBoxList runat="server">
                <asp:ListItem Text="1" />
                <asp:ListItem Text="2" />
                <asp:ListItem Text="3" />
                <asp:ListItem Text="4" />
                <asp:ListItem Text="5" />
            </asp:CheckBoxList>
            <asp:DropDownList runat="server" ID="drowList" AutoPostBack="True" OnSelectedIndexChanged="OnSelectedIndexChanged">
                <asp:ListItem Text="1" />
                <asp:ListItem Text="2" />
                <asp:ListItem Text="3" />
                <asp:ListItem Text="4" />
                <asp:ListItem Text="5" />
            </asp:DropDownList>
            <br/>
            <asp:ListBox runat="server" Id="myListBox" SelectionMode="Multiple">
                 <asp:ListItem Text="1" />
                <asp:ListItem Text="2" />
                <asp:ListItem Text="3" />
                <asp:ListItem Text="4" />
                <asp:ListItem Text="5" />
            </asp:ListBox>
        </div>
    </form>
</body>
</html>
