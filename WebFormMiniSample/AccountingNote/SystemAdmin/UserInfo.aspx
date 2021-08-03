<%@ Page Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="UserInfo.aspx.cs" Inherits="AccountingNote.SystemAdmin.UserInfo" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentHeader" runat="server">
    <h1 style="margin-left: 10rem;">個人資訊</h1>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentBody" runat="server">
    <table cellpadding="15" style="width: 100%; text-align: left; line-height: 2rem;" border="1">
        <tr>
            <th style="width: 30%;">Account</th>
            <td style="width: 70%;">
                <asp:Literal runat="server" ID="ltAccount"></asp:Literal>
            </td>
        </tr>
        <tr>
            <th>Name</th>
            <td>
                <asp:Literal runat="server" ID="ltName"></asp:Literal>
            </td>
        </tr>
        <tr>
            <th>E-mail</th>
            <td>
                <asp:Literal runat="server" ID="ltEmail"></asp:Literal>
            </td>
        </tr>
    </table>
    <table style="margin-top: 1rem">
        <tr>
            <td>
                <asp:Button runat="server" ID="btnLogout" Text="Logout" Font-Size="Medium" />
            </td>
            <td></td>
        </tr>
    </table>
</asp:Content>
