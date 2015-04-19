<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SearchPanelParam.ascx.cs" Inherits="WebAppLessons.SearchPanelParam" %>

<asp:CheckBox ID="CheckBoxSearch" runat="server" Text="Точно" />
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
    <asp:Label ID="Label" runat="server" Text="Искать по"></asp:Label>
<br />
<asp:TextBox runat="server" ID="TitleText" Width="<%# Size %>"></asp:TextBox>
<br />
<asp:TextBox runat="server" ID="AutorText" Width="<%# Size %>"></asp:TextBox>
<br />
<br />
<asp:Button runat="server" ID="ButtonSearch" Text="Искать" OnClick="ButtonSearch_OnClick" Width="<%# Size %>"></asp:Button>
<br />
<asp:Button runat="server" ID="ButtonClearResult" Text="Очистить результаты поиска" OnClick="ButtonClearResult_OnClick" Width="<%# Size %>"></asp:Button>
<br />
<asp:GridView runat="server" Width="<%# Size %>" ID="GridViewLesson" AutoGenerateColumns="False" AllowPaging="True" 
    PageSize="5" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="3" Font-Size="X-Small" OnRowDeleting="GridViewLesson_OnRowDeleting">
    <Columns>
        <asp:BoundField HeaderText="ID" DataField="Id" SortExpression="Id" ReadOnly="True">
            <ItemStyle Width="5%"></ItemStyle>
        </asp:BoundField>
        <asp:BoundField HeaderText="Название" DataField="Title" SortExpression="Title" ReadOnly="True">
            <ItemStyle Width="30%"></ItemStyle>
        </asp:BoundField>
        <asp:BoundField HeaderText="Автор" DataField="Autor" SortExpression="Autor" ReadOnly="True">
            <ItemStyle Width="20%"></ItemStyle>
        </asp:BoundField>
        <asp:BoundField HeaderText="Дата создания" DataField="DateCreate" SortExpression="TimeCreate" ReadOnly="True">
            <ItemStyle Width="20%"></ItemStyle>
        </asp:BoundField>
        <asp:CommandField ShowDeleteButton="True">
            <ItemStyle Width="20%"></ItemStyle>
        </asp:CommandField>
    </Columns>
    <EmptyDataRowStyle ForeColor="#FF6600" HorizontalAlign="Center" VerticalAlign="Middle" />
    <EmptyDataTemplate>
        Записей нет
    </EmptyDataTemplate>

    <FooterStyle BackColor="White" ForeColor="#000066" />
    <HeaderStyle BackColor="#006699" Font-Bold="True" ForeColor="White" />
    <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
    <RowStyle ForeColor="#000066" HorizontalAlign="Center" VerticalAlign="Middle" Width="60px" />
    <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
    <SortedAscendingCellStyle BackColor="#F1F1F1" />
    <SortedAscendingHeaderStyle BackColor="#007DBB" />
    <SortedDescendingCellStyle BackColor="#CAC9C9" />
    <SortedDescendingHeaderStyle BackColor="#00547E" />

</asp:GridView>

<%--<asp:ObjectDataSource ID="ObjectDataSource1" runat="server"
    DataObjectTypeName="LessonDB.LessonInfoId"
    TypeName="WebAppLessons.LessonInfoView"
    DeleteMethod="RemoveProduct"
    SelectMethod="SelectAllProducts"></asp:ObjectDataSource>--%>
