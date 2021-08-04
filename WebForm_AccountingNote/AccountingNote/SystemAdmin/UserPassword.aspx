<%@ Page Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="UserPassword.aspx.cs" Inherits="AccountingNote.SystemAdmin.UserPassword" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentHeader" runat="server">
    <h1 style="margin-left: 10rem;">會員管理</h1>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentBody" runat="server">
    <table>
        <tr>
            <td>
                <div style="float: left;">帳號: </div>
            </td>
            <td>
                <div style="float: left; margin-left: 1rem;">
                    <asp:Label runat="server" ID="lblUserAccount"></asp:Label>
                </div>
            </td>
        </tr>
        <tr>
            <td>
                <div style="float: left; margin-top: 1rem;">原密碼:</div>
            </td>
            <td>
                <div style="float: left; margin-left: 1rem; margin-top: 1rem;">
                    <asp:TextBox ID="txtOldPwd" runat="server" TextMode="Password"></asp:TextBox>
                </div>
            </td>
        </tr>
        <tr>
            <td>
                <div style="float: left; margin-top: 1rem;">確認密碼:</div>
            </td>
            <td>
                <div style="float: left; margin-left: 1rem; margin-top: 1rem;">
                    <asp:TextBox ID="txtOldCommitPwd" runat="server" TextMode="Password"></asp:TextBox>
                </div>
            </td>
        </tr>
        <tr>
            <td>
                <div style="float: left; margin-top: 1rem;">新密碼:</div>
            </td>
            <td>
                <div style="float: left; margin-left: 1rem; margin-top: 1rem;">
                    <asp:TextBox ID="txtNewPwd" runat="server" TextMode="Password"></asp:TextBox>
                </div>
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <div style="float: left;">
                    <asp:Button runat="server" ID="btnSave" OnClick="btnSave_Click" Text="變更密碼" Style="margin-top: 1rem;" Font-Size="Medium" />
                </div>
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <asp:Literal runat="server" ID="ltMsg"></asp:Literal>
            </td>
        </tr>
    </table>
</asp:Content>
