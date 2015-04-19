<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ViewGrids.aspx.cs" Inherits="WebAppLessons.ViewGrids" %>

<%@ Register TagPrefix="mycontrol" TagName="SearchPanelParam" Src="~/SearchPanelParam.ascx" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <style>
        .glaw1 {
            text-align: center;
            margin: auto;
            position: absolute;
            left: 5%;
            right: 5%
        }

        .glaw2 {
            border: 1px solid;
            display: inline;
            text-align: center;
            margin: auto;
        }

        .blok2, .blok3, .blok4 {
            float: left;
            margin: 5px;
            width: 440px;
            height: 500px;
            padding: 5px;
            text-align: center;
            border-radius: 4px;
            box-shadow: rgba(0,0,0,1.2) 0px 1px 3px;
        }
    </style>
</head>

<body>
    <form id="form1" runat="server">
        <div class="glaw1">
            <h2>Поиск по урокам</h2>
            <div class="glaw2">
                <div class="blok2">
                    <mycontrol:SearchPanelParam runat="server" KindSearch="param" Size="<%# SizeColumn %>"/>
                </div>
                <div class="blok3">
                    <mycontrol:SearchPanelParam runat="server" KindSearch="story" Size="<%# SizeColumn %>"/>
                </div>
                <div class="blok4">
                    <mycontrol:SearchPanelParam runat="server" KindSearch="simply" Size="<%# SizeColumn %>"/>
                </div>
            </div>
        </div>
    </form>
</body>
</html>
