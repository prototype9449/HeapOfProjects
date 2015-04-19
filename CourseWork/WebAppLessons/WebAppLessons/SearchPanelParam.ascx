<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SearchPanelParam.ascx.cs" Inherits="WebAppLessons.SearchPanelParam" %>
<asp:CheckBox ID="CheckBoxSearch" runat="server" Text="Точно" />
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
<asp:Label ID="Label" runat="server" Text="Искать по"></asp:Label>
<br />
<asp:TextBox runat="server" ID="TitleText" Width="300px"></asp:TextBox>
<br />
<asp:TextBox runat="server" ID="AutorText" Width="300px"></asp:TextBox>
<br />
<br />
<asp:Button runat="server" ID="ButtonSearch" Text="Искать" Width="300px" OnClick="ButtonSearch_OnClick"></asp:Button>
<br />
<asp:Button runat="server" ID="ButtonClearResult" Text="Очистить результаты поиска" Width="300px" OnClick="ButtonClearResult_OnClick"></asp:Button>
<br />
<asp:ListBox ID="ListBox" runat="server" Width="300px"></asp:ListBox>
